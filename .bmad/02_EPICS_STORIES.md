# Epics & Stories - Formation .NET Legacy → .NET 8

**Version** : 1.0  
**Date** : 19 mars 2026  
**Product Manager** : Agent BMAD  
**Statut** : ✅ Validé

---

## 📋 Vue d'Ensemble

Ce document découpe le PRD de la formation en **5 Epics** (un par jour) et **20 Stories** (une par session).

Chaque Story suit le format :
- **ID Unique** : FR001, FR002, etc.
- **User Story** : En tant que [persona], je veux [action], afin de [bénéfice]
- **Critères d'Acceptation** : Checklist mesurable
- **Dépendances** : Stories bloquantes
- **Effort Estimé** : Temps de développement (génération du contenu)
- **Priorité** : MoSCoW (Must, Should, Could, Won't)

---

## Epic 1 - Jour 1 : Fondations d'une Application Moderne

**Objectif Epic** : Prouver que le code legacy est dangereux et créer l'architecture cible (Clean Architecture)

**Valeur Business** : Convaincre les développeurs que le refactoring est nécessaire (ROI démontré)

**Stories** : FR001, FR002, FR003, FR004

---

### Story FR001 - Session 09h00 : Analyse du Batch Legacy

**En tant que** formateur  
**Je veux** un document avec théorie + exercice détection 5 anti-patterns  
**Afin de** prouver que le code legacy est dangereux avec impact business chiffré

#### Critères d'Acceptation

**Contenu Théorique** :
- [ ] Section 🧠 **Concepts Fondamentaux** : Dette Technique expliquée
  - Définition dette technique
  - Les 5 catégories d'anti-patterns (Sécurité, Performance, Robustesse, Maintenabilité, Déploiement)
  - Tableau avec "Question clé" et "Impact Business" par catégorie
- [ ] Diagramme Mermaid : Workflow Legacy (AS-IS)
  - Flux : InputDB → Program.cs → Validation → OutputEmail/XML
  - Style : Code monolithique en rouge
- [ ] Section 💡 **L'Astuce Pratique** : Principe SOLID comme détecteur
  - Explication SRP (Single Responsibility Principle)
  - Comment détecter une violation SOLID

**Interaction Pédagogique** :
- [ ] Section 💬 **Analyse Collective** : Question ouverte à la salle
  - Question : "Combien de temps pour être certain qu'une modification ne casse rien ?"
  - Instruction formateur : Silence 5-8 secondes
  - Réponse attendue : "Des heures, voire des jours"

**Exercice Pratique** :
- [ ] Section ⚙️ **Défi d'Application** : Détective du Code Legacy
  - Durée : 15 minutes
  - Contexte : Héritage du batch ValidFlow
  - Mission : Identifier 5 problèmes (1 par catégorie) avec numéros de ligne
  - Format de réponse attendu fourni
  - Critères de succès : 5 problèmes identifiés + impact business documenté

**Scaffolding (CRITIQUE)** :
- [ ] Section 💡 **Pistes de réflexion** :
  - Sécurité : "Cherchez les mots de passe dans le code (lignes 15-20)"
  - Performance : "Les appels SQL sont-ils asynchrones ?"
  - Robustesse : "Que se passe-t-il si SQL Server plante ?"
  - Maintenabilité : "Pouvez-vous tester ValidateData() sans lancer SQL ?"
  - Déploiement : "Le chemin 'output.xml' fonctionne-t-il sur Linux ?"

**Scripts Formateur** :
- [ ] 🎤 **Script Ouverture** (2 min) :
  - Accueil + présentation objectif journée
  - Annonce : "On va auditer avant de coder"
  - Pourquoi : "Prouver que le code est dangereux pour obtenir budget refactoring"
- [ ] 🎤 **Script Lancement Exercice** (1 min) :
  - "Vous avez 15 minutes. Top chrono !"
  - Action : Lancer chronomètre projeté à l'écran

**Timing Détaillé** :
- [ ] 09h00-09h10 : Théorie (10 min) - Concepts + Diagramme
- [ ] 09h10-09h20 : Interaction (10 min) - Astuce Pratique + Question Collective
- [ ] 09h20-09h22 : Lancement (2 min) - Script exercice
- [ ] 09h22-09h37 : Exercice (15 min) - Stagiaires cherchent anti-patterns
- [ ] 09h37-09h45 : Surveillance (8 min) - Formateur débloque via chat
- [ ] 09h45-10h00 : Correction (15 min) - Partage solution Drive + Revue collective
- **Total** : 1h00 (session courte car première du jour)

**Solution Drive** :
- [ ] Fichier `J1_S1_Solution_09h00_Analyse.md` créé sur Drive
- [ ] Contenu :
  - 5 problèmes documentés avec numéros de ligne exacts
  - Impact business chiffré par problème
  - Code avant/après pour chaque anti-pattern
  - Tableau synthèse : Cartographie des risques (coût 85k€-550k€)
  - Diagramme Mermaid AS-IS vs TO-BE

**Validation Qualité** :
- [ ] Zéro mention IA (scan grep)
- [ ] Toutes les icônes présentes (🧠💡💬⚙️🔗)
- [ ] Lien Drive vers solution à la fin
- [ ] Timing total = 1h00

#### Dépendances

**Bloquants** :
- ✅ Code legacy `ValidFlow.Legacy/Program.cs` existe (déjà présent)
- ✅ README.md avec instructions clone (déjà présent)

**Bloqué par** : Aucune (première story)

#### Effort Estimé

**Temps de développement** : 2h
- 1h : Rédaction contenu théorique + exercice + scripts
- 30 min : Création solution Drive
- 30 min : Validation qualité (scan IA, vérification icônes, timing)

#### Priorité

**MoSCoW** : **MUST** (MVP V1)

---

### Story FR002 - Session 10h40 : Scaffolding de la Clean Architecture

**En tant que** stagiaire  
**Je veux** créer 5 projets .NET 8 isolés via dotnet CLI  
**Afin de** remplacer le monolithe par une architecture testable

#### Critères d'Acceptation

**Contenu Théorique** :
- [ ] Section 🧠 **Concepts Fondamentaux** : Clean Architecture expliquée
  - Principe Inversion de Dépendances (Dependency Inversion)
  - Les 5 couches : Domain (cœur) → Application → Infrastructure → Console → Tests
  - Pourquoi Domain est au centre (zéro dépendance externe)
- [ ] Diagramme Mermaid : Architecture Clean (TO-BE)
  - Flux : Console → Application → Domain ← Infrastructure
  - Flèches : Infrastructure dépend de Domain (inversion)
  - Style : Domain en vert (zone stérile)

**Interaction Pédagogique** :
- [ ] Section 💡 **L'Astuce Pratique** : Métaphore "Île Stérile"
  - Domain = île isolée, rien n'entre sauf du C# pur
  - Pas de bateau SQL, pas d'avion SMTP
- [ ] Section 💬 **Analyse Collective** : Question
  - "Pourquoi ne pas mettre Entity Framework dans le Domain ?"
  - Réponse attendue : "Parce que EF est une dépendance externe (NuGet)"

**Exercice Pratique** :
- [ ] Section ⚙️ **Défi d'Application** : Créer 5 projets .NET 8
  - Durée : 30 minutes
  - Commandes dotnet CLI fournies (copier-coller)
  - Ordre de création : Domain → Tests → Application → Infrastructure → Console
  - Ajout des références projet (dotnet add reference)
  - Validation : dotnet build passe au vert

**Scaffolding** :
- [ ] Section 💡 **Pistes de réflexion** :
  - "Créez d'abord le Domain (classlib sans dépendances)"
  - "Le projet Tests référence Domain, pas l'inverse"
  - "Console référence Application, pas Infrastructure directement"
  - "Si erreur CS0234, vérifiez l'ordre des références"

**Scripts Formateur** :
- [ ] 🎤 **Script Démonstration Live** (15 min) :
  - Ouvrir terminal sur écran 2
  - Montrer création Domain en direct
  - Commenter chaque commande dotnet
  - Montrer fichier .csproj généré
- [ ] 🎤 **Script Lancement Exercice** (1 min) :
  - "À vous maintenant. Reproduisez exactement ce que j'ai fait."
  - "Objectif : dotnet build au vert dans 30 minutes"

**Timing Détaillé** :
- [ ] 10h40-10h50 : Théorie (10 min) - Clean Architecture + Diagramme
- [ ] 10h50-11h05 : Démo Live (15 min) - Formateur crée 5 projets en direct
- [ ] 11h05-11h35 : Exercice (30 min) - Stagiaires reproduisent
- [ ] 11h35-11h50 : Correction (15 min) - Solution Drive + Revue structure
- **Total** : 1h10 (pause avant 13h30)

**Solution Drive** :
- [ ] Fichier `J1_S2_Solution_10h40_Architecture.md`
- [ ] Contenu :
  - Commandes dotnet CLI complètes (copier-coller)
  - Structure finale des dossiers (arborescence)
  - Diagramme Mermaid des dépendances projet
  - Troubleshooting : Erreurs courantes (CS0234, CS0246)

**Validation Qualité** :
- [ ] Zéro mention IA
- [ ] Icônes 🧠💡💬⚙️🔗 présentes
- [ ] Timing = 1h10

#### Dépendances

**Bloquants** :
- ✅ FR001 complété (stagiaires comprennent pourquoi on refactorise)

**Bloqué par** : FR001

#### Effort Estimé

**Temps** : 2h30
- 1h : Rédaction théorie + démo live scripté
- 1h : Création solution Drive (commandes CLI testées)
- 30 min : Validation qualité

#### Priorité

**MoSCoW** : **MUST** (MVP V1)

---

### Story FR003 - Session 13h30 : Implémentation du Cœur Métier (Domain)

**En tant que** stagiaire  
**Je veux** extraire l'entité Client et les règles de validation vers le projet Domain  
**Afin de** isoler le cœur métier sans dépendance externe (testable en 15ms)

#### Critères d'Acceptation

**Contenu Théorique** :
- [ ] Section 🧠 **Concepts Fondamentaux** : Domain-Driven Design (DDD)
  - Entité vs Value Object
  - Règle métier = logique qui change rarement
  - Pourquoi zéro dépendance externe (Testabilité)
- [ ] Diagramme Mermaid : classDiagram (Legacy vs Cible)
  - Legacy : Program.cs → IRule → SQL/SMTP (couplage)
  - Cible : Client (record) → IValidationRule (isolation)

**Interaction Pédagogique** :
- [ ] Section 💡 **L'Astuce Pratique** : Crash Test
  - Démonstration formateur : Lancer test sans SQL → NullReferenceException
  - Avec Domain isolé : Test passe en 15ms
- [ ] Section 💬 **Analyse Collective** :
  - "Combien de temps pour tester une règle métier dans le code legacy ?"
  - Réponse : "10 minutes (lancer SQL + SMTP + insérer données)"

**Exercice Pratique** :
- [ ] Section ⚙️ **Défi d'Application** : Migration vers Domain
  - Durée : 45 minutes
  - Étapes :
    1. Créer dossiers (Entities, Interfaces, ValueObjects)
    2. Créer entité Client (record C# 12)
    3. Créer interface IValidationRule
    4. Implémenter MinLengthRule, MaxLengthRule, MandatoryRule (pattern matching)
    5. Créer tests unitaires (xUnit)
    6. Lancer dotnet test → Vert en < 100ms
  - Critères de succès : Tests passent, zéro dépendance NuGet dans Domain

**Scaffolding** :
- [ ] Section 💡 **Pistes de réflexion** :
  - "Utilisez `record` pour Client (immuabilité)"
  - "Pattern matching : `value switch { null or \"\" => false, ... }`"
  - "Tests : Arrangez (new Client), Agissez (IsValid()), Assertez (Assert.True)"
  - "Référence projet : Tests → Domain (dotnet add reference)"

**Scripts Formateur** :
- [ ] 🎤 **Script Crash Test** (5 min) :
  - "Je vais tester ValidateData() du legacy"
  - "Sans SQL Server lancé... BOUM ! NullReferenceException"
  - "Avec Domain isolé... 15ms, tests verts"
- [ ] 🎤 **Script Lancement Exercice** (1 min) :
  - "45 minutes pour migrer 3 règles vers le Domain"
  - "Objectif : dotnet test au vert en < 100ms"

**Timing Détaillé** :
- [ ] 13h30-13h40 : Théorie (10 min) - DDD + classDiagram
- [ ] 13h40-13h45 : Crash Test (5 min) - Démo NullRef vs 15ms
- [ ] 13h45-14h30 : Exercice (45 min) - Migration Domain
- [ ] 14h30-14h50 : Correction (20 min) - Solution Drive + Revue tests
- **Total** : 1h20

**Solution Drive** :
- [ ] Fichier `J1_S3_Solution_13h30_Domain.md`
- [ ] Contenu :
  - Code complet C# 12 (Client.cs, IValidationRule.cs, règles)
  - Tests unitaires complets (ClientTests.cs, ValidationRulesTests.cs)
  - Explication pattern matching (switch expressions)
  - Diagramme classDiagram final

**Validation Qualité** :
- [ ] Zéro mention IA
- [ ] Icônes présentes
- [ ] Timing = 1h20

#### Dépendances

**Bloquants** :
- ✅ FR002 complété (projet Domain créé)

**Bloqué par** : FR002

#### Effort Estimé

**Temps** : 3h
- 1h30 : Rédaction théorie + crash test + exercice
- 1h : Solution Drive (code C# 12 complet testé)
- 30 min : Validation

#### Priorité

**MoSCoW** : **MUST** (MVP V1)

---

### Story FR004 - Session 15h10 : Modernisation de la Syntaxe (C# 12)

**En tant que** stagiaire  
**Je veux** refactoriser la syntaxe legacy vers C# 12  
**Afin de** réduire la complexité et améliorer la lisibilité

#### Critères d'Acceptation

**Contenu Théorique** :
- [ ] Section 🧠 **Concepts Fondamentaux** : Nouveautés C# 12
  - File-scoped namespaces (économie 2 niveaux indentation)
  - Primary constructors (réduction boilerplate)
  - Collection expressions (syntaxe `[...]`)
  - Using declarations vs using imbriqués
- [ ] Tableau comparatif : Avant C# 12 vs Après C# 12

**Interaction Pédagogique** :
- [ ] Section 💡 **L'Astuce Pratique** : ROI de la modernisation
  - -30% de lignes de code
  - +50% de lisibilité (études)
- [ ] Section 💬 **Analyse Collective** :
  - "Pourquoi moderniser si le code legacy fonctionne ?"
  - Réponse : "Maintenabilité, onboarding nouveaux dev, dette technique"

**Exercice Pratique** :
- [ ] Section ⚙️ **Défi d'Application** : Refactoriser 3 classes
  - Durée : 30 minutes
  - Classes cibles : MinLengthRule, MaxLengthRule, MandatoryRule
  - Changements :
    1. File-scoped namespace
    2. Primary constructors (pour MinLengthRule, MaxLengthRule)
    3. Simplifier switch expressions
  - Critères : Tests restent verts après refactoring

**Scaffolding** :
- [ ] Section 💡 **Pistes de réflexion** :
  - "File-scoped : Supprimez `{ }` autour du namespace"
  - "Primary constructor : `record MinLengthRule(int MinLength)`"
  - "Vérifiez que dotnet test passe toujours au vert"

**Scripts Formateur** :
- [ ] 🎤 **Script Démo Live Before/After** (10 min) :
  - Afficher MinLengthRule avant (C# 9)
  - Refactoriser en direct vers C# 12
  - Compter lignes : Avant 25, Après 18 (-28%)
- [ ] 🎤 **Script Lancement Exercice** (1 min) :
  - "30 minutes pour moderniser vos 3 règles"
  - "Attention : Les tests doivent rester verts !"

**Timing Détaillé** :
- [ ] 15h10-15h20 : Théorie (10 min) - C# 12 + Tableau comparatif
- [ ] 15h20-15h30 : Démo Live (10 min) - Before/After en direct
- [ ] 15h30-16h00 : Exercice (30 min) - Refactoring 3 classes
- [ ] 16h00-16h20 : Correction (20 min) - Solution Drive + Diff
- **Total** : 1h10

**Solution Drive** :
- [ ] Fichier `J1_S4_Solution_15h10_CSharp12.md`
- [ ] Contenu :
  - Code avant/après pour chaque classe
  - Diff visuel (rouge/vert)
  - Explication ligne par ligne des changements
  - Métriques : Lignes avant/après, complexité cyclomatique

**Validation Qualité** :
- [ ] Zéro mention IA
- [ ] Icônes présentes
- [ ] Timing = 1h10

#### Dépendances

**Bloquants** :
- ✅ FR003 complété (règles de validation créées en C# 12 basique)

**Bloqué par** : FR003

#### Effort Estimé

**Temps** : 2h
- 1h : Théorie + démo live scripté
- 45 min : Solution Drive (diff avant/après)
- 15 min : Validation

#### Priorité

**MoSCoW** : **MUST** (MVP V1)

---

## Epic 2 - Jour 2 : Maîtriser l'Accès aux Données et l'Injection de Dépendances

**Objectif Epic** : Découpler l'application pour pouvoir isoler les composants et préparer les tests

**Valeur Business** : Rendre le code testable sans infrastructure complète

**Stories** : FR005, FR006, FR007, FR008

---

### Story FR005 - Session 09h00 : L'Injection de Dépendances (DI)

**En tant que** stagiaire  
**Je veux** comprendre le conteneur IoC de .NET 8  
**Afin de** orchestrer l'application et injecter le moteur de validation

#### Critères d'Acceptation (Simplifiés - À détailler en V2)

- [ ] Section 🧠 : Principe IoC (Inversion of Control)
- [ ] Diagramme Mermaid : Sans DI vs Avec DI
- [ ] Section ⚙️ : Configurer ServiceCollection
- [ ] Section 💡 Pistes : Lifetime (Transient vs Singleton)
- [ ] Scripts 🎤 : Démo live Program.cs
- [ ] Solution Drive : Code complet DI

**Dépendances** : FR004 (Jour 1 complet)

**Effort** : 2h30

**Priorité** : **SHOULD** (V2)

---

### Story FR006 - Session 10h40 : Introduction à Entity Framework Core 8

**En tant que** stagiaire  
**Je veux** abandonner SqlConnection raw et utiliser EF Core  
**Afin de** avoir un accès données typé et sécurisé

#### Critères d'Acceptation (Simplifiés - À détailler en V2)

- [ ] Section 🧠 : ORM vs SQL raw
- [ ] Section ⚙️ : Créer DbContext + entité Client typée
- [ ] Section 💡 Pistes : Code First vs Database First
- [ ] Scripts 🎤 : Démo migration

**Dépendances** : FR005

**Effort** : 3h

**Priorité** : **SHOULD** (V2)

---

### Story FR007 - Session 13h30 : Le Repository Pattern

**En tant que** stagiaire  
**Je veux** séparer la logique métier de l'accès données  
**Afin de** rendre le code testable avec une base In-Memory

#### Critères d'Acceptation (Simplifiés)

- [ ] Section 🧠 : Pattern Repository
- [ ] Section ⚙️ : Créer IClientRepository + implémentation EF
- [ ] Section 💡 : Injection dans Application layer

**Dépendances** : FR006

**Effort** : 2h30

**Priorité** : **SHOULD** (V2)

---

### Story FR008 - Session 15h10 : Migrations et Testabilité Data

**En tant que** stagiaire  
**Je veux** gérer l'évolution de la base de données  
**Afin de** éviter les scripts SQL manuels risqués

#### Critères d'Acceptation (Simplifiés)

- [ ] Section 🧠 : Migrations EF Core
- [ ] Section ⚙️ : dotnet ef migrations add InitialCreate
- [ ] Section 💡 : In-Memory DB pour tests

**Dépendances** : FR007

**Effort** : 2h

**Priorité** : **SHOULD** (V2)

---

## Epic 3 - Jour 3 : Sécuriser la Configuration et les Services

**Stories** : FR009 (Externalisation Config), FR010 (Secrets), FR011 (MailKit), FR012 (Logging)

**Priorité** : **SHOULD** (V2)

---

## Epic 4 - Jour 4 : Garantir la Qualité (Tests) et Conteneurisation

**Stories** : FR013 (Tests Unitaires), FR014 (Tests Intégration), FR015 (Cross-Platform), FR016 (Docker)

**Priorité** : **SHOULD** (V2)

---

## Epic 5 - Jour 5 : CI/CD & Bilan

**Stories** : FR017 (GitHub Actions), FR018 (Revue Code), FR019 (Documentation), FR020 (Bilan AS-IS/TO-BE)

**Priorité** : **COULD** (V3)

---

## 📊 Récapitulatif Effort & Priorités

| Story | Epic | Session | Effort | Priorité | Sprint |
|-------|------|---------|--------|----------|--------|
| FR001 | 1 | Jour 1 - 09h00 | 2h | MUST | Sprint 1 |
| FR002 | 1 | Jour 1 - 10h40 | 2h30 | MUST | Sprint 2 |
| FR003 | 1 | Jour 1 - 13h30 | 3h | MUST | Sprint 3 |
| FR004 | 1 | Jour 1 - 15h10 | 2h | MUST | Sprint 4 |
| FR005 | 2 | Jour 2 - 09h00 | 2h30 | SHOULD | V2 |
| FR006 | 2 | Jour 2 - 10h40 | 3h | SHOULD | V2 |
| FR007 | 2 | Jour 2 - 13h30 | 2h30 | SHOULD | V2 |
| FR008 | 2 | Jour 2 - 15h10 | 2h | SHOULD | V2 |
| FR009-FR020 | 3-5 | Jours 3-5 | TBD | SHOULD/COULD | V2/V3 |

**Total Effort MVP (V1 - Epic 1)** : 9h30 (génération contenu Jour 1 complet)

---

## 🎯 Définition of Done (DoD) par Story

Une Story est considérée comme **DONE** si et seulement si :

### Livrables
1. ✅ Fichier `Jour_X_Theme.md` créé dans `03_Support_Quotidien/` (Git)
2. ✅ Fichier `JX_SY_Solution_HHhMM_Theme.md` créé sur Drive

### Qualité Contenu
3. ✅ Toutes les sections obligatoires présentes : 🧠💡💬⚙️🔗
4. ✅ Scaffolding (💡 Pistes de réflexion) présent pour tous les exercices
5. ✅ Scripts téléprompter 🎤 (minimum 2 par session)
6. ✅ Timing documenté et total = durée session (1h00-1h30)
7. ✅ Diagramme Mermaid présent et correct
8. ✅ Zéro mention IA (scan grep négatif)

### Validation Technique
9. ✅ Solution Drive testée (code compile, tests passent)
10. ✅ Commandes CLI testées sur Windows PowerShell
11. ✅ Markdown valide (pas d'erreurs syntaxe)

### Validation Utilisateur
12. ✅ USER a validé la Story (feedback GO/NOGO)

---

## 🚀 Ordre d'Exécution (Sprints)

### Sprint 1 : Story FR001 (Session 09h00)
**Objectif** : Première session validée avec scaffolding complet

**Actions** :
1. Générer `Jour_1_Fondations.md` (section Session 09h00 uniquement)
2. Générer `J1_S1_Solution_09h00_Analyse.md` (Drive)
3. Validation qualité (DoD 1-11)
4. **PAUSE** → Attendre validation USER

**Si USER = GO** → Sprint 2  
**Si USER = NOGO** → Corriger selon feedback

---

### Sprint 2 : Story FR002 (Session 10h40)
**Dépendance** : FR001 validée

**Actions** :
1. Ajouter section Session 10h40 dans `Jour_1_Fondations.md`
2. Générer `J1_S2_Solution_10h40_Architecture.md`
3. Validation qualité
4. **PAUSE** → Validation USER

---

### Sprint 3 : Story FR003 (Session 13h30)
**Dépendance** : FR002 validée

---

### Sprint 4 : Story FR004 (Session 15h10)
**Dépendance** : FR003 validée

**Livrable Final Sprint 4** :
- ✅ Fichier `Jour_1_Fondations.md` complet (4 sessions)
- ✅ 4 solutions Drive
- ✅ Validation USER → **GO pour V2 (Jours 2-3)** ou FIN V1

---

## 📝 Notes de Planification

### Stratégie Itérative
- **Une session à la fois** : Évite de générer du contenu non validé
- **Feedback immédiat** : Corrections rapides avant de continuer
- **Validation bloquante** : Ne pas passer à Sprint N+1 sans validation Sprint N

### Gestion Risques
- **Risque** : USER demande changement structure après Sprint 2
- **Mitigation** : Template de session validé AVANT Sprint 1 (Phase ARCHITECT)

---

**Fin Epics & Stories - Version 1.0**

**Prochaine Étape** : Créer `.bmad/03_ARCHITECTURE_DOCUMENTAIRE.md` (Phase ARCHITECT)
