# Déroulement Granulaire - Formation Modernisation .NET (5 Jours)

**Formation** : Modernisation d'une Application de .NET Framework à .NET 8  
**Durée** : 5 jours (35 heures)  
**Approche** : Diviser pour Régner - Maîtrise totale du temps et des livrables  
**Date de création** : 6 mars 2026

---

## Stratégie Pédagogique : Découpage par Tranches Horaires

### Philosophie "Diviser pour Régner"

Chaque **tranche horaire** (1h30 à 2h00) correspond à :
- ✅ Un **objectif pédagogique précis**
- ✅ Un **livrable mesurable**
- ✅ Une **branche Git dédiée** (flexibilité maximale)
- ✅ Une **validation formateur** avant de passer à la suite

**Avantages** :
- Maîtrise du temps (on sait exactement où on en est)
- Récupération facile en cas d'erreur (retour à la branche précédente)
- Points de synchronisation pour les apprenants (chaque pause)
- Traçabilité pédagogique parfaite

---

## Stratégie Git par Tranche Horaire

### Format de Nommage

```
jourX-HHhMM-nom-livrable
```

**Exemples** :
- `jour1-09h00-start` : Point de départ du jour 1
- `jour1-10h30-analyse-legacy` : Fin de l'analyse legacy
- `jour1-12h30-structure-5-projets` : Structure créée
- `jour1-17h00-end` : Fin du jour 1 (= jour1-end)

### Timeline Git Jour 1

```
jour1-09h00-start (Legacy Bootstrap)
  ↓
jour1-10h30-analyse-legacy (Schéma TO-BE créé)
  ↓
jour1-12h30-structure-5-projets (Solution .NET 8 créée)
  ↓
jour1-15h00-extraction-metier (Domain isolé)
  ↓
jour1-17h00-end (Code avec syntaxe C# 12)
```

---

## Jour 1 — Bâtir les Fondations d'une Application Moderne

### 09h00 - 10h30 (1h30) | Analyse du batch legacy et stratégie

**Branche Git** : `jour1-09h00-start` → `jour1-10h30-analyse-legacy`

**Objectifs** :
- Comprendre les enjeux de la migration vers .NET 8
- Identifier les 5 problèmes majeurs du code Legacy
- Découvrir les "seams" (coutures) pour le découplage
- Dessiner l'architecture TO-BE en 5 projets

**Contenu Théorique** :
1. Pourquoi migrer vers .NET 8 ?
   - Performance (10x plus rapide)
   - Sécurité (TLS 1.3, patches actifs)
   - Multi-plateforme (Linux, Docker, Kubernetes)
   - Coûts (cloud-native, réduction infra)
2. Les 5 problèmes du code Legacy :
   - ⚠️ Sécurité : Credentials hardcodés
   - 🐌 Performance : Code synchrone bloquant
   - 💥 Robustesse : Aucune gestion d'erreurs
   - 🔧 Maintenabilité : Pas de DI, couplage fort
   - 📦 Déploiement : Windows uniquement
3. Concept de "Seam" (Michael Feathers)
   - Chaque abstraction = une couture
   - Permet le découplage et les tests

**Démonstration (DataGuard)** :
1. Ouvrir le code Legacy (.NET Framework 4.8)
2. Analyser `Program.cs` ligne par ligne
3. Identifier les hardcoded values (lignes précises)
4. Repérer les appels synchrones bloquants
5. Dessiner le schéma d'architecture TO-BE au tableau

**Atelier Pratique (ValidFlow)** :
- **Énoncé** : Analysez le code Legacy de ValidFlow et créez un document listant les 5 problèmes majeurs avec les numéros de lignes
- **Livrable** : Schéma d'architecture TO-BE dessiné (Domain, Infrastructure, Application.Services, Application.Console, Tests)

**Support Markdown** : `Jour1_09h00-10h30_Analyse_Legacy.md`

**Fichiers Code** :
- `DataGuard.Legacy/Program.cs` (commenté avec annotations ⚠️🐌💥🔧📦)
- `ValidFlow.Legacy/Program.cs` (pour atelier)
- `ValidFlow.Solutions/Analyse_Problemes.md` (solution atelier)

---

### 10h40 - 12h30 (1h50) | Création de la nouvelle structure en 5 projets

**Branche Git** : `jour1-10h30-analyse-legacy` → `jour1-12h30-structure-5-projets`

**Objectifs** :
- Créer la solution .NET 8 avec 5 projets
- Configurer les références inter-projets
- Comprendre le rôle de chaque projet
- Valider la compilation

