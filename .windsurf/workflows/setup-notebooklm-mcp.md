---
description: Configuration MCP NotebookLM pour consultation dynamique des bases de connaissances
---

# Workflow : Configuration NotebookLM MCP dans Windsurf

**Objectif** : Établir une connexion dynamique entre Windsurf et vos notes NotebookLM pour consultation en temps réel pendant la génération des supports de formation.

**Durée estimée** : 10 minutes (configuration unique)

**Prérequis** :
- Windsurf IDE installé
- Compte Google `contact@digitar.be` avec accès aux notes NotebookLM
- Notes NotebookLM existantes et partagées

---

## Phase 1 : Installation du Serveur MCP (5 minutes)

### Étape 1.1 : Accéder à la Configuration MCP

1. **Ouvrir Windsurf**
2. **Dans le panneau Cascade** (chat AI), cliquez sur l'icône **MCPs** (🔨 marteau) dans le menu supérieur droit
3. **Cliquez sur "Configure"** pour ouvrir le fichier de configuration MCP

**Emplacement du fichier** : `~/.codeium/windsurf/mcp_config.json`

### Étape 1.2 : Ajouter la Configuration NotebookLM

**Copier-coller cette configuration exacte** :

```json
{
  "mcpServers": {
    "notebooklm": {
      "command": "npx",
      "args": ["-y", "notebooklm-mcp@latest"],
      "env": {
        "NOTEBOOKLM_PROFILE": "standard"
      }
    }
  }
}
```

**Si vous avez déjà d'autres serveurs MCP** :

```json
{
  "mcpServers": {
    "notebooklm": {
      "command": "npx",
      "args": ["-y", "notebooklm-mcp@latest"],
      "env": {
        "NOTEBOOKLM_PROFILE": "standard"
      }
    },
    "autre-serveur": {
      "command": "...",
      "args": ["..."]
    }
  }
}
```

### Étape 1.3 : Activer le Serveur

1. **Sauvegarder** le fichier `mcp_config.json`
2. **Retourner dans Windsurf Cascade**
3. **Cliquer sur "Refresh"** dans le panneau MCP pour activer le serveur
4. **Vérifier** que "notebooklm" apparaît dans la liste des serveurs actifs

**Indicateur de succès** : Le serveur NotebookLM apparaît avec un point vert dans le panneau MCP.

---

## Phase 2 : Authentification Google (3 minutes)

### Étape 2.1 : Lancer l'Authentification

**Dans le chat Cascade, taper** :

```
Log me in to NotebookLM
```

**OU**

```
Open NotebookLM auth setup
```

### Étape 2.2 : Connexion Navigateur

1. **Une fenêtre Chrome s'ouvrira automatiquement**
2. **Connectez-vous avec** : `contact@digitar.be`
3. **Autoriser les permissions** demandées par NotebookLM
4. **Fermer la fenêtre** une fois l'authentification réussie

**Stockage des credentials** : `~/.config/notebooklm-mcp/`

**Durée de validité** : 2-4 semaines (ré-authentification automatique ensuite)

### Étape 2.3 : Vérifier la Connexion

**Dans Cascade, taper** :

```
List my NotebookLM notebooks
```

**OU**

```
Show our notebooks
```

**Résultat attendu** : Liste de toutes vos notes NotebookLM incluant :
- "Wetic Elearning"
- "outofthebox"
- "net-mod-legacy WETIC-Solene - Dev .NET Moderne"

---

## Phase 3 : Configuration de la Bibliothèque (2 minutes)

### Étape 3.1 : Partager les Notes NotebookLM

Pour chaque note, **sur notebooklm.google.com** :

1. **Ouvrir la note**
2. **Cliquer sur ⚙️ Share**
3. **Sélectionner "Anyone with link"**
4. **Copier le lien**

### Étape 3.2 : Ajouter les Notes à la Bibliothèque MCP

**Dans Cascade, pour chaque note** :

```
Add [LIEN_NOTE] to library tagged "pedagogie, formation, dotnet-modernisation"
```

**Exemple concret** :

```
Add https://notebooklm.google.com/notebook/abc123-wetic-elearning to library tagged "pedagogie, scaffolding, learning-by-doing"
```

```
Add https://notebooklm.google.com/notebook/def456-outofthebox to library tagged "methodologie, transformation, as-is-to-be"
```

```
Add https://notebooklm.google.com/notebook/ghi789-wetic-solene to library tagged "projet, client, dotnet8, architecture"
```

### Étape 3.3 : Vérifier la Bibliothèque

**Dans Cascade** :

```
Show our notebooks library
```

**Résultat attendu** : Liste des 3 notes avec leurs tags et descriptions.

---

## Phase 4 : Utilisation Dynamique (Immédiat)

### Commandes de Base

**Poser une question sur une note spécifique** :

