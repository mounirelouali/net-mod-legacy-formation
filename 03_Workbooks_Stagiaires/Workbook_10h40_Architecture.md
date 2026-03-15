# Workbook Stagiaire - ValidFlow

## Session 10h40 : Création de la Clean Architecture (.NET 8)

**Votre Mission :** Recréer la coquille vide pour le nouveau projet `ValidFlow.Modern`.

**Commandes CLI à exécuter (depuis le dossier `02_Atelier_Stagiaires/ValidFlow.Modern/`) :**

```bash
# 1. Création de la solution
dotnet new sln -n ValidFlow.Modern

# 2. Création des 5 projets
dotnet new classlib -n ValidFlow.Domain
dotnet new classlib -n ValidFlow.Infrastructure
dotnet new classlib -n ValidFlow.Application.Services
dotnet new console -n ValidFlow.Console
dotnet new xunit -n ValidFlow.Tests

# 3. Ajout à la solution
dotnet sln add ValidFlow.Domain
dotnet sln add ValidFlow.Infrastructure
dotnet sln add ValidFlow.Application.Services
dotnet sln add ValidFlow.Console
dotnet sln add ValidFlow.Tests

# 4. Ajout des références (Dépendances)
dotnet add ValidFlow.Infrastructure reference ValidFlow.Domain
dotnet add ValidFlow.Application.Services reference ValidFlow.Domain
dotnet add ValidFlow.Console reference ValidFlow.Infrastructure
dotnet add ValidFlow.Console reference ValidFlow.Application.Services
dotnet add ValidFlow.Tests reference ValidFlow.Domain
dotnet add ValidFlow.Tests reference ValidFlow.Application.Services
dotnet add ValidFlow.Tests reference ValidFlow.Infrastructure

# 5. Ajout des packages NuGet essentiels
dotnet add ValidFlow.Infrastructure package Microsoft.EntityFrameworkCore
dotnet add ValidFlow.Infrastructure package Microsoft.EntityFrameworkCore.SqlServer
dotnet add ValidFlow.Infrastructure package MailKit

dotnet add ValidFlow.Console package Microsoft.Extensions.Configuration
dotnet add ValidFlow.Console package Microsoft.Extensions.Configuration.Json
dotnet add ValidFlow.Console package Microsoft.Extensions.DependencyInjection
dotnet add ValidFlow.Console package Microsoft.Extensions.Hosting

dotnet add ValidFlow.Tests package Moq
dotnet add ValidFlow.Tests package FluentAssertions

# 6. Vérification
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
└─ ValidFlow.Tests/             (Tests - dépend de tout)
```

---

## Principes Clean Architecture

1. **Domain au centre** : Aucune dépendance externe
2. **Infrastructure implémente** : Les interfaces définies par Domain
3. **Application orchestre** : Coordonne Domain + Infrastructure
4. **Console configure** : DI + Configuration uniquement
5. **Tests couvrent** : Toutes les couches

---

## Checkpoint de Validation

Après avoir exécuté toutes les commandes, vérifiez :

- [ ] `dotnet build` réussit sans erreur
- [ ] 5 projets créés
- [ ] Références configurées correctement
- [ ] Packages NuGet installés

---

**Durée estimée** : 15 minutes  
**Prochaine étape** : Migration du code Legacy vers l'architecture moderne

---

> 💡 **Correction :** [Voir le script complet sur GitHub](https://github.com/mounirelouali/net-mod-legacy-formation/blob/main/Solutions_Web_Reference/Solution_10h40_Architecture.md)
