# 🏢 Opération DataGuard : Refonte ValidFlow

**Contexte :** Notre vieux batch monolithique (.NET Framework 4.8) est devenu critique. Il plante silencieusement, contient des secrets en clair et n'est pas déployable sous Linux.

**Votre Mission :** Transformer cette application en une solution **.NET 8 Clean Architecture**, moderne, testable et conteneurisée.

---

## 📋 Backlog (User Stories)

### Epic 1 : Fondations Architecturales (Jour 1)

* **US 1.1 - Audit :** Identifier les failles du code actuel (Sécurité, Perf, Couplage).
* **US 1.2 - Scaffolding :** Créer une structure Clean Architecture (5 projets).
* **US 1.3 - Cœur Métier :** Extraire les règles de validation dans le projet Domain.

### Epic 2 : Infrastructure et Persistance (Jour 2)

* **US 2.1 - Repository Pattern :** Implémenter l'accès base de données avec EF Core.
* **US 2.2 - Configuration :** Externaliser les secrets (Azure Key Vault, User Secrets).
* **US 2.3 - Logging :** Intégrer Serilog pour la traçabilité en production.

### Epic 3 : Tests et Qualité (Jour 3)

* **US 3.1 - Tests Unitaires :** Couvrir le Domain Layer (100% coverage).
* **US 3.2 - Tests d'Intégration :** Valider les repositories avec base de test.
* **US 3.3 - CI/CD :** Pipeline GitHub Actions (build, test, deploy).

### Epic 4 : Conteneurisation et Déploiement (Jour 4)

* **US 4.1 - Dockerfile :** Créer l'image Docker multi-stage.
* **US 4.2 - Docker Compose :** Orchestrer l'app + SQL Server.
* **US 4.3 - Production :** Déployer sur Azure Container Apps.

---

## 📁 Structure du Projet

```
net-mod-legacy/
├─ 00_Reference_Client/          (Code Legacy original - NE PAS MODIFIER)
│  └─ generationxml/             (Projet .NET Framework 4.8 du client)
├─ 02_Atelier_Stagiaires/        (Vos exercices pratiques)
│  └─ ValidFlow.Legacy/          (Votre point de départ Legacy)
└─ 03_Workbooks_Stagiaires/      (Instructions des exercices)
   ├─ Workbook_09h00_Analyse.md
   ├─ Workbook_10h40_Architecture.md
   └─ Workbook_13h30_Migration_Domain.md
```

---

## 🚀 Démarrage Rapide

### Prérequis

- .NET 8 SDK ([télécharger](https://dotnet.microsoft.com/download))
- Visual Studio 2022 ou VS Code + extension C#
- Docker Desktop (pour les ateliers de conteneurisation)

### Cloner le Repository

```bash
git clone --single-branch --branch jour1-09h00-start https://github.com/mounirelouali/net-mod-legacy-formation.git
cd net-mod-legacy-formation
```

> ⚠️ **Important** : Vous travaillez sur une branche dédiée à votre journée. Ne faites PAS de `git pull` pendant la formation pour éviter les conflits.

---

## 🎯 Pédagogie

Cette formation suit une approche **"Montrer puis Faire"** :

1. **Démonstration Formateur** : Sur le projet **DataGuard** (ex-generationxml)
2. **Pratique Stagiaire** : Sur le projet **ValidFlow** (votre atelier)

Les corrections sont partagées par le formateur **uniquement à la fin du temps imparti** pour préserver la difficulté désirable.

---

## 📚 Ressources

- [Documentation .NET 8](https://learn.microsoft.com/dotnet/)
- [Clean Architecture Guide](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [EF Core Documentation](https://learn.microsoft.com/ef/core/)
- [Docker Best Practices](https://docs.docker.com/develop/dev-best-practices/)

---

## 📞 Support

En cas de blocage technique :
1. Consulter le Workbook de la session en cours
2. Demander au formateur pendant les pauses
3. Utiliser le chat Teams pour les questions asynchrones

---

## 📝 Licence

Ce matériel de formation est fourni à titre pédagogique uniquement.
