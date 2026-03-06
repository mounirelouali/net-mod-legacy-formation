# JOUR 1 - 09h00-10h30 : Analyse du Batch Legacy et Stratégie de Migration

**Formation** : Modernisation d'une Application de .NET Framework à .NET 8  
**Durée** : 1h30  
**Projet Démo** : DataGuard  
**Projet Atelier** : ValidFlow  
**Branche Git** : `jour1-09h00-start` → `jour1-10h30-analyse-legacy`

---

## 📚 1. THÉORIE (30 minutes)

### 1.1 Pourquoi Migrer vers .NET 8 ?

#### Contexte Historique

**.NET Framework 4.8** (2019) est la **dernière version** du .NET Framework classique :
- ⚠️ **Fin de support actif** : Maintenance uniquement, pas de nouvelles fonctionnalités
- 🪟 **Windows uniquement** : Impossible de déployer sur Linux ou macOS
- 🐌 **Performance limitée** : Basé sur un runtime vieux de 20 ans
- 💸 **Coûts élevés** : Nécessite des serveurs Windows (licences IIS, Windows Server)

**.NET 8** (2023) est la version **moderne, unifiée et open-source** :
- ✅ **Support Long-Term (LTS)** : Support Microsoft jusqu'en novembre 2026
- 🌍 **Multi-plateforme** : Windows, Linux, macOS, Docker, Kubernetes
- 🚀 **Performance exceptionnelle** : 10x plus rapide sur certaines opérations
- 💰 **Réduction des coûts** : Conteneurs Linux légers, cloud-native
- 🔐 **Sécurité renforcée** : TLS 1.3, patches réguliers, cryptographie moderne

#### Gains Concrets Mesurables

| Métrique | .NET Framework 4.8 | .NET 8 | Gain |
|----------|-------------------|---------|------|
| **Temps de traitement** | 500ms (1000 records) | 50ms | **-90%** |
| **Mémoire utilisée** | 150 MB | 30 MB | **-80%** |
| **Démarrage application** | 2000ms | 200ms | **-90%** |
| **Coût serveur/mois** | 150€ (Windows Server) | 20€ (Linux container) | **-87%** |
| **Déploiement** | 15 min (manuel) | 30s (CI/CD Docker) | **-97%** |

**Source** : Benchmarks officiels Microsoft (.NET 8 Performance Improvements)

---

### 1.2 Les 5 Problèmes Majeurs du Code Legacy

Nous allons analyser un **batch de validation XML** typique en .NET Framework 4.8 et identifier ses faiblesses critiques.

#### Problème #1 : ⚠️ SÉCURITÉ - Credentials Hardcodés

**Symptôme** : ConnectionString et mots de passe écrits en dur dans le code source.

```csharp
// ⚠️ PROBLÈME : Credentials en clair dans le code
string connectionString = "Server=prod-db;User=sa;Password=SuperSecret123!";
var smtp = new SmtpClient("smtp.gmail.com")
{
    Credentials = new NetworkCredential("admin@company.com", "MyP@ssw0rd")
};
```

**Conséquences** :
- 🔓 Exposition des secrets si le code est committé sur Git
- 🔓 Impossible de changer les mots de passe sans recompiler
- 🔓 Violation des normes de sécurité (ISO 27001, SOC 2)
- 🔓 Risque de fuite en cas de décompilation du DLL

**Solution .NET 8** :
- `appsettings.json` pour configuration non sensible
- `.NET Secret Manager` pour développement local
- `Azure Key Vault` ou `Environment Variables` pour production

---

#### Problème #2 : 🐌 PERFORMANCE - Code Synchrone Bloquant

**Symptôme** : Toutes les opérations I/O (base de données, réseau) sont synchrones.

```csharp
// 🐌 PROBLÈME : Appel bloquant - le thread est gelé pendant l'attente
var data = GetDataFromDb(connectionString); // Bloque 200ms
SendEmail(to, subject, body);               // Bloque 500ms
```

**Conséquences** :
- Le thread est **totalement bloqué** pendant les I/O (CPU idle à 2%)
- Impossible de traiter plusieurs batches en parallèle
- Scalabilité limitée (1 thread = 1 batch à la fois)
- Waste de ressources serveur

**Solution .NET 8** :
```csharp
// ✅ SOLUTION : Async/await libère le thread pendant les I/O
var data = await GetDataFromDbAsync(connectionString); // Thread libre
await SendEmailAsync(to, subject, body);               // Thread libre
```

**Gain** : Le même serveur peut traiter **100x plus de batches** simultanément.

---

#### Problème #3 : 💥 ROBUSTESSE - Aucune Gestion d'Erreurs

**Symptôme** : Pas de `try-catch`, pas de retry logic, pas de logs.

```csharp
// 💥 PROBLÈME : Si la DB est down, l'application crash
var conn = new SqlConnection(connectionString);
conn.Open(); // ❌ Exception non gérée si DB indisponible

// 💥 PROBLÈME : Si le serveur SMTP refuse, l'application crash
smtp.Send(message); // ❌ Exception non gérée si SMTP timeout
```

**Conséquences** :
- L'application **crash totalement** si la DB a un micro-coupure
- Pas de retry automatique (une coupure de 100ms = échec total)
- Impossible de diagnostiquer les problèmes (pas de logs)
- Les utilisateurs ne savent pas pourquoi ça a planté

