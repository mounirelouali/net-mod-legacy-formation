# ValidFlow.Legacy - Code de Départ Atelier

## 🎯 Votre Mission

Ce code monolithique contient **7 anti-patterns** que vous allez moderniser pendant la formation.

## ⚠️ Anti-patterns à Identifier

| # | Catégorie | Ligne | Description |
|---|-----------|-------|-------------|
| 1 | 🔓 Sécurité | 15-19 | Secrets en dur dans le code |
| 2 | 🐌 Performance | 22-40 | Tout dans Main() - pas de séparation |
| 3 | 💥 Robustesse | 41-44 | catch(Exception) générique |
| 4 | 🔧 Maintenabilité | 47-66 | Couplage fort SqlConnection |
| 5 | 🔧 Maintenabilité | 69-98 | Règles métier mélangées avec infra |
| 6 | 💥 Robustesse | 86-92 | Pas de gestion du null |
| 7 | 🔧 Maintenabilité | 101-119 | Couplage fort SmtpClient |

## 📁 Structure Cible (après modernisation)

```
ValidFlow.Modern/
├── ValidFlow.Domain/           (Règles métier isolées)
├── ValidFlow.Application/      (Cas d'usage)
├── ValidFlow.Infrastructure/   (SQL, SMTP)
├── ValidFlow.Console/          (Point d'entrée)
└── ValidFlow.Tests/            (Tests unitaires)
```

## 🚀 Instructions

1. Ouvrez `Program.cs` dans VS Code
2. Identifiez chaque anti-pattern marqué par `⚠️`
3. Suivez les instructions du Workbook pour migrer vers Clean Architecture

---

> **Ne modifiez pas ce fichier directement.**  
> Créez votre propre solution `ValidFlow.Modern/` à côté.
