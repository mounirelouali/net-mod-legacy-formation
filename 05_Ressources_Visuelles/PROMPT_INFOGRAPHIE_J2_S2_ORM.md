# Prompt Infographie NotebookLM - Jour 2 Session 2 : Entity Framework Core 8

**Objectif** : Générer une infographie pédagogique pour expliquer ORM vs SQL raw avec la métaphore du traducteur automatique.

**Fichier cible** : `J2_S2_Infographie_ORM_Traducteur.png`

---

## 📋 Contexte Pédagogique

**Session** : Jour 2 - Session 2 (10h40)  
**Thème** : Entity Framework Core 8 - ORM vs SQL raw  
**Public** : Développeurs .NET migrant du legacy vers .NET 8  
**Durée session** : 3h

**Objectif pédagogique** :
- Comprendre le rôle d'un ORM (Object-Relational Mapping)
- Différencier code SQL raw (15 lignes) vs EF Core (1 ligne)
- Visualiser EF Core comme un "traducteur automatique" (Google Translate)

---

## 🎨 Contenu Visuel à Générer

### Titre de l'infographie
**"ORM - Le Traducteur Automatique C# → SQL"**

### Métaphore Centrale : Google Translate

**Scénario SANS ORM (AVANT - Legacy)** :
- **Visuel** : Développeur qui épelle des lettres au téléphone en italien 📞
- Le développeur doit :
  - Écrire SQL à la main : `SELECT Id, Name, Email FROM Clients WHERE Id = @id`
  - Gérer SqlConnection manuellement
  - Mapper les résultats ligne par ligne avec `reader.GetInt32(0)`, `reader.GetString(1)`
  - Gérer les fuites mémoire (oubli de `.Dispose()`)
- **Lignes de code** : 15 lignes pour lire 1 client
- **Problème** : Risque injection SQL, bugs de mapping, code verbeux
- **Code visuel** : 
  ```csharp
  var command = new SqlCommand("SELECT ...", connection);
  var reader = command.ExecuteReader();
  client = new Client(reader.GetInt32(0), reader.GetString(1), ...);
  ```

**Scénario AVEC ORM (APRÈS - EF Core 8)** :
- **Visuel** : Développeur qui parle français, Google Translate traduit automatiquement en chinois 🌍
- **Analogie** : EF Core = Google Translate pour la base de données
  - Vous parlez C# (LINQ) → EF Core traduit en SQL automatiquement
  - `db.Clients.FirstOrDefault(c => c.Id == 1)` → `SELECT [Id], [Name], [Email] FROM [Clients] WHERE [Id] = 1`
- **Lignes de code** : 1 ligne pour lire 1 client
- **Avantage** : Zéro injection SQL, type-safety, productivité 15x
- **Code visuel** :
  ```csharp
  var client = db.Clients.FirstOrDefault(c => c.Id == 1);
  ```

### Éléments Visuels Clés

**Zone 1 - AVANT (Sans ORM)** :
- Icône : ❌ (rouge)
- Titre : "Sans ORM - SQL raw (Legacy)"
- Illustration : Développeur qui épelle "S comme Sierra, E comme Echo, L comme Lima..."
- Ligne de code : `var client = new Client(reader.GetInt32(0), reader.GetString(1), ...)`
- Lignes : 15 lignes
- Problème : "Injection SQL, bugs de mapping, fuites mémoire"

**Zone 2 - APRÈS (Avec ORM)** :
- Icône : ✅ (vert)
- Titre : "Avec ORM - Entity Framework Core 8"
- Illustration : Google Translate qui traduit "Français (C#)" → "Chinois (SQL)"
- Ligne de code : `var client = db.Clients.FirstOrDefault(c => c.Id == 1);`
- Lignes : 1 ligne
- Avantage : "Type-safety, zéro injection SQL, 15x plus rapide à écrire"

**Zone 3 - Le Traducteur EF Core (Rôle Central)** :
- Icône : 🌍 (traducteur)
- Titre : "Entity Framework Core = Google Translate pour SQL"
- Rôle : "Traduit automatiquement LINQ (C#) → SQL"
- Exemples :
  - `db.Clients.ToList()` → `SELECT * FROM Clients`
  - `db.Clients.Add(client)` → `INSERT INTO Clients (...) VALUES (...)`
  - `db.SaveChanges()` → `UPDATE Clients SET ... WHERE Id = ...`

### Statistiques Clés à Afficher

- **Avant (SQL raw)** : 15 lignes de code pour 1 lecture
- **Après (EF Core)** : 1 ligne de code
- **Gain** : 15x moins de code, zéro injection SQL

---

## 🎯 Messages Clés à Faire Passer

