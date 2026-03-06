# Base de Connaissances : notebooklm-py

## Vue d'ensemble

**notebooklm-py** est une API Python non officielle pour Google NotebookLM qui offre un accès programmatique complet aux fonctionnalités de NotebookLM, y compris des capacités que l'interface web n'expose pas.

### Informations du projet

- **Repository** : https://github.com/teng-lin/notebooklm-py
- **Licence** : MIT
- **Type** : Bibliothèque non officielle
- **Langage** : Python (API asynchrone)

### ⚠️ Avertissement important

Cette bibliothèque utilise des API Google non documentées susceptibles de changer sans préavis :
- **Non affilié à Google** - Projet communautaire
- **APIs peuvent changer** - Google peut modifier les endpoints internes à tout moment
- **Limites de taux** - L'utilisation intensive peut être limitée
- **Recommandation** : Idéal pour prototypes, recherche et projets personnels

## Fonctionnalités principales

### 1. Automatisation de la recherche
- Import en masse de sources (URLs, PDFs, YouTube, Google Drive)
- Requêtes de recherche web/Drive avec auto-import
- Extraction programmatique d'insights
- Pipelines de recherche reproductibles

### 2. Génération de contenu
Génération de multiples formats :
- 🎙️ **Audio Overviews** (podcasts)
- 🎥 **Vidéos**
- 📊 **Présentations** (slide decks)
- ❓ **Quiz**
- 🗂️ **Flashcards**
- 📈 **Infographies**
- 📋 **Tableaux de données**
- 🧠 **Cartes mentales** (mind maps)
- 📚 **Guides d'étude**

Contrôle complet sur les formats, styles et sorties.

### 3. Téléchargements et export
- 💾 Téléchargement de tous les artefacts générés localement
- **Formats supportés** : MP3, MP4, PDF, PNG, CSV, JSON, Markdown
- Export vers Google Docs/Sheets

### 4. Intégration d'agents IA
- Intégration dans Claude Code ou autres agents LLM
- Skills Claude Code pour automatisation en langage naturel (`notebooklm skill install`)
- API Python asynchrone pour construire ses propres intégrations

## Fonctionnalités exclusives à l'API (non disponibles dans l'UI web)

Ces fonctionnalités sont disponibles uniquement via l'API/CLI :

1. **Téléchargements en lot** - Télécharger tous les artefacts d'un type en une fois
2. **Export Quiz/Flashcards** - Obtenir du JSON, Markdown ou HTML structuré
3. **Extraction de données de cartes mentales** - Export JSON hiérarchique pour outils de visualisation
4. **Export CSV de tableaux de données** - Télécharger des tableaux structurés en feuilles de calcul
5. **Slide deck en PPTX** - Télécharger des fichiers PowerPoint éditables
6. **Révision de slides** - Modifier des slides individuels avec des prompts en langage naturel
7. **Personnalisation des templates de rapports** - Ajouter des instructions supplémentaires aux templates de format intégrés
8. **Sauvegarder le chat en notes** - Sauvegarder les réponses Q&A ou l'historique de conversation
9. **Accès au texte complet des sources** - Récupérer le contenu textuel indexé de n'importe quelle source
10. **Partage programmatique** - Gérer les permissions sans l'interface UI

## Installation

### Installation basique
```bash
pip install notebooklm-py
```

### Installation avec support de connexion navigateur
```bash
# Requis pour la première configuration
pip install "notebooklm-py[browser]"
playwright install chromium
```

### Installation pour développement
```bash
# Pour contributeurs ou tests de fonctionnalités non publiées
pip install git+https://github.com/teng-lin/notebooklm-py@main
```

⚠️ La branche `main` peut contenir des changements instables. Utilisez les releases PyPI pour la production.

## Démarrage rapide

### 1. Interface en ligne de commande (CLI)

#### Authentification
```bash
# Ouvre le navigateur pour l'authentification
notebooklm login
```

#### Création et gestion de notebook
```bash
# Créer un notebook
notebooklm create "Ma Recherche"

# Utiliser un notebook existant
notebooklm use <notebook_id>

# Ajouter des sources
notebooklm source add "https://en.wikipedia.org/wiki/Artificial_intelligence"
notebooklm source add "./paper.pdf"
```

#### Interaction avec les sources
```bash
# Poser des questions
notebooklm ask "Quels sont les thèmes clés ?"
```

#### Génération de contenu
```bash
# Audio (podcast)
notebooklm generate audio "rendre cela engageant" --wait

# Vidéo
notebooklm generate video --style whiteboard --wait

# Quiz
notebooklm generate quiz --difficulty hard

# Flashcards
notebooklm generate flashcards --quantity more

# Slide deck
notebooklm generate slide-deck

# Infographie
notebooklm generate infographic --orientation portrait

# Carte mentale
notebooklm generate mind-map

# Tableau de données
notebooklm generate data-table "comparer les concepts clés"
```

