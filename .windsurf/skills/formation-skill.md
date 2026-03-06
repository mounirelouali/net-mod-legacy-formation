# SKILL : Création de Formations Techniques et Ateliers Pédagogiques

**Type** : Méthodologie réutilisable  
**Domaine** : Formation technique, création de supports pédagogiques, gestion d'ateliers  
**Version** : 1.0  
**Dernière mise à jour** : 6 mars 2026

---

## 🎯 Objectif du Skill

Ce skill documente la méthodologie validée pour **créer des formations techniques de haute qualité** avec une approche granulaire, itérative et contrôlée. Applicable à n'importe quelle formation technique (développement, architecture, DevOps, etc.).

---

## 📋 Principes Fondamentaux Validés

### 1. Approche "Diviser pour Régner"

**Principe** : Découper la formation en **tranches horaires précises** (1h30-2h00 max) avec :
- ✅ Un **objectif pédagogique unique** et mesurable
- ✅ Un **livrable concret** attendu
- ✅ Une **branche Git dédiée** pour traçabilité
- ✅ Une **validation formateur** avant de continuer

**Avantages** :
- Maîtrise totale du temps (on sait exactement où on en est)
- Récupération facile en cas d'erreur (rollback Git)
- Points de synchronisation pour les apprenants (chaque pause)
- Traçabilité pédagogique parfaite

**Format de nommage Git** :
```
jourX-HHhMM-nom-livrable
```

**Exemples** :
- `jour1-09h00-start` : Point de départ
- `jour1-10h30-analyse-legacy` : Fin de l'analyse
- `jour1-12h30-structure-5-projets` : Structure créée
- `jour1-17h00-end` : Fin de journée

---

### 2. Structure Pédagogique en 3 Phases

**Pour chaque tranche horaire, structurer le support selon** :

#### Phase 1 : THÉORIE (30-35% du temps)