**Solution .NET 8** :
```csharp
// ✅ SOLUTION : Gestion d'erreurs + Retry Policy (Polly)
try
{
    var data = await _repository.GetDataAsync();
}
catch (SqlException ex) when (ex.IsTransient)
{
    _logger.LogWarning("DB temporarily unavailable, retrying...");
    await Policy.Handle<SqlException>()
                .WaitAndRetryAsync(3, retryAttempt => 
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .ExecuteAsync(() => _repository.GetDataAsync());
}
catch (Exception ex)
{
    _logger.LogError(ex, "Fatal error during data retrieval");
    throw; // Re-throw après logging
}
```

---

#### Problème #4 : 🔧 MAINTENABILITÉ - Couplage Fort et Absence de Tests

**Symptôme** : Instanciation directe avec `new`, impossible de tester.

```csharp
// 🔧 PROBLÈME : Couplage fort - impossible de mocker
public void ProcessData()
{
    var smtp = new SmtpClient(); // Instanciation directe
    smtp.Send(message);          // Impossible de tester sans vraie connexion SMTP
    
    var conn = new SqlConnection(connString); // Couplage à SQL Server
    // Impossible de tester sans vraie base de données
}
```

**Conséquences** :
- **Zéro test unitaire possible** (nécessite vraie DB + vraie SMTP)
- Modification d'une classe = risque de casser toute l'application
- Impossible de remplacer SqlConnection par un autre provider
- Code non évolutif (ajouter une fonctionnalité = risque élevé)

**Solution .NET 8** (Dependency Injection) :
```csharp
// ✅ SOLUTION : Injection de dépendances via interfaces
public class DataProcessor
{
    private readonly IDataRepository _repository;
    private readonly IEmailService _emailService;
    
    // Constructor Injection - dépendances passées de l'extérieur
    public DataProcessor(IDataRepository repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }
    
    public async Task ProcessDataAsync()
    {
        var data = await _repository.GetDataAsync(); // Interface testable
        await _emailService.SendAsync(message);       // Interface testable
    }
}

// Tests unitaires possibles avec mocks
[Fact]
public async Task ProcessData_WhenNoData_SendsNoEmail()
{
    var mockRepo = new Mock<IDataRepository>();
    mockRepo.Setup(r => r.GetDataAsync()).ReturnsAsync(new List<Data>());
    
    var mockEmail = new Mock<IEmailService>();
    
    var processor = new DataProcessor(mockRepo.Object, mockEmail.Object);
    await processor.ProcessDataAsync();
    
    mockEmail.Verify(e => e.SendAsync(It.IsAny<Message>()), Times.Never);
}
```

---

#### Problème #5 : 📦 DÉPLOIEMENT - Windows Uniquement

**Symptôme** : L'application nécessite Windows Server + .NET Framework installé.

**Conséquences** :
- 💸 **Coûts élevés** : Licence Windows Server (~800€/an)
- 🐌 **Serveurs lourds** : Windows Server = 4 GB RAM minimum
- 🚫 **Pas de conteneurisation** : Impossible de packager en Docker Linux
- 🚫 **Pas de Kubernetes** : Impossible d'orchestrer dans un cluster moderne
- ⏱️ **Déploiement manuel** : RDP sur le serveur, copier DLL, redémarrer service

**Solution .NET 8** (Docker + Linux) :
```dockerfile
# Conteneur Linux ultra-léger (100 MB vs 4 GB Windows)
FROM mcr.microsoft.com/dotnet/runtime:8.0-alpine
COPY ./publish /app
ENTRYPOINT ["dotnet", "/app/DataGuard.dll"]
```

**Gains** :
- ✅ Image Docker de **100 MB** (vs 4 GB Windows Server)
- ✅ Déploiement en **30 secondes** via CI/CD
- ✅ Compatible **Kubernetes**, AWS ECS, Azure Container Apps
- ✅ Coût serveur réduit de **87%** (Linux containers)

---

### 1.3 Le Concept de "Seam" (Couture) pour le Découplage

**Définition** (Michael Feathers, *Working Effectively with Legacy Code*) :

> Une **Seam** (couture) est un endroit dans le code où vous pouvez modifier le comportement sans éditer le code à cet endroit.

#### Exemple Concret

**Code Legacy (sans seam)** :
```csharp
public void SendNotification()
{
    var smtp = new SmtpClient(); // ❌ Couplage direct, pas de seam
    smtp.Send(message);
}

// Impossible de tester sans vraie connexion SMTP
// Impossible de changer le provider (MailKit, SendGrid, etc.)
```

**Code Moderne (avec seam)** :
```csharp
public interface IEmailService // ✅ SEAM créée
{
    Task SendAsync(EmailMessage message);
}

public class DataProcessor
{
    private readonly IEmailService _emailService;
    
    public DataProcessor(IEmailService emailService) // Injection
    {
        _emailService = emailService; // Dépendance abstraite
    }
    
    public async Task SendNotificationAsync()
    {
        await _emailService.SendAsync(message); // Appel via l'interface
    }
}
```

**Avantages de la seam** :
- ✅ Tests unitaires : `new DataProcessor(new MockEmailService())`
- ✅ Changement de provider : `new SmtpEmailService()` ou `new SendGridEmailService()`
- ✅ Pas besoin de modifier `DataProcessor` pour changer le comportement

#### Les 3 Seams à Créer dans Notre Legacy

1. **IDataRepository** : Abstraction de l'accès aux données (SqlConnection → EF Core)
2. **IEmailService** : Abstraction de l'envoi d'emails (SmtpClient → MailKit)
3. **IValidator** : Abstraction de la validation métier (règles configurables)

