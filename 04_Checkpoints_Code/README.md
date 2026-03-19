# 📦 Checkpoints Code - Roues de Secours Formation

## 🎯 Objectif

Ce dossier contient les **points de sauvegarde** du code complet et fonctionnel après chaque journée de formation.

Si votre code ne compile pas ou si vous étiez absent, vous pouvez repartir sur une base saine en copiant le contenu du checkpoint correspondant.

---

## 📂 Structure

```
04_Checkpoints_Code/
├── README.md (ce fichier)
├── Jour_1_Fini/          # Code complet après Jour 1
│   ├── DataGuard.Domain/
│   ├── DataGuard.Application.Services/
│   ├── DataGuard.Infrastructure/
│   ├── DataGuard.Console/
│   ├── DataGuard.Tests/
│   └── DataGuard.Modern.sln
├── Jour_2_Fini/          # Code complet après Jour 2 (à venir)
├── Jour_3_Fini/          # Code complet après Jour 3 (à venir)
├── Jour_4_Fini/          # Code complet après Jour 4 (à venir)
└── Jour_5_Fini/          # Code complet après Jour 5 (à venir)
```

---

## 🚑 Comment Utiliser un Checkpoint

### Option 1 : Remplacer Complètement Votre Code

**Si votre code est cassé et que vous voulez repartir de zéro :**

1. **Sauvegardez votre travail actuel** (au cas où) :
   ```powershell
   Move-Item -Path "02_Atelier_Stagiaires\*" -Destination "C:\dev\backup_stagiaire_$(Get-Date -Format 'yyyyMMdd_HHmmss')"
   ```

2. **Copiez le checkpoint dans votre espace de travail** :
   ```powershell
   Copy-Item -Path "04_Checkpoints_Code\Jour_1_Fini\*" -Destination "02_Atelier_Stagiaires\" -Recurse -Force
   ```

3. **Ouvrez la solution** :
   ```powershell
   cd 02_Atelier_Stagiaires
   code DataGuard.Modern.sln
   ```

---

### Option 2 : Comparer Votre Code avec le Checkpoint

**Si vous voulez comprendre ce qui manque dans votre code :**

1. Ouvrez les deux projets côte à côte dans VS Code
2. Utilisez la vue "Comparaison de fichiers" (clic droit > "Comparer avec...")
3. Identifiez les différences et corrigez votre code

---

## ⚠️ Règles Importantes

### ✅ CE QUE VOUS POUVEZ FAIRE
- Copier le contenu d'un checkpoint vers `02_Atelier_Stagiaires/`
- Comparer votre code avec le checkpoint
- Utiliser le checkpoint comme référence pour débugger

### ❌ CE QUE VOUS NE DEVEZ JAMAIS FAIRE
- **NE MODIFIEZ JAMAIS** les fichiers dans `04_Checkpoints_Code/`
- **NE COMMITEZ JAMAIS** de modifications dans ce dossier
- **NE SUPPRIMEZ JAMAIS** ce dossier

> 💡 **Pourquoi ?** Le dossier `04_Checkpoints_Code/` est géré uniquement par le formateur via Git. Si vous le modifiez, vous aurez des conflits de merge lors du prochain `git pull`.

---

## 📅 Contenu de Chaque Checkpoint

### Jour_1_Fini (Session S1-S4 complètes)

**Ce que vous devez avoir après le Jour 1 :**
- ✅ Architecture Clean (5 projets séparés)
- ✅ Domain Layer migré (Entities, ValueObjects, Interfaces)
- ✅ Code modernisé en C# 12 (file-scoped namespace, primary constructors, collection expressions)
- ✅ Tests unitaires fonctionnels (< 20ms)

**Vérification rapide** :
```powershell
cd 04_Checkpoints_Code\Jour_1_Fini
dotnet build DataGuard.Modern.sln
dotnet test
```

Si tout compile et que les tests passent → Votre checkpoint est valide ✅

---

## 🎓 Philosophie Pédagogique

> "L'échec du Jour 1 ne doit pas condamner le reste de la semaine."

Les checkpoints sont un **filet de sécurité**, pas une solution de facilité.

**Utilisez-les quand :**
- Vous étiez absent et devez rattraper
- Votre code ne compile plus et vous ne trouvez pas l'erreur
- Vous voulez valider votre compréhension en comparant avec la solution officielle

**Essayez d'abord de résoudre par vous-même** :
- Relisez le Workbook de la session
- Consultez la Solution partagée par le formateur
- Demandez de l'aide au formateur ou aux autres stagiaires

---

## 📞 Support

Si vous avez un problème avec un checkpoint :
1. Vérifiez que vous avez fait `git pull` ce matin
2. Vérifiez que vous n'avez pas modifié les fichiers dans `04_Checkpoints_Code/`
3. Contactez le formateur

---

**Bonne formation ! 🚀**
