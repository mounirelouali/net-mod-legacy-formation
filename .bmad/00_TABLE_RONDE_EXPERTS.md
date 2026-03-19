# 🎭 Table Ronde BMAD - Analyse Formation .NET Legacy

**Date** : 19 mars 2026  
**Contexte** : Création d'une formation 5 jours pour moderniser une application .NET Framework legacy vers .NET 8  
**Problème Détecté** : L'IA a sauté les phases Brief/Manage/Architect et a directement généré du contenu sans PRD ni planning

---

## 👥 Participants de la Table Ronde

### 🎯 Expert #1 - Product Manager (Méthode BMAD)
**Rôle** : Garantir que le projet a un PRD solide, un MVP défini, des Epics/Stories claires

### 🏗️ Expert #2 - Architecte Pédagogique
**Rôle** : Définir la structure documentaire, les conventions de formation, le "scaffolding" pour stagiaires

### 💻 Expert #3 - Développeur .NET Senior
**Rôle** : Valider la cohérence technique du programme 5 jours, la progression C# legacy → moderne

### 🎨 Expert #4 - Expert UX/Formateur
**Rôle** : S'assurer que le formateur a une ligne conductrice claire et que les stagiaires peuvent suivre

### 📊 Expert #5 - Expert BMAD Framework
**Rôle** : Vérifier que la méthode BMAD est correctement appliquée (PRD → Epics → Planning → Sprints)

---

## 🔍 PHASE 1 - BRIEF (Discovery & Diagnostic)

### 🎯 Product Manager - Analyse du Besoin Client

**Question fondamentale** : Qu'est-ce qu'on construit exactement ?

**Réponse** :
- **Produit** : Une formation de 5 jours (35h) pour développeurs .NET expérimentés
- **Objectif Business** : Transformer un batch legacy .NET Framework en solution .NET 8 moderne, testable, sécurisée, déployable sur Linux/Docker
- **Douleurs Client** :
  1. Peur de modifier le code legacy (zéro tests)
  2. Verrouillage sur Windows Server (coûts licences)
  3. Dette de sécurité (secrets hardcodés, RGPD)
  4. Impossibilité de tester (couplage SQL/SMTP)
  5. Vélocité divisée par 10 (3 jours pour changer une règle métier)

**MVP (Minimum Viable Product)** :
- Jour 1 : Session 09h00 fonctionnelle avec analyse legacy + exercice détecteur anti-patterns
- Livrable Quotidien Unique (Git) + Solutions Externalisées (Drive)
- Design Informationnel à Double Lecture (icônes 🧠💡💬⚙️🔗)
- Zéro meta-texte (aucune mention IA)