---

### 1.4 Architecture TO-BE : 5 Projets pour le Découplage Total

#### Schéma de l'Architecture Cible

```
┌─────────────────────────────────────────────────────────────────┐
│                     DataGuard.sln                               │
└─────────────────────────────────────────────────────────────────┘
                              │
        ┌─────────────────────┼─────────────────────┐
        │                     │                     │
        ▼                     ▼                     ▼
┌───────────────┐    ┌────────────────┐    ┌──────────────────┐
│   1. DOMAIN   │    │2. INFRASTRUCTURE│    │ 3. APPLICATION   │
│               │    │                 │    │    .SERVICES     │
│ - Models      │◄───│ - EF Core       │◄───│ - Orchestrators  │
│ - Interfaces  │    │ - Repositories  │    │ - XmlGenerator   │
│ - Validators  │    │ - EmailService  │    │                  │
└───────────────┘    └────────────────┘    └──────────────────┘
                                                    ▲
                                                    │
                                            ┌───────┴────────┐
                                            │4. APPLICATION  │
                                            │   .CONSOLE     │
                                            │ - Program.cs   │
                                            │ - Worker.cs    │
                                            └────────────────┘

┌────────────────────────────────────────────────────────────────┐
│                      5. TESTS                                  │
│  - Unit Tests (Domain isolé)                                   │
│  - Integration Tests (Infrastructure + Domain)                 │
│  - Application Tests (Orchestrateurs)                          │
└────────────────────────────────────────────────────────────────┘
```

#### Responsabilités de Chaque Projet

##### 1. DataGuard.Domain (Logique Métier Pure)

**Rôle** : Contient la logique métier sans AUCUNE dépendance externe.

**Contenu** :
- `Models/` : Entités métier (`XmlData`, `ValidationResult`)
- `Interfaces/` : Contrats (`IRule`, `IDataRepository`, `IEmailService`)
- `Validators/` : Règles métier (`MandatoryRule`, `MinLengthRule`, etc.)

**Règles strictes** :
- ❌ AUCUNE référence à `System.Data`, `System.Net`, `Microsoft.EntityFrameworkCore`
- ❌ AUCUNE dépendance externe (packages NuGet)
- ✅ Seulement `System` de base (String, Int, List, etc.)
- ✅ 100% testable sans infrastructure

**Pourquoi** : Le Domain est le **cœur de l'application**. Il doit être totalement isolé pour garantir que la logique métier ne soit jamais corrompue par des détails techniques.

---

##### 2. DataGuard.Infrastructure (Détails Techniques)

**Rôle** : Implémente les contrats du Domain avec des technologies concrètes.

**Contenu** :
- `Data/DataGuardContext.cs` : DbContext EF Core
- `Repositories/EfDataRepository.cs` : Implémentation de `IDataRepository`
- `Services/MailKitEmailService.cs` : Implémentation de `IEmailService`

**Dépendances autorisées** :
- ✅ Référence au projet `Domain` (pour implémenter les interfaces)
- ✅ Packages NuGet techniques (`Microsoft.EntityFrameworkCore.SqlServer`, `MailKit`)

**Pourquoi** : Si demain on veut passer de SQL Server à PostgreSQL, on change UNIQUEMENT ce projet. Le Domain ne change pas.

---

##### 3. DataGuard.Application.Services (Orchestration Réutilisable)

**Rôle** : Coordonne les appels entre Domain et Infrastructure pour exécuter les cas d'usage métier.

**Contenu** :
- `Orchestrators/ValidationOrchestrator.cs` : Logique principale du batch
  ```csharp
  public class ValidationOrchestrator
  {
      public async Task ExecuteAsync()
      {
          var data = await _repository.GetDataAsync();      // Infrastructure
          var results = _validator.Validate(data);          // Domain
          var xml = _xmlGenerator.Generate(validData);      // Application
          await _emailService.SendReportAsync(invalidData); // Infrastructure
      }
  }
  ```
- `Services/XmlGenerationService.cs` : Service applicatif de génération XML

**Dépendances** :
- ✅ Référence `Domain` et `Infrastructure`
- ✅ Packages utilitaires (`System.Xml.Linq`, etc.)

**Pourquoi** : Ce projet est **réutilisable**. Si on crée une API Web, on référence juste ce projet et on appelle `ValidationOrchestrator`.

---

##### 4. DataGuard.Application.Console (Point d'Entrée "Humble Object")

**Rôle** : Point d'entrée minimal qui configure l'application et délègue tout le travail.

**Contenu** :
- `Program.cs` : **Composition Root** uniquement
  ```csharp
  var builder = Host.CreateApplicationBuilder(args);
  
  // Configuration de l'Injection de Dépendances
  builder.Services.AddDbContext<DataGuardContext>(options =>
      options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
  
  builder.Services.AddScoped<IDataRepository, EfDataRepository>();
  builder.Services.AddScoped<IEmailService, MailKitEmailService>();
  builder.Services.AddScoped<ValidationOrchestrator>();
  
  builder.Services.AddHostedService<Worker>(); // BackgroundService
  
  var host = builder.Build();
  await host.RunAsync();
  ```

