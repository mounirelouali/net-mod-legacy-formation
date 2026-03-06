# Architecture et Pédagogie - Formation Modernisation .NET

**Formation** : Modernisation d'une Application de .NET Framework à .NET 8  
**Durée** : 5 jours  
**Public** : Développeurs .NET Framework cherchant à migrer vers .NET 8  
**Date de création** : 6 mars 2026

---

## 1. Contexte et Objectifs de la Formation

### 1.1 Contexte Métier

Cette formation répond à un besoin critique : **moderniser des applications .NET Framework 4.8 vers .NET 8** pour bénéficier de :
- **Performance** : Jusqu'à 10x plus rapide sur certaines opérations
- **Sécurité** : Support actif, patches de sécurité, TLS 1.3
- **Multi-plateforme** : Déploiement Linux/Docker/Kubernetes
- **Maintenabilité** : Syntaxe C# 12, DI native, architecture moderne
- **Coûts** : Réduction des coûts d'infrastructure (cloud-native)

### 1.2 Objectifs Pédagogiques

À l'issue de la formation, les apprenants seront capables de :

1. **Analyser** une application Legacy et identifier les points de modernisation
2. **Restructurer** une solution en 5 projets .NET 8 modulaires et testables
3. **Implémenter** l'Injection de Dépendances avec `IServiceCollection` et Composition Root
4. **Migrer** de ADO.NET vers Entity Framework Core 8
5. **Sécuriser** la configuration avec Secret Manager et `IOptions<T>`
6. **Moderniser** les services (MailKit, async/await, Polly)
7. **Tester** avec xUnit et tests d'intégration
8. **Conteneuriser** avec Docker pour déploiement multi-plateforme

---

## 2. Les Deux Projets Fil Rouges

### 2.1 Projet Démo : **DataGuard** (Le Formateur)

**Domaine** : Système de validation et génération de fichiers XML à partir de données SQL

**Architecture Legacy (Point de départ)** :
- **Framework** : .NET Framework 4.8
- **Accès données** : ADO.NET avec `SqlConnection` directe
- **Configuration** : Hardcodée dans le code (⚠️ sécurité)
- **Validation** : Système de règles par réflexion (synchrone)
- **Email** : `System.Net.Mail.SmtpClient` (obsolète)
- **Tests** : Aucun test unitaire
- **Déploiement** : Windows uniquement

**Problèmes identifiés** (source NotebookLM) :
1. ⚠️ **Sécurité** : ConnectionString et credentials SMTP en dur
2. 🐌 **Performance** : Tout synchrone, pas de pooling, pas de parallélisation
3. 💥 **Robustesse** : Aucune gestion d'erreurs, pas de retry logic
4. 🔧 **Maintenabilité** : Pas de DI, couplage fort, pas de tests
5. 📦 **Déploiement** : Dépendant de Windows, pas conteneurisable

**Architecture Cible (Objectif Jour 5)** :
- **Framework** : .NET 8.0
- **Accès données** : Entity Framework Core 8 avec Code First
- **Configuration** : `appsettings.json` + Secret Manager + `IOptions<T>`
- **Validation** : Système de règles avec DI et async
- **Email** : MailKit (moderne, cross-platform)
- **Tests** : xUnit + tests d'intégration EF Core
- **Déploiement** : Docker Linux-compatible

### 2.2 Projet Atelier : **ValidFlow** (Les Apprenants)

**Domaine** : Système de validation et export de données clients vers fichiers structurés

**Différences avec DataGuard** :
- **Données** : Gestion de clients/commandes au lieu de tags XML génériques
- **Règles** : Validation d'emails, téléphones, adresses au lieu de caractères
- **Export** : JSON + CSV en plus du XML
- **Complexité** : Légèrement plus simple pour permettre l'autonomie

**Architecture Legacy identique** :
- Même stack .NET Framework 4.8
- Même problèmes (sécurité, performance, robustesse)
- Même objectif de modernisation vers .NET 8

---

## 3. Architecture Technique de la Solution Modernisée

### 3.1 Structure de la Solution (.NET 8)

Chaque projet (DataGuard et ValidFlow) sera structuré en **5 projets** :

