# Prompt Infographie - Jour 3 Session 3 : Modernisation Services Externes avec MailKit

**Destination** : NotebookLM - Génération Infographie  
**Format** : Landscape (Paysage)  
**Style** : Professionnel, technique, architecture logicielle

---

## Objectif de l'Infographie

Expliquer visuellement la différence entre l'ancien client SMTP Legacy (.NET Framework) et le nouveau client moderne MailKit (.NET 8).

**Message clé** : "SmtpClient est obsolète. MailKit est le standard moderne : asynchrone, sécurisé (TLS obligatoire), et découplé."

---

## Métaphore Visuelle : Vieille Camionnette vs Camion Électrique Moderne

**Concept** : Comparer deux systèmes de livraison d'emails

### Partie Gauche : ❌ AVANT (.NET Framework - SmtpClient)

**Scène visuelle** :
- Une vieille camionnette de livraison rouillée (SmtpClient)
- Un panneau "OBSOLÈTE" marqué Microsoft
- Des fils électriques apparents (pas de TLS par défaut)
- Code synchrone visible : `smtpClient.Send(message)` qui bloque un thread
- Un cadenas ouvert (TLS optionnel, parfois désactivé)
- Un pictogramme de thread bloqué ⏸️

**Texte** :
- Titre : "❌ AVANT - System.Net.Mail.SmtpClient"
- Sous-titre : "Obsolète depuis .NET Core"
- Problème 1 : "Bloque un thread pendant l'envoi (Send synchrone)"
- Problème 2 : "TLS/SSL optionnel → risque de faille"
- Problème 3 : "Microsoft ne maintient plus cette librairie"
- Problème 4 : "Impossible à tester (couplage fort avec SMTP)"

**Icônes à afficher** :
- ⚠️ Obsolète
- 🔓 Sécurité faible
- ⏸️ Thread bloqué
- 📦 Couplage fort

---

### Partie Droite : ✅ APRÈS (.NET 8 - MailKit)

**Scène visuelle divisée en 3 zones verticales** :

#### Zone 1 : Architecture Découplée (Haut)
- Interface `IEmailService` au centre
- Flèche vers implémentation `MailKitEmailService`
- Logo MailKit (envelope moderne)
- Annotation "Principe d'inversion des dépendances"

#### Zone 2 : Sécurité Obligatoire (Milieu)
- Cadenas fermé avec "TLS 1.2/1.3 OBLIGATOIRE"
- Code visible : `SecureSocketOptions.StartTls`
- Badge "RGPD Compliant"
- Icône chiffrement

#### Zone 3 : Asynchrone et Performant (Bas)
- Code async : `await client.SendAsync(message)`
- Thread libéré → pictogramme thread vert disponible
- Graphique montrant 1000 requests/sec vs 10 requests/sec
- Annotation "ThreadPool optimisé"

**Texte Zone MailKit** :
- "✅ MailKit - Standard Moderne .NET 8"
- "Asynchrone : await SendAsync() libère les threads"
- "TLS obligatoire : SecureSocketOptions.StartTls"
- "Testable : injection IEmailService"
- "Maintenu activement par la communauté"

---

### Centre : Pattern d'Architecture

**Zone centrale (pont entre Legacy et Moderne)** :
- Diagramme UML simplifié montrant :
  - Interface `IEmailService`
  - Méthode `Task SendAsync(string to, string subject, string body)`
  - Implémentation `MailKitEmailService`
  - Injection de `IOptions<EmailOptions>` (host, port, username, password)

**Code exemple** :
```csharp
public interface IEmailService
{
    Task SendAsync(string to, string subject, string body);
}

// Injection DI
builder.Services.AddTransient<IEmailService, MailKitEmailService>();
```

**Icônes** :
- 🔐 Sécurité TLS
- ⚡ Asynchrone
- 🧩 Découplé (DI)
- ✅ Testable
- 🚀 Performant

---

## Zones de Texte à Inclure

### Titre Principal (en haut)
"📧 Modernisation Email : SmtpClient Legacy → MailKit"

### Sous-titre
"De la camionnette rouillée au camion électrique moderne"

### Encadré "Commande Installation" (coin supérieur droit)
```bash
dotnet add package MailKit
```

### Encadré "Règle d'Or" (en bas)
📌 **Règle d'Or** : Ne JAMAIS utiliser `System.Net.Mail.SmtpClient` dans de nouveaux projets .NET 8. Toujours utiliser MailKit avec TLS obligatoire.

### Encadré "Migration Rapide"
⚡ **Migration** :
1. Installer MailKit
2. Créer interface IEmailService
3. Implémenter MailKitEmailService avec TLS
4. Injecter via DI (AddTransient)
5. Remplacer tous les `.Send()` par `await .SendAsync()`

---

## Palette de Couleurs Suggérée

- **Legacy (Gauche)** : Gris foncé / Marron (obsolète, rouille)
- **Moderne (Droite)** : Bleu électrique / Vert (moderne, performant)
- **Sécurité TLS** : Vert foncé avec cadenas doré
- **Zone Centrale Architecture** : Blanc cassé (neutre)

---

## Éléments Visuels Clés

1. **Contraste visuel** : Gauche terne (obsolète) vs Droite lumineuse (moderne)
2. **Flux de données** : Flèches montrant le cycle SMTP : Connect → Auth → Send → Disconnect
3. **Comparaison Performance** : Graphique comparant threads bloqués vs threads libérés
4. **Séparation claire** : Ligne verticale séparant Legacy et Moderne
5. **Hiérarchie visuelle** : Titre → Métaphore → Code → Architecture → Règle d'Or

---

## Instructions pour NotebookLM

Créez une infographie en format **paysage (landscape)** qui :
1. Utilise la métaphore de la camionnette rouillée (Legacy SmtpClient) vs camion électrique (MailKit)
2. Montre visuellement les 3 zones pour MailKit : Architecture, Sécurité, Performance
3. Illustre le pattern d'architecture avec interface IEmailService
4. Compare visuellement synchrone bloquant vs asynchrone non-bloquant
5. Met en évidence TLS obligatoire avec SecureSocketOptions
6. Affiche des extraits de code C# clés (interface, async/await, DI)
7. Inclut une section "Migration Rapide" en 5 étapes
8. Met en évidence la règle d'or en bas

**Orientation** : Landscape (Paysage) obligatoire  
**Style** : Professionnel, technique, architecture logicielle moderne  
**Public** : Développeurs .NET intermédiaires migrant du legacy vers .NET 8