- `Worker.cs` : BackgroundService qui lance l'orchestrateur
  ```csharp
  public class Worker : BackgroundService
  {
      private readonly ValidationOrchestrator _orchestrator;
      
      public Worker(ValidationOrchestrator orchestrator)
      {
          _orchestrator = orchestrator;
      }
      
      protected override async Task ExecuteAsync(CancellationToken stoppingToken)
      {
          await _orchestrator.ExecuteAsync(); // Délègue tout le travail
      }
  }
  ```

**Règles strictes** :
- ❌ AUCUNE logique métier ici
- ✅ Seulement configuration (DI, logs, settings)
- ✅ Maximum 50 lignes de code dans `Program.cs`

**Pourquoi** : Si on veut créer une API Web, on crée juste `DataGuard.Application.Api` avec un autre `Program.cs`. La logique reste dans `.Services`.

---

##### 5. DataGuard.Tests (Validation Automatisée)

**Rôle** : Garantir que le code fonctionne et ne régresse pas.

**Contenu** :
- `Unit/` : Tests du Domain isolé (règles de validation)
  ```csharp
  [Fact]
  public void MandatoryRule_WhenNull_ReturnsFalse()
  {
      var rule = new MandatoryRule();
      Assert.False(rule.IsValid(null));
  }
  ```

- `Integration/` : Tests avec vraie DB (EF Core)
  ```csharp
  [Fact]
  public async Task Repository_CanRetrieveData()
  {
      using var context = new DataGuardContext(_testDbOptions);
      var repo = new EfDataRepository(context);
      
      var data = await repo.GetAllDataAsync();
      
      Assert.NotEmpty(data);
  }
  ```

- `Application/` : Tests de l'orchestrateur avec mocks
  ```csharp
  [Fact]
  public async Task Orchestrator_WhenInvalidData_SendsEmail()
  {
      var mockRepo = new Mock<IDataRepository>();
      var mockEmail = new Mock<IEmailService>();
      
      var orchestrator = new ValidationOrchestrator(mockRepo.Object, mockEmail.Object);
      await orchestrator.ExecuteAsync();
      
      mockEmail.Verify(e => e.SendAsync(It.IsAny<Email>()), Times.Once);
  }
  ```

**Pourquoi** : Les tests sont la **garantie qualité**. Sans tests, on ne peut pas moderniser en toute confiance.

---

## 🖥️ 2. DÉMONSTRATION (45 minutes)

### Étape 1 : Cloner le Code Legacy

```bash
# Se placer dans le dossier de travail
cd d:\formations\dotnet-modernization\

# Cloner le projet DataGuard Legacy
git clone https://github.com/formation-dotnet/dataguard-legacy.git
cd dataguard-legacy

# Vérifier la branche
git branch
# * legacy

# Ouvrir dans Visual Studio
start DataGuard.Legacy.sln
```

---

### Étape 2 : Analyser Program.cs - Problème #1 (Sécurité)

**Ouvrez** : `DataGuard.Legacy/Program.cs`

**Lignes 12-13** : Credentials hardcodés

```csharp
// ============================================================
// ⚠️ PROBLÈME #1 : SÉCURITÉ - ConnectionString hardcodée
// ============================================================
// LIGNE 12 : ConnectionString SQL en clair avec password
string connectionString = "Server=prod-sql.company.local;Database=DataGuardDb;" +
                         "User Id=sa;Password=SuperSecret123!;TrustServerCertificate=True;";
// CONSÉQUENCE : Si ce code est sur Git, le mot de passe est exposé publiquement
// SOLUTION .NET 8 : Secret Manager + appsettings.json
```

**Lignes 68-72** : Credentials SMTP hardcodés

```csharp
// ============================================================
// ⚠️ PROBLÈME #1 : SÉCURITÉ - Credentials SMTP hardcodés
// ============================================================
// LIGNE 68-72 : Username et Password SMTP en clair
var smtpClient = new SmtpClient("smtp.gmail.com")
{
    Port = 587,
    Credentials = new NetworkCredential("admin@company.com", "MyP@ssw0rd123!"),
    EnableSsl = true
};
// CONSÉQUENCE : Violation ISO 27001, impossible d'auditer qui a le mot de passe
// SOLUTION .NET 8 : dotnet user-secrets set "Email:Password" "xxx"
```

**Annotation pour les apprenants** :

> 💡 **Point d'apprentissage** : Dans un code moderne .NET 8, **AUCUNE** valeur sensible ne doit être en dur. Toutes les données de configuration doivent provenir de `appsettings.json` (valeurs publiques) ou du Secret Manager / Key Vault (valeurs sensibles).

---

### Étape 3 : Analyser Program.cs - Problème #2 (Performance)

**Lignes 15-25** : Appel synchrone bloquant à la base de données

```csharp
// ============================================================
// 🐌 PROBLÈME #2 : PERFORMANCE - Appel synchrone bloquant
// ============================================================
// LIGNE 15-25 : GetDataFromDb est 100% synchrone
static Dictionary<string, string> GetDataFromDb(string connectionString)
{
    var data = new Dictionary<string, string>();
    
    using (var conn = new SqlConnection(connectionString))
    {
        conn.Open(); // ❌ BLOQUE le thread pendant 50-200ms
        
        using (var cmd = new SqlCommand("SELECT Tag, Value FROM XmlDataTable", conn))
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read()) // ❌ BLOQUE à chaque ligne
            {
                data[reader.GetString(0)] = reader.GetString(1);
            }
        }
    }
    
    return data;
}
// CONSÉQUENCE : Le thread est gelé pendant toute la requête SQL
// CPU idle à 2% pendant que le thread attend la réponse DB
// SOLUTION .NET 8 : async/await pour libérer le thread
```