```
DataGuard.sln
│
├── DataGuard.Domain/                     # Couche Métier (Domain)
│   ├── Models/                           # Entités EF Core
│   ├── Interfaces/                       # Contrats (IRule, IRepository, etc.)
│   └── Validators/                       # Implémentation des règles
│
├── DataGuard.Infrastructure/             # Couche Infrastructure
│   ├── Data/                             # DbContext EF Core
│   ├── Repositories/                     # Implémentation IRepository
│   └── Services/                         # EmailService (MailKit)
│
├── DataGuard.Application.Services/       # Couche Application (Orchestration)
│   ├── Orchestrators/                    # ValidationOrchestrator
│   ├── Services/                         # XmlGenerationService
│   └── Interfaces/                       # Contrats applicatifs
│
├── DataGuard.Application.Console/        # Point d'entrée "Humble Object"
│   ├── Program.cs                        # Composition Root uniquement
│   ├── Worker.cs                         # BackgroundService
│   ├── appsettings.json                  # Configuration
│   └── Configuration/                    # Options classes
│
└── DataGuard.Tests/                      # Tests xUnit
    ├── Unit/                             # Tests unitaires (règles)
    ├── Integration/                      # Tests d'intégration (EF Core)
    └── Application/                      # Tests orchestrateurs
```

**Justification de l'architecture** (source : Software Architecture with C# 12, Dependency Injection in .NET, Modernizing .NET) :
- **Pattern "Humble Object"** : Le projet Console est minimal, contient uniquement le Composition Root (DI + configuration)
- **Séparation Orchestration/Livraison** : Application.Services contient la logique d'orchestration réutilisable, Console n'est qu'un point d'entrée
- **Testabilité maximale** : Application.Services est testable sans lancer le processus Console complet
- **Réutilisabilité** : Application.Services peut être référencé par une API Web, Blazor ou Azure Function sans code dupliqué
- **Maintenabilité** : Chaque couche a un rôle unique (SOLID)
- **Évolutivité** : Facile d'ajouter de nouveaux points d'entrée sans toucher à la logique métier

### 3.2 Stack Technique Cible

| Composant | Legacy (.NET Framework 4.8) | Moderne (.NET 8) |
|-----------|----------------------------|-------------------|
| **Framework** | .NET Framework 4.8 | .NET 8.0 |
| **Langage** | C# 7.3 | C# 12 (file-scoped, records, pattern matching) |
| **Accès données** | ADO.NET (`SqlConnection`) | Entity Framework Core 8 (Code First) |
| **DI** | ❌ Aucune | ✅ `Microsoft.Extensions.DependencyInjection` |
| **Configuration** | Hardcodée | `appsettings.json` + Secret Manager + `IOptions<T>` |
| **Email** | `SmtpClient` (obsolète) | MailKit + `IEmailService` |
| **Async** | ❌ Tout synchrone | ✅ `async/await` partout |
| **Tests** | ❌ Aucun | xUnit + FluentAssertions + Moq |
| **Logs** | ❌ Aucun | `ILogger<T>` + Serilog |
| **Conteneurs** | ❌ Windows uniquement | ✅ Docker (Linux Alpine) |
| **CI/CD** | ❌ Manuel | GitHub Actions (optionnel Jour 5) |

### 3.3 Patterns et Principes Appliqués

**Patterns (source : Dependency Injection in .NET + Software Architecture)** :
1. **Repository Pattern** : Abstraction de l'accès aux données
2. **Dependency Injection** : Constructor Injection + Composition Root
3. **Options Pattern** : Configuration fortement typée avec `IOptions<T>`
4. **Strategy Pattern** : Système de règles de validation interchangeables
5. **Factory Pattern** : Création de validateurs via DI

**Principes SOLID** :
- **S** : Chaque classe a une responsabilité unique
- **O** : Ouvert à l'extension (nouvelles règles), fermé à la modification
- **L** : Les implémentations respectent leurs contrats
- **I** : Interfaces fines et ciblées (`IRule`, `IRepository`, `IEmailService`)
- **D** : Dépendance aux abstractions, pas aux implémentations concrètes

---

## 4. Stratégie Pédagogique

### 4.1 Approche "Show & Practice"

Chaque jour suit le **flux pédagogique obligatoire** :