#### Téléchargement des artefacts
```bash
# Audio
notebooklm download audio ./podcast.mp3

# Vidéo
notebooklm download video ./overview.mp4

# Quiz (formats multiples)
notebooklm download quiz --format markdown ./quiz.md

# Flashcards
notebooklm download flashcards --format json ./cards.json

# Slide deck
notebooklm download slide-deck ./slides.pdf

# Carte mentale
notebooklm download mind-map ./mindmap.json

# Tableau de données
notebooklm download data-table ./data.csv
```

### 2. API Python

```python
import asyncio
from notebooklm import NotebookLMClient

async def main():
    async with await NotebookLMClient.from_storage() as client:
        # Créer un notebook et ajouter des sources
        nb = await client.notebooks.create("Recherche")
        await client.sources.add_url(nb.id, "https://example.com", wait=True)
        
        # Discuter avec les sources
        result = await client.chat.ask(nb.id, "Résumer ceci")
        print(result.answer)
        
        # Générer du contenu (podcast, vidéo, quiz, etc.)
        status = await client.artifacts.generate_audio(
            nb.id, 
            instructions="rendre cela amusant"
        )
        await client.artifacts.wait_for_completion(nb.id, status.task_id)
        await client.artifacts.download_audio(nb.id, "podcast.mp3")
        
        # Générer un quiz et télécharger en JSON
        status = await client.artifacts.generate_quiz(nb.id)
        await client.artifacts.wait_for_completion(nb.id, status.task_id)
        await client.artifacts.download_quiz(
            nb.id, 
            "quiz.json", 
            output_format="json"
        )
        
        # Générer une carte mentale et l'exporter
        result = await client.artifacts.generate_mind_map(nb.id)
        await client.artifacts.download_mind_map(nb.id, "mindmap.json")

asyncio.run(main())
```

### 3. Skills pour agents (Claude Code)

```bash
# Installer via CLI ou demander à Claude Code de le faire
notebooklm skill install
```

Exemples d'utilisation en langage naturel :
- "Créer un podcast sur l'informatique quantique"
- "Télécharger le quiz en markdown"
- "/notebooklm generate video"

## Architecture et API

### Structure de l'API

Le client `NotebookLMClient` expose plusieurs namespaces :

1. **notebooks** - Gestion des notebooks
   - `create()` - Créer un nouveau notebook
   - `list()` - Lister les notebooks
   - `get()` - Obtenir un notebook spécifique
   - `delete()` - Supprimer un notebook

2. **sources** - Gestion des sources
   - `add_url()` - Ajouter une URL comme source
   - `add_file()` - Ajouter un fichier (PDF, etc.)
   - `list()` - Lister les sources
   - `delete()` - Supprimer une source

3. **chat** - Interaction conversationnelle
   - `ask()` - Poser une question
   - `get_history()` - Obtenir l'historique

4. **artifacts** - Génération et téléchargement de contenu
   - `generate_audio()` - Générer un audio overview
   - `generate_video()` - Générer une vidéo
   - `generate_quiz()` - Générer un quiz
   - `generate_flashcards()` - Générer des flashcards
   - `generate_slide_deck()` - Générer un slide deck
   - `generate_infographic()` - Générer une infographie
   - `generate_mind_map()` - Générer une carte mentale
   - `generate_data_table()` - Générer un tableau de données
   - `download_*()` - Méthodes de téléchargement pour chaque type
   - `wait_for_completion()` - Attendre la fin d'une tâche

## Documentation complète