**Version .NET 8 moderne (à montrer en comparaison)** :

```csharp
// ✅ SOLUTION .NET 8 : Version asynchrone
static async Task<Dictionary<string, string>> GetDataFromDbAsync(string connectionString)
{
    var data = new Dictionary<string, string>();
    
    await using (var conn = new SqlConnection(connectionString))
    {
        await conn.OpenAsync(); // ✅ Thread libéré pendant l'attente
        
        await using (var cmd = new SqlCommand("SELECT Tag, Value FROM XmlDataTable", conn))
        await using (var reader = await cmd.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync()) // ✅ Thread libéré à chaque ligne
            {
                data[reader.GetString(0)] = reader.GetString(1);
            }
        }
    }
    
    return data;
}
// GAIN : Le thread peut traiter d'autres batches pendant les I/O
// Scalabilité : 1 serveur peut traiter 100x plus de batches simultanément
```

**Annotation pour les apprenants** :

> 💡 **Point d'apprentissage** : Toute opération I/O (base de données, réseau, fichiers) doit être **asynchrone** en .NET 8. Règle : si ça attend quelque chose d'externe, utilisez `async/await`.

---

### Étape 4 : Analyser Program.cs - Problème #3 (Robustesse)

**Lignes 15-25** : Aucune gestion d'erreur sur l'accès DB

```csharp
// ============================================================
// 💥 PROBLÈME #3 : ROBUSTESSE - Aucune gestion d'erreurs
// ============================================================
// LIGNE 19 : conn.Open() sans try-catch
conn.Open(); // ❌ Si la DB est down, l'application crash totalement

// LIGNE 23 : cmd.ExecuteReader() sans try-catch
using (var reader = cmd.ExecuteReader()) // ❌ Si timeout SQL, exception non gérée

// CONSÉQUENCE : Une micro-coupure DB de 100ms = crash total du batch
// Pas de retry, pas de logs, l'utilisateur ne sait pas pourquoi ça a planté
// SOLUTION .NET 8 : try-catch + Polly retry policy + ILogger
```

**Lignes 74** : Aucune gestion d'erreur sur l'envoi email

```csharp
// LIGNE 74 : smtpClient.Send() sans try-catch
smtpClient.Send(mailMessage); // ❌ Si SMTP timeout, exception non gérée

// CONSÉQUENCE : Si le serveur SMTP est temporairement indisponible, crash
// SOLUTION .NET 8 : try-catch + logs + queue asynchrone
```

**Annotation pour les apprenants** :

> 💡 **Point d'apprentissage** : En production, **tout** peut échouer (DB, réseau, disque). Un code robuste anticipe TOUTES les exceptions possibles et logue pour diagnostiquer.

---

### Étape 5 : Analyser Program.cs - Problème #4 (Maintenabilité)

**Lignes 68-74** : Instanciation directe impossible à tester

```csharp
// ============================================================
// 🔧 PROBLÈME #4 : MAINTENABILITÉ - Couplage fort
// ============================================================
// LIGNE 68 : new SmtpClient() - instanciation directe
var smtpClient = new SmtpClient("smtp.gmail.com"); // ❌ Couplage direct

// CONSÉQUENCE : Impossible d'écrire un test unitaire sans vraie connexion SMTP
// Impossible de changer de provider (MailKit, SendGrid) sans modifier cette fonction
// SOLUTION .NET 8 : IEmailService + Constructor Injection
```

**Code Legacy complet (Main)** :

```csharp
static void Main(string[] args)
{
    // Tout est dans une seule fonction - impossible à tester isolément
    string connectionString = "..."; // Hardcodé
    
    var data = GetDataFromDb(connectionString); // Appel direct
    
    var rules = new List<TagRule> { ... }; // Instanciation directe
    
    var validModels = new List<MyXmlModel>();
    var invalidEntries = new List<string>();
    
    // Validation
    ValidateObject(model, rules, invalidEntries, ref hasValid);
    
    // Email
    var smtp = new SmtpClient(); // Instanciation directe
    smtp.Send(message);
    
    // XML
    var serializer = new XmlSerializer(typeof(List<MyXmlModel>));
    serializer.Serialize(writer, validModels);
}
```

**Problèmes** :
- ❌ Impossible de tester `Main()` sans vraie DB + vraie SMTP + vrai filesystem
- ❌ Modification d'une règle = risque de casser toute la chaîne
- ❌ Pas de séparation des responsabilités (tout dans une fonction)

**Annotation pour les apprenants** :

> 💡 **Point d'apprentissage** : Le mot-clé `new` crée un **couplage fort**. En .NET 8 moderne, on utilise l'Injection de Dépendances pour recevoir les objets déjà créés (Constructor Injection).

---

### Étape 6 : Dessiner l'Architecture TO-BE au Tableau

**Consigne** : Dessinez ce schéma au tableau pendant l'explication.