**Contenu Théorique** :
1. Architecture en couches moderne
   - Domain : Logique métier pure (zéro dépendance externe)
   - Infrastructure : Techniques (EF Core, EmailService)
   - Application.Services : Orchestration réutilisable
   - Application.Console : Point d'entrée "Humble Object"
   - Tests : Validation automatisée
2. Pattern "Humble Object"
   - Console ne contient QUE le Composition Root
   - Toute la logique est déléguée aux Services
3. Avantages de la séparation en 5 projets
   - Testabilité (Services testables sans Console)
   - Réutilisabilité (Services → API Web, Blazor, Azure Function)
   - Maintenabilité (Responsabilité unique)

**Démonstration (DataGuard)** :
1. Création de la solution
   ```bash
   dotnet new sln -n DataGuard
   ```
2. Création des 5 projets
   ```bash
   dotnet new classlib -n DataGuard.Domain -f net8.0
   dotnet new classlib -n DataGuard.Infrastructure -f net8.0
   dotnet new classlib -n DataGuard.Application.Services -f net8.0
   dotnet new console -n DataGuard.Application.Console -f net8.0
   dotnet new xunit -n DataGuard.Tests -f net8.0
   ```
3. Ajout à la solution
   ```bash
   dotnet sln add **/*.csproj
   ```
4. Configuration des références
   - Console → Services → Domain + Infrastructure → Domain
   - Tests → tous les projets

**Atelier Pratique (ValidFlow)** :
- **Énoncé** : Créez la solution ValidFlow avec 5 projets .NET 8 et configurez les références
- **Livrable** : Solution compilable avec `dotnet build` sans erreurs

**Support Markdown** : `Jour1_10h40-12h30_Structure_5_Projets.md`

**Fichiers Code** :
- `DataGuard.sln` + 5 projets vides mais avec références configurées
- `ValidFlow.sln` (pour atelier - structure de base)

---

### 13h30 - 15h00 (1h30) | Extraction et modernisation de la logique métier

**Branche Git** : `jour1-12h30-structure-5-projets` → `jour1-15h00-extraction-metier`

**Objectifs** :
- Déplacer le code métier vers le projet Domain
- Créer les abstractions (interfaces)
- Isoler la logique de validation
- Rendre le Domain testable

**Contenu Théorique** :
1. Principe de Séparation des Responsabilités
   - Domain ne doit avoir AUCUNE dépendance externe
   - Pas de référence à System.Data, System.Net, etc.
2. Abstraction vs Implémentation
   - Interface = Contrat
   - Implémentation = Détail technique
3. Dependency Inversion Principle (DIP)
   - Les modules de haut niveau ne dépendent pas des modules de bas niveau
   - Les deux dépendent d'abstractions

**Démonstration (DataGuard)** :
1. Création du modèle Domain
   ```csharp
   // DataGuard.Domain/Models/XmlData.cs
   public record XmlData(int Id, string Tag, string Value);
   ```
2. Création des interfaces
   ```csharp
   // DataGuard.Domain/Interfaces/IRule.cs
   public interface IRule
   {
       bool IsValid(string? value);
       string ErrorMessage(string tagName, string? value);
   }
   
   // DataGuard.Domain/Interfaces/IDataRepository.cs
   public interface IDataRepository
   {
       Task<List<XmlData>> GetAllDataAsync();
   }
   ```
3. Implémentation des règles de validation
   ```csharp
   // DataGuard.Domain/Validators/MandatoryRule.cs
   public class MandatoryRule : IRule
   {
       public bool IsValid(string? value) => !string.IsNullOrEmpty(value);
       public string ErrorMessage(string tagName, string? value) =>
           $"Value for '{tagName}' is mandatory and cannot be empty.";
   }
   ```

**Atelier Pratique (ValidFlow)** :
- **Énoncé** : Créez le modèle Domain pour les entités Client/Commande et implémentez les règles EmailRule, PhoneRule
- **Livrable** : Projet Domain avec modèles et validateurs, compilation réussie

**Support Markdown** : `Jour1_13h30-15h00_Extraction_Metier.md`

**Fichiers Code** :
- `DataGuard.Domain/` complet avec Models, Interfaces, Validators
- `ValidFlow.Domain/` (pour atelier - squelette)
- `ValidFlow.Solutions/Domain/` (solution complète)

---

### 15h10 - 17h00 (1h50) | Syntaxe C# 12 et optimisation du code

**Branche Git** : `jour1-15h00-extraction-metier` → `jour1-17h00-end`

**Objectifs** :
- Découvrir les nouveautés C# 12
- Refactoriser le code avec syntaxe moderne
- Ajouter les signatures asynchrones
- Valider par des tests unitaires simples

