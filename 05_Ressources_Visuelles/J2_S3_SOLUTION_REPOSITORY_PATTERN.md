# Solution Jour 2 Session 3 (13h30) - Repository Pattern

> **Formation** : Migration .NET Legacy vers .NET 8  
> **Jour** : 2 sur 5  
> **Session** : 3 sur 4 (13h30)  
> **Thème** : Le Repository Pattern

---

## 🎯 Objectif de la Solution

Cette solution montre comment implémenter le Repository Pattern pour séparer la logique métier de l'accès données, rendant le code testable avec un `FakeRepository` (sans base de données réelle).

---

## 📂 Structure de la Solution

```
ValidFlow.Domain/
├─ Interfaces/
│   └─ IClientRepository.cs      (Interface - Contrat)
└─ Entities/
    └─ Client.cs                  (Entité existante)

ValidFlow.Infrastructure/
├─ Repositories/
│   └─ ClientRepository.cs        (Implémentation EF Core)
└─ Data/
    └─ AppDbContext.cs            (DbContext existant)

ValidFlow.Application/            (NOUVEAU PROJET)
├─ Services/
│   └─ ClientService.cs           (Logique métier)
└─ ValidFlow.Application.csproj

ValidFlow.Console/
├─ Program.cs                     (Configuration DI + Test)
└─ ValidFlow.Console.csproj
```

---

## 📝 Fichier 1 : IClientRepository.cs (Interface dans Domain)

**Emplacement** : `ValidFlow.Domain/Interfaces/IClientRepository.cs`

```csharp
using ValidFlow.Domain.Entities;

namespace ValidFlow.Domain.Interfaces;

/// <summary>
/// Interface du Repository pour l'entité Client.
/// Définit le contrat pour toutes les opérations d'accès aux données.
/// </summary>
public interface IClientRepository
{
    /// <summary>
    /// Ajoute un nouveau client (sans sauvegarder).
    /// </summary>
    void Add(Client client);

    /// <summary>
    /// Récupère un client par son ID.
    /// </summary>
    Client? GetById(int id);

    /// <summary>
    /// Récupère tous les clients.
    /// </summary>
    List<Client> GetAll();

    /// <summary>
    /// Met à jour un client existant (sans sauvegarder).
    /// </summary>
    void Update(Client client);

    /// <summary>
    /// Supprime un client par son ID (sans sauvegarder).
    /// </summary>
    void Delete(int id);

    /// <summary>
    /// Sauvegarde toutes les modifications en base de données.
    /// </summary>
    void SaveChanges();
}
```

---

## 📝 Fichier 2 : ClientRepository.cs (Implémentation EF Core dans Infrastructure)

**Emplacement** : `ValidFlow.Infrastructure/Repositories/ClientRepository.cs`

```csharp
using ValidFlow.Domain.Entities;
using ValidFlow.Domain.Interfaces;
using ValidFlow.Infrastructure.Data;

namespace ValidFlow.Infrastructure.Repositories;

/// <summary>
/// Implémentation du Repository Client avec Entity Framework Core.
/// Encapsule toutes les opérations d'accès aux données.
/// </summary>
public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _db;

    public ClientRepository(AppDbContext db)
    {
        _db = db;
    }

    public void Add(Client client)
    {
        _db.Clients.Add(client);
    }

    public Client? GetById(int id)
    {
        return _db.Clients.FirstOrDefault(c => c.Id == id);
    }

    public List<Client> GetAll()
    {
        return _db.Clients.ToList();
    }

    public void Update(Client client)
    {
        var existing = _db.Clients.FirstOrDefault(c => c.Id == client.Id);
        if (existing != null)
        {
            // Pour un record immutable, on supprime et on recrée
            _db.Clients.Remove(existing);
            _db.Clients.Add(client);
        }
    }

    public void Delete(int id)
    {
        var client = _db.Clients.FirstOrDefault(c => c.Id == id);
        if (client != null)
        {
            _db.Clients.Remove(client);
        }
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
}
```

---

## 📝 Fichier 3 : ValidFlow.Application.csproj (Nouveau projet)

**Commandes de création** :
```bash
cd 01_Demo_Formateur/ValidFlow.Modern
dotnet new classlib -n ValidFlow.Application
cd ValidFlow.Application
dotnet add reference ../ValidFlow.Domain/ValidFlow.Domain.csproj
```

**Fichier généré** : `ValidFlow.Application/ValidFlow.Application.csproj`

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\ValidFlow.Domain\ValidFlow.Domain.csproj" />
  </ItemGroup>

</Project>
```

---

## 📝 Fichier 4 : ClientService.cs (Logique métier dans Application)

**Emplacement** : `ValidFlow.Application/Services/ClientService.cs`

```csharp
using ValidFlow.Domain.Entities;
using ValidFlow.Domain.Interfaces;