```
┌─────────────────────────────────────────────────────────────┐
│               CODE LEGACY (Monolithe)                       │
│                                                             │
│  Program.cs (500 lignes)                                    │
│  ├─ ConnectionString hardcodé                               │
│  ├─ GetDataFromDb() synchrone                               │
│  ├─ Validation en dur                                       │
│  ├─ new SmtpClient() direct                                 │
│  └─ XmlSerializer direct                                    │
│                                                             │
│  ❌ Impossible à tester                                     │
│  ❌ Impossible à faire évoluer                              │
│  ❌ Windows uniquement                                      │
└─────────────────────────────────────────────────────────────┘

                          ↓ MIGRATION

┌─────────────────────────────────────────────────────────────┐
│           ARCHITECTURE TO-BE (5 Projets)                    │
│                                                             │
│  1. Domain (Logique Métier)                                 │
│     ├─ XmlData.cs (record)                                  │
│     ├─ IRule.cs (interface)                                 │
│     └─ MandatoryRule.cs (implémentation)                    │
│     ✅ Zéro dépendance externe                              │
│     ✅ 100% testable                                        │
│                                                             │
│  2. Infrastructure (Techniques)                             │
│     ├─ DataGuardContext.cs (EF Core DbContext)              │
│     ├─ EfDataRepository.cs (async)                          │
│     └─ MailKitEmailService.cs (async)                       │
│     ✅ Implémentations concrètes                            │
│                                                             │
│  3. Application.Services (Orchestration)                    │
│     ├─ ValidationOrchestrator.cs                            │
│     └─ XmlGenerationService.cs                              │
│     ✅ Réutilisable (API, Blazor, Function)                 │
│                                                             │
│  4. Application.Console (Point d'entrée)                    │
│     ├─ Program.cs (DI uniquement)                           │
│     └─ Worker.cs (BackgroundService)                        │
│     ✅ "Humble Object" - minimal                            │
│                                                             │
│  5. Tests (Validation)                                      │
│     ├─ Unit/ (Domain isolé)                                 │
│     ├─ Integration/ (EF Core)                               │
│     └─ Application/ (Orchestrateurs)                        │
│     ✅ Couverture > 80%                                     │
│                                                             │
│  ✅ Testable                                                │
│  ✅ Évolutif                                                │
│  ✅ Multi-plateforme (Docker Linux)                         │
└─────────────────────────────────────────────────────────────┘
```

**Expliquez** :
1. Le **Domain** ne contient QUE la logique métier (règles de validation)
2. L'**Infrastructure** implémente les détails techniques (DB, Email)
3. L'**Application.Services** orchestre les appels
4. La **Console** est juste un point d'entrée (50 lignes max)
5. Les **Tests** valident chaque couche isolément

**Annotation pour les apprenants** :

> 💡 **Point d'apprentissage** : Une bonne architecture sépare les **préoccupations** (concerns). La logique métier ne doit JAMAIS dépendre des détails techniques (DB, réseau).

---

### Étape 7 : Identifier les 3 Seams à Créer

**Dessinez les 3 interfaces clés** :

#### Seam #1 : IDataRepository

```csharp
// Abstraction de l'accès aux données
public interface IDataRepository
{
    Task<List<XmlData>> GetAllDataAsync();
    Task<XmlData?> GetByIdAsync(int id);
    Task AddAsync(XmlData data);
}

// Implémentation EF Core (Infrastructure)
public class EfDataRepository : IDataRepository
{
    private readonly DataGuardContext _context;
    
    public async Task<List<XmlData>> GetAllDataAsync()
    {
        return await _context.XmlDataSet.AsNoTracking().ToListAsync();
    }
}

// Implémentation Mock (Tests)
public class MockDataRepository : IDataRepository
{
    public async Task<List<XmlData>> GetAllDataAsync()
    {
        return new List<XmlData> 
        { 
            new XmlData(1, "Test", "Value") 
        };
    }
}
```

#### Seam #2 : IEmailService

```csharp
// Abstraction de l'envoi d'emails
public interface IEmailService
{
    Task SendAsync(string to, string subject, string body);
}

// Implémentation MailKit (Infrastructure)
public class MailKitEmailService : IEmailService
{
    public async Task SendAsync(string to, string subject, string body)
    {
        using var client = new SmtpClient();
        await client.ConnectAsync("smtp.gmail.com", 587, true);
        await client.SendAsync(message);
    }
}

// Implémentation Mock (Tests)
public class MockEmailService : IEmailService
{
    public List<string> SentEmails { get; } = new();
    
    public Task SendAsync(string to, string subject, string body)
    {
        SentEmails.Add($"{to}: {subject}");
        return Task.CompletedTask;
    }
}
```

#### Seam #3 : IValidator

```csharp
// Abstraction de la validation métier
public interface IValidator
{
    ValidationResult Validate(XmlData data, List<IRule> rules);
}

// Implémentation (Domain)
public class DataValidator : IValidator
{
    public ValidationResult Validate(XmlData data, List<IRule> rules)
    {
        foreach (var rule in rules)
        {
            if (!rule.IsValid(data.Value))
            {
                return ValidationResult.Invalid(rule.ErrorMessage(data.Tag, data.Value));
            }
        }
        return ValidationResult.Valid();
    }
}
```

**Annotation pour les apprenants** :

> 💡 **Point d'apprentissage** : Chaque fois que vous créez une interface et que vous injectez l'implémentation, vous créez une **seam**. Cela rend le code testable et évolutif.

---

## 🎯 3. ATELIER PRATIQUE (15 minutes)

### Énoncé : Analyse du Code Legacy ValidFlow

**Contexte** : ValidFlow est un batch de validation de données clients avant export.

**Fichier à analyser** : `ValidFlow.Legacy/Program.cs`

**Votre mission** :