**Contenu Théorique** :
1. Nouveautés C# 12
   - File-scoped namespaces
   - Primary constructors
   - Collection expressions
   - Pattern matching avancé
2. Async/Await
   - Task vs Task<T>
   - Signature asynchrone
   - ConfigureAwait(false)

**Démonstration (DataGuard)** :
1. File-scoped namespaces
   ```csharp
   // Avant (C# 7)
   namespace DataGuard.Domain.Models
   {
       public class XmlData { }
   }
   
   // Après (C# 12)
   namespace DataGuard.Domain.Models;
   
   public class XmlData { }
   ```

2. Primary constructors
   ```csharp
   // Avant
   public class MinLengthRule : IRule
   {
       private readonly int _minLength;
       public MinLengthRule(int minLength) 
       { 
           _minLength = minLength; 
       }
   }
   
   // Après (C# 12)
   public class MinLengthRule(int minLength) : IRule
   {
       public bool IsValid(string? value) => 
           value != null && value.Length >= minLength;
   }
   ```

3. Signatures asynchrones
   ```csharp
   public interface IRule
   {
       // Peut être async si validation externe (API, DB)
       Task<bool> IsValidAsync(string? value);
       string ErrorMessage(string tagName, string? value);
   }
   ```

4. Tests unitaires basiques
   ```csharp
   // DataGuard.Tests/Unit/MandatoryRuleTests.cs
   public class MandatoryRuleTests
   {
       [Fact]
       public void IsValid_WhenNull_ReturnsFalse()
       {
           var rule = new MandatoryRule();
           Assert.False(rule.IsValid(null));
       }
       
       [Fact]
       public void IsValid_WhenNotEmpty_ReturnsTrue()
       {
           var rule = new MandatoryRule();
           Assert.True(rule.IsValid("test"));
       }
   }
   ```

**Atelier Pratique (ValidFlow)** :
- **Énoncé** : Refactorisez votre code Domain avec syntaxe C# 12 et ajoutez 5 tests unitaires pour vos règles
- **Livrable** : Code modernisé + tests au vert (`dotnet test`)

**Support Markdown** : `Jour1_15h10-17h00_Syntaxe_CSharp12.md`