namespace ValidFlow.Application.Services;

/// <summary>
/// Service métier pour gérer les clients.
/// Contient la logique de validation et orchestre les opérations via le Repository.
/// </summary>
public class ClientService
{
    private readonly IClientRepository _repository;

    /// <summary>
    /// Constructeur avec injection de dépendances.
    /// Dépend de l'interface IClientRepository, PAS de l'implémentation concrète.
    /// </summary>
    public ClientService(IClientRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Crée un nouveau client après validation.
    /// </summary>
    /// <returns>True si le client a été créé, False si validation échouée.</returns>
    public bool CreateClient(string name, string email)
    {
        // ========================================
        // LOGIQUE MÉTIER (Validation)
        // ========================================

        // Validation : nom obligatoire et minimum 2 caractères
        if (string.IsNullOrWhiteSpace(name) || name.Length < 2)
        {
            Console.WriteLine("❌ Erreur : Le nom doit contenir au moins 2 caractères.");
            return false;
        }

        // Validation : email obligatoire et doit contenir @
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
        {
            Console.WriteLine("❌ Erreur : L'email doit être valide (contenir @).");
            return false;
        }

        // ========================================
        // PERSISTANCE via Repository (Abstraction)
        // ========================================

        var client = new Client(0, name, email);
        _repository.Add(client);
        _repository.SaveChanges();

        return true;
    }

    /// <summary>
    /// Récupère tous les clients.
    /// </summary>
    public List<Client> GetAllClients()
    {
        return _repository.GetAll();
    }

    /// <summary>
    /// Récupère un client par son ID.
    /// </summary>
    public Client? GetClientById(int id)
    {
        return _repository.GetById(id);
    }

    /// <summary>
    /// Met à jour l'email d'un client.
    /// </summary>
    public bool UpdateClientEmail(int id, string newEmail)
    {
        // Validation
        if (string.IsNullOrWhiteSpace(newEmail) || !newEmail.Contains("@"))
        {
            Console.WriteLine("❌ Erreur : L'email doit être valide.");
            return false;
        }

        var client = _repository.GetById(id);
        if (client == null)
        {
            Console.WriteLine($"❌ Erreur : Client {id} introuvable.");
            return false;
        }

        // Mise à jour via Repository
        var updatedClient = client with { Email = newEmail };
        _repository.Update(updatedClient);
        _repository.SaveChanges();

        return true;
    }

    /// <summary>
    /// Supprime un client par son ID.
    /// </summary>
    public bool DeleteClient(int id)
    {
        var client = _repository.GetById(id);
        if (client == null)
        {
            Console.WriteLine($"❌ Erreur : Client {id} introuvable.");
            return false;
        }

        _repository.Delete(id);
        _repository.SaveChanges();

        return true;
    }
}
```

---

## 📝 Fichier 5 : ValidFlow.Console.csproj (Mise à jour)

**Ajouter la référence vers Application** :
```bash
cd ValidFlow.Console
dotnet add reference ../ValidFlow.Application/ValidFlow.Application.csproj
```

**Fichier résultant** : `ValidFlow.Console/ValidFlow.Console.csproj`

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\ValidFlow.Domain\ValidFlow.Domain.csproj" />
    <ProjectReference Include="..\ValidFlow.Infrastructure\ValidFlow.Infrastructure.csproj" />
    <ProjectReference Include="..\ValidFlow.Application\ValidFlow.Application.csproj" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="9.0.3" />
    <PackageReference Include="Microsoft.Extensions.Hosting" Version="9.0.3" />
  </ItemGroup>

</Project>
```

---

## 📝 Fichier 6 : Program.cs (Configuration DI + Test Repository Pattern)

**Emplacement** : `ValidFlow.Console/Program.cs`

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ValidFlow.Application.Services;
using ValidFlow.Domain.Interfaces;
using ValidFlow.Infrastructure.Data;
using ValidFlow.Infrastructure.Repositories;

Console.WriteLine("╔════════════════════════════════════════════════════════╗");
Console.WriteLine("║   Test Repository Pattern - Solution J2S3             ║");
Console.WriteLine("╚════════════════════════════════════════════════════════╝");
Console.WriteLine();

// ========================================
// CONFIGURATION DI (Injection de Dépendances)
// ========================================

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(services =>
{
    // Enregistrer DbContext (Infrastructure)
    services.AddDbContext<AppDbContext>();

    // Enregistrer Repository (Interface → Implémentation)
    // ✅ Inversion de dépendance : on enregistre l'interface, pas la classe concrète
    services.AddScoped<IClientRepository, ClientRepository>();

    // Enregistrer Service métier (Application Layer)
    services.AddScoped<ClientService>();
});

var host = builder.Build();

// ========================================
// RÉSOLUTION DES DÉPENDANCES
// ========================================

using var scope = host.Services.CreateScope();
var clientService = scope.ServiceProvider.GetRequiredService<ClientService>();

Console.WriteLine("🔧 Services enregistrés et résolus avec succès !");
Console.WriteLine("   - AppDbContext (Infrastructure)");
Console.WriteLine("   - IClientRepository → ClientRepository (Infrastructure)");
Console.WriteLine("   - ClientService (Application)");
Console.WriteLine();

// ========================================
// TEST 1 : CREATE
// ========================================

Console.WriteLine("📝 TEST 1 : CREATE");
Console.WriteLine(new string('-', 60));

var success1 = clientService.CreateClient("Charlie Brown", "charlie@example.com");
Console.WriteLine($"Résultat : {(success1 ? "✅ Client créé" : "❌ Échec")}");

var success2 = clientService.CreateClient("A", "invalid"); // Nom trop court + email invalide
Console.WriteLine($"Résultat : {(success2 ? "✅ Client créé" : "❌ Validation échouée (attendu)")}");

Console.WriteLine();

// ========================================
// TEST 2 : READ (GetAll)
// ========================================

Console.WriteLine("📋 TEST 2 : READ (GetAll)");
Console.WriteLine(new string('-', 60));

var clients = clientService.GetAllClients();
Console.WriteLine($"Nombre de clients en base : {clients.Count}");

foreach (var c in clients)
{
    Console.WriteLine($"   • [{c.Id}] {c.Name} ({c.Email})");
}

Console.WriteLine();

// ========================================
// TEST 3 : READ (GetById)
// ========================================

Console.WriteLine("🔍 TEST 3 : READ (GetById)");
Console.WriteLine(new string('-', 60));

if (clients.Count > 0)
{
    var firstClientId = clients[0].Id;
    var client = clientService.GetClientById(firstClientId);

    if (client != null)
    {
        Console.WriteLine($"✅ Client trouvé : [{client.Id}] {client.Name} ({client.Email})");
    }
}
else
{
    Console.WriteLine("⚠️ Aucun client en base pour tester GetById.");
}

Console.WriteLine();

// ========================================
// TEST 4 : UPDATE
// ========================================

Console.WriteLine("✏️ TEST 4 : UPDATE");
Console.WriteLine(new string('-', 60));

if (clients.Count > 0)
{
    var firstClientId = clients[0].Id;
    var oldEmail = clients[0].Email;

    var updateSuccess = clientService.UpdateClientEmail(firstClientId, "charlie.brown@example.com");
    Console.WriteLine($"Résultat : {(updateSuccess ? "✅ Email mis à jour" : "❌ Échec")}");

    var updatedClient = clientService.GetClientById(firstClientId);
    if (updatedClient != null)
    {
        Console.WriteLine($"   Avant : {oldEmail}");
        Console.WriteLine($"   Après : {updatedClient.Email}");
    }
}
else
{
    Console.WriteLine("⚠️ Aucun client en base pour tester Update.");
}

Console.WriteLine();

// ========================================
// TEST 5 : DELETE
// ========================================

Console.WriteLine("🗑️ TEST 5 : DELETE");
Console.WriteLine(new string('-', 60));

if (clients.Count > 0)
{
    var firstClientId = clients[0].Id;
    var clientName = clients[0].Name;

    var deleteSuccess = clientService.DeleteClient(firstClientId);
    Console.WriteLine($"Résultat : {(deleteSuccess ? $"✅ Client '{clientName}' supprimé" : "❌ Échec")}");
}
else
{
    Console.WriteLine("⚠️ Aucun client en base pour tester Delete.");
}

Console.WriteLine();

// ========================================
// VÉRIFICATION FINALE
// ========================================

Console.WriteLine("✅ VÉRIFICATION FINALE");
Console.WriteLine(new string('-', 60));

var remainingClients = clientService.GetAllClients();
Console.WriteLine($"Clients restants en base : {remainingClients.Count}");

foreach (var c in remainingClients)
{
    Console.WriteLine($"   • [{c.Id}] {c.Name} ({c.Email})");
}

Console.WriteLine();
Console.WriteLine("╔════════════════════════════════════════════════════════╗");
Console.WriteLine("║        Tous les tests Repository Pattern OK !         ║");
Console.WriteLine("╚════════════════════════════════════════════════════════╝");
```

---

## 🚀 Instructions d'Exécution

### Étape 1 : Créer le projet Application

```bash
cd 01_Demo_Formateur/ValidFlow.Modern
dotnet new classlib -n ValidFlow.Application
cd ValidFlow.Application
dotnet add reference ../ValidFlow.Domain/ValidFlow.Domain.csproj
```

### Étape 2 : Créer les fichiers

1. **IClientRepository.cs** dans `ValidFlow.Domain/Interfaces/`
2. **ClientRepository.cs** dans `ValidFlow.Infrastructure/Repositories/`
3. **ClientService.cs** dans `ValidFlow.Application/Services/`
4. Mettre à jour **Program.cs** dans `ValidFlow.Console/`

### Étape 3 : Ajouter référence Application dans Console

```bash
cd ../ValidFlow.Console
dotnet add reference ../ValidFlow.Application/ValidFlow.Application.csproj
```

### Étape 4 : Exécuter le test

```bash
dotnet run
```

**Résultat attendu** :
```
╔════════════════════════════════════════════════════════╗
║   Test Repository Pattern - Solution J2S3             ║
╚════════════════════════════════════════════════════════╝

🔧 Services enregistrés et résolus avec succès !
   - AppDbContext (Infrastructure)
   - IClientRepository → ClientRepository (Infrastructure)
   - ClientService (Application)

📝 TEST 1 : CREATE
------------------------------------------------------------
✅ Client créé
❌ Erreur : Le nom doit contenir au moins 2 caractères.
❌ Erreur : L'email doit être valide (contenir @).
Résultat : ❌ Validation échouée (attendu)

📋 TEST 2 : READ (GetAll)
------------------------------------------------------------
Nombre de clients en base : 1
   • [1] Charlie Brown (charlie@example.com)

🔍 TEST 3 : READ (GetById)
------------------------------------------------------------
✅ Client trouvé : [1] Charlie Brown (charlie@example.com)

✏️ TEST 4 : UPDATE
------------------------------------------------------------
✅ Email mis à jour
   Avant : charlie@example.com
   Après : charlie.brown@example.com

🗑️ TEST 5 : DELETE
------------------------------------------------------------
✅ Client 'Charlie Brown' supprimé

✅ VÉRIFICATION FINALE
------------------------------------------------------------
Clients restants en base : 0

╔════════════════════════════════════════════════════════╗
║        Tous les tests Repository Pattern OK !         ║
╚════════════════════════════════════════════════════════╝
```

---

## 🔧 Dépannage

### Erreur : "No service for type IClientRepository"

**Cause** : Le Repository n'est pas enregistré dans le conteneur DI.

**Solution** : Vérifiez que vous avez bien cette ligne dans `Program.cs` :
```csharp
services.AddScoped<IClientRepository, ClientRepository>();
```

### Erreur : "Circular dependency detected"

**Cause** : Dépendance circulaire (Service → Repository → Service).

**Solution** : Repository ne doit PAS injecter Service. Repository = accès données uniquement.

### Tests échouent : Validation ne fonctionne pas

**Cause** : La validation est dans `ClientService`, pas dans `ClientRepository`.

**Vérification** : Repository = persistance seulement (pas de logique métier).

---

## 🎯 Points Clés à Retenir

1. **Interface dans Domain** : `IClientRepository` définit le contrat (dépendance abstraite)
2. **Implémentation dans Infrastructure** : `ClientRepository` utilise EF Core (détail technique)
3. **Service dans Application** : `ClientService` contient la logique métier et dépend de l'interface
4. **Injection de Dépendances** : `services.AddScoped<IClientRepository, ClientRepository>()`
5. **SOLID - D** : Dependency Inversion Principle (dépendre d'abstractions, pas d'implémentations)

---

## 🧪 Pour Aller Plus Loin : FakeRepository pour Tests

**Création d'un FakeRepository** (liste en mémoire, pas de base SQL) :

```csharp
public class FakeClientRepository : IClientRepository
{
    private readonly List<Client> _clients = new();
    private int _nextId = 1;

