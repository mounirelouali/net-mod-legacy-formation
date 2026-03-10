# Guide Complet : Connecter Windsurf avec NotebookLM via MCP (2026)

## Vue d'ensemble

L'intégration de Windsurf avec NotebookLM via le protocole MCP (Model Context Protocol) représente la méthode la plus récente et validée en 2026 pour établir une connexion dynamique entre votre éditeur de code AI et la base de connaissances NotebookLM. Cette approche permet à Windsurf de consulter automatiquement vos notes NotebookLM dans un pipeline structuré, éliminant les hallucinations et fournissant des réponses basées sur vos propres sources documentaires.[^1][^2]

Le Model Context Protocol est un standard ouvert développé par Anthropic qui fonctionne comme un "USB-C pour l'IA" - un protocole unique permettant à n'importe quel modèle d'IA de se connecter à n'importe quel outil. Windsurf supporte nativement l'intégration MCP depuis 2026, permettant d'ajouter des serveurs MCP personnalisés pour que Cascade puisse accéder à des outils et services externes.[^3][^4]

## La Solution Recommandée : NotebookLM MCP Server

### Pourquoi Cette Approche ?

Le serveur MCP NotebookLM (notebooklm-mcp) développé par PleasePrompto est devenu la solution de référence validée par la communauté avec 448 étoiles sur GitHub. Cette approche résout plusieurs problèmes critiques :[^1]

**Problèmes résolus :**
- **Consommation massive de tokens** : Plus besoin de lire répétitivement plusieurs fichiers de documentation
- **Récupération inexacte** : Évite les recherches par mots-clés qui manquent le contexte
- **Hallucinations** : NotebookLM refuse de répondre si l'information n'est pas dans vos sources
- **Coût et lenteur** : Réduction drastique des lectures répétées de fichiers[^1]

**Avantages de NotebookLM vs RAG local :**

| Approche | Coût en tokens | Temps de setup | Hallucinations | Qualité de réponse |
|----------|----------------|----------------|----------------|-------------------|
| Documentation dans Claude | 🔴 Très élevé | Instantané | Oui - comble les lacunes | Variable |
| Recherche web | 🟡 Moyen | Instantané | Élevé | Aléatoire |
| RAG local | 🟡 Moyen-Élevé | Heures | Moyen | Dépend du setup |
| **NotebookLM MCP** | 🟢 Minimal | **5 minutes** | **Zéro** | **Synthèse experte** |

[^1]

### Caractéristiques Clés

NotebookLM offre une supériorité technique grâce à :

1. **Pré-traitement par Gemini** : Les documents sont téléchargés une fois et transformés en base de connaissances experte
2. **Q&A en langage naturel** : Pas seulement de la récupération, mais une vraie compréhension et synthèse
3. **Corrélation multi-sources** : Connecte les informations à travers plus de 50 documents simultanément
4. **Citations** : Chaque réponse inclut des références aux sources
5. **Aucune infrastructure** : Pas de bases de données vectorielles, d'embeddings ou de stratégies de chunking nécessaires[^1]

NotebookLM possède une fenêtre de contexte énorme de 1 million de tokens, permettant d'analyser des livres complets, des rapports financiers d'une année entière, plus de 300 PDFs, ou des collections de documents massives.[^5][^6]

## Installation et Configuration

### Étape 1 : Installation du Serveur MCP

Pour Windsurf, la configuration MCP se fait via le fichier `mcp_config.json` situé dans `~/.codeium/windsurf/mcp_config.json`.[^3]

**Configuration pour Windsurf :**

```json
{
  "mcpServers": {
    "notebooklm": {
      "command": "npx",
      "args": ["-y", "notebooklm-mcp@latest"]
    }
  }
}
```


**Méthode d'installation via l'interface :**

1. Ouvrez Windsurf
2. Dans le panneau Cascade, cliquez sur l'icône **MCPs** (icône marteau) dans le menu supérieur droit
3. Cliquez sur **Configure** pour ouvrir le fichier `mcp_config.json`
4. Collez la configuration ci-dessus
5. Cliquez sur **Refresh** dans le panneau de gestion des plugins pour activer le serveur[^7][^8]