```
Use the "Wetic Elearning" notebook and answer: Quelle est la meilleure approche pour la démonstration - side-by-side ou transformation progressive ?
```

**Recherche multi-notes automatique** :

```
Research in NotebookLM before answering: Comment structurer un support de formation pour maximiser l'apprentissage actif ?
```

**Sélection contextuelle automatique** :

```
Based on my NotebookLM library, what are the best practices for AS-IS to TO-BE transformation in legacy code migration?
```

### Workflow Type pour Génération de Support

**Exemple concret** :

```
Je vais créer le support pour Jour1 10h40-12h30 (Création Structure .NET 8).

Avant de générer le support :
1. Consulte "Wetic Elearning" pour les principes pédagogiques (scaffolding, timing optimal)
2. Consulte "outofthebox" pour la méthodologie de transformation progressive
3. Consulte "WETIC-Solene" pour le contexte spécifique du projet client

Puis génère le support en appliquant ces principes.
```

**Cascade va** :
1. Interroger automatiquement les 3 notes NotebookLM
2. Synthétiser les informations pertinentes
3. Générer le support en respectant les méthodologies documentées
4. Citer les sources (sans mentionner NotebookLM dans le support final apprenant)

---

## Phase 5 : Maintenance et Dépannage

### Commandes de Maintenance

**Vérifier la santé du serveur** :

```
Check NotebookLM connection health
```

**Ré-authentifier** (si session expirée) :

```
Repair NotebookLM authentication
```

**Voir la conversation NotebookLM en direct** :

```
Show me the browser
```

**Nettoyer les données** (nouveau départ) :

```
Run NotebookLM cleanup
```

**Supprimer tout sauf la bibliothèque** :

```
Cleanup but keep my library
```

### Problèmes Courants

| Problème | Solution |
|----------|----------|
| Serveur MCP ne démarre pas | Vérifier `mcp_config.json` et cliquer Refresh |
| Authentification expirée | Taper `Fix auth` dans Cascade |
| Note non trouvée | Vérifier que le lien est partagé "Anyone with link" |
| Trop de tools chargés | Changer `NOTEBOOKLM_PROFILE` à `minimal` |
| Réponses lentes | Utiliser profil `minimal` ou désactiver outils inutiles |

### Vérification de la Configuration

**Fichier de référence** : `.windsurf/mcp/notebooklm-config.json` (ce projet)

**Commande de vérification** :

```powershell
# Vérifier que npx est installé
npx --version

# Tester le serveur MCP directement
npx -y notebooklm-mcp@latest config get
```

---

## Configuration Avancée (Optionnel)

### Profils d'Outils

**Minimal** (5 outils - query uniquement) :

```json
"env": {
  "NOTEBOOKLM_PROFILE": "minimal"
}
```

**Standard** (10 outils - query + bibliothèque) :

```json
"env": {
  "NOTEBOOKLM_PROFILE": "standard"
}
```

**Full** (16 outils - toutes fonctionnalités) :

```json
"env": {
  "NOTEBOOKLM_PROFILE": "full"
}
```

### Désactiver des Outils Spécifiques

```json
"env": {
  "NOTEBOOKLM_PROFILE": "standard",
  "NOTEBOOKLM_DISABLED_TOOLS": "cleanup_data,re_auth,remove_notebook"
}
```

---

## Checklist de Validation Finale

- [ ] Serveur MCP "notebooklm" actif dans Windsurf (point vert)
- [ ] Authentification réussie avec `contact@digitar.be`
- [ ] Les 3 notes apparaissent dans `List my notebooks`
- [ ] Les 3 notes ajoutées à la bibliothèque avec tags
- [ ] Test réussi : Question posée et réponse obtenue depuis NotebookLM
- [ ] Configuration sauvegardée dans `.windsurf/mcp/notebooklm-config.json`

---

## Résultat Attendu

**Avant MCP** :
- Export manuel des notes en Markdown
- Lecture statique des fichiers locaux
- Pas de mise à jour automatique si notes changent
- Cascade ne peut pas poser de questions de suivi

**Après MCP** :
- ✅ Consultation dynamique en temps réel
- ✅ Cascade interroge NotebookLM directement
- ✅ Questions de suivi automatiques pour approfondir
- ✅ Synthèse experte par Gemini (moteur de NotebookLM)
- ✅ Zéro hallucination (refuse de répondre si info absente)
- ✅ Citations des sources dans les réponses
- ✅ Mises à jour automatiques si notes modifiées

---

**Durée de vie** : Configuration pérenne, persiste entre sessions Windsurf.

**Prochaine session** : Aucune configuration requise, MCP actif automatiquement.

**Mise à jour des notes** : Les changements sur NotebookLM sont immédiatement accessibles, pas besoin de reconfigurer.

---

**Créé le** : 9 mars 2026  
**Version** : 1.0  
**Projet** : Formation .NET Modernisation  
**Type** : Workflow de configuration unique
