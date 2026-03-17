# Workbook Stagiaire - ValidFlow

## Session 13h30 : Migration du Cœur Métier (Projet Domain)

### 🧠 1. Fondations Théoriques : Pourquoi le Domain est la Zone la Plus Importante

Le diagnostic de ce matin a révélé le problème central du code legacy : **la logique métier est mélangée à l'infrastructure** (SQL, SMTP). Cette architecture rend le code :

- ❌ **Impossible à tester unitairement** : Pour tester une règle de validation, il faut une vraie base de données.
- ❌ **Dangereux à modifier** : Changer une ligne peut casser l'envoi d'emails ou les requêtes SQL.
- ❌ **Bloqué techniquement** : Impossible de migrer vers .NET 8, Docker ou Linux.

**La Clean Architecture résout ce problème** en isolant le cœur métier dans un projet `Domain` qui :

- ✅ **N'a AUCUNE dépendance externe** : Pas de NuGet, pas de SQL, pas de framework.
- ✅ **Est 100% testable en isolation** : Les tests s'exécutent en millisecondes.
- ✅ **Crée un filet de sécurité** : Vous pouvez refactoriser sans peur car les tests vous alertent immédiatement.

### 📊 2. Modélisation du Domain (classDiagram)

Voici la structure que nous allons construire dans le projet `ValidFlow.Domain` :

```mermaid
classDiagram
    direction TB
    
    class Client {
        +string Id
        +string Name
        +string Email
        +bool IsValid()
    }
    
    class IValidationRule {
        <<interface>>
        +bool IsValid(string value)
        +string GetErrorMessage(string fieldName)
    }
    
    class MinLengthRule {
        +int MinLength
        +bool IsValid(string value)
        +string GetErrorMessage(string fieldName)
    }
    
    class MaxLengthRule {
        +int MaxLength
        +bool IsValid(string value)
        +string GetErrorMessage(string fieldName)
    }
    
    class MandatoryRule {
        +bool IsValid(string value)
        +string GetErrorMessage(string fieldName)
    }
    
    IValidationRule <|.. MinLengthRule : implémente
    IValidationRule <|.. MaxLengthRule : implémente
    IValidationRule <|.. MandatoryRule : implémente
    
    Client ..> IValidationRule : utilise
    
    note for Client "Entité métier pure\n(record C# 12)"
    note for IValidationRule "Abstraction dans Domain\nZéro dépendance externe"
```

### 🎯 3. Votre Mission : Migration du Legacy vers le Domain (45 min)

Reproduisez les étapes montrées par le formateur pour migrer les règles métier du code legacy vers votre projet `ValidFlow.Domain`.

---

**Étape 1 : Création de la structure de dossiers**

1. Ouvrez un terminal dans le dossier `02_Atelier_Stagiaires/ValidFlow.Modern/ValidFlow.Domain/`.
2. Créez les dossiers pour organiser votre Domain :

```bash
mkdir Entities
mkdir Interfaces
mkdir ValueObjects
```

3. Supprimez le fichier `Class1.cs` généré automatiquement :

```bash
del Class1.cs
```

---

**Étape 2 : Création de l'entité Client (C# 12 record)**

Créez le fichier `Entities/Client.cs` avec le contenu suivant :

```csharp
// ValidFlow.Domain/Entities/Client.cs
namespace ValidFlow.Domain.Entities;

/// <summary>
/// Entité Client - Zone stérile du Domain (aucune dépendance externe)
/// Utilise la syntaxe record C# 12 pour l'immuabilité
/// </summary>
public record Client
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    
    /// <summary>
    /// Validation métier pure - testable en isolation totale
    /// </summary>
    public bool IsValid() => 
        !string.IsNullOrWhiteSpace(Name) && 
        Name.Length >= 2 &&
        Email.Contains('@');
}
```

> 💡 **C# 12** : Le mot-clé `required` garantit que les propriétés sont initialisées. Le mot-clé `init` rend l'objet immuable après création.

---

**Étape 3 : Création de l'interface IValidationRule**

Créez le fichier `Interfaces/IValidationRule.cs` :

