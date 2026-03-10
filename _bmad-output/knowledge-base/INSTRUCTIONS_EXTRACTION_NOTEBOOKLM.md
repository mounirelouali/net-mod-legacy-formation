# Instructions : Extraction des Notes NotebookLM

**Objectif** : Exporter le contenu des 3 notebooks NotebookLM en fichiers Markdown locaux pour consultation par l'IA.

---

## 📋 Prérequis

### 1. Installation de Python et dépendances

```powershell
# Vérifier que Python est installé
python --version  # Doit être >= 3.8

# Installer notebooklm-py avec support navigateur
pip install "notebooklm-py[browser]"

# Installer Chromium pour l'authentification
playwright install chromium
```

### 2. Compte NotebookLM

- **Compte à utiliser** : `contact@digitar.be` (compte PAYANT)
- **⚠️ NE PAS utiliser** : `op.wetic@gmail.com` (connecté dans Windsurf)

---

## 🚀 Exécution du Script

### Étape 1 : Ouvrir PowerShell

```powershell
# Se placer dans le dossier du projet
cd d:\devnet\playground\net-mod-legacy\_bmad-output\knowledge-base\
```

### Étape 2 : Exécuter le script

```powershell
python extract_notebooklm_notes.py
```

### Étape 3 : Authentification

**Une fenêtre Chrome va s'ouvrir automatiquement.**

**IMPORTANT** :
1. ✅ **SE CONNECTER AVEC** : `contact@digitar.be`
2. ❌ **NE PAS utiliser** : `op.wetic@gmail.com`
3. Autoriser les permissions demandées
4. Fermer la fenêtre une fois connecté

Le script va ensuite :
- Lister tous vos notebooks
- Extraire le contenu des 3 notebooks cibles
- Sauvegarder en Markdown local

---

## 📊 Notebooks Extraits

| Notebook | ID | Fichier de sortie |
|----------|-----|-------------------|
| **Outofthebox** | `e5f03699-fa74-4451-b16b-3babcdb780c4` | `outofthebox_Methodologie.md` |
| **Wetic ELearning** | `53cd7abb-73ff-4e0f-9835-078dd31cbd98` | `Wetic_Elearning_Principes.md` |
| **WETIC-Solene** | `3afedc6b-1d43-4132-aef6-30cde947eb4a` | `WETIC_Solene_DotNet_Moderne.md` |

---

## 📁 Fichiers Créés

Après exécution, vous aurez **3 fichiers Markdown** dans :

```
d:\devnet\playground\net-mod-legacy\_bmad-output\knowledge-base\
├── outofthebox_Methodologie.md
├── Wetic_Elearning_Principes.md
└── WETIC_Solene_DotNet_Moderne.md
```

**Contenu de chaque fichier** :
- Métadonnées du notebook (titre, ID, date)
- Liste des sources (documents, URLs, PDFs)
- Principes clés extraits
- Méthodologies et approches
- Recommandations et bonnes pratiques
- Exemples et cas d'usage
- Points d'attention
- Synthèse complète

---

## 🔧 Dépannage

### Erreur : `ModuleNotFoundError: No module named 'notebooklm'`

**Solution** :
```powershell
pip install "notebooklm-py[browser]"
```

### Erreur : `Playwright not installed`

**Solution** :
```powershell
playwright install chromium
```

### Erreur : `Authentication failed`

**Cause** : Mauvais compte Google utilisé

**Solution** :
1. Supprimer les credentials existants :
   ```powershell
   rm -r ~/.notebooklm
   ```
2. Relancer le script
3. **SE CONNECTER AVEC `contact@digitar.be`**

### Erreur : `Notebook not found`

**Cause** : ID de notebook incorrect ou notebook non partagé

**Solution** :
1. Vérifier sur https://notebooklm.google.com que les notebooks existent
2. Vérifier que vous êtes connecté avec `contact@digitar.be`
3. Partager le notebook avec "Anyone with the link" si nécessaire

### Le script est très lent

**Cause** : NotebookLM utilise Gemini pour synthétiser les réponses (peut prendre 5-30 secondes par question)

**Solution** : C'est normal, attendez. Le script affiche la progression.

---

## ✅ Vérification du Résultat

### 1. Vérifier que les fichiers existent

```powershell
ls *.md
```

**Attendu** :
```
outofthebox_Methodologie.md
Wetic_Elearning_Principes.md
WETIC_Solene_DotNet_Moderne.md
```

### 2. Vérifier le contenu

```powershell
# Afficher les premières lignes
Get-Content outofthebox_Methodologie.md -Head 20
```

**Attendu** : Titre du notebook + métadonnées + contenu structuré

### 3. Vérifier la taille

```powershell
Get-ChildItem *.md | Select-Object Name, @{Name="Size (KB)";Expression={[math]::Round($_.Length/1KB,1)}}
```

**Attendu** : Fichiers de plusieurs dizaines de KB (contenu substantiel)

---

## 🔄 Prochaines Étapes

Une fois les fichiers extraits :

1. **Windsurf pourra les lire** :
   ```
   Je vais maintenant lire vos 3 notes :
   - outofthebox_Methodologie.md
   - Wetic_Elearning_Principes.md
   - WETIC_Solene_DotNet_Moderne.md
   ```

2. **Réviser le support Jour1 09h00-10h30** en consultant ces notes

3. **Appliquer les principes pédagogiques** documentés dans Wetic ELearning

4. **Suivre la méthodologie** documentée dans Outofthebox

5. **Intégrer le contexte projet** documenté dans WETIC-Solene

---

## 📝 Notes Importantes

### Compte Google

- **Windsurf IDE** : Connecté avec `op.wetic@gmail.com`
- **NotebookLM** : Doit utiliser `contact@digitar.be` (compte payant)
- Ce sont **2 comptes différents** - Le script ouvrira un navigateur séparé pour l'authentification NotebookLM

### Sécurité

- Les credentials sont stockés dans `~/.notebooklm/` (sécurisé)
- Durée de validité : 2-4 semaines
- Ré-authentification automatique si expiration

### Performance

- Temps d'exécution total : **5-10 minutes** (3 notebooks × 6 questions × 10-30s par question)
- Le script affiche la progression en temps réel
- Les réponses de Gemini peuvent prendre du temps (c'est normal)

---

## 🆘 Support

En cas de problème :

1. **Documentation notebooklm-py** : https://github.com/teng-lin/notebooklm-py
2. **Issues GitHub** : https://github.com/teng-lin/notebooklm-py/issues
3. **Troubleshooting** : https://github.com/teng-lin/notebooklm-py/blob/main/docs/troubleshooting.md

---

**Créé le** : 9 mars 2026  
**Version** : 1.0  
**Projet** : Formation .NET Modernisation
