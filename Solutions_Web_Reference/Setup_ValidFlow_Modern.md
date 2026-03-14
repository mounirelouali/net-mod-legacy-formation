# Correction : Architecture ValidFlow.Modern

Voici les commandes complètes pour créer la Clean Architecture de l'atelier :

```bash
dotnet new sln -n ValidFlow.Modern
dotnet new classlib -n ValidFlow.Domain
dotnet new classlib -n ValidFlow.Infrastructure
dotnet new classlib -n ValidFlow.Application.Services
dotnet new console -n ValidFlow.Console
dotnet new xunit -n ValidFlow.Tests

dotnet sln add ValidFlow.Domain
dotnet sln add ValidFlow.Infrastructure
dotnet sln add ValidFlow.Application.Services
dotnet sln add ValidFlow.Console
dotnet sln add ValidFlow.Tests

dotnet add ValidFlow.Infrastructure reference ValidFlow.Domain
dotnet add ValidFlow.Application.Services reference ValidFlow.Domain
dotnet add ValidFlow.Console reference ValidFlow.Infrastructure
dotnet add ValidFlow.Console reference ValidFlow.Application.Services

dotnet build
```

---

## Architecture Cible

```
ValidFlow.Modern/
├─ ValidFlow.sln
├─ ValidFlow.Domain/            (Logique métier pure - ZÉRO dépendance)
├─ ValidFlow.Infrastructure/    (Implémentations techniques - dépend de Domain)
├─ ValidFlow.Application.Services/ (Orchestration - dépend de Domain)
├─ ValidFlow.Console/           (Point d'entrée - dépend de Application + Infrastructure)
└─ ValidFlow.Tests/             (Tests - dépend de toutes les couches)
```

---

## Vérification

Après avoir exécuté toutes les commandes, vous devriez obtenir :

```
Build succeeded.
    0 Warning(s)
    0 Error(s)
```
