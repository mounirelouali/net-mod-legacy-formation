# Prompt Infographie NotebookLM - Jour 2 Session 1 : Injection de Dépendances

**Objectif** : Générer une infographie pédagogique pour expliquer l'Injection de Dépendances (DI) avec la métaphore du restaurant.

**Fichier cible** : `J2_S1_Infographie_DI_Restaurant.png`

---

## 📋 Contexte Pédagogique

**Session** : Jour 2 - Session 1 (09h00)  
**Thème** : Injection de Dépendances avec .NET 8  
**Public** : Développeurs .NET migrant du legacy vers .NET 8  
**Durée session** : 2h30

**Objectif pédagogique** :
- Comprendre le principe IoC (Inversion of Control)
- Différencier code sans DI (couplage fort) vs avec DI (couplage faible)
- Visualiser le rôle du conteneur IoC comme un "serveur au restaurant"

---

## 🎨 Contenu Visuel à Générer

### Titre de l'infographie
**"Injection de Dépendances - Le Serveur au Restaurant"**

### Métaphore Centrale : Le Restaurant

**Scénario SANS DI (AVANT - Legacy)** :
- **Visuel** : Un client (classe) qui entre dans la cuisine du restaurant
- Le client doit :
  - Cultiver le blé pour faire la farine 🌾
  - Élever la vache pour le fromage 🐄
  - Planter les tomates 🍅
  - Cuisiner lui-même la pizza 👨‍🍳
- **Temps total** : 6 mois
- **Problème** : Le client connaît TOUTE la chaîne de production (couplage fort)
- **Code visuel** : `new Pizza(new Pâte(new Blé()), new Fromage(new Vache()), new Sauce(new Tomates()))`

**Scénario AVEC DI (APRÈS - .NET 8)** :
- **Visuel** : Un client (classe) assis à une table
- Le serveur (IoC Container) apporte la pizza toute prête 🍕
- **Temps total** : 15 minutes
- **Avantage** : Le client ne connaît PAS la cuisine (couplage faible)
- **Code visuel** : `MapPost("/api/validate", (IClientValidator validator) => { ... })` ← Le serveur (IoC) injecte `validator`

### Éléments Visuels Clés

**Zone 1 - AVANT (Sans DI)** :
- Icône : ❌ (rouge)
- Titre : "Sans Injection de Dépendances (Legacy)"
- Illustration : Client dans la cuisine, en train de tout faire lui-même
- Ligne de code : `var repo = new ClientRepository(new SqlConnection("..."))`
- Temps : "6 mois pour une pizza"
- Problème : "Couplage fort - Impossible de tester sans vraie cuisine"

**Zone 2 - APRÈS (Avec DI)** :
- Icône : ✅ (vert)
- Titre : "Avec Injection de Dépendances (.NET 8)"
- Illustration : Client assis, serveur qui apporte la pizza
- Ligne de code : `builder.Services.AddScoped<IClientRepository, ClientRepository>();`
- Temps : "15 minutes"
- Avantage : "Couplage faible - Testable avec un faux serveur (mock)"

**Zone 3 - Le Serveur IoC (Rôle Central)** :
- Icône : 🧑‍🍳 (serveur/chef)
- Titre : "Le Conteneur IoC = Le Serveur"
- Rôle : "Le serveur connaît toutes les recettes (ServiceCollection)"
- Exemples :
  - Client demande `IClientValidator` → Serveur apporte `ClientValidator`
  - Client demande `IClientRepository` → Serveur apporte `ClientRepository`

### Statistiques Clés à Afficher

- **Avant (Sans DI)** : 50+ lignes à modifier pour changer de base de données
- **Après (Avec DI)** : 1 seule ligne à modifier
- **Gain** : 50x moins de couplage

---

## 🎯 Messages Clés à Faire Passer

1. **DI = Ne plus créer les dépendances directement**
   - AVANT : `new ClientRepository(...)` partout dans le code
   - APRÈS : Le conteneur IoC injecte automatiquement

2. **Le conteneur IoC est un serveur**
   - Vous commandez (déclarez les besoins)
   - Le serveur apporte (injecte les dépendances)

3. **Testabilité garantie**
   - AVANT : Impossible de tester sans vraie DB SQL
   - APRÈS : On injecte un `FakeRepository` pour les tests

---

## 🎨 Principes de Design à Respecter

