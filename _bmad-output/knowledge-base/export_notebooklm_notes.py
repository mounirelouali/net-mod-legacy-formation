"""
Script d'export des notes NotebookLM vers fichiers Markdown locaux

Utilisation:
1. Installer: pip install "notebooklm-py[browser]" && playwright install chromium
2. S'authentifier: notebooklm login (utiliser contact@digitar.be)
3. Lancer: python export_notebooklm_notes.py

Le script exporte automatiquement les notes spécifiées vers _bmad-output/knowledge-base/
"""

import asyncio
import os
from pathlib import Path
from notebooklm import NotebookLMClient

# Configuration
NOTES_TO_EXPORT = [
    {
        "name": "Wetic Elearning",
        "output_file": "Wetic_Elearning_Principes.md"
    },
    {
        "name": "outofthebox",
        "output_file": "outofthebox_Methodologie.md"
    },
    {
        "name": "net-mod-legacy WETIC-Solene - Dev .NET Moderne",
        "output_file": "WETIC_Solene_DotNet.md"
    }
]

OUTPUT_DIR = Path(__file__).parent


async def export_notebook_content(client, notebook_name: str, output_file: str):
    """
    Exporte le contenu d'une note NotebookLM vers un fichier Markdown local
    
    Args:
        client: Client NotebookLM authentifié
        notebook_name: Nom de la note dans NotebookLM
        output_file: Nom du fichier de sortie (dans knowledge-base/)
    """
    try:
        print(f"\n{'='*60}")
        print(f"Recherche de la note: {notebook_name}")
        print(f"{'='*60}")
        
        # Lister tous les notebooks
        notebooks = await client.notebooks.list()
        
        # Trouver le notebook par nom
        target_notebook = None
        for nb in notebooks:
            if nb.title.lower() == notebook_name.lower():
                target_notebook = nb
                break
        
        if not target_notebook:
            print(f"❌ ERREUR: Note '{notebook_name}' introuvable")
            print(f"Notes disponibles:")
            for nb in notebooks:
                print(f"  - {nb.title}")
            return False
        
        print(f"✅ Note trouvée: {target_notebook.title} (ID: {target_notebook.id})")
        
        # Récupérer les sources
        print(f"\nRécupération des sources...")
        sources = await client.sources.list(target_notebook.id)
        print(f"✅ {len(sources)} source(s) trouvée(s)")
        
        # Récupérer l'historique de conversation
        print(f"\nRécupération de l'historique de conversation...")
        try:
            history = await client.chat.get_history(target_notebook.id)
            print(f"✅ {len(history)} message(s) trouvé(s)")
        except Exception as e:
            print(f"⚠️ Historique non disponible: {e}")
            history = []
        
        # Construire le fichier Markdown
        print(f"\nCréation du fichier Markdown...")
        output_path = OUTPUT_DIR / output_file
        
        with open(output_path, 'w', encoding='utf-8') as f:
            # En-tête
            f.write(f"# {target_notebook.title}\n\n")
            f.write(f"**Exporté depuis NotebookLM le**: {asyncio.get_event_loop().time()}\n")
            f.write(f"**Notebook ID**: {target_notebook.id}\n\n")
            f.write("---\n\n")
            
            # Sources
            f.write("## Sources\n\n")
            if sources:
                for i, source in enumerate(sources, 1):
                    f.write(f"### {i}. {source.title}\n\n")
                    f.write(f"- **Type**: {source.source_type}\n")
                    if hasattr(source, 'url') and source.url:
                        f.write(f"- **URL**: {source.url}\n")
                    f.write("\n")
                    
                    # Tenter de récupérer le contenu texte complet
                    try:
                        fulltext = await client.sources.get_fulltext(target_notebook.id, source.id)
                        if fulltext:
                            f.write("**Contenu**:\n\n")
                            f.write(f"```\n{fulltext}\n```\n\n")
                    except Exception as e:
                        print(f"  ⚠️ Impossible de récupérer le texte de '{source.title}': {e}")
            else:
                f.write("*Aucune source disponible*\n\n")
            
            f.write("---\n\n")
            
            # Historique de conversation
            f.write("## Conversations et Insights\n\n")
            if history:
                for i, msg in enumerate(history, 1):
                    f.write(f"### Message {i}\n\n")
                    f.write(f"**Question**: {msg.query}\n\n")
                    if hasattr(msg, 'answer') and msg.answer:
                        f.write(f"**Réponse**:\n\n{msg.answer}\n\n")
                    f.write("---\n\n")
            else:
                f.write("*Aucune conversation disponible*\n\n")
        
        print(f"✅ Fichier créé avec succès: {output_path}")
        return True
        
    except Exception as e:
        print(f"❌ ERREUR lors de l'export de '{notebook_name}': {e}")
        import traceback
        traceback.print_exc()
        return False


async def main():
    """
    Fonction principale: Export de toutes les notes configurées
    """
    print("="*60)
    print("EXPORT DES NOTES NOTEBOOKLM VERS MARKDOWN LOCAL")
    print("="*60)
    print(f"\nCompte utilisé: contact@digitar.be")
    print(f"Dossier de sortie: {OUTPUT_DIR}")
    print(f"Notes à exporter: {len(NOTES_TO_EXPORT)}")
    
    try:
        # Connexion au client (utilise les credentials stockés par 'notebooklm login')
        async with await NotebookLMClient.from_storage() as client:
            print("\n✅ Connexion au client NotebookLM réussie")
            
            results = []
            for note_config in NOTES_TO_EXPORT:
                success = await export_notebook_content(
                    client, 
                    note_config["name"], 
                    note_config["output_file"]
                )
                results.append((note_config["name"], success))
            
            # Résumé
            print("\n" + "="*60)
            print("RÉSUMÉ DE L'EXPORT")
            print("="*60)
            for note_name, success in results:
                status = "✅ SUCCÈS" if success else "❌ ÉCHEC"
                print(f"{status}: {note_name}")
            
            success_count = sum(1 for _, s in results if s)
            print(f"\nTotal: {success_count}/{len(results)} note(s) exportée(s) avec succès")
            
    except FileNotFoundError:
        print("\n❌ ERREUR: Credentials NotebookLM non trouvés")
        print("\nVeuillez d'abord vous authentifier:")
        print("1. Installer: pip install \"notebooklm-py[browser]\" && playwright install chromium")
        print("2. Lancer: notebooklm login")
        print("3. Sélectionner le compte: contact@digitar.be")
        print("4. Relancer ce script")
    except Exception as e:
        print(f"\n❌ ERREUR CRITIQUE: {e}")
        import traceback
        traceback.print_exc()


if __name__ == "__main__":
    asyncio.run(main())
