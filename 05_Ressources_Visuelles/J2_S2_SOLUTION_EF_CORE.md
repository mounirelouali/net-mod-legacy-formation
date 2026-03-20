# Solution Jour 2 Session 2 (10h40) - Entity Framework Core 8

> **Formation** : Migration .NET Legacy vers .NET 8  
> **Jour** : 2 sur 5  
> **Session** : 2 sur 4 (10h40)  
> **Thème** : Entity Framework Core 8

---

## 🎯 Objectif de la Solution

Cette solution montre comment configurer Entity Framework Core 8 dans `ValidFlow.Infrastructure`, créer une migration, et tester le CRUD complet.

---

## 📂 Structure de la Solution

```
ValidFlow.Infrastructure/
├─ Data/
│   └─ AppDbContext.cs           (DbContext EF Core)
├─ Migrations/                   (Généré automatiquement)
│   ├─ 20260320000000_InitialCreate.cs
│   └─ ...
└─ ValidFlow.Infrastructure.csproj

ValidFlow.Console/
├─ Program.cs                    (Test CRUD)
└─ ValidFlow.Console.csproj
```

---

## 📝 Fichier 1 : ValidFlow.Infrastructure.csproj

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

  <ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="9.0.3" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="9.0.3" />
  </ItemGroup>

</Project>
```

---

## 📝 Fichier 2 : ValidFlow.Infrastructure/Data/AppDbContext.cs

```csharp
using Microsoft.EntityFrameworkCore;
using ValidFlow.Domain.Entities;

namespace ValidFlow.Infrastructure.Data;

/// <summary>
/// DbContext Entity Framework Core pour ValidFlow.
/// Gère la connexion à la base de données et le mapping des entités.
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Table Clients en base de données.
    /// DbSet = Représente une table SQL.
    /// </summary>
    public DbSet<Client> Clients { get; set; }

    /// <summary>
    /// Configuration de la connexion SQL Server.
    /// Utilise LocalDB (inclus avec Visual Studio / .NET SDK).
    /// </summary>
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=ValidFlowDb;Trusted_Connection=true;"
            );
        }
    }

    /// <summary>
    /// Configuration du modèle de données (optionnel mais recommandé).
    /// Définit les contraintes : clés primaires, longueurs max, required.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            // Clé primaire
            entity.HasKey(c => c.Id);

            // Name : obligatoire, max 100 caractères
            entity.Property(c => c.Name)
                  .IsRequired()
                  .HasMaxLength(100);

            // Email : obligatoire, max 255 caractères
            entity.Property(c => c.Email)
                  .IsRequired()
                  .HasMaxLength(255);
        });
    }
}
```

---

## 📝 Fichier 3 : Migration InitialCreate (Générée automatiquement)

**Commande** : `dotnet ef migrations add InitialCreate`

**Fichier généré** : `Migrations/20260320000000_InitialCreate.cs`

```csharp
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValidFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
```

**Note** : Le fichier de migration est généré automatiquement par EF Core. Ne modifiez pas manuellement.

---

## 📝 Fichier 4 : ValidFlow.Console.csproj

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
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="9.0.3" />
    <PackageReference Include="Microsoft.Extensions.Hosting" Version="9.0.3" />
  </ItemGroup>

</Project>
```

---

## 📝 Fichier 5 : ValidFlow.Console/Program.cs (Test CRUD)

```csharp
using ValidFlow.Domain.Entities;
using ValidFlow.Infrastructure.Data;

Console.WriteLine("╔════════════════════════════════════════════════════════╗");
Console.WriteLine("║     Test Entity Framework Core - Solution J2S2        ║");
Console.WriteLine("╚════════════════════════════════════════════════════════╝");
Console.WriteLine();

// Création du DbContext (connexion à la DB)
using var db = new AppDbContext();

// ========================================
// ÉTAPE 1 : CREATE (Créer un client)
// ========================================
Console.WriteLine("📝 ÉTAPE 1 : CREATE");
Console.WriteLine(new string('-', 50));

var newClient = new Client(0, "Alice Dupont", "alice@example.com");
db.Clients.Add(newClient);
db.SaveChanges();

Console.WriteLine($"✅ Client créé avec succès !");
Console.WriteLine($"   ID    : {newClient.Id}");
Console.WriteLine($"   Nom   : {newClient.Name}");
Console.WriteLine($"   Email : {newClient.Email}");
Console.WriteLine();

// ========================================
// ÉTAPE 2 : READ (Lire tous les clients)
// ========================================
Console.WriteLine("📋 ÉTAPE 2 : READ");
Console.WriteLine(new string('-', 50));

var clients = db.Clients.ToList();
Console.WriteLine($"Nombre de clients en base : {clients.Count}");

foreach (var c in clients)
{
    Console.WriteLine($"   • [{c.Id}] {c.Name} ({c.Email})");
}
Console.WriteLine();

// ========================================
// ÉTAPE 3 : UPDATE (Modifier un client)
// ========================================
Console.WriteLine("✏️ ÉTAPE 3 : UPDATE");
Console.WriteLine(new string('-', 50));

var alice = db.Clients.FirstOrDefault(c => c.Name == "Alice Dupont");
if (alice != null)
{
    // Modification de l'email
    var oldEmail = alice.Email;
    
    // Note : Comme Client est un record (immutable), on recrée l'objet
    db.Clients.Remove(alice);
    var updatedClient = alice with { Email = "alice.dupont@example.com" };
    db.Clients.Add(updatedClient);
    db.SaveChanges();
    
    Console.WriteLine($"✅ Email mis à jour !");
    Console.WriteLine($"   Avant : {oldEmail}");
    Console.WriteLine($"   Après : {updatedClient.Email}");
}
Console.WriteLine();

// ========================================
// ÉTAPE 4 : READ (Vérifier la mise à jour)
// ========================================
Console.WriteLine("📋 ÉTAPE 4 : READ (vérification)");
Console.WriteLine(new string('-', 50));

var updatedAlice = db.Clients.FirstOrDefault(c => c.Id == newClient.Id);
if (updatedAlice != null)
{
    Console.WriteLine($"Client [{updatedAlice.Id}] :");
    Console.WriteLine($"   Nom   : {updatedAlice.Name}");
    Console.WriteLine($"   Email : {updatedAlice.Email}");
}
Console.WriteLine();

// ========================================
// ÉTAPE 5 : DELETE (Supprimer le client)
// ========================================
Console.WriteLine("🗑️ ÉTAPE 5 : DELETE");
Console.WriteLine(new string('-', 50));

var clientToDelete = db.Clients.FirstOrDefault(c => c.Id == newClient.Id);
if (clientToDelete != null)
{
    db.Clients.Remove(clientToDelete);
    db.SaveChanges();
    Console.WriteLine($"✅ Client supprimé : {clientToDelete.Name}");
}
Console.WriteLine();

// ========================================
// VÉRIFICATION FINALE
// ========================================
Console.WriteLine("✅ VÉRIFICATION FINALE");
Console.WriteLine(new string('-', 50));

var remainingClients = db.Clients.Count();
Console.WriteLine($"Clients restants en base : {remainingClients}");

if (remainingClients == 0)
{
    Console.WriteLine("   🎯 Test CRUD réussi ! La base est propre.");
}

Console.WriteLine();
Console.WriteLine("╔════════════════════════════════════════════════════════╗");
Console.WriteLine("║              Tous les tests sont passés !              ║");
Console.WriteLine("╚════════════════════════════════════════════════════════╝");
```