    public void Add(Client client)
    {
        var clientWithId = client with { Id = _nextId++ };
        _clients.Add(clientWithId);
    }

    public Client? GetById(int id) => _clients.FirstOrDefault(c => c.Id == id);

    public List<Client> GetAll() => _clients.ToList();

    public void Update(Client client)
    {
        var existing = GetById(client.Id);
        if (existing != null)
        {
            _clients.Remove(existing);
            _clients.Add(client);
        }
    }

    public void Delete(int id)
    {
        var client = GetById(id);
        if (client != null)
            _clients.Remove(client);
    }

    public void SaveChanges()
    {
        // Rien à faire (déjà en mémoire)
    }
}
```

**Test unitaire avec FakeRepository** :
```csharp
[Test]
public void CreateClient_NameTooShort_ReturnsFalse()
{
    // Arrange : Fake Repository (pas de base SQL)
    var fakeRepo = new FakeClientRepository();
    var service = new ClientService(fakeRepo);

    // Act
    var result = service.CreateClient("A", "a@example.com");

    // Assert
    Assert.False(result); // Test instantané, fiable, simple !
}
```

**Avantage** : Tests **ultra-rapides** (mémoire), **fiables** (pas de dépendance DB), **simples** (pas de setup).

---

**Fin de la solution J2S3 - Repository Pattern**
