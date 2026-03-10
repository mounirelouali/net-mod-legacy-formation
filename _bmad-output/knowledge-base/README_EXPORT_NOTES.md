# 📋 Procédure d'Export des Notes NotebookLM

**Date** : 9 mars 2026  
**Objectif** : Rendre les notes NotebookLM accessibles localement pour utilisation dans la formation

---

## 🔴 Clarification Technique

**L'assistant IA (Cascade) NE PEUT PAS** :
- Se connecter à NotebookLM ou autres services web
- Lire des notes en ligne
- Maintenir des connexions entre sessions

**Le fichier `notebooklm-py_base_de_connaissances.md` est uniquement** :
- Documentation de l'API Python notebooklm-py
- PAS une connexion active à votre compte

---

## ✅ Solution : Export Manuel en Markdown

### Notes à Exporter

Exportez le contenu complet de ces 3 notes depuis votre compte **contact@digitar.be** :

1. **Wetic Elearning** → `Wetic_Elearning_Principes.md`
2. **outofthebox** → `outofthebox_Methodologie.md`
3. **net-mod-legacy WETIC-Solene - Dev .NET Moderne** → `WETIC_Solene_DotNet.md`

---

## 📝 Procédure d'Export (Étape par Étape)

### Pour chaque note :

1. **Connectez-vous à NotebookLM**
   - URL : https://notebooklm.google.com
   - Compte : contact@digitar.be

2. **Ouvrez la note**
   - Cliquez sur le nom de la note dans la liste

3. **Copiez tout le contenu**
   - Sélectionnez tout le texte (Ctrl+A)
   - Copiez (Ctrl+C)

4. **Créez le fichier Markdown local**
   - Ouvrez un éditeur de texte
   - Collez le contenu (Ctrl+V)
   - Sauvegardez dans : `_bmad-output\knowledge-base\NOM_DU_FICHIER.md`

---

## 📁 Fichiers à Créer

### 1. Wetic_Elearning_Principes.md

**Contenu attendu** :
- Principes pédagogiques
- Approche progressive/itérative
- Scaffolding
- Learning by Doing
- Etc.

**Template** :
```markdown
# Principes Pédagogiques Wetic Elearning

## Vue d'ensemble
[Copier le contenu de la note NotebookLM ici]

## Principes Clés
[...]

## Approches Recommandées
[...]

## Exemples d'Application
[...]
```

**Emplacement** : `d:\devnet\playground\net-mod-legacy\_bmad-output\knowledge-base\Wetic_Elearning_Principes.md`

---

### 2. outofthebox_Methodologie.md

**Contenu attendu** :
- Méthodologie de transformation AS-IS → TO-BE
- Approche itérative
- Gestion du changement
- Etc.

**Template** :
```markdown
# Méthodologie outofthebox

## Vue d'ensemble
[Copier le contenu de la note NotebookLM ici]

## Phases de Transformation
[...]

## Bonnes Pratiques
[...]

## Cas d'Usage
[...]
```

**Emplacement** : `d:\devnet\playground\net-mod-legacy\_bmad-output\knowledge-base\outofthebox_Methodologie.md`

---

### 3. WETIC_Solene_DotNet.md

**Contenu attendu** :
- Contexte spécifique du projet client
- Architecture cible
- Contraintes techniques
- Etc.

**Template** :
```markdown
# Projet WETIC-Solene - Dev .NET Moderne

## Contexte du Projet
[Copier le contenu de la note NotebookLM ici]

## Architecture Cible
[...]

## Contraintes et Exigences
[...]

## Particularités
[...]
```

**Emplacement** : `d:\devnet\playground\net-mod-legacy\_bmad-output\knowledge-base\WETIC_Solene_DotNet.md`

---

## ✅ Validation de l'Export

Une fois les 3 fichiers créés, vérifiez :

- [ ] Les 3 fichiers existent dans `_bmad-output\knowledge-base\`
- [ ] Chaque fichier contient du contenu (pas vide)
- [ ] Le formatage Markdown est correct
- [ ] Les informations sont complètes

**Ensuite, dites à l'assistant** : "Les notes sont exportées, tu peux les utiliser maintenant"

L'assistant pourra alors :
- Lire les fichiers localement
- Appliquer les principes et méthodologies
- Utiliser le contexte dans la génération des supports
- **SANS jamais mentionner les sources internes dans les supports apprenants**

---

## 🔄 Utilisation Future

**À chaque nouvelle session** :
- Les fichiers restent accessibles localement
- L'assistant peut les lire automatiquement
- Pas besoin de les réexporter (sauf si contenu modifié sur NotebookLM)

**Si vous modifiez les notes sur NotebookLM** :
- Réexportez uniquement les notes modifiées
- Remplacez les fichiers locaux correspondants

---

## 📊 Statut Actuel

| Note NotebookLM | Fichier Local | Statut |
|-----------------|---------------|--------|
| Wetic Elearning | `Wetic_Elearning_Principes.md` | ❌ À exporter |
| outofthebox | `outofthebox_Methodologie.md` | ❌ À exporter |
| net-mod-legacy WETIC-Solene | `WETIC_Solene_DotNet.md` | ❌ À exporter |

**Une fois exporté**, changez ❌ en ✅ ci-dessus.

---

**Document créé le** : 9 mars 2026  
**Auteur** : Configuration Formation .NET Modernisation
