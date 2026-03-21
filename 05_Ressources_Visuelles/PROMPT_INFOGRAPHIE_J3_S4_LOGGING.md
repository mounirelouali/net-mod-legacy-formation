# Prompt Infographie - Jour 3 Session 4 : Logging Sécurisé avec Serilog

**Destination** : NotebookLM - Génération Infographie  
**Format** : Landscape (Paysage)  
**Style** : Professionnel, sécurité et observabilité

---

## Objectif de l'Infographie

Expliquer visuellement la différence entre les pratiques Legacy (Console.WriteLine + logs texte non structurés) et Modernes (Serilog + logs JSON structurés + masquage PII).

**Message clé** : "Logs structurés JSON + masquage PII = sécurité et observabilité"

---

## Métaphore Visuelle : Registre Papier Désorganisé vs Base de Données Sécurisée

**Concept** : Comparer deux systèmes de traçabilité

### Partie Gauche : ❌ AVANT (Legacy - Console.WriteLine)

**Scène visuelle** :
- Un registre papier en désordre avec des notes manuscrites illisibles
- Code visible : `Console.WriteLine($"User {email} logged in with password {password}");`
- Un mot de passe en clair visible dans le registre "Password=Admin123!"
- Des lignes de texte non structurées difficiles à lire
- Un développeur qui cherche une aiguille dans une botte de foin

**Texte** :
- Titre : "❌ AVANT - Console.WriteLine() Non Structuré"
- Sous-titre : "Logs texte impossible à requêter"
- Problème 1 : "Logs non structurés (impossible à indexer)"
- Problème 2 : "PII en clair (emails, mots de passe, tokens)"
- Problème 3 : "Interpolation $\"...\" crée un nouveau template à chaque fois"
- Problème 4 : "Impossible de filtrer par niveau (Info, Warning, Error)"

**Icônes à afficher** :
- 📝 Texte non structuré
- 🔓 Données sensibles exposées
- 🔍 Recherche impossible
- ⚠️ Fuite RGPD

---

### Partie Droite : ✅ APRÈS (.NET 8 - Serilog JSON)

**Scène visuelle divisée en 3 zones** :

#### Zone 1 : Logs Structurés JSON (Haut)
- Code JSON structuré visible :
```json
{
  "@timestamp": "2026-03-20T22:00:00Z",
  "@level": "Information",
  "@message": "User {UserEmail} logged in",
  "UserEmail": "jo***@example.com",
  "RequestId": "abc-123"
}
```
- Annotation "Indexable et requêtable"
- Logo Serilog

#### Zone 2 : Masquage PII (Milieu)
- Avant : `john.doe@example.com` + `Password=Admin123!`
- Après : `jo***@example.com` + `[REDACTED]`
- Code masquage visible :
```csharp
string maskedEmail = email.Substring(0,2) + "***@" + domain;
// Ne JAMAIS logger les mots de passe
```
- Badge "RGPD Compliant"

#### Zone 3 : Validation Inputs (Bas)
- Code Data Annotations visible :
```csharp
[Required]
[EmailAddress]
[StringLength(100, MinimumLength=8)]
public string Email { get; set; }
```
- Annotation "Validation stricte avant traitement"
- Icône bouclier de protection

**Texte Zone Serilog** :
- "✅ Serilog - Logging Moderne Structuré"
- "Logs JSON indexables (ElasticSearch, Seq)"
- "Template constant : _logger.LogInfo(\"User {UserId}\", id)"
- "Masquage automatique des PII"
- "Niveaux de log : Debug, Info, Warning, Error, Critical"

---

### Centre : Pipeline de Sécurité

**Zone centrale (flux de gauche à droite)** :

Flux en 4 étapes :
1. **Input** → Validation Data Annotations ([Required], [EmailAddress])
2. **Processing** → Code métier
3. **Logging** → Serilog avec template structuré
4. **Storage** → JSON dans fichier/Seq (PII masqués)