```
┌─────────────┐
│  1. THÉORIE │ ← Concepts sourcés via NotebookLM (livres + analyse Legacy)
└──────┬──────┘
       │
       ▼
┌──────────────────┐
│ 2. DÉMONSTRATION │ ← Étapes hyper détaillées sur **DataGuard** (antisèche)
└──────┬───────────┘
       │
       ▼
┌──────────────────────┐
│ 3. ATELIER PRATIQUE  │ ← Consignes claires pour **ValidFlow** (autonomie)
└──────────────────────┘
```

### 4.2 Exemple de Flux (Jour 2 - Migration EF Core)

**1. THÉORIE (30 min)** :
- Pourquoi EF Core 8 vs ADO.NET ? (performance, maintenabilité, LINQ)
- Code First vs Database First
- DbContext, DbSet, Migrations
- Best practices (AsNoTracking, projections, N+1 queries)

**2. DÉMONSTRATION (1h30)** :
```csharp
// Étape 1 : Installation du package
// dotnet add package Microsoft.EntityFrameworkCore.SqlServer

// Étape 2 : Création de l'entité
public class XmlData
{
    public int Id { get; set; }
    public string Tag { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}

// Étape 3 : Création du DbContext
public class DataGuardContext : DbContext
{
    public DataGuardContext(DbContextOptions<DataGuardContext> options) 
        : base(options) { }
    
    public DbSet<XmlData> XmlDataSet => Set<XmlData>();
}

// Étape 4 : Enregistrement dans Program.cs
builder.Services.AddDbContext<DataGuardContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Étape 5 : Migration
// dotnet ef migrations add InitialCreate
// dotnet ef database update
```

**3. ATELIER PRATIQUE (1h)** :
> **Consigne** : Appliquez la même démarche sur **ValidFlow** pour migrer vos entités `Client` et `Commande` vers EF Core 8.  
> - Créez `ValidFlowContext` avec les 2 DbSet
> - Configurez la connexion SQL dans `appsettings.json`
> - Exécutez la migration initiale
> - **Bonus** : Ajoutez une relation `Client` → `Commandes` (1-N)

### 4.3 Supports de Cours (Markdown)

Chaque jour dispose de son document Markdown structuré :

```
Jour1_Fondations_Architecture_Moderne.md
├── 1. Théorie
│   ├── 1.1 Pourquoi migrer vers .NET 8 ?
│   ├── 1.2 Architecture en couches
│   ├── 1.3 Nouveautés C# 12
│   └── 1.4 Analyse du code Legacy
├── 2. Démonstration (DataGuard)
│   ├── 2.1 Création de la solution 4 projets
│   ├── 2.2 Implémentation du Domain
│   ├── 2.3 Moteur de validation testable
│   └── 2.4 Syntaxe C# 12 (file-scoped, records)
└── 3. Atelier Pratique (ValidFlow)
    ├── 3.1 Consignes étape par étape
    ├── 3.2 Points de vigilance
    └── 3.3 Solution attendue (structure)
```

---

## 5. Validation de l'Apprentissage

### 5.1 Évaluation Continue

**Par jour** :
- ✅ Code review de l'atelier pratique (feedback immédiat)
- ✅ Questions/réponses sur la théorie
- ✅ Tests unitaires obligatoires pour valider l'implémentation

**Fin de formation** :
- 📝 QCM de validation (20 questions)
- 💻 Projet ValidFlow complet et fonctionnel
- 📊 Présentation AS-IS vs TO-BE (performance, sécurité, maintenabilité)

### 5.2 Livrables Attendus

**Pour le formateur (DataGuard)** :
- ✅ Code source complet avec branches Git atomiques
- ✅ Support de cours Markdown pour chaque jour
- ✅ Dockerfile fonctionnel
- ✅ Documentation technique (README)

**Pour les apprenants (ValidFlow)** :
- ✅ Solution .NET 8 en 4 projets
- ✅ Tests xUnit avec couverture > 80%
- ✅ Configuration sécurisée (Secret Manager)
- ✅ Application conteneurisée (Docker)
- ✅ README technique

---

## 6. Prérequis et Environnement

### 6.1 Prérequis Participants