**Fichiers Code** :
- `DataGuard.Domain/` avec syntaxe C# 12
- `DataGuard.Tests/Unit/` avec tests xUnit
- `ValidFlow.Solutions/` (code complet C# 12 + tests)

---

## Jour 2 — Maîtriser l'Accès aux Données et l'Injection de Dépendances

### 09h00 - 10h30 (1h30) | Mise en place de l'Injection de Dépendances (DI)

**Branche Git** : `jour2-09h00-start` → `jour2-10h30-di-setup`

**Objectifs** :
- Comprendre l'Injection de Dépendances
- Configurer le Generic Host (.NET 8)
- Enregistrer les services dans IServiceCollection
- Créer le Composition Root

**Contenu Théorique** :
1. Problèmes du couplage fort (mot-clé `new`)
2. Pattern Dependency Injection
   - Constructor Injection (recommandé)
   - Property Injection (à éviter)
   - Method Injection (cas spécifiques)
3. Composition Root
   - Point unique de configuration DI
   - Dans Program.cs du projet Console
4. Durées de vie
   - Transient (nouvelle instance à chaque fois)
   - Scoped (par requête/scope)
   - Singleton (instance unique)

**Démonstration** : Configuration DI complète dans Console

**Livrable** : Application qui démarre avec DI fonctionnelle

---

### 10h40 - 12h30 (2h00) | Abstraction de l'ancien code SqlConnection

**Branche Git** : `jour2-10h30-di-setup` → `jour2-12h30-abstraction-ado`

**Objectifs** :
- Analyser les problèmes ADO.NET Legacy
- Créer l'interface IDataRepository
- Implémenter un Mock temporaire
- Valider l'exécution découplée

**Livrable** : Repository Pattern implémenté avec Mock

---

### 13h30 - 15h00 (1h30) | Migration vers EF Core 8 (Part 1)

**Branche Git** : `jour2-12h30-abstraction-ado` → `jour2-15h00-efcore-part1`

**Objectifs** :
- Installer EF Core packages
- Créer le DbContext
- Configurer les entités (Code First)
- Générer la migration initiale

**Livrable** : DbContext configuré + migration créée

---

### 15h10 - 17h00 (2h00) | Migration vers EF Core 8 (Part 2)

**Branche Git** : `jour2-15h00-efcore-part1` → `jour2-17h00-end`

**Objectifs** :
- Implémenter EfDataRepository
- Remplacer Mock par implémentation EF Core
- Optimiser les requêtes (AsNoTracking, projections)
- Tester l'exécution complète

**Livrable** : Application lit les données via EF Core async

---

## Jour 3 — Sécuriser la Configuration et les Services

### 09h00 - 10h30 (1h30) | Externalisation de la configuration

**Branche Git** : `jour3-09h00-start` → `jour3-10h30-appsettings`

**Objectifs** :
- Créer appsettings.json
- Configurer IOptions<T>
- Lire la configuration typée
- Supprimer les hardcoded values

**Livrable** : Configuration externalisée

---

### 10h40 - 12h30 (2h00) | Mise en place du .NET Secret Manager

**Branche Git** : `jour3-10h30-appsettings` → `jour3-12h30-secret-manager`

**Objectifs** :
- Comprendre les risques Git
- Initialiser Secret Manager
- Stocker credentials sensibles
- Valider sécurité

**Livrable** : Zéro credential dans le code

---

### 13h30 - 15h00 (1h30) | Robustesse : Logs et Retry Policy

**Branche Git** : `jour3-12h30-secret-manager` → `jour3-15h00-logs-retry`

**Objectifs** :
- Ajouter ILogger<T>
- Implémenter Polly pour retry
- Gérer les exceptions proprement
- Tracer l'exécution

**Livrable** : Application robuste avec logs

---

### 15h10 - 17h00 (2h00) | Remplacement de SmtpClient par MailKit

**Branche Git** : `jour3-15h00-logs-retry` → `jour3-17h00-end`

**Objectifs** :
- Analyser obsolescence SmtpClient
- Installer MailKit
- Créer IEmailService
- Tester avec smtp4dev

**Livrable** : Emails envoyés via MailKit

---

## Jour 4 — Tests et Conteneurisation

### 09h00 - 10h30 (1h30) | Tests Unitaires (xUnit)

**Branche Git** : `jour4-09h00-start` → `jour4-10h30-tests-unitaires`

**Livrable** : Suite de tests unitaires au vert

---

### 10h40 - 12h30 (2h00) | Mocking et isolation

**Branche Git** : `jour4-10h30-tests-unitaires` → `jour4-12h30-mocking`

**Livrable** : Orchestrateur testé avec mocks

---

### 13h30 - 15h00 (1h30) | Tests d'Intégration

**Branche Git** : `jour4-12h30-mocking` → `jour4-15h00-tests-integration`

**Livrable** : Tests EF Core avec base réelle

---

### 15h10 - 17h00 (2h00) | Conteneurisation Docker

**Branche Git** : `jour4-15h00-tests-integration` → `jour4-17h00-end`

**Livrable** : Application Dockerisée

---

## Jour 5 — Finalisation et Documentation

### 09h00 - 10h30 (1h30) | Documentation technique

**Branche Git** : `jour5-09h00-start` → `jour5-10h30-documentation`

**Livrable** : README complet

---

### 10h40 - 12h30 (2h00) | Revue de code et analyse statique

**Branche Git** : `jour5-10h30-documentation` → `jour5-12h30-code-review`

**Livrable** : Codebase propre (zéro warning)

---

### 13h30 - 15h00 (1h30) | Bilan AS-IS vs TO-BE

**Branche Git** : `jour5-12h30-code-review` → `jour5-15h00-bilan`

**Livrable** : Document de synthèse

---

### 15h10 - 17h00 (2h00) | Prochaines étapes CI/CD

**Branche Git** : `jour5-15h00-bilan` → `jour5-17h00-end`

**Livrable** : Pipeline CI/CD conceptuel

---

## Validation et Contrôle Qualité

### Checklist par Tranche Horaire

Avant de valider une tranche et passer à la suivante :

- [ ] **Support Markdown** rédigé (Théorie + Démonstration + Atelier)
- [ ] **Code DataGuard** avec commentaires détaillés
- [ ] **Code ValidFlow** pour atelier (énoncé clair)
- [ ] **Solution ValidFlow** documentée
- [ ] **Branche Git** créée et taguée
- [ ] **Compilation** réussie (`dotnet build`)
- [ ] **Tests** au vert si applicable (`dotnet test`)
- [ ] **Livrable** visible et mesurable

### Points de Synchronisation

À chaque pause (10h30, 12h30, 15h00, 17h00) :
- ✅ Vérification des livrables avec les apprenants
- ✅ Questions/réponses sur la tranche écoulée
- ✅ Commit + push de la branche horaire
- ✅ Validation formateur avant de continuer

---

**Document créé le** : 6 mars 2026  
**Version** : 1.0  
**Auteur** : Expert Architecte Logiciel .NET (BMAD Method)  
**Alignement** : 100% conforme au programme client