**Code exemple** :
```csharp
// Configuration Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(new JsonFormatter())
    .WriteTo.File(new JsonFormatter(), "logs/app.json")
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();

// Log structuré
_logger.LogInformation("Payment {Amount} for card {MaskedCard}", 
    request.Amount, MaskCreditCard(request.CardNumber));
```

**Icônes** :
- 🔐 Validation stricte
- 📊 Logs structurés
- 🎭 Masquage PII
- 🔍 Requêtable
- ✅ RGPD compliant

---

## Zones de Texte à Inclure

### Titre Principal (en haut)
"🔒 Logging Sécurisé : Console.WriteLine → Serilog JSON + Masquage PII"

### Sous-titre
"Du registre papier désorganisé à la base de données sécurisée"

### Encadré "Installation" (coin supérieur droit)
```bash
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.File
dotnet add package Serilog.Sinks.Seq
```

### Encadré "Anti-Patterns à Éviter" (milieu gauche)
❌ **NE JAMAIS FAIRE** :
```csharp
// Interpolation (crée un nouveau template)
_logger.LogInfo($"User {userId} login");

// Logger des PII en clair
_logger.LogInfo("Password: {Password}", password);
```

### Encadré "Best Practices" (milieu droit)
✅ **TOUJOURS FAIRE** :
```csharp
// Template constant
_logger.LogInformation("User {UserId} login", userId);

// Masquer les PII
_logger.LogInfo("User {MaskedEmail}", MaskEmail(email));
```

### Encadré "Règle d'Or" (en bas)
📌 **Règle d'Or** : 
1. Utiliser Serilog avec logs structurés JSON
2. Ne JAMAIS logger de PII en clair (emails, mots de passe, numéros de carte)
3. Valider TOUS les inputs avec Data Annotations
4. Utiliser des templates constants (pas d'interpolation $"...")

---

## Palette de Couleurs Suggérée

- **Legacy (Gauche)** : Rouge / Orange (danger, chaos)
- **Moderne (Droite)** : Bleu / Vert (sécurité, organisation)
- **Masquage PII** : Violet (protection données)
- **Validation** : Vert foncé (bouclier)
- **Zone Centrale Pipeline** : Gris clair (flux neutre)

---

## Éléments Visuels Clés

1. **Contraste visuel** : Gauche chaotique (papier désorganisé) vs Droite organisée (base de données structurée)
2. **Flux de données** : Pipeline de sécurité en 4 étapes avec flèches
3. **Avant/Après masquage** : Comparaison visuelle claire des PII
4. **Code JSON structuré** : Exemple JSON bien formaté et coloré
5. **Hiérarchie visuelle** : Titre → Métaphore → Pipeline → Code → Règles

---

## Données à Masquer (Exemples visuels)

**Avant masquage** :
- Email : `john.doe@company.com`
- Mot de passe : `Admin123!`
- Carte bancaire : `4532-1234-5678-9012`
- Token : `eyJhbGciOiJIUzI1NiIs...`

**Après masquage** :
- Email : `jo***@company.com`
- Mot de passe : `[REDACTED]`
- Carte bancaire : `****-****-****-9012`
- Token : `[REDACTED]`

---

## Instructions pour NotebookLM

Créez une infographie en format **paysage (landscape)** qui :
1. Utilise la métaphore du registre papier désorganisé (Legacy) vs base de données sécurisée (Serilog)
2. Montre visuellement les 3 zones pour Serilog : Logs Structurés, Masquage PII, Validation
3. Illustre le pipeline de sécurité en 4 étapes : Input → Processing → Logging → Storage
4. Compare visuellement Console.WriteLine non structuré vs Serilog JSON structuré
5. Met en évidence le masquage PII avec exemples Avant/Après
6. Affiche des extraits de code C# clés (Data Annotations, Serilog config, templates)
7. Inclut les anti-patterns et best practices côte à côte
8. Met en évidence la règle d'or en 4 points en bas

**Orientation** : Landscape (Paysage) obligatoire  
**Style** : Professionnel, sécurité et observabilité, RGPD  
**Public** : Développeurs .NET intermédiaires migrant du legacy vers .NET 8