**Alternative via CLI (pour autres éditeurs) :**

```bash
# Claude Code
claude mcp add notebooklm npx notebooklm-mcp@latest

# Codex
codex mcp add notebooklm -- npx notebooklm-mcp@latest

# Cursor (ajouter à ~/.cursor/mcp.json)
{
  "mcpServers": {
    "notebooklm": {
      "command": "npx",
      "args": ["-y", "notebooklm-mcp@latest"]
    }
  }
}
```


### Étape 2 : Authentification (Une Seule Fois)

Une fois le serveur MCP installé, vous devez vous authentifier avec votre compte Google :

1. Dans le chat Windsurf Cascade, tapez : **"Log me in to NotebookLM"** ou **"Open NotebookLM auth setup"**
2. Une fenêtre Chrome s'ouvrira automatiquement
3. Connectez-vous avec votre compte Google
4. L'authentification est persistante et stockée localement dans `~/.config/notebooklm-mcp/`[^9][^1]

**Note de sécurité :** Chrome s'exécute localement, vos identifiants ne quittent jamais votre machine. Il est recommandé d'utiliser un compte Google dédié pour l'automatisation.[^1]

**Durée de vie de l'authentification :**

| Composant | Durée | Rafraîchissement |
|-----------|-------|------------------|
| Cookies | ~2-4 semaines | Ré-extraire de Chrome quand expiré |
| Token CSRF | Par session MCP | Auto-extrait au démarrage |
| Session ID | Par session MCP | Auto-extrait au démarrage |

[^10]

### Étape 3 : Création de Votre Base de Connaissances