1. **ORM = Traducteur automatique**
   - AVANT : Vous écrivez SQL à la main (15 lignes)
   - APRÈS : EF Core traduit LINQ → SQL automatiquement (1 ligne)

2. **EF Core est comme Google Translate**
   - Vous parlez C# (LINQ)
   - EF Core traduit en SQL
   - Résultat : zéro erreur de traduction (type-safety)

3. **Sécurité garantie**
   - AVANT : Risque injection SQL
   - APRÈS : Requêtes paramétrées automatiques

---

## 🎨 Principes de Design à Respecter

### Couleurs
- **Zone AVANT (Sans ORM)** : Rouge/Orange (danger, anti-pattern)
- **Zone APRÈS (Avec ORM)** : Vert (success, best practice)
- **EF Core Traducteur** : Bleu (neutre, infrastructure)

### Police et Lisibilité
- **Titre principal** : Grande taille, gras
- **Code** : Police monospace (Courier ou Consolas)
- **Texte explicatif** : Police sans-serif (Arial ou Roboto)

### Layout
- **Orientation** : Landscape (paysage)
- **Structure** : Deux colonnes (AVANT vs APRÈS) avec EF Core Traducteur au centre ou en bas

### Principes Cognitifs
- **Contiguïté spatiale** : Mettre le code et l'illustration côte à côte
- **Signalisation** : Utiliser ❌ pour AVANT, ✅ pour APRÈS
- **Cohérence** : Même style visuel que les infographies Jour 1 et Jour 2 Session 1

---

## 🚫 Ce qu'il faut ÉVITER

- Images purement décoratives (photos de bases de données réelles)
- Texte en légendes séparées (causes attention divisée)
- Surcharge visuelle (trop de détails SQL)
- Design réaliste (privilégier le schématique)

---

## 📝 Prompt Complet pour NotebookLM

```
Crée une infographie pédagogique en orientation LANDSCAPE (paysage) sur le thème "ORM - Le Traducteur Automatique C# → SQL".

OBJECTIF : Expliquer visuellement le rôle d'Entity Framework Core 8 comme traducteur automatique entre C# (LINQ) et SQL.

CONTENU :

1. ZONE GAUCHE (AVANT - Sans ORM) :
   - Titre : "❌ Sans ORM - SQL raw (Legacy)"
   - Illustration : Développeur qui épelle des lettres au téléphone en italien "S comme Sierra, E comme Echo..."
   - Code : var client = new Client(reader.GetInt32(0), reader.GetString(1), ...);
   - Lignes : 15 lignes pour lire 1 client
   - Problème : Injection SQL, bugs de mapping, fuites mémoire

2. ZONE DROITE (APRÈS - Avec ORM) :
   - Titre : "✅ Avec ORM - Entity Framework Core 8"
   - Illustration : Google Translate qui traduit "Français (C#)" → "Chinois (SQL)"
   - Code : var client = db.Clients.FirstOrDefault(c => c.Id == 1);
   - Lignes : 1 ligne
   - Avantage : Type-safety, zéro injection SQL, 15x plus rapide

3. ZONE CENTRALE/BAS (EF Core Traducteur) :
   - Titre : "Entity Framework Core = Google Translate pour SQL"
   - Rôle : Traduit automatiquement LINQ (C#) → SQL
   - Exemples :
     * db.Clients.ToList() → SELECT * FROM Clients
     * db.Clients.Add(client) → INSERT INTO Clients (...) VALUES (...)
     * db.SaveChanges() → UPDATE Clients SET ... WHERE Id = ...

STATISTIQUES :
- Avant : 15 lignes de code pour 1 lecture
- Après : 1 ligne de code
- Gain : 15x moins de code, zéro injection SQL

MESSAGES CLÉS :
1. ORM = Traducteur automatique (LINQ → SQL)
2. EF Core est comme Google Translate
3. Sécurité garantie (requêtes paramétrées auto)

DESIGN :
- Orientation : Landscape (paysage)
- Couleurs : Rouge/Orange pour AVANT, Vert pour APRÈS, Bleu pour EF Core
- Style : Schématique simple, éviter le réalisme
- Code : Police monospace
- Layout : Deux colonnes (AVANT vs APRÈS) avec EF Core au centre/bas

ÉVITER :
- Photos réelles de bases de données
- Texte en légendes séparées
- Surcharge visuelle
- Design complexe

Utilise la contiguïté spatiale (code + illustration côte à côte), la signalisation (❌✅), et la cohérence visuelle avec les infographies précédentes (Jour 1 S3 Domain Isolation, Jour 2 S1 DI Restaurant).
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
8. **Renomme** : `J2_S2_Infographie_ORM_Traducteur.png`
9. **Place** dans : `05_Ressources_Visuelles/`

---

**Fin du prompt**