```csharp
// ValidFlow.Domain/Interfaces/IValidationRule.cs
namespace ValidFlow.Domain.Interfaces;

/// <summary>
/// Interface de règle de validation - Abstraction pure
/// Inspirée de IRule du code legacy, mais modernisée
/// </summary>
public interface IValidationRule
{
    /// <summary>
    /// Vérifie si la valeur respecte la règle
    /// </summary>
    bool IsValid(string? value);
    
    /// <summary>
    /// Retourne le message d'erreur formaté
    /// </summary>
    string GetErrorMessage(string fieldName);
}
```

---

**Étape 4 : Implémentation des règles de validation (Pattern Matching)**

Créez le fichier `ValueObjects/MinLengthRule.cs` :

```csharp
// ValidFlow.Domain/ValueObjects/MinLengthRule.cs
namespace ValidFlow.Domain.ValueObjects;

using ValidFlow.Domain.Interfaces;

/// <summary>
/// Règle de longueur minimale - Utilise le Pattern Matching C# 12
/// </summary>
public record MinLengthRule(int MinLength) : IValidationRule
{
    public bool IsValid(string? value) => value switch
    {
        null or "" => false,
        { Length: var len } when len >= MinLength => true,
        _ => false
    };
    
    public string GetErrorMessage(string fieldName) => 
        $"Le champ '{fieldName}' doit contenir au moins {MinLength} caractères.";
}
```

Créez le fichier `ValueObjects/MandatoryRule.cs` :

```csharp
// ValidFlow.Domain/ValueObjects/MandatoryRule.cs
namespace ValidFlow.Domain.ValueObjects;

using ValidFlow.Domain.Interfaces;

/// <summary>
/// Règle de champ obligatoire - Pattern Matching avec 'not'
/// </summary>
public record MandatoryRule : IValidationRule
{
    public bool IsValid(string? value) => value is not (null or "");
    
    public string GetErrorMessage(string fieldName) => 
        $"Le champ '{fieldName}' est obligatoire.";
}
```

---

**Étape 5 : Création d'un test unitaire**

Dans le projet `ValidFlow.Tests/`, créez le fichier `ClientTests.cs` :

```csharp
// ValidFlow.Tests/ClientTests.cs
namespace ValidFlow.Tests;

using ValidFlow.Domain.Entities;
using Xunit;

public class ClientTests
{
    [Fact]
    public void Client_WithValidData_ShouldBeValid()
    {
        // Arrange
        var client = new Client 
        { 
            Id = "CLT-001", 
            Name = "Acme Corporation", 
            Email = "contact@acme.com" 
        };
        
        // Act & Assert
        Assert.True(client.IsValid());
    }
    
    [Theory]
    [InlineData("", "test@email.com")]      // Nom vide
    [InlineData("A", "test@email.com")]     // Nom trop court
    [InlineData("Acme", "invalid-email")]   // Email sans @
    public void Client_WithInvalidData_ShouldBeInvalid(string name, string email)
    {
        // Arrange
        var client = new Client 
        { 
            Id = "CLT-001", 
            Name = name, 
            Email = email 
        };
        
        // Act & Assert
        Assert.False(client.IsValid());
    }
}
```

---

**Étape 6 : Ajout de la référence et validation**

1. Ajoutez la référence du projet `Domain` au projet `Tests` :

```bash
cd ..
dotnet add ValidFlow.Tests reference ValidFlow.Domain
```

2. Exécutez les tests pour valider votre travail :

```bash
dotnet test
```

**Résultat attendu :**
```
Passed!  - Failed:     0, Passed:     2, Skipped:     0, Total:     2
```

---

### ✅ Critères de Succès

- [ ] Le projet `ValidFlow.Domain` n'a **aucun package NuGet** (vérifiez le `.csproj`)
- [ ] L'entité `Client` utilise la syntaxe **record C# 12**
- [ ] Les règles de validation utilisent le **Pattern Matching**
- [ ] `dotnet test` passe au **vert** en moins de **100ms**

---

> 💡 **Correction :** Le formateur partagera le fichier de correction officiel directement dans le chat à la fin du temps imparti.