1. **Identifiez les 5 problèmes** dans le code Legacy de ValidFlow :
   - ⚠️ Credentials hardcodés (indiquez les numéros de lignes)
   - 🐌 Appels synchrones bloquants (indiquez les fonctions)
   - 💥 Absence de gestion d'erreurs (indiquez les lignes à risque)
   - 🔧 Couplage fort (indiquez les `new` directs)
   - 📦 Dépendance à Windows (expliquez pourquoi)

2. **Créez un document** `Analyse_ValidFlow.md` qui liste :
   ```markdown
   # Analyse du Code Legacy ValidFlow
   
   ## Problème #1 : Sécurité
   - Ligne 15 : ConnectionString hardcodé
   - Ligne 82 : Password SMTP hardcodé
   
   ## Problème #2 : Performance
   - Fonction GetClientsFromDb() : synchrone (ligne 20-35)
   - Fonction SendReport() : synchrone (ligne 90)
   
   ## Problème #3 : Robustesse
   - Ligne 25 : conn.Open() sans try-catch
   - Ligne 92 : smtp.Send() sans gestion d'exception
   
   ## Problème #4 : Maintenabilité
   - Ligne 82 : new SmtpClient() - impossible à tester
   - Ligne 40 : new EmailRule() - couplage fort
   
   ## Problème #5 : Déploiement
   - Windows uniquement (System.Net.Mail, pas de Docker)
   ```

3. **Dessinez l'architecture TO-BE** pour ValidFlow avec 5 projets :
   - ValidFlow.Domain (Client, Commande, EmailRule, PhoneRule)
   - ValidFlow.Infrastructure (EF Core, MailKit)
   - ValidFlow.Application.Services (ValidationOrchestrator, CsvExporter)
   - ValidFlow.Application.Console (Program.cs, Worker.cs)
   - ValidFlow.Tests (Unit, Integration, Application)

**Temps** : 15 minutes

**Livrable attendu** :
- Document `Analyse_ValidFlow.md` avec les 5 problèmes documentés
- Schéma d'architecture TO-BE (dessin à la main ou Excalidraw)

---

### Solution de l'Atelier

**Fichier** : `ValidFlow.Solutions/Analyse_ValidFlow.md`

```markdown
# Analyse du Code Legacy ValidFlow

## Problème #1 : ⚠️ SÉCURITÉ - Credentials Hardcodés

**Ligne 12** : ConnectionString SQL Server en clair
```csharp
string connectionString = "Server=prod-sql;Database=ValidFlowDb;User Id=admin;Password=P@ssw0rd123;";
```

**Ligne 78-82** : Credentials SMTP Gmail en clair
```csharp
var smtp = new SmtpClient("smtp.gmail.com")
{
    Credentials = new NetworkCredential("sales@company.com", "MyEmailP@ss!")
};
```

**Risques** :
- Exposition des mots de passe si commit Git
- Violation RGPD/ISO 27001
- Impossible de faire un audit des accès

**Solution .NET 8** :
- `appsettings.json` pour les valeurs non sensibles
- `.NET Secret Manager` pour le développement local
- `Azure Key Vault` pour la production

---

## Problème #2 : 🐌 PERFORMANCE - Code Synchrone Bloquant

**Fonction GetClientsFromDb()** (lignes 18-38) : 100% synchrone
```csharp
static List<Client> GetClientsFromDb(string connectionString)
{
    var clients = new List<Client>();
    using (var conn = new SqlConnection(connectionString))
    {
        conn.Open(); // ❌ Bloque le thread 50-200ms
        using (var cmd = new SqlCommand("SELECT * FROM Clients", conn))
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read()) // ❌ Bloque à chaque ligne
            {
                clients.Add(new Client { ... });
            }
        }
    }
    return clients;
}
```

**Fonction SendReport()** (ligne 88-95) : Envoi email synchrone
```csharp
smtp.Send(mailMessage); // ❌ Bloque pendant 500ms-2s
```

**Conséquences** :
- Thread gelé pendant les I/O (CPU idle)
- Impossible de traiter plusieurs exports en parallèle
- Scalabilité limitée

**Solution .NET 8** :
```csharp
static async Task<List<Client>> GetClientsFromDbAsync(string connectionString)
{
    await using var conn = new SqlConnection(connectionString);
    await conn.OpenAsync(); // ✅ Thread libéré
    // ...
    while (await reader.ReadAsync()) { } // ✅ Async
}

await smtp.SendAsync(mailMessage); // ✅ Async
```

---

## Problème #3 : 💥 ROBUSTESSE - Aucune Gestion d'Erreurs

**Ligne 24** : conn.Open() sans try-catch
```csharp
conn.Open(); // ❌ Si DB down, exception non gérée → crash total
```

**Ligne 31** : cmd.ExecuteReader() sans try-catch
```csharp
using (var reader = cmd.ExecuteReader()) // ❌ Si timeout SQL, crash
```

**Ligne 92** : smtp.Send() sans try-catch
```csharp
smtp.Send(mailMessage); // ❌ Si SMTP indisponible, crash
```

**Aucun logging** : Impossible de diagnostiquer les problèmes

**Conséquences** :
- Une micro-coupure DB = échec total du batch
- Pas de retry automatique
- Pas de logs pour déboguer

**Solution .NET 8** :
```csharp
try
{
    var clients = await _repository.GetClientsAsync();
}
catch (SqlException ex)
{
    _logger.LogError(ex, "Database error");
    // Retry policy (Polly)
    await _retryPolicy.ExecuteAsync(() => _repository.GetClientsAsync());
}
```

---

## Problème #4 : 🔧 MAINTENABILITÉ - Couplage Fort

**Ligne 78** : new SmtpClient() - impossible à tester
```csharp
var smtp = new SmtpClient("smtp.gmail.com"); // ❌ Couplage direct
```

**Ligne 45-50** : new EmailRule() - règles instanciées en dur
```csharp
var rules = new List<IValidationRule>
{
    new EmailRule(),     // ❌ Instanciation directe
    new PhoneRule(),     // ❌ Impossible de configurer
    new MandatoryRule()  // ❌ Impossible de tester isolément
};
```

**Fonction Main()** : Tout dans une seule fonction (150 lignes)
- Impossible de tester sans vraie DB + vraie SMTP + vrai filesystem
- Modification d'une partie = risque de casser tout le batch

**Solution .NET 8** :
```csharp
// Injection de dépendances
public class ClientProcessor
{
    private readonly IClientRepository _repository;
    private readonly IEmailService _emailService;
    