**Contenu** :
- Concepts fondamentaux avec définitions précises
- Contexte et justifications (pourquoi c'est important)
- Comparaison AVANT/APRÈS (Legacy vs Moderne)
- Gains mesurables (chiffres, benchmarks)
- Sources et références (livres, docs officielles)

**Format** :
```markdown
## 📚 1. THÉORIE (30 minutes)

### 1.1 Concept Principal
Définition claire...

### 1.2 Problèmes Identifiés
- Problème #1 : Titre
  - Symptôme : ...
  - Conséquences : ...
  - Solution : ...
```

**Règles strictes** :
- ❌ JAMAIS mentionner les outils de préparation interne (rester neutre)
- ✅ Fournir des sources officielles (Microsoft Docs, livres reconnus)
- ✅ Utiliser des chiffres et métriques concrets

---

#### Phase 2 : DÉMONSTRATION (40-45% du temps)

**Contenu** :
- Démonstration pas-à-pas avec projet fil rouge principal
- Code annoté ligne par ligne avec explications
- Identification visuelle des problèmes (⚠️🐌💥🔧📦)
- Comparaison code Legacy vs code Moderne

**Format** :
```markdown
## 🖥️ 2. DÉMONSTRATION (45 minutes)

### Étape 1 : Titre de l'étape
Instructions précises...

**Ligne X-Y** : Annotation du problème
```csharp
// ============================================================
// ⚠️ PROBLÈME #1 : SÉCURITÉ - Description
// ============================================================
// LIGNE X : Explication précise
// CONSÉQUENCE : Impact concret
// SOLUTION : Approche moderne
// ============================================================
code_example
```

**Règles strictes** :
- ✅ Code richement commenté avec blocs visuels
- ✅ Numéros de lignes précis pour référence
- ✅ Émojis pour identification visuelle (⚠️🐌💥🔧📦)
- ✅ Comparaison systématique avec solution moderne

---

#### Phase 3 : ATELIER PRATIQUE (20-25% du temps)

**Contenu** :
- Énoncé clair avec contexte et mission
- Projet similaire mais domaine différent (éviter la copie)
- Livrable attendu précis et mesurable
- Solution complète séparée pour correction

**Format** :
```markdown
## 🎯 3. ATELIER PRATIQUE (15 minutes)

### Énoncé : Titre de la mission

**Contexte** : Projet X fait Y...

**Votre mission** :
1. Action 1 (précise)
2. Action 2 (mesurable)

**Livrable attendu** :
- Document/Code/Schéma X
- Critères de validation Y

**Temps** : 15 minutes
```

**Règles strictes** :
- ✅ Projet différent du projet démo (éviter copier-coller)
- ✅ Énoncé autonome (apprenant peut travailler seul)
- ✅ Livrable mesurable (on peut valider objectivement)
- ✅ Solution complète dans dossier séparé `ProjectName.Solutions/`

---

### 3. Organisation des Fichiers

**Structure validée** :

```
project-root/
│
├── _bmad-output/
│   ├── planning-artifacts/           # Documents stratégiques
│   │   ├── 01_Architecture_et_Pedagogie.md
│   │   ├── 02_Plan_Git_Sprints.md
│   │   └── 03_Deroulement_Granulaire_5_Jours.md
│   │
│   └── implementation-artifacts/      # Supports de cours
│       ├── Jour1_09h00-10h30_Titre.md
│       ├── Jour1_10h40-12h30_Titre.md
│       └── ...
│
├── ProjetDemo.Legacy/                 # Code Legacy pour démonstration
│   └── Program.cs (RICHEMENT commenté)
│
├── ProjetAtelier.Legacy/              # Code Legacy pour atelier apprenants
│   └── Program.cs (SANS commentaires)
│
├── ProjetAtelier.Solutions/           # Solutions des ateliers
│   ├── Analyse_ProjetAtelier.md
│   └── Code/ (si applicable)
│
└── .windsurf/
    └── skills/
        └── formation-skill.md         # Ce fichier
```

---

### 4. Règles de Confidentialité et Professionnalisme

**Règles strictes pour les supports destinés aux apprenants** :

❌ **INTERDIT** :
- Mentionner les outils de préparation interne du formateur
- Inclure des références à des bases de connaissances privées
- Exposer des credentials ou informations sensibles
- Faire référence à des comptes personnels

✅ **OBLIGATOIRE** :
- Sources publiques uniquement (Microsoft Docs, livres publiés, articles reconnus)
- Professionnalisme total dans la rédaction
- Neutralité sur les outils de préparation
- Confidentialité des informations du formateur

**Séparation claire** :
- **Matériel interne formateur** : Notes personnelles, bases de connaissances privées, outils de préparation
- **Matériel apprenant** : Supports de cours, code, solutions (100% professionnel et neutre)

---

### 5. Méthodologie de Création Itérative

**Processus validé** :

#### Étape 1 : Planification Granulaire

1. Définir le découpage par tranches horaires
2. Identifier l'objectif pédagogique de chaque tranche
3. Définir le livrable mesurable attendu
4. Planifier les branches Git

**Livrable** : Document `XX_Deroulement_Granulaire.md`

---

#### Étape 2 : Génération Tranche par Tranche

**Pour chaque tranche** :

1. **Créer le support Markdown** (Théorie + Démo + Atelier)
2. **Créer le code démo** (richement commenté)
3. **Créer le code atelier** (sans commentaires)
4. **Créer la solution atelier** (documentation complète)
5. **Créer la branche Git** correspondante
6. **ATTENDRE VALIDATION** avant de continuer

**Important** : Ne jamais générer plusieurs tranches d'un coup sans validation intermédiaire.

---

#### Étape 3 : Validation Formateur

**Checklist avant de passer à la tranche suivante** :

- [ ] Support Markdown complet (Théorie + Démo + Atelier)
- [ ] Code démo avec annotations exhaustives
- [ ] Code atelier autonome et compréhensible
- [ ] Solution atelier documentée
- [ ] Branche Git créée et taggée
- [ ] Compilation réussie (si applicable)
- [ ] Tests au vert (si applicable)
- [ ] Livrable visible et mesurable

---

### 6. Standards de Qualité du Code Pédagogique

#### Code Démo (Projet Formateur)

**Règles** :
- ✅ **Commentaires exhaustifs** : Chaque problème annoté avec bloc visuel
- ✅ **Numéros de lignes** : Référencés dans le support Markdown
- ✅ **Émojis** : Identification visuelle (⚠️ Sécurité, 🐌 Performance, 💥 Robustesse, 🔧 Maintenabilité, 📦 Déploiement)
- ✅ **Comparaison** : Code Legacy vs Solution Moderne
- ✅ **Récapitulatif** : Bloc final résumant tous les problèmes

**Exemple** :
```csharp
// ============================================================
// ⚠️ PROBLÈME #1 : SÉCURITÉ - ConnectionString hardcodée
// ============================================================
// LIGNE 31-32 : ConnectionString SQL en clair avec password
// CONSÉQUENCE : Si ce code est sur Git, le mot de passe est exposé
// SOLUTION .NET 8 : Secret Manager + appsettings.json
// ============================================================
string connectionString = "Server=...;Password=SuperSecret123!;";
```

---

#### Code Atelier (Projet Apprenants)

**Règles** :
- ❌ **AUCUN commentaire** explicatif (travail de l'apprenant)
- ✅ **Domaine différent** du projet démo (éviter copier-coller)
- ✅ **Complexité équivalente** au projet démo
- ✅ **Mêmes problèmes** à identifier (transposition)
- ✅ **Autonome** : Apprenant peut travailler sans le formateur

---

#### Solution Atelier

**Règles** :
- ✅ **Documentation exhaustive** : Markdown détaillé
- ✅ **Lignes précises** : Numéros de lignes des problèmes identifiés
- ✅ **Explications** : Pourquoi c'est un problème, impact, solution
- ✅ **Code moderne** : Exemples de solutions .NET 8 (si applicable)
- ✅ **Schémas** : Architecture TO-BE dessinée

---

### 7. Gestion de Versions avec Git

**Stratégie validée** :

#### Branches par Tranche Horaire

**Format** : `jourX-HHhMM-nom-livrable`

**Avantages** :
- Traçabilité parfaite de chaque étape
- Rollback facile en cas d'erreur
- Parallélisation possible (plusieurs formateurs)
- Points de synchronisation clairs

#### Timeline Exemple (Jour 1)

```
jour1-09h00-start (Code Legacy Bootstrap)
  ↓
jour1-10h30-analyse-legacy (Analyse complète + Schéma TO-BE)
  ↓
jour1-12h30-structure-5-projets (Solution .NET 8 créée)
  ↓
jour1-15h00-extraction-metier (Domain isolé)
  ↓
jour1-17h00-end (Code avec syntaxe C# 12)
```

#### Commits Standards

**Format** : 
```
[JourX-HHhMM] Titre du livrable

- Détail 1
- Détail 2
- Livrable : Description précise
```

**Exemple** :
```
[Jour1-10h30] Analyse du batch legacy et stratégie de migration

- Support Markdown complet (50 pages)
- Code DataGuard.Legacy avec annotations exhaustives
- Code ValidFlow.Legacy pour atelier
- Solution ValidFlow documentée (45 pages)
- Livrable : Document d'analyse + Schéma architecture TO-BE
```

---

### 8. Métriques de Qualité

**Pour chaque tranche horaire, vérifier** :

| Métrique | Cible |
|----------|-------|
| **Longueur support Markdown** | 30-60 pages (10 000-15 000 mots) |
| **Code démo** | 100% commenté (ratio commentaires/code > 0.5) |
| **Code atelier** | Autonome (apprenant peut commencer sans formateur) |
| **Solution atelier** | Exhaustive (30-50 pages) |
| **Temps préparation formateur** | 1-2h de lecture avant la session |
| **Temps session** | 1h30-2h00 max |
| **Livrable mesurable** | Oui/Non vérifiable |

---

### 9. Adaptation à Différents Contextes

**Ce skill est applicable à** :

#### Formations Techniques
- Modernisation d'applications (.NET, Java, Python, etc.)
- Migration vers le cloud (Azure, AWS, GCP)
- Adoption de DevOps (CI/CD, Infrastructure as Code)
- Sécurité applicative (OWASP, secure coding)

#### Ateliers Pratiques
- Code review collaboratif
- Refactoring de code legacy
- Architecture logicielle
- Tests automatisés

#### Bootcamps
- Développement web (React, Angular, Vue)
- Backend API (REST, GraphQL)
- Bases de données (SQL, NoSQL)
- Conteneurisation (Docker, Kubernetes)

**Adaptation** :
1. Définir le découpage granulaire (tranches horaires)
2. Identifier les 3-5 problèmes principaux à corriger
3. Créer projet démo + projet atelier (domaines différents)
4. Structurer chaque tranche en Théorie → Démo → Atelier
5. Valider après chaque tranche avant de continuer

---

### 10. Checklist de Démarrage d'une Nouvelle Formation

**Avant de commencer** :

- [ ] **Objectif global** défini (ex: "Migrer de .NET Framework à .NET 8")
- [ ] **Durée totale** connue (ex: 5 jours = 35 heures)
- [ ] **Public cible** identifié (niveau, prérequis)
- [ ] **Découpage granulaire** planifié (tranches de 1h30-2h00)
- [ ] **Projets fil rouge** choisis (1 démo + 1 atelier minimum)
- [ ] **Stack technique** définie (outils, frameworks, versions)
- [ ] **Environnement** préparé (IDE, accès, licences)
- [ ] **Repository Git** initialisé avec stratégie de branches

**Pendant la création** :

- [ ] Créer `XX_Architecture_et_Pedagogie.md` (contexte, objectifs, stack)
- [ ] Créer `XX_Deroulement_Granulaire.md` (planning détaillé)
- [ ] Générer tranche par tranche (ne jamais sauter d'étapes)
- [ ] Valider après chaque tranche
- [ ] Tester le code (compilation + exécution)
- [ ] Relire les supports (orthographe, clarté)

**Après la formation** :

- [ ] Recueillir feedback apprenants
- [ ] Ajuster les supports si nécessaire
- [ ] Archiver version finale
- [ ] Mettre à jour ce skill avec nouvelles bonnes pratiques

---

## 📊 Historique des Validations (Session du 6 mars 2026)

### Approches Validées

1. ✅ **Architecture 5 projets** (Domain, Infrastructure, Application.Services, Application.Console, Tests)
   - Séparation "Humble Object" pour le point d'entrée Console
   - Application.Services réutilisable pour API/Blazor/Function

2. ✅ **Branches Git par tranche horaire** (`jourX-HHhMM-livrable`)
   - Flexibilité maximale
   - Traçabilité parfaite
   - Rollback facile

3. ✅ **Structure Théorie → Démonstration → Atelier** (30% - 45% - 25%)
   - Équilibre validé pour absorption optimale
   - Permet validation incrémentale

4. ✅ **Génération itérative avec validation formateur**
   - Ne jamais générer plusieurs tranches d'un coup
   - Attendre validation avant de continuer

5. ✅ **Règles de confidentialité strictes**
   - Aucune mention d'outils internes dans supports apprenants
   - Séparation matériel interne vs matériel apprenant

### Corrections et Ajustements

1. **Architecture** : Passage de 4 projets → 5 projets
   - Ajout de la séparation Application.Services / Application.Console
   - Justification : Pattern "Humble Object" + réutilisabilité

2. **Support pédagogique** : Annotations code enrichies
   - Ajout blocs visuels avec émojis (⚠️🐌💥🔧📦)
   - Numéros de lignes précis pour référence

---

## 🔄 Mise à Jour du Skill

**Ce skill sera mis à jour à chaque fois que** :

1. Une nouvelle **approche pédagogique** est validée
2. Une **correction méthodologique** est appliquée
3. Un nouveau **standard de qualité** est défini
4. Une **bonne pratique** émerge pendant la création

**Processus de mise à jour** :
1. Formateur valide une nouvelle approche
2. Ajouter dans la section correspondante de ce skill
3. Indiquer la date de mise à jour
4. Commiter avec message `[SKILL] Description de la mise à jour`

---

## 📚 Références

**Méthodologies pédagogiques** :
- Taxonomie de Bloom (objectifs d'apprentissage)
- Apprentissage par la pratique (Learning by Doing)
- Scaffolding (échafaudage pédagogique)

**Architecture logicielle** :
- Clean Architecture (Robert C. Martin)
- Domain-Driven Design (Eric Evans)
- Dependency Injection Principles (Mark Seemann)

**Gestion de projet** :
- Agile (itérations courtes avec validation)
- Kanban (visualisation du flux)
- Git Flow (gestion de branches)

---

**Document créé le** : 6 mars 2026  
**Version** : 1.0  
**Auteur** : Formation .NET Modernisation  
**Prochaine révision** : Après chaque session de formation