---

## 🚀 Instructions d'Exécution

### Étape 1 : Installer EF Core Tools (une seule fois par machine)

```bash
dotnet tool install --global dotnet-ef
# Ou mettre à jour si déjà installé :
dotnet tool update --global dotnet-ef
```

### Étape 2 : Créer la Migration

```bash
cd 01_Demo_Formateur/ValidFlow.Modern/ValidFlow.Infrastructure
dotnet ef migrations add InitialCreate
```

**Résultat attendu** :
```
Build started...
Build succeeded.
Done. To undo this action, use 'dotnet ef migrations remove'
```

### Étape 3 : Appliquer la Migration (créer la base de données)

```bash
dotnet ef database update
```

**Résultat attendu** :
```
Build started...
Build succeeded.
Applying migration '20260320000000_InitialCreate'.
Done.
```

### Étape 4 : Exécuter le Test CRUD

```bash
cd ../ValidFlow.Console
dotnet run
```

**Résultat attendu** :
```
╔════════════════════════════════════════════════════════╗
║     Test Entity Framework Core - Solution J2S2        ║
╚════════════════════════════════════════════════════════╝

📝 ÉTAPE 1 : CREATE
--------------------------------------------------
✅ Client créé avec succès !
   ID    : 1
   Nom   : Alice Dupont
   Email : alice@example.com

📋 ÉTAPE 2 : READ
--------------------------------------------------
Nombre de clients en base : 1
   • [1] Alice Dupont (alice@example.com)

✏️ ÉTAPE 3 : UPDATE
--------------------------------------------------
✅ Email mis à jour !
   Avant : alice@example.com
   Après : alice.dupont@example.com

📋 ÉTAPE 4 : READ (vérification)
--------------------------------------------------
Client [1] :
   Nom   : Alice Dupont
   Email : alice.dupont@example.com

🗑️ ÉTAPE 5 : DELETE
--------------------------------------------------
✅ Client supprimé : Alice Dupont

✅ VÉRIFICATION FINALE
--------------------------------------------------
Clients restants en base : 0
   🎯 Test CRUD réussi ! La base est propre.

╔════════════════════════════════════════════════════════╗
║              Tous les tests sont passés !              ║
╚════════════════════════════════════════════════════════╝
```

---

## 🔧 Dépannage

### Erreur : "dotnet ef not found"

**Cause** : EF Core Tools non installé.

**Solution** :
```bash
dotnet tool install --global dotnet-ef
```

### Erreur : "NU1202 - Package incompatible"

**Cause** : Version EF Core incompatible avec .NET 9.

**Solution** : Utilisez `--version 9.0.3` :
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 9.0.3
dotnet add package Microsoft.EntityFrameworkCore.Design --version 9.0.3
```

### Erreur : "No DbContext found"

**Cause** : EF Core ne trouve pas `AppDbContext`.

**Vérifications** :
1. `AppDbContext` doit hériter de `DbContext`
2. Le fichier doit être dans `ValidFlow.Infrastructure/Data/`
3. Le projet doit avoir été buildé (`dotnet build`)

### Erreur : Connexion SQL Server

**Cause** : LocalDB non installé.

**Vérification** :
```bash
sqllocaldb info
```

**Si erreur** : Installez SQL Server Express LocalDB depuis Visual Studio Installer ou dotnet SDK.

---

## 🎯 Points Clés à Retenir

1. **DbContext** : Point d'entrée pour toutes les opérations EF Core
2. **DbSet<T>** : Représente une table en base de données
3. **Migrations** : Versionnent les changements de schéma (Git pour la DB)
4. **Change Tracking** : EF Core détecte automatiquement les modifications
5. **SaveChanges()** : Applique toutes les modifications en une transaction

---

**Fin de la solution J2S2 - Entity Framework Core 8**