    public ClientProcessor(IClientRepository repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }
}

// Tests unitaires possibles
var mockRepo = new Mock<IClientRepository>();
var processor = new ClientProcessor(mockRepo.Object, mockEmail.Object);
```

---

## Problème #5 : 📦 DÉPLOIEMENT - Windows Uniquement

**Dépendances Windows** :
- `System.Net.Mail.SmtpClient` (Windows uniquement)
- .NET Framework 4.8 (Windows uniquement)
- Déploiement manuel (copier DLL sur serveur Windows)

**Conséquences** :
- Coût : Licence Windows Server (~800€/an)
- Serveur lourd : Windows Server = 4 GB RAM minimum
- Pas de conteneurisation Docker Linux
- Pas de Kubernetes/cloud moderne
- Déploiement manuel chronophage

**Solution .NET 8** :
```dockerfile
FROM mcr.microsoft.com/dotnet/runtime:8.0-alpine
COPY ./publish /app
ENTRYPOINT ["dotnet", "/app/ValidFlow.dll"]
```

**Gains** :
- Image Docker 100 MB (vs 4 GB Windows)
- Coût réduit de 87% (Linux containers)
- Déploiement en 30s via CI/CD
- Compatible Kubernetes/AWS ECS/Azure Container Apps

---

## Architecture TO-BE Recommandée (5 Projets)

```
ValidFlow.sln
│
├── 1. ValidFlow.Domain/
│   ├── Models/
│   │   ├── Client.cs (record)
│   │   ├── Commande.cs (record)
│   │   └── ValidationResult.cs
│   ├── Interfaces/
│   │   ├── IValidationRule.cs
│   │   ├── IClientRepository.cs
│   │   └── IEmailService.cs
│   └── Validators/
│       ├── EmailRule.cs
│       ├── PhoneRule.cs
│       └── MandatoryRule.cs
│
├── 2. ValidFlow.Infrastructure/
│   ├── Data/
│   │   └── ValidFlowContext.cs (DbContext EF Core 8)
│   ├── Repositories/
│   │   └── EfClientRepository.cs (async)
│   └── Services/
│       └── MailKitEmailService.cs (async)
│
├── 3. ValidFlow.Application.Services/
│   ├── Orchestrators/
│   │   └── ClientValidationOrchestrator.cs
│   └── Services/
│       ├── CsvExportService.cs
│       └── JsonExportService.cs
│
├── 4. ValidFlow.Application.Console/
│   ├── Program.cs (Composition Root uniquement)
│   ├── Worker.cs (BackgroundService)
│   └── appsettings.json
│
└── 5. ValidFlow.Tests/
    ├── Unit/ (EmailRule, PhoneRule, etc.)
    ├── Integration/ (EfClientRepository avec vraie DB)
    └── Application/ (Orchestrateur avec mocks)
```

**Justification** :
- **Domain** : Logique métier pure (règles de validation) - 0 dépendance externe
- **Infrastructure** : EF Core + MailKit - implémentations techniques
- **Application.Services** : Orchestration réutilisable (API/Blazor/Function)
- **Application.Console** : Point d'entrée minimal ("Humble Object")
- **Tests** : Validation automatisée (couverture > 80%)
```

---

## 📊 Récapitulatif de la Session

### Ce que nous avons appris :

1. ✅ **Les 5 problèmes critiques** du code Legacy :
   - Sécurité (credentials hardcodés)
   - Performance (code synchrone)
   - Robustesse (pas de gestion d'erreurs)
   - Maintenabilité (couplage fort)
   - Déploiement (Windows uniquement)

2. ✅ **Le concept de "Seam"** (couture) pour le découplage

3. ✅ **L'architecture TO-BE** en 5 projets pour une application moderne

### Ce que nous ferons dans les prochaines sessions :

- **10h40-12h30** : Création de la structure 5 projets .NET 8
- **13h30-15h00** : Extraction de la logique métier vers le Domain
- **15h10-17h00** : Refactoring avec syntaxe C# 12 + tests unitaires

### Livrable de cette session :

- ✅ Document `Analyse_ValidFlow.md` avec les 5 problèmes identifiés
- ✅ Schéma d'architecture TO-BE en 5 projets

---

**Branche Git** : `jour1-10h30-analyse-legacy` (créée et poussée)

**Prochaine étape** : Validation formateur avant de continuer ➡️ 10h40

---

**Document créé le** : 6 mars 2026  
**Version** : 1.0  
**Auteur** : Expert Architecte Logiciel .NET (BMAD Method)
