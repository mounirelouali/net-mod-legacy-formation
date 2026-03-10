"""
Script d'extraction des notes NotebookLM
========================================

Ce script se connecte au compte NotebookLM contact@digitar.be
et exporte le contenu des 3 notebooks en Markdown local.

Notebooks cibles :
- Outofthebox: e5f03699-fa74-4451-b16b-3babcdb780c4
- Wetic ELearning: 53cd7abb-73ff-4e0f-9835-078dd31cbd98
- net-mod-legacy WETIC-Solene: 3afedc6b-1d43-4132-aef6-30cde947eb4a

Prérequis :
-----------
pip install "notebooklm-py[browser]"
playwright install chromium

Authentification :
------------------
Ce script ouvrira un navigateur pour l'authentification.
IMPORTANT : Connectez-vous avec contact@digitar.be (pas op.wetic@gmail.com)

Usage :
-------
python extract_notebooklm_notes.py
"""

import asyncio
import os
from pathlib import Path
from notebooklm import NotebookLMClient

# Configuration
NOTEBOOKS = {
    "outofthebox": {
        "id": "e5f03699-fa74-4451-b16b-3babcdb780c4",
        "output_file": "outofthebox_Methodologie.md",
        "description": "Méthodologie de transformation AS-IS to TO-BE"
    },
    "wetic_elearning": {
        "id": "53cd7abb-73ff-4e0f-9835-078dd31cbd98",
        "output_file": "Wetic_Elearning_Principes.md",
        "description": "Principes pédagogiques et scaffolding"
    },
    "wetic_solene": {
        "id": "3afedc6b-1d43-4132-aef6-30cde947eb4a",
        "output_file": "WETIC_Solene_DotNet_Moderne.md",
        "description": "Contexte projet .NET modernisation"
    }
}

OUTPUT_DIR = Path(__file__).parent


async def extract_notebook_content(client: NotebookLMClient, notebook_id: str, notebook_name: str):
    """
    Extrait le contenu d'un notebook NotebookLM.
    
    Stratégie d'extraction :
    1. Récupérer les métadonnées du notebook
    2. Lister toutes les sources
    3. Poser des questions pour extraire le contenu structuré
    4. Récupérer le texte complet des sources si disponible
    """
    print(f"\n{'='*60}")
    print(f"Extraction : {notebook_name}")
    print(f"ID : {notebook_id}")
    print(f"{'='*60}")
    
    content = []
    
    try:
        # 1. Récupérer les métadonnées du notebook
        print("📋 Récupération métadonnées...")
        notebook = await client.notebooks.get(notebook_id)
        content.append(f"# {notebook.title}")
        content.append(f"\n**ID** : {notebook.id}")
        content.append(f"**Créé le** : {notebook.created_at if hasattr(notebook, 'created_at') else 'N/A'}")
        content.append("\n---\n")
        
        # 2. Lister les sources
        print("📚 Listing des sources...")
        sources = await client.sources.list(notebook_id)
        content.append(f"\n## Sources ({len(sources)} documents)\n")
        
        for idx, source in enumerate(sources, 1):
            source_title = getattr(source, 'title', f'Source {idx}')
            source_type = getattr(source, 'type', 'unknown')
            content.append(f"{idx}. **{source_title}** ({source_type})")
        
        content.append("\n---\n")
        
        # 3. Extraction du contenu via questions ciblées
        print("💬 Extraction du contenu via chat...")
        
        questions = [
            {
                "question": "Quels sont les principes clés ou concepts principaux documentés dans ces sources ?",
                "title": "Principes Clés"
            },
            {
                "question": "Quelles sont les méthodologies, approches ou processus décrits ?",
                "title": "Méthodologies et Approches"
            },
            {
                "question": "Y a-t-il des recommandations, bonnes pratiques ou guidelines spécifiques ?",
                "title": "Recommandations et Bonnes Pratiques"
            },
            {
                "question": "Quels sont les exemples concrets, cas d'usage ou illustrations fournis ?",
                "title": "Exemples et Cas d'Usage"
            },
            {
                "question": "Y a-t-il des points d'attention, erreurs à éviter ou considérations importantes ?",
                "title": "Points d'Attention"
            }
        ]
        
        for q_data in questions:
            print(f"  ❓ {q_data['title']}...")
            try:
                result = await client.chat.ask(notebook_id, q_data["question"])
                content.append(f"\n## {q_data['title']}\n")
                content.append(result.answer)
                content.append("\n")
            except Exception as e:
                print(f"  ⚠️ Erreur : {e}")
                content.append(f"\n## {q_data['title']}\n")
                content.append(f"*Erreur lors de l'extraction : {e}*\n")
        
        # 4. Synthèse globale
        print("📝 Synthèse globale...")
        try:
            synthesis = await client.chat.ask(
                notebook_id,
                "Fournis une synthèse complète de tout le contenu disponible dans ces sources, "
                "en structurant par thèmes principaux et en incluant tous les détails importants."
            )
            content.append("\n## Synthèse Complète\n")
            content.append(synthesis.answer)
            content.append("\n")
        except Exception as e:
            print(f"  ⚠️ Erreur synthèse : {e}")
        
        print(f"✅ Extraction terminée : {len(content)} sections")
        
    except Exception as e:
        print(f"❌ Erreur lors de l'extraction : {e}")
        content.append(f"\n## ⚠️ ERREUR\n")
        content.append(f"Impossible d'extraire le contenu : {e}\n")
    
    return "\n".join(content)