**Hors Scope V1** :
- Générer les 5 jours complets immédiatement (on fait Jour 1 d'abord, puis validation USER avant Jour 2)
- Créer des vidéos ou supports audio
- Intégrer des plateformes LMS (Moodle, etc.)

**KPI de Succès** :
- [ ] Le formateur peut donner la session 09h00 sans préparation supplémentaire
- [ ] Les stagiaires reproduisent l'exercice sans bloquer
- [ ] Zéro mention IA détectée dans les documents stagiaires
- [ ] Le formateur a une ligne conductrice claire (script téléprompter)

---

### 🏗️ Architecte Pédagogique - Structure Documentaire

**Problème identifié** : L'IA a créé `Jour_1_Fondations_NEW.md` sans définir l'architecture globale des fichiers.

**Architecture Cible** :

```
net-mod-legacy/ (Git - Repository stagiaires)
├─ 02_Atelier_Stagiaires/
│  └─ ValidFlow.Legacy/
│     └─ Program.cs (Code legacy avec anti-patterns)
│
├─ 03_Support_Quotidien/ (NOUVEAU - Livrable Quotidien Unique)
│  ├─ Jour_1_Fondations.md (fusion Master + Workbook avec Design Double Lecture)
│  ├─ Jour_2_Data_DI.md
│  ├─ Jour_3_Securite.md
│  ├─ Jour_4_Tests_Docker.md
│  └─ Jour_5_CICD_Bilan.md
│
├─ .bmad/ (NOUVEAU - Persistance BMAD)
│  ├─ 00_TABLE_RONDE_EXPERTS.md
│  ├─ 01_PRD_FORMATION.md
│  ├─ 02_EPICS_STORIES.md
│  ├─ 03_ARCHITECTURE_DOCUMENTAIRE.md
│  ├─ 04_PROJECT_CONTEXT.md (Conventions, règles, stack)
│  └─ 05_TASK_TRACKING.md (Fichier Markdown de suivi)
│
└─ README.md (Instructions clone pour stagiaires)

G:\Drive partagés\...\Solutions_A_Partager/ (Drive - Solutions Formateur UNIQUEMENT)
├─ J1_S1_Solution_09h00_Analyse.md
├─ J1_S2_Solution_10h40_Architecture.md
├─ J1_S3_Solution_13h30_Domain.md
├─ J1_S4_Solution_15h10_CSharp12.md
└─ (idem pour Jours 2-5)
```

**Conventions de Nommage** :
- Support Quotidien : `Jour_X_Theme.md`
- Solutions : `JX_SY_Solution_HHhMM_Theme.md` (X=jour, Y=session)
- Documents BMAD : Numérotés `00_`, `01_`, etc.

**Design Informationnel à Double Lecture** :
- 🧠 **Concepts Fondamentaux** : Théorie pure (formateur lit, explique)
- 💡 **L'Astuce Pratique** : Anecdote ou best-practice à partager
- 💬 **Analyse Collective** : Question ouverte + silence 5-8s
- ⚙️ **Défi d'Application** : Exercice avec durée + critères de succès
- 🔗 **Lien vers la Solution** : Phrase standard + lien Drive

---

### 💻 Développeur .NET Senior - Cohérence Technique

**Analyse du Programme 5 Jours** :

✅ **Jour 1 : Fondations** - Cohérent
- S1 (09h00) : Analyse legacy (5 anti-patterns) → Bon exercice de diagnostic
- S2 (10h40) : Scaffolding Clean Architecture → Logique (après diagnostic)
- S3 (13h30) : Implémentation Domain → Parfait (extraction du cœur métier)
- S4 (15h10) : Modernisation C# 12 → Cohérent (syntax upgrade)

✅ **Jour 2 : Data & DI** - Cohérent
- Progression logique : DI → EF Core → Repository → Migrations

✅ **Jour 3 : Sécurité** - Cohérent
- Adresse les anti-patterns Jour 1 : Secrets, Config, SMTP, Logging

✅ **Jour 4 : Tests & Docker** - Cohérent
- Tests unitaires → Tests intégration → Cross-platform → Docker

✅ **Jour 5 : CI/CD & Bilan** - Cohérent
- GitHub Actions → Refactoring → Documentation → ROI

**Problème Détecté** :
- ❌ Aucune instruction de **clone du repository** pour les stagiaires
- ❌ Le code legacy `ValidFlow.Legacy/Program.cs` existe, mais pas de README expliquant comment démarrer
- ❌ Pas de "point de départ" clair pour les stagiaires (ils arrivent avec quoi ?)

---

### 🎨 Expert UX/Formateur - Expérience Formateur & Stagiaires

**Question Critique** : Est-ce que le formateur peut donner la session 09h00 SANS préparation ?

**Analyse du fichier `Jour_1_Fondations_NEW.md`** :

✅ Points Forts :
- Icônes claires (🧠💡💬⚙️🔗)
- Exercice structuré (15 min, format de réponse défini)
- Diagramme Mermaid (workflow AS-IS)

❌ Points Manquants pour le Formateur :
- Pas de **script téléprompter** (que dire exactement ?)
- Pas de **timing détaillé** (combien de temps par section ?)
- Pas de **démonstration live** (le formateur montre quoi sur le 2e écran ?)
- Pas de **pistes de réflexion** pour guider sans donner la réponse

**Analyse Expérience Stagiaire** :

✅ Points Forts :
- Exercice clair (chercher 5 anti-patterns dans `Program.cs`)
- Format de réponse guidé

❌ Points Manquants :
- **Comment les stagiaires démarrent ?** Pas d'instructions de clone
- **Ont-ils VS Code ouvert ?** Pas de pré-requis mentionnés
- **Scaffolding insuffisant** : Pas de "💡 Pistes de réflexion" pour débloquer sans donner la solution

**Recommandation Scaffolding (Option C)** :

Au lieu de :
```
⚙️ Défi : Trouvez les 5 anti-patterns
```

Faire :
```
⚙️ Défi : Trouvez les 5 anti-patterns (15 min)

💡 Pistes de réflexion :
- Sécurité : Cherchez les mots de passe dans le code (lignes 15-20)
- Performance : Les appels SQL sont-ils asynchrones ?
- Robustesse : Que se passe-t-il si SQL Server plante ?
- Maintenabilité : Pouvez-vous tester ValidateData() sans lancer SQL ?
- Déploiement : Le chemin "output.xml" fonctionne-t-il sur Linux ?
```

---

### 📊 Expert BMAD Framework - Validation Méthode

**Diagnostic Sévère** : ❌ **La méthode BMAD n'a PAS été appliquée**

**Ce qui a été fait** :
- ✅ Création de contenu (fichier `Jour_1_Fondations_NEW.md`)
- ✅ Création de solution (`J1_S1_Solution_09h00_Analyse_NEW.md`)

**Ce qui MANQUE (méthode BMAD)** :

1. ❌ **BRIEF - Phase de Discovery**
   - Pas de brainstorming structuré avec l'utilisateur
   - Pas de validation du périmètre exact (combien de sessions à générer ?)
   - Pas de définition des User Journeys (Qui est le formateur ? Qui sont les stagiaires ?)

2. ❌ **MANAGE - PRD & Epics/Stories**
   - Pas de PRD (Product Requirements Document) de la formation
   - Pas de découpage en Epics (ex: Epic 1 = Jour 1, Epic 2 = Jour 2)
   - Pas de Stories (ex: Story J1-S1 = Session 09h00 Analyse, Story J1-S2 = Session 10h40 Architecture)
   - Pas de critères de succès par Story

3. ❌ **ARCHITECT - Architecture Documentaire**
   - Pas de "Project Context" (fichier de conventions)
   - Pas de définition de la stack documentaire (Markdown + Mermaid + Drive + Git)
   - Pas de règles de nommage validées
   - Pas de template de session défini

4. ❌ **DEVELOP - Planning & Sprints**
   - Pas de fichier Markdown de suivi des tâches (`.bmad/05_TASK_TRACKING.md`)
   - Pas de sprints définis (Sprint 1 = Jour 1, Sprint 2 = Jour 2, etc.)
   - Pas de validation itérative (générer Session 09h00 → tester → valider → Session 10h40)

**Conséquence** :
- L'IA génère du contenu "au hasard" sans vision globale
- Risque de désalignement entre les 4 sessions du Jour 1
- Impossible de tracer l'avancement (pas de checklist)
- Impossible de reprendre le travail demain (pas de persistance documentaire)

---

## 🎯 Décisions de la Table Ronde

### Décision #1 - Arrêt Immédiat & Redémarrage BMAD

**Vote unanime (5/5 experts)** : ❌ Rejeter les fichiers `_NEW` créés

**Action** :
- Supprimer `Jour_1_Fondations_NEW.md` et `J1_S1_Solution_09h00_Analyse_NEW.md`
- Redémarrer avec la méthode BMAD complète

**Raison** :
- Les fichiers créés ne respectent pas le scaffolding (pas de pistes de réflexion)
- Pas de script téléprompter pour le formateur
- Pas d'instructions de démarrage pour les stagiaires
- Impossible de tracer l'avancement sans PRD/Epics/Task Tracking

---

### Décision #2 - Créer le PRD de la Formation

**Phase BMAD** : MANAGE

**Responsable** : Product Manager

**Livrables** :
- `.bmad/01_PRD_FORMATION.md` contenant :
  - Vision & Objectif de la formation
  - Douleurs Client (5 douleurs identifiées)
  - User Journeys (Formateur, Stagiaire Débutant .NET, Stagiaire Senior .NET)
  - Définition du MVP (Jour 1 complet)
  - Scope V1, V2, V3 (V1 = Jour 1, V2 = Jours 2-3, V3 = Jours 4-5)
  - KPI de Succès par Jour
  - Besoins Fonctionnels (FR001 à FR020)
  - Besoins Non-Fonctionnels (NFR001 à NFR010)

**Critères de Validation** :
- [ ] L'utilisateur a validé le périmètre exact
- [ ] Les 5 douleurs client sont documentées
- [ ] Les User Journeys couvrent formateur + stagiaires
- [ ] Le MVP est clair (Jour 1 uniquement en V1)

---

### Décision #3 - Découper en Epics & Stories

**Phase BMAD** : MANAGE

**Responsable** : Product Manager + Architecte Pédagogique

**Structure proposée** :

```
Epic 1 - Jour 1 : Fondations
├─ Story J1-S1 (FR001) : Session 09h00 - Analyse Legacy
├─ Story J1-S2 (FR002) : Session 10h40 - Scaffolding Architecture
├─ Story J1-S3 (FR003) : Session 13h30 - Implémentation Domain
└─ Story J1-S4 (FR004) : Session 15h10 - Modernisation C# 12

Epic 2 - Jour 2 : Data & DI
├─ Story J2-S1 (FR005) : Session 09h00 - Injection de Dépendances
├─ Story J2-S2 (FR006) : Session 10h40 - Entity Framework Core 8
├─ Story J2-S3 (FR007) : Session 13h30 - Repository Pattern
└─ Story J2-S4 (FR008) : Session 15h10 - Migrations & Testabilité

(idem pour Jours 3, 4, 5)
```

**Critères de Validation** :
- [ ] Chaque Story a un identifiant unique (FR001, FR002, etc.)
- [ ] Chaque Story a des critères de succès mesurables
- [ ] Les dépendances entre Stories sont documentées (J1-S2 dépend de J1-S1)

---

### Décision #4 - Définir l'Architecture Documentaire

**Phase BMAD** : ARCHITECT

**Responsable** : Architecte Pédagogique + Développeur .NET

**Livrables** :
- `.bmad/03_ARCHITECTURE_DOCUMENTAIRE.md` contenant :
  - Structure des dossiers (Git vs Drive)
  - Conventions de nommage (fichiers, sections, icônes)
  - Template de session (structure Markdown standard)
  - Règles de scaffolding (💡 Pistes de réflexion)
  - Format des solutions (Drive)

- `.bmad/04_PROJECT_CONTEXT.md` contenant :
  - Stack documentaire (Markdown + Mermaid + Git + Drive)
  - Règles de rédaction (zéro meta-texte, français uniquement)
  - Design Informationnel (icônes 🧠💡💬⚙️🔗)
  - Règles de sécurité (aucune mention IA)

**Critères de Validation** :
- [ ] Le template de session est défini avec toutes les sections obligatoires
- [ ] Les règles de scaffolding sont claires (quand donner des pistes vs la solution)
- [ ] La séparation Git/Drive est documentée

---

### Décision #5 - Créer le Planning de Tâches

**Phase BMAD** : ARCHITECT → DEVELOP

**Responsable** : Product Manager + Expert BMAD

**Livrable** :
- `.bmad/05_TASK_TRACKING.md` (fichier Markdown de suivi) contenant :

```markdown
# Planning de Tâches - Formation .NET Legacy

## Sprint 1 - Jour 1 (Epic 1)

### Story J1-S1 : Session 09h00 - Analyse Legacy (FR001)
- [ ] Rédiger section 🧠 Concepts Fondamentaux (Dette Technique)
- [ ] Créer diagramme Mermaid (Workflow Legacy AS-IS)
- [ ] Rédiger section 💡 L'Astuce Pratique (Principe SOLID)
- [ ] Rédiger section 💬 Analyse Collective (Question : Peur de modifier)
- [ ] Rédiger section ⚙️ Défi d'Application (Détective Anti-Patterns)
- [ ] Ajouter 💡 Pistes de réflexion (scaffolding)
- [ ] Rédiger section 🔗 Lien vers la Solution
- [ ] Créer solution Drive (J1_S1_Solution_09h00_Analyse.md)
- [ ] Validation USER

### Story J1-S2 : Session 10h40 - Scaffolding Architecture (FR002)
- [ ] (à définir après validation J1-S1)

(etc.)
```

**Critères de Validation** :
- [ ] Chaque Story a des tâches granulaires (< 30 min)
- [ ] Les tâches sont cochables (checkboxes Markdown)
- [ ] L'ordre des tâches respecte les dépendances
- [ ] Validation USER prévue après chaque Story

---

### Décision #6 - Ajouter Instructions de Démarrage (README)

**Problème Critique** : Les stagiaires n'ont pas d'instructions pour cloner le repo

**Action** :
- Créer `README.md` à la racine du repository Git avec :

```markdown
# Formation .NET Legacy → .NET 8 Moderne

## 🚀 Démarrage Rapide (Avant la Session 09h00)

### Prérequis
- Windows 11 / macOS / Linux
- .NET 8 SDK installé
- VS Code + Extension C# Dev Kit
- Git installé

### Étape 1 : Clone du Repository

```bash
cd C:\dev
git clone https://github.com/mounirelouali/net-mod-legacy-formation.git
cd net-mod-legacy-formation
```

### Étape 2 : Ouvrir dans VS Code

```bash
code .
```

### Étape 3 : Vérifier le Code Legacy

Ouvrez le fichier :
- `02_Atelier_Stagiaires/ValidFlow.Legacy/Program.cs`

Vous êtes prêt pour la session 09h00 ! 🎯
```

**Critères de Validation** :
- [ ] Les instructions fonctionnent sur Windows (PowerShell)
- [ ] Les stagiaires peuvent cloner et ouvrir VS Code en < 2 minutes
- [ ] Le README mentionne les prérequis obligatoires

---

## 📋 Plan d'Exécution BMAD

### Phase 1 - BRIEF (Validation Utilisateur)
1. ✅ Consulter NotebookLM sur BMAD
2. ✅ Organiser Table Ronde 5 experts
3. ⏳ **EN ATTENTE** : Validation USER du diagnostic et des décisions

### Phase 2 - MANAGE (Création PRD & Epics)
4. ⏳ Créer `.bmad/01_PRD_FORMATION.md`
5. ⏳ Créer `.bmad/02_EPICS_STORIES.md`
6. ⏳ Validation USER du PRD et découpage

### Phase 3 - ARCHITECT (Architecture Documentaire)
7. ⏳ Créer `.bmad/03_ARCHITECTURE_DOCUMENTAIRE.md`
8. ⏳ Créer `.bmad/04_PROJECT_CONTEXT.md`
9. ⏳ Créer template de session vide
10. ⏳ Validation USER de l'architecture

### Phase 4 - DEVELOP (Sprints)
11. ⏳ Créer `.bmad/05_TASK_TRACKING.md`
12. ⏳ Sprint 1 : Générer Story J1-S1 (Session 09h00)
13. ⏳ Validation USER Story J1-S1 → GO/NOGO pour J1-S2
14. ⏳ Sprint 2 : Générer Story J1-S2 (Session 10h40)
15. ⏳ (etc.)

---

## 🚨 Message à l'Utilisateur

**Diagnostic de la Table Ronde** :

Les 5 experts BMAD ont identifié que j'ai **complètement sauté les phases Brief, Manage et Architect** de la méthode BMAD. J'ai généré du contenu sans :
- PRD de la formation
- Découpage en Epics/Stories
- Architecture documentaire définie
- Fichier de suivi des tâches

**Recommandation unanime (5/5 experts)** :
1. ❌ Supprimer les fichiers `_NEW` créés (incomplets)
2. ✅ Redémarrer avec BMAD complet
3. ✅ Créer PRD → Epics → Architecture → Planning → Sprints
4. ✅ Générer Session 09h00 avec scaffolding (pistes de réflexion)
5. ✅ Validation USER après chaque Story avant de continuer

**Prochaine Action** :
- Attendre votre validation de ce diagnostic
- Si OK : Je crée le PRD de la formation (`.bmad/01_PRD_FORMATION.md`)
- Si NOK : Vous me dites ce qui doit être ajusté

**Voulez-vous que je procède avec la création du PRD selon la méthode BMAD ?**