### Documentation utilisateur
- [CLI Reference](https://github.com/teng-lin/notebooklm-py/blob/main/docs/cli-reference.md) - Documentation complète des commandes
- [Python API](https://github.com/teng-lin/notebooklm-py/blob/main/docs/python-api.md) - Référence complète de l'API
- [Configuration](https://github.com/teng-lin/notebooklm-py/blob/main/docs/configuration.md) - Stockage et paramètres
- [Troubleshooting](https://github.com/teng-lin/notebooklm-py/blob/main/docs/troubleshooting.md) - Problèmes courants et solutions
- [API Stability](https://github.com/teng-lin/notebooklm-py/blob/main/docs/stability.md) - Politique de versionnage et garanties de stabilité

### Documentation pour contributeurs
- [Development Guide](https://github.com/teng-lin/notebooklm-py/blob/main/docs/development.md) - Architecture, tests et releases
- [RPC Development](https://github.com/teng-lin/notebooklm-py/blob/main/docs/rpc-development.md) - Capture de protocole et débogage
- [RPC Reference](https://github.com/teng-lin/notebooklm-py/blob/main/docs/rpc-reference.md) - Structures de payloads
- [Changelog](https://github.com/teng-lin/notebooklm-py/blob/main/CHANGELOG.md) - Historique des versions et notes de release
- [Security](https://github.com/teng-lin/notebooklm-py/blob/main/SECURITY.md) - Politique de sécurité et gestion des credentials

## Cas d'usage

### 1. Pipeline de recherche automatisé
```python
async def research_pipeline(topic: str):
    async with await NotebookLMClient.from_storage() as client:
        # Créer un notebook dédié
        nb = await client.notebooks.create(f"Recherche: {topic}")
        
        # Importer plusieurs sources
        urls = [
            f"https://en.wikipedia.org/wiki/{topic}",
            f"https://scholar.google.com/scholar?q={topic}"
        ]
        for url in urls:
            await client.sources.add_url(nb.id, url, wait=True)
        
        # Extraire les insights
        summary = await client.chat.ask(nb.id, "Résumer les points clés")
        
        # Générer un rapport
        await client.artifacts.generate_slide_deck(nb.id)
        await client.artifacts.download_slide_deck(nb.id, f"{topic}_report.pdf")
```

### 2. Génération de contenu éducatif
```python
async def create_study_materials(subject: str, source_file: str):
    async with await NotebookLMClient.from_storage() as client:
        nb = await client.notebooks.create(f"Étude: {subject}")
        await client.sources.add_file(nb.id, source_file, wait=True)
        
        # Générer différents formats d'étude
        tasks = [
            client.artifacts.generate_quiz(nb.id, difficulty="medium"),
            client.artifacts.generate_flashcards(nb.id, quantity="more"),
            client.artifacts.generate_mind_map(nb.id),
            client.artifacts.generate_audio(nb.id, instructions="style pédagogique")
        ]
        
        # Télécharger tous les artefacts
        await client.artifacts.download_quiz(nb.id, "quiz.json", format="json")
        await client.artifacts.download_flashcards(nb.id, "flashcards.md", format="markdown")
        await client.artifacts.download_mind_map(nb.id, "mindmap.json")
        await client.artifacts.download_audio(nb.id, "lesson.mp3")
```

### 3. Intégration dans un agent IA
```python
# Exemple d'intégration dans un agent personnalisé
class ResearchAgent:
    def __init__(self):
        self.client = None
    
    async def setup(self):
        self.client = await NotebookLMClient.from_storage()
    
    async def analyze_topic(self, topic: str, sources: list):
        nb = await self.client.notebooks.create(topic)
        
        for source in sources:
            if source.startswith("http"):
                await self.client.sources.add_url(nb.id, source, wait=True)
            else:
                await self.client.sources.add_file(nb.id, source, wait=True)
        
        insights = await self.client.chat.ask(
            nb.id, 
            "Quelles sont les conclusions principales ?"
        )
        
        return insights.answer
```

## Formats de sortie supportés

### Quiz
- JSON (structuré avec questions/réponses/explications)
- Markdown (format texte lisible)
- HTML (affichage interactif)

### Flashcards
- JSON (paires question-réponse structurées)
- Markdown (format carte)
- HTML (cartes interactives)

### Autres formats
- **Audio** : MP3
- **Vidéo** : MP4
- **Slides** : PDF, PPTX (PowerPoint éditable)
- **Cartes mentales** : JSON (hiérarchique pour outils de visualisation)
- **Tableaux de données** : CSV, JSON
- **Infographies** : PNG, PDF

## Bonnes pratiques

### Sécurité
1. **Ne jamais hardcoder les credentials** - Utiliser le système de stockage sécurisé de la bibliothèque
2. **Gérer les API keys de manière sécurisée** - Suivre les recommandations du guide de sécurité
3. **Valider les entrées** - Toujours valider les données utilisateur avant traitement

### Performance
1. **Utiliser wait=True** lors de l'ajout de sources pour s'assurer qu'elles sont indexées
2. **Attendre la complétion** des tâches longues avec `wait_for_completion()`
3. **Gérer les limites de taux** - Implémenter des mécanismes de retry avec backoff
4. **Batch operations** - Regrouper les opérations quand c'est possible

### Gestion des erreurs
```python
async def robust_generation():
    async with await NotebookLMClient.from_storage() as client:
        try:
            nb = await client.notebooks.create("Test")
            status = await client.artifacts.generate_audio(nb.id)
            await client.artifacts.wait_for_completion(nb.id, status.task_id)
        except Exception as e:
            print(f"Erreur lors de la génération: {e}")
            # Consulter la documentation de troubleshooting
```

## Limitations et considérations

### Limitations techniques
- Utilise des APIs Google non documentées
- Susceptible aux changements sans préavis
- Limites de taux imposées par Google
- Nécessite une authentification Google valide

### Recommandations d'utilisation
- ✅ **Idéal pour** : Prototypes, recherche, projets personnels, automatisation
- ⚠️ **À éviter pour** : Applications critiques en production sans plan de contingence
- 📝 **Conseil** : Consulter régulièrement le changelog pour les changements d'API

## Ressources supplémentaires

### Communauté et support
- **Issues GitHub** : https://github.com/teng-lin/notebooklm-py/issues
- **Discussions** : https://github.com/teng-lin/notebooklm-py/discussions
- **Releases** : https://github.com/teng-lin/notebooklm-py/releases

### Contribution
Le projet accepte les contributions selon le guide de développement disponible dans la documentation.

### Topics associés
- API Python
- Google NotebookLM
- Automatisation
- IA/ML
- Génération de contenu
- Outils de recherche

---

**Dernière mise à jour** : Mars 2026
**Version de la base de connaissances** : 1.0
**Source** : https://github.com/teng-lin/notebooklm-py