### Couleurs
- **Zone AVANT (Sans DI)** : Rouge/Orange (danger, anti-pattern)
- **Zone APRÈS (Avec DI)** : Vert (success, best practice)
- **Conteneur IoC** : Bleu (neutre, infrastructure)

### Police et Lisibilité
- **Titre principal** : Grande taille, gras
- **Code** : Police monospace (Courier ou Consolas)
- **Texte explicatif** : Police sans-serif (Arial ou Roboto)

### Layout
- **Orientation** : Landscape (paysage)
- **Structure** : Deux colonnes (AVANT vs APRÈS) avec le Conteneur IoC au centre ou en bas

### Principes Cognitifs
- **Contiguïté spatiale** : Mettre le code et l'illustration côte à côte
- **Signalisation** : Utiliser ❌ pour AVANT, ✅ pour APRÈS
- **Cohérence** : Même style visuel que l'infographie Jour 1 Session 3 (Domain Isolation)

---

## 🚫 Ce qu'il faut ÉVITER

- Images purement décoratives (photos de restaurants réels)
- Texte en légendes séparées (causes attention divisée)
- Surcharge visuelle (trop de détails techniques)
- Design réaliste (privilégier le schématique)

---

## 📝 Prompt Complet pour NotebookLM

```
Crée une infographie pédagogique en orientation LANDSCAPE (paysage) sur le thème "Injection de Dépendances - Le Serveur au Restaurant".

OBJECTIF : Expliquer visuellement le principe d'Injection de Dépendances en .NET 8 avec la métaphore du restaurant.

CONTENU :

1. ZONE GAUCHE (AVANT - Sans DI) :
   - Titre : "❌ Sans Injection de Dépendances (Legacy)"
   - Illustration : Client dans la cuisine qui cultive le blé, élève la vache, plante les tomates pour faire une pizza
   - Code : var repo = new ClientRepository(new SqlConnection("..."));
   - Temps : 6 mois pour une pizza
   - Problème : Couplage fort - Impossible de tester sans vraie cuisine

2. ZONE DROITE (APRÈS - Avec DI) :
   - Titre : "✅ Avec Injection de Dépendances (.NET 8)"
   - Illustration : Client assis à une table, serveur qui apporte la pizza toute prête
   - Code : builder.Services.AddScoped<IClientRepository, ClientRepository>();
   - Temps : 15 minutes
   - Avantage : Couplage faible - Testable avec un faux serveur (mock)

3. ZONE CENTRALE/BAS (Le Serveur IoC) :
   - Titre : "Le Conteneur IoC = Le Serveur"
   - Rôle : Le serveur connaît toutes les recettes (ServiceCollection)
   - Exemples :
     * Client demande IClientValidator → Serveur apporte ClientValidator
     * Client demande IClientRepository → Serveur apporte ClientRepository

STATISTIQUES :
- Avant : 50+ lignes à modifier pour changer de DB
- Après : 1 seule ligne à modifier
- Gain : 50x moins de couplage

MESSAGES CLÉS :
1. DI = Ne plus créer les dépendances directement
2. Le conteneur IoC est un serveur qui apporte ce que vous demandez
3. Testabilité garantie (injection de fakes pour les tests)

DESIGN :
- Orientation : Landscape (paysage)
- Couleurs : Rouge/Orange pour AVANT, Vert pour APRÈS, Bleu pour IoC
- Style : Schématique simple, éviter le réalisme
- Code : Police monospace
- Layout : Deux colonnes (AVANT vs APRÈS) avec IoC au centre/bas

ÉVITER :
- Photos réelles de restaurants
- Texte en légendes séparées
- Surcharge visuelle
- Design complexe

Utilise la contiguïté spatiale (code + illustration côte à côte), la signalisation (❌✅), et la cohérence visuelle.
```

---

## 📝 Instructions Génération

1. **Ouvre NotebookLM** : https://notebooklm.google.com/
2. **Accède au notebook e-learning** : ID `53cd7abb-73ff-4e0f-9835-078dd31cbd98`
3. **Studio → Créer une Infographie**
4. **Colle le prompt complet** (section ci-dessus)
5. **Configure** :
   - Type : **Professional**
   - Orientation : **Landscape** (paysage)
   - Niveau de détail : **Standard**
6. **Génère l'infographie**
7. **Télécharge** l'image PNG
8. **Renomme** : `J2_S1_Infographie_DI_Restaurant.png`
9. **Place** dans : `05_Ressources_Visuelles/`

---

**Fin du prompt**