**Connaissances** :
- ✅ C# et .NET Framework (niveau intermédiaire)
- ✅ Bases SQL (SELECT, INSERT, UPDATE)
- ✅ Notions de POO (classes, interfaces, héritage)
- ⚠️ Git de base (clone, commit, branch) - sera revu

**Environnement** :
- Windows 10/11 ou macOS/Linux
- Visual Studio 2022 ou VS Code + C# DevKit
- .NET SDK 8.0 (dernière version stable)
- SQL Server Express ou Docker SQL Server
- Git installé
- Docker Desktop (Jour 4)

### 6.2 Setup Formateur

**Matériel** :
- Notebook avec .NET 8 + Visual Studio 2022
- Deuxième écran pour le code (présenté aux apprenants)
- Connexion Internet stable (packages NuGet, Docker Hub)

**Repositories Git** :
- `dataguard-legacy` (branche `main`) : Code .NET Framework 4.8 de départ
- `dataguard-modern` (branches `jour1-start` à `jour5-end`) : Évolution vers .NET 8
- `validflow-starter` : Code de départ pour les apprenants
- `validflow-solutions` : Solutions des ateliers pratiques

---

## 7. Sources et Références

Cette architecture et cette pédagogie sont basées sur :

### 7.1 Analyse du Code Legacy
- ✅ **NotebookLM** : Analyse complète du projet `generationxml` (.NET Framework 4.8)
- ✅ Identification des 5 problèmes majeurs (sécurité, performance, robustesse, maintenabilité, déploiement)

### 7.2 Livres de Référence (NotebookLM)
1. **Modernizing .NET** - W. Herceg
   - Stratégies de migration (Side-by-Side, In-Place)
   - Remplacement WCF, Web Forms, EF6
2. **Software Architecture with C# 12 and .NET 8** - Gabriel Baptista
   - Architectures modernes (Microservices, DDD, Cloud-Native)
   - Patterns de résilience (Retry, Circuit-Breaker, CQRS)
3. **Dependency Injection in .NET** - Mark Seemann
   - Constructor Injection, Composition Root
   - Durées de vie (Transient, Scoped, Singleton)
4. **Programming C# 12** - Ian Griffiths
   - Nouveautés C# 12 (file-scoped, records, pattern matching)
5. **Working Effectively with Legacy Code** - Michael Feathers
   - Stratégies de refactoring sécurisé
   - Ajout de tests sur code existant

### 7.3 Documentation Officielle
- Microsoft Learn : .NET 8 Migration Guide
- Entity Framework Core 8 Documentation
- ASP.NET Core Security Best Practices

---

## 8. Roadmap et Évolutions Possibles

### 8.1 Extensions Possibles (Après Jour 5)

**Jour 6 (Optionnel)** : API REST avec ASP.NET Core 8
- Transformation de DataGuard en API Web
- Swagger/OpenAPI
- JWT Authentication

**Jour 7 (Optionnel)** : Blazor Server/WASM
- Interface web pour ValidFlow
- SignalR pour notifications temps réel

**Jour 8 (Optionnel)** : CI/CD et Production
- GitHub Actions
- Azure App Service ou Kubernetes
- Monitoring avec Application Insights

### 8.2 Adaptations Possibles

Cette formation peut être adaptée pour :
- ✅ **Migration WCF → gRPC** (si contexte microservices)
- ✅ **Migration Web Forms → Blazor** (si application web)
- ✅ **Cloud Azure/AWS** (si contexte cloud-native)
- ✅ **DDD avancé** (si système complexe multi-contextes)

---

## 9. Conclusion

Cette architecture pédagogique garantit :

1. ✅ **Apprentissage progressif** : Du simple (architecture) au complexe (Docker)
2. ✅ **Pratique intensive** : 60% du temps en code (démonstration + atelier)
3. ✅ **Traçabilité complète** : Branches Git atomiques pour chaque étape
4. ✅ **Réutilisabilité** : Les apprenants repartent avec 2 projets complets
5. ✅ **Standards professionnels** : Architecture moderne, tests, conteneurisation

**Validation Architecture** : ✅ APPROUVÉE  
**Prêt pour** : Rédaction du Plan Git Sprints (02_Plan_Git_Sprints.md)

---

**Document créé le** : 6 mars 2026  
**Version** : 1.0  
**Auteur** : Expert Architecte Logiciel .NET (BMAD Method)