async def main():
    """
    Fonction principale d'extraction.
    """
    print("="*60)
    print("EXTRACTION DES NOTES NOTEBOOKLM")
    print("="*60)
    print(f"\n📁 Dossier de sortie : {OUTPUT_DIR}")
    print(f"📊 Notebooks à extraire : {len(NOTEBOOKS)}")
    
    # Vérifier si l'authentification est nécessaire
    print("\n🔐 Initialisation du client NotebookLM...")
    print("⚠️  IMPORTANT : Si le navigateur s'ouvre, connectez-vous avec contact@digitar.be")
    
    try:
        # Créer le client (ouvrira le navigateur si nécessaire)
        async with await NotebookLMClient.from_storage() as client:
            print("✅ Client initialisé avec succès\n")
            
            # Lister tous les notebooks pour vérification
            print("📋 Listing de tous vos notebooks...")
            try:
                all_notebooks = await client.notebooks.list()
                print(f"✅ {len(all_notebooks)} notebooks trouvés :\n")
                for nb in all_notebooks:
                    print(f"   - {nb.title} (ID: {nb.id})")
                print()
            except Exception as e:
                print(f"⚠️ Impossible de lister les notebooks : {e}\n")
            
            # Extraire chaque notebook cible
            for key, config in NOTEBOOKS.items():
                try:
                    # Extraire le contenu
                    content = await extract_notebook_content(
                        client,
                        config["id"],
                        config["description"]
                    )
                    
                    # Sauvegarder en Markdown
                    output_path = OUTPUT_DIR / config["output_file"]
                    with open(output_path, "w", encoding="utf-8") as f:
                        f.write(content)
                    
                    print(f"💾 Sauvegardé : {output_path}")
                    
                except Exception as e:
                    print(f"❌ Erreur pour {key} : {e}")
                    continue
            
            print("\n" + "="*60)
            print("✅ EXTRACTION TERMINÉE")
            print("="*60)
            print(f"\n📁 Fichiers créés dans : {OUTPUT_DIR}")
            for config in NOTEBOOKS.values():
                output_path = OUTPUT_DIR / config["output_file"]
                if output_path.exists():
                    size_kb = output_path.stat().st_size / 1024
                    print(f"   ✅ {config['output_file']} ({size_kb:.1f} KB)")
                else:
                    print(f"   ❌ {config['output_file']} (non créé)")
    
    except Exception as e:
        print(f"\n❌ ERREUR FATALE : {e}")
        print("\n🔍 Troubleshooting :")
        print("   1. Vérifiez que notebooklm-py est installé : pip install 'notebooklm-py[browser]'")
        print("   2. Vérifiez que Chromium est installé : playwright install chromium")
        print("   3. Assurez-vous d'être connecté avec contact@digitar.be lors de l'authentification")
        print("   4. Vérifiez que les IDs des notebooks sont corrects")
        return 1
    
    return 0


if __name__ == "__main__":
    exit_code = asyncio.run(main())
    exit(exit_code)