1. Allez sur [notebooklm.google.com](https://notebooklm.google.com)
2. Créez un nouveau notebook
3. Téléchargez vos sources :
   - 📄 PDFs, Google Docs, fichiers markdown
   - 🔗 Sites web, dépôts GitHub
   - 🎥 Vidéos YouTube
   - 📚 Plusieurs sources par notebook (jusqu'à 50+)
4. Partagez le notebook : **⚙️ Share → Anyone with link → Copy**[^1]

### Étape 4 : Utilisation dans Windsurf

Une fois configuré, vous pouvez utiliser NotebookLM directement depuis Windsurf :

```
"Je développe avec [bibliothèque]. Voici mon NotebookLM: [lien]"
```

Windsurf/Cascade interrogera automatiquement NotebookLM pour obtenir les informations nécessaires avant d'écrire du code.[^1]

## Fonctionnalités Avancées

### Gestion de Bibliothèque Intelligente

Le serveur MCP NotebookLM permet de sauvegarder des liens NotebookLM avec des tags et descriptions. Claude/Cascade sélectionne automatiquement le notebook pertinent selon votre tâche :

```
"Ajoute [lien] à la bibliothèque taggé 'frontend, react, components'"
```

**Commandes de gestion :**

| Intention | Commande | Résultat |
|-----------|----------|----------|
| Ajouter un notebook | *"Add [link] to library"* | Sauvegarde avec métadonnées |
| Lister les notebooks | *"Show our notebooks"* | Liste tous les notebooks sauvegardés |
| Recherche avant codage | *"Research this in NotebookLM before coding"* | Session multi-questions |
| Sélectionner un notebook | *"Use the React notebook"* | Définit le notebook actif |
| Mettre à jour | *"Update notebook tags"* | Modifie les métadonnées |
| Supprimer | *"Remove [notebook] from library"* | Supprime de la bibliothèque |

[^1]

### Recherche Itérative Profonde

L'un des avantages majeurs est la capacité de Claude à poser automatiquement des questions de suivi pour construire une compréhension complète :

**Exemple de conversation AI-à-AI :**

```
Claude → "Comment fonctionne l'intégration Gmail dans n8n?"
NotebookLM → "Utilise Gmail Trigger avec polling, ou Gmail node avec Get Many..."

Claude → "Comment décoder le corps d'email base64?"
NotebookLM → "Le corps est encodé en base64url dans payload.parts, utilise Function node..."

Claude → "Comment parser la réponse OpenAI comme JSON?"
NotebookLM → "Définis responseFormat à json, utilise {{ $json.spam }} dans IF node..."

Claude → ✅ "Voici ton workflow JSON complet..."
```


### Profils d'Outils pour Optimisation

Pour réduire la consommation de tokens, vous pouvez configurer des profils qui chargent uniquement les outils nécessaires :

| Profil | Outils | Cas d'usage |
|--------|--------|-------------|
| **minimal** | 5 | Query uniquement : `ask_question`, `get_health`, `list_notebooks`, `select_notebook`, `get_notebook` |
| **standard** | 10 | + Gestion bibliothèque : `setup_auth`, `list_sessions`, `add_notebook`, `update_notebook`, `search_notebooks` |
| **full** | 16 | Tous les outils incluant `cleanup_data`, `re_auth`, `remove_notebook`, `reset_session`, `close_session`, `get_library_stats` |

[^1]

**Configuration via CLI :**

```bash
# Vérifier les paramètres actuels
npx notebooklm-mcp config get

# Définir un profil
npx notebooklm-mcp config set profile minimal
npx notebooklm-mcp config set profile standard
npx notebooklm-mcp config set profile full

# Désactiver des outils spécifiques
npx notebooklm-mcp config set disabled-tools "cleanup_data,re_auth"

# Réinitialiser aux valeurs par défaut
npx notebooklm-mcp config reset
```


**Configuration via variables d'environnement :**

```bash
# Définir le profil
export NOTEBOOKLM_PROFILE=minimal

# Désactiver des outils spécifiques
export NOTEBOOKLM_DISABLED_TOOLS="cleanup_data,re_auth,remove_notebook"
```


Les paramètres sont sauvegardés dans `~/.config/notebooklm-mcp/settings.json` et persistent entre les sessions.[^1]

## Architecture et Flux de Données

Le flux de travail complet fonctionne comme suit :

```
Votre Tâche → Claude/Windsurf → Serveur MCP → Automatisation Chrome 
→ NotebookLM → Gemini 2.5 → Vos Documents → Gemini 2.5 
→ NotebookLM → Automatisation Chrome → Serveur MCP 
→ Claude/Windsurf → Code Précis
```


Cette architecture garantit :
- **Séparation des responsabilités** : MCP gère la récupération de données, Windsurf gère le raisonnement
- **Contexte optimisé** : Chaque appel API consomme une partie de la même fenêtre de contexte
- **Prévisibilité** : L'agent reste réactif et prévisible[^11]

## Solutions Alternatives et Comparaisons

### Alternative 1 : API NotebookLM Enterprise (Limitée)

Google a lancé l'API NotebookLM mais elle est **uniquement disponible pour l'édition Enterprise**. Cette API permet :[^12]
- Accès programmatique aux notebooks
- Création et gestion de notebooks
- Intégration avec des applications personnalisées

**Limitations :**
- Accès restreint (Enterprise uniquement)
- Nécessite une configuration Google Cloud Console
- Plus complexe à mettre en place pour un usage individuel[^12]

### Alternative 2 : Serveur MCP HTTP Direct

Un développeur a créé un serveur MCP qui utilise des appels HTTP/RPC directs au lieu de l'automatisation du navigateur. Cette approche offre :[^13]

**Avantages :**
- **Vitesse supérieure** : Pas de navigateur headless, moins de ressources
- **31 outils distincts** : Création de notebooks, upload de sources, synchronisation Google Drive, génération d'audio overviews[^13]
- **Fonctionnement** : Permet des instructions comme "Crée un nouveau notebook sur le sujet X, fais une recherche approfondie, inclus toutes les sources, puis génère un audio overview, une infographie et un document de briefing"[^13]

**GitHub :** [jacob-bd/notebooklm-mcp](https://github.com/jacob-bd/notebooklm-mcp)[^10]

**Outils disponibles :**

| Catégorie | Outils |
|-----------|--------|
| **Gestion notebooks** | `notebook_list`, `notebook_create`, `notebook_get`, `notebook_describe`, `notebook_rename`, `notebook_delete` |
| **Sources** | `source_describe`, `source_get_content`, `notebook_add_url`, `notebook_add_text`, `notebook_add_drive`, `source_list_drive`, `source_sync_drive`, `source_delete` |
| **Recherche** | `notebook_query`, `research_start`, `research_status`, `research_import` |
| **Studio** | `audio_overview_create`, `video_overview_create`, `infographic_create`, `slide_deck_create`, `studio_status`, `studio_delete` |
| **Configuration** | `chat_configure`, `save_auth_tokens` |

[^10]

### Alternative 3 : Workflow n8n + NotebookLM

Pour des flux de travail plus complexes impliquant plusieurs systèmes, n8n peut servir de couche d'orchestration. Cette approche permet :[^14]

**Architecture typique :**
1. **Webhook Node (n8n)** : Reçoit les payloads JSON des mises à jour NotebookLM
2. **Data Transformation Node** : Parse les métadonnées et le contenu des documents
3. **AI Node** : Affine les résumés ou catégorise les insights
4. **Storage Node** : Envoie les sorties structurées vers Airtable ou une base de données
5. **Notification Node** : Poste les mises à jour sur Slack ou par email[^14]

**Cas d'usage :**
- Génération automatique de briefs à partir de nouveaux pods de recherche
- Notifications déclenchées quand les résumés sont mis à jour
- Alimentation de sorties structurées dans Notion, Airtable ou HubSpot
- Résumés périodiques planifiés quand le dataset évolue
- Taggage et classification d'insights avec des classifieurs AI avant archivage[^14]

**Considérations de gouvernance :**
- **Contrôle d'accès** : Déclencher uniquement depuis des sources vérifiées
- **Protection PII** : Rédiger les informations sensibles avant transmission
- **Audit logging** : Utiliser les logs d'exécution intégrés de n8n
- **SSO et chiffrement** : Implémenter OAuth 2.0 et credentials chiffrés[^14]

### Alternative 4 : Obsidian + Windsurf + NotebookLM

Cette approche combine trois outils pour un workflow de création de contenu :[^15]

**Processus :**
1. **Obsidian** : Agrégation de recherche et prise de notes en Markdown
2. **Windsurf** : Génération de contenu AI basée sur vos notes Obsidian
3. **NotebookLM** : Comparaison et validation (utilisé manuellement)

**Avantages :**
- **Images préservées** : Obsidian Web Clipper sauve les images avec le texte
- **Règles détaillées** : Possibilité de créer des fichiers `.windsurfrules` pour guider l'AI
- **Contrôle du processus** : Plus de contrôle sur chaque étape[^15]

**Limitations :**
- Pas d'intégration automatique avec NotebookLM
- Nécessite des étapes manuelles de validation
- Moins fluide que le MCP direct[^15]

## Bonnes Pratiques et Recommandations

### Configuration Optimale pour Production

Pour une utilisation en équipe ou en production, plusieurs options sont disponibles :

**Contrôles administratifs (Teams & Enterprises) :**
Les administrateurs d'équipe peuvent basculer l'accès MCP et établir une liste blanche de serveurs MCP approuvés. Une fois qu'un seul serveur MCP est ajouté à la liste blanche, **tous les serveurs non listés seront bloqués** pour l'équipe.[^3]

**Options de configuration :**

1. **Plugin Store Default** (Recommandé) : Laisser le champ Server Config vide pour permettre la configuration par défaut
2. **Exact Match** : Fournir la configuration exacte que les utilisateurs doivent utiliser
3. **Flexible Regex** : Utiliser des patterns regex pour permettre des variations tout en maintenant le contrôle de sécurité[^3]

### Sécurité et Confidentialité

**Recommandations de sécurité :**
- Utiliser un compte Google dédié pour l'automatisation plutôt que votre compte principal
- L'automatisation du navigateur inclut des fonctionnalités d'humanisation (vitesses de frappe réalistes, délais naturels) mais Google pourrait potentiellement détecter l'usage automatisé[^1]
- Chrome s'exécute localement, les credentials ne quittent jamais votre machine[^1]

**Gestion des sessions :**
- Les cookies durent 2-4 semaines avant expiration
- Les tokens CSRF et Session ID sont auto-extraits à chaque démarrage MCP
- Commande de ré-authentification : *"Repair NotebookLM authentication"* ou *"Re-authenticate with different Google account"*[^1]

### Optimisation des Performances

**Réduction de la consommation de tokens :**
1. Utiliser le profil `minimal` pour les tâches de query uniquement
2. Désactiver les outils spécifiques non nécessaires avec `disabled-tools`
3. Limiter le nombre de MCPs actifs simultanément (Cascade a une limite de 100 outils totaux)[^3]

**Gestion du contexte :**
- Séparer les responsabilités : MCP pour la récupération de données, Windsurf pour le raisonnement
- Éviter de charger tous les outils si seuls quelques-uns sont nécessaires
- Utiliser la bibliothèque de notebooks pour une sélection automatique et contextuelle[^11]

### Dépannage

**Problèmes courants et solutions :**

| Problème | Solution |
|----------|----------|
| MCP ne se connecte pas | Vérifier `~/.codeium/windsurf/mcp_config.json` et cliquer sur Refresh |
| Authentification expirée | Exécuter *"Repair NotebookLM authentication"* |
| Serveur ne répond pas | Vérifier les logs Windsurf : `Cmd/Ctrl + Shift + P` → "Developer: Show Logs.." → Windsurf |
| Trop de tools chargés | Configurer un profil `minimal` ou `standard` |
| Conflit de regex (Teams) | Vérifier l'échappement des caractères spéciaux dans la whitelist |

[^16][^3]

**Commandes de maintenance :**

```
"Show me the browser"          # Voir la conversation NotebookLM en direct
"Fix auth"                      # Réparer l'authentification
"Run NotebookLM cleanup"       # Supprimer toutes les données pour un nouveau départ
"Cleanup but keep my library"  # Préserver les notebooks
"Delete all NotebookLM data"   # Suppression complète
```


## Cas d'Usage Réels

### Exemple 1 : Développement sans Hallucinations (n8n Workflow)

**Défi :** L'API n8n est nouvelle, Claude hallucine les noms de nœuds et fonctions.

**Solution :**
1. Téléchargement de la documentation complète n8n → fusion en chunks gérables
2. Upload vers NotebookLM
3. Instruction à Claude : *"Construis-moi un workflow de filtre spam Gmail. Utilise ce NotebookLM: [lien]"*

**Résultat :** Workflow parfait du premier coup. Pas de débogage d'APIs hallucinées.[^1]

### Exemple 2 : Documentation Produit Dynamique

Les équipes produit peuvent intégrer NotebookLM avec des dépôts et générer de la documentation AI actualisée synchronisée avec le contrôle de version ou les pages Notion.[^14]

**Flux typique :**
1. Trigger : Nouveau fichier ajouté au dossier Google Drive partagé
2. Process : n8n appelle l'API NotebookLM pour récupérer les résumés mis à jour
3. Refine : Un nœud GPT réécrit le contenu en "executive brief"
4. Store : Le brief est envoyé à Airtable, catégorisé par sujet
5. Distribute : n8n poste un résumé dans Slack et génère une nouvelle entrée Notion[^14]

### Exemple 3 : Recherche Marketing à Grande Échelle

Avec la fenêtre de contexte de 1 million de tokens, NotebookLM peut analyser :
- Des livres entiers (150 000-200 000 mots)
- 50+ rapports concurrentiels simultanément
- 12 mois de données de performance de campagne
- Transcripts de 20-30 épisodes de podcast
- Bibliothèques complètes de recherche client[^6]

**Workflow typique :**
1. Créer un notebook "Content Versioning" avec :
   - Article de blog original (Google Doc)
   - Guidelines de ton et voix de la marque
   - Posts de médias sociaux passés
2. Demander à Claude de générer des variations pour différentes plateformes
3. NotebookLM assure la cohérence de la marque à travers toutes les versions[^6]

## Limites et Considérations

### Limites Techniques

**Rate limits :**
- Le tier gratuit a des limites de requêtes quotidiennes par compte Google
- Solution : Changement rapide de compte supporté pour continuer la recherche[^1]

**Dépendance au navigateur :**
- Nécessite Chrome pour l'automatisation (approche PleasePrompto)
- Risque potentiel de détection par Google (bien que minimisé par humanisation)[^1]

**Disponibilité API officielle :**
- L'API NotebookLM officielle est Enterprise uniquement
- Les solutions MCP utilisent l'automatisation du navigateur ou des appels HTTP non officiels[^12][^13]

### Considérations Légales et Éthiques

**Automation et TOS :**
- L'automatisation du navigateur peut potentiellement violer les conditions d'utilisation
- Recommandation : Utiliser un compte dédié, pas votre compte principal[^1]

**Responsabilité :**
- Les outils CLI AI peuvent faire des erreurs
- Toujours revoir les changements avant commit ou déploiement
- Tester dans des environnements sûrs d'abord
- Garder des backups du travail important[^1]

## Évolutions Futures et Tendances 2026

### Nouvelles Fonctionnalités NotebookLM 2026

Google a discrètement déployé 15 mises à jour majeures pour NotebookLM ces dernières semaines :[^5]

**Studio Panel :** Nouveau centre de commande pour générer des présentations, infographies, quiz et mind maps en un clic.[^17]

**Audio interactif :** Possibilité de "lever la main" pendant un podcast AI pour poser des questions de suivi en temps réel.[^17]

**Génération de slides automatique :** Transformation de 50+ sources désordonnées en présentation structurée de 25 slides utilisant le moteur visuel Nano Banana Pro.[^17]

**Intégration Gemini :** Nouveau menu "Attachment" permettant d'extraire des notebooks NotebookLM entiers dans Gemini 3 pour exécution créative.[^17]

**Extraction de données :** Conversion automatique de centaines de pages concurrentes en tables CSV propres et exportables.[^17]

**Custom Project Expert :** Création d'un "jardin fermé" AI qui ne connaît que les SOPs de votre entreprise, évitant les hallucinations des chatbots génériques.[^17]

### Évolution du Protocole MCP

Le MCP évolue rapidement avec :
- Support OAuth pour tous les types de transport
- Intégration HTTP streamable et SSE (Server-Sent Events)
- Meilleure gestion des ressources, prompts et outils
- Débogage amélioré avec MCP Inspector[^4][^3]

### Tendances d'Adoption

**Impact mesurable :**
- 70-90% de réduction du temps de résumé manuel
- Contrôle de version fluide entre notes de recherche et livrables publiés
- Structure cohérente à travers toutes les sorties de connaissance
- Découvrabilité accrue des actifs de recherche internes[^14]

**Adoption en entreprise :**
Les systèmes de connaissance AI évoluent de la *récupération* vers le *raisonnement* vers l'*automation*. NotebookLM représente la couche de raisonnement ; les outils comme MCP et n8n fournissent la couche d'automation. Connectés, ils forment un véritable **workflow agentique** - une boucle intelligente qui contextualise et agit sans intervention manuelle.[^14]

## Conclusion et Prochaines Étapes

L'intégration de Windsurf avec NotebookLM via le protocole MCP représente en 2026 la solution la plus mature, validée et efficace pour établir une connexion dynamique entre votre éditeur de code AI et vos bases de connaissances. Cette approche élimine les hallucinations, réduit drastiquement la consommation de tokens, et permet à vos agents AI de raisonner sur vos propres documents plutôt que d'inventer des réponses plausibles mais incorrectes.

### Résumé des Options Validées

**Pour usage individuel (Recommandé) :**
- **Serveur MCP PleasePrompto** : 5 minutes de setup, authentification persistante, bibliothèque de notebooks[^1]
- Installation : `npx notebooklm-mcp@latest` dans `mcp_config.json`

**Pour automatisations avancées :**
- **Serveur MCP HTTP/RPC Direct** : 31 outils, génération de contenu studio, plus rapide[^13][^10]
- Idéal pour pipelines CI/CD et automatisations serverless

**Pour orchestration multi-systèmes :**
- **n8n + NotebookLM** : Workflows complexes, intégrations tierces, gouvernance d'entreprise[^14]
- Nécessite configuration middleware mais offre flexibilité maximale

### Checklist de Démarrage Rapide

- [ ] Installer le serveur MCP dans Windsurf (`mcp_config.json`)
- [ ] S'authentifier avec compte Google dédié
- [ ] Créer un notebook NotebookLM avec documentation clé
- [ ] Partager le notebook (Anyone with link)
- [ ] Tester avec une question simple : "Research this in NotebookLM before coding"
- [ ] Configurer le profil d'outils selon vos besoins (`minimal`, `standard`, `full`)
- [ ] Ajouter le notebook à la bibliothèque avec tags pertinents

### Meilleures Pratiques pour Démarrer

1. **Commencez petit** : Un seul notebook avec 5-10 documents clés de votre projet actuel
2. **Testez la qualité** : Posez des questions dont vous connaissez la réponse pour valider
3. **Augmentez progressivement** : Ajoutez plus de notebooks à mesure que vous gagnez en confiance
4. **Organisez votre bibliothèque** : Utilisez des tags cohérents pour faciliter la sélection automatique
5. **Surveillez les tokens** : Utilisez le profil `minimal` par défaut, passez à `standard` ou `full` au besoin

### Ressources Supplémentaires

**Documentation officielle :**
- [Windsurf MCP Documentation](https://docs.windsurf.com/windsurf/cascade/mcp)[^3]
- [NotebookLM MCP Server GitHub](https://github.com/PleasePrompto/notebooklm-mcp)[^1]
- [Model Context Protocol Specification](https://modelcontextprotocol.io)

**Communautés et support :**
- GitHub Issues pour bug reports et feature requests
- Reddit : r/windsurf, r/notebooklm
- Discord servers des projets open source MCP

**Alternatives à explorer :**
- [jacob-bd/notebooklm-mcp](https://github.com/jacob-bd/notebooklm-mcp) pour HTTP direct[^10]
- [SurfSense](https://surfsense.com) comme alternative open source à NotebookLM[^18]
- n8n workflows pour orchestration complexe[^14]

L'écosystème MCP + NotebookLM + Windsurf continue d'évoluer rapidement. Cette intégration transforme fondamentalement la façon dont les développeurs interagissent avec la documentation et les bases de connaissances, passant d'un modèle de recherche manuelle et copier-coller à un modèle d'agents AI autonomes qui recherchent, synthétisent et appliquent les connaissances de manière fluide et fiable.

---

## References

1. [PleasePrompto/notebooklm-mcp](https://github.com/PleasePrompto/notebooklm-mcp) - MCP server for NotebookLM - Let your AI agents (Claude Code, Codex) research documentation directly ...

2. [Connect Windsurf to an MCP Server | Generate SDKs for your API ... - liblab](https://liblab.com/docs/mcp/howto-connect-mcp-to-windsurf) - Learn how to connect Windsurf to a local or hosted MCP server, enabling you to interact with your AP...

3. [Cascade MCP Integration - Windsurf Docs](https://docs.windsurf.com/windsurf/cascade/mcp) - Cascade now natively integrates with MCP, allowing you to bring your own selection of MCP servers fo...

4. [The Model Context Protocol - NotebookLM video](https://www.youtube.com/watch?v=zc68eWGGBwQ) - I prompted Claude with "Can you re-write this as an text doc I can use in NotebookLM to produce a po...

5. [Google quietly dropped 15 major updates for NotebookLM over the last few weeks. Here are all the new features explained with workflows and prompt templates to turn you into a power user who gets top 1% results.](https://www.reddit.com/r/promptingmagic/comments/1oz8lgf/google_quietly_dropped_15_major_updates_for/) - Google quietly dropped 15 major updates for NotebookLM over the last few weeks. Here are all the new...

6. [The Complete Guide To Using Notebook LM For Marketing In 2026](https://marketingagent.blog/2026/02/05/the-complete-guide-to-using-notebook-lm-for-marketing-in-2026/) - Create notebook: “Content Versioning” with: Original blog post (Google Doc); Brand tone and voice gu...

7. [Power of MCP in Windsurf IDE : A Developer's Step-by-step Guide](https://www.thetoolnerd.com/p/power-of-mcp-in-windsurf-ide-a-developers-guide) - Guide to setup MCP in Windsurf IDE. Setting up MCP in Windsurf can help you get access to local file...

8. [Configuring Your First MCP Server - Windsurf](https://windsurf.com/university/tutorials/configuring-first-mcp-server) - Learn how to extend Cascade with external tools and data sources using MCP

9. [NotebookLM MCP Server](https://mcpservers.org/servers/roomi-fields/notebooklm-mcp) - Chat with Google NotebookLM via MCP or HTTP REST API for zero-hallucination answers from your docs. ...

10. [jacob-bd/notebooklm-mcp - GitHub](https://github.com/jacob-bd/notebooklm-mcp) - Contribute to jacob-bd/notebooklm-mcp development by creating an account on GitHub.

11. [Using Windsurf Workflows to Automate the Software Development ...](https://www.vladkhambir.com/posts/windsurf-workflows-software-development-lifecycle/) - In this article, I'll describe how Windsurf Workflows can be structured to automate common developme...

12. [Google NotebookLM API | Enterprise-Only Access & How It Works | Programmatic Access Demo](https://www.youtube.com/watch?v=tXZlegQhV6Q) - Google just released the NotebookLM API, but it’s only available in the Enterprise edition. In this ...

13. [I created a direct HTTP/RPC calls NotebookLM MCP - Reddit](https://www.reddit.com/r/notebooklm/comments/1q0inws/i_created_a_direct_httprpc_calls_notebooklm_mcp/) - I looked at existing MCP (Model Context Protocol) solutions, but I noticed most of them rely on brow...

14. [NotebookLM with n8n: From Research Pods to Automated Briefs](https://scalevise.com/resources/notebooklm-with-n8n/) - Turn Google’s NotebookLM into a workflow engine. Learn how to connect it with n8n to automate resear...

15. [Obsidian x Windsurf で記事作成は捗るのか？NotebookLM ...](https://note.com/komzweb/n/n47516cd86a56) - 最近、「Obsidian」と「Cursor / Windsurf」を使用して記事のドラフトを作成する方法が注目を集めています。Cursor や Windsurf は、本来はアプリ開発などに使用するツー...

16. [MCP Setup Guide for Windsurf IDE](https://natoma.ai/blog/how-to-enabling-mcp-in-windsurf) - Learn how to integrate Windsurf with MCP servers to enable AI access to GitHub, Asana, and Datadog. ...

17. [#313 Max: NotebookLM 2026 – 7 Use Cases That Changed Everything](https://www.youtube.com/watch?v=fFcwPv_NN4A) - I was wrong about NotebookLM. 🤯 It’s no longer just a "PDF chatter." We’re breaking down the seven 2...

18. [NEW AI Tool DESTROYS Google NotebookLM? (FREE + Open‑Source)](https://www.youtube.com/watch?v=Wk1ZGlxShUk) - Want to make money and save time with AI? Get AI Coaching, Support & Courses 👉 https://www.skool.com...

