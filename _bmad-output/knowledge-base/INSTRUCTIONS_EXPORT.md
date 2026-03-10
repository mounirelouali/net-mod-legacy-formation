# 🚀 Instructions d'Export NotebookLM (Automatisé)

**Date**: 9 mars 2026  
**Objectif**: Exporter automatiquement les notes NotebookLM vers Markdown local

---

## ⚠️ Clarification Importante

**L'assistant IA (Cascade) ne peut PAS exécuter ce script.**

**Pourquoi?**
- Aucune capacité d'exécution Python
- Aucune capacité d'ouverture de navigateur
- Aucune connexion à des services externes

**Solution**: **VOUS** exécutez le script fourni.

---

## 📋 Prérequis

### 1. Installer notebooklm-py

```powershell
pip install "notebooklm-py[browser]"
playwright install chromium
```

### 2. S'authentifier (Une seule fois)

```powershell
notebooklm login
```

**Important**: 
- Une fenêtre de navigateur s'ouvrira
- **Connectez-vous avec**: `contact@digitar.be`
- Les credentials seront sauvegardés localement pour réutilisation

---

## 🎯 Exécution du Script

### Étape 1: Naviguer vers le dossier

```powershell
cd "d:\devnet\playground\net-mod-legacy\_bmad-output\knowledge-base"
```

### Étape 2: Lancer le script

```powershell
python export_notebooklm_notes.py
```

### Étape 3: Vérifier les résultats

Le script va créer 3 fichiers:
- ✅ `Wetic_Elearning_Principes.md`
- ✅ `outofthebox_Methodologie.md`
- ✅ `WETIC_Solene_DotNet.md`

---

## 📊 Ce que fait le script

1. **Connexion**: Utilise les credentials sauvegardés par `notebooklm login`
2. **Recherche**: Trouve les notes par nom exact
3. **Récupération**: 
   - Liste des sources
   - Contenu texte complet de chaque source
   - Historique de conversations
4. **Export**: Crée un fichier Markdown structuré pour chaque note
5. **Rapport**: Affiche un résumé des exports réussis/échoués

---

## 🔧 Structure des Fichiers Exportés

Chaque fichier Markdown contiendra:

```markdown
# [Nom de la Note]

**Exporté depuis NotebookLM le**: [Date/Heure]
**Notebook ID**: [ID]

---

## Sources

### 1. [Titre Source 1]
- **Type**: [url/file/etc]
- **URL**: [si applicable]

**Contenu**:
[Texte complet de la source]

---

## Conversations et Insights

### Message 1
**Question**: [Question posée]
**Réponse**: [Réponse générée]

---
```

---

## ❓ Dépannage

### Erreur: "Credentials non trouvés"

**Solution**: Relancer `notebooklm login` et sélectionner `contact@digitar.be`

### Erreur: "Note introuvable"

**Solution**: Le script affiche la liste des notes disponibles. Vérifiez le nom exact dans la configuration.

### Erreur: "Permission denied"

**Solution**: Vérifiez que le compte `contact@digitar.be` a accès aux notes.

---

## 🔄 Utilisation Future

**Première fois**:
```powershell
pip install "notebooklm-py[browser]"
playwright install chromium
notebooklm login  # Sélectionner contact@digitar.be
python export_notebooklm_notes.py
```

**Prochaines fois**:
```powershell
python export_notebooklm_notes.py
```

Les credentials restent sauvegardés, pas besoin de se reconnecter.

---

## ⚙️ Configuration du Script

Pour modifier les notes à exporter, éditez `export_notebooklm_notes.py`:

```python
NOTES_TO_EXPORT = [
    {
        "name": "Nom Exact de la Note",  # Doit correspondre au titre dans NotebookLM
        "output_file": "Fichier_Sortie.md"
    },
    # Ajouter d'autres notes ici...
]
```

---

## ✅ Validation Post-Export

Une fois le script exécuté avec succès:

1. **Vérifier** que les 3 fichiers existent
2. **Ouvrir** chaque fichier pour vérifier le contenu
3. **Informer l'assistant**: "Les notes sont exportées, tu peux les utiliser"

L'assistant pourra alors:
- Lire les fichiers localement
- Utiliser le contenu pour répondre aux questions stratégiques
- Appliquer les méthodologies dans la génération des supports

---

**Créé le**: 9 mars 2026  
**Auteur**: Formation .NET Modernisation
