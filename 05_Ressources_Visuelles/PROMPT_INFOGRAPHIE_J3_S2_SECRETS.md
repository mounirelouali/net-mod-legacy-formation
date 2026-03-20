# Prompt Infographie - Jour 3 Session 2 : Gestion des Secrets avec .NET 8

**Destination** : NotebookLM - Génération Infographie  
**Format** : Landscape (Paysage)  
**Style** : Professionnel, éducatif, métaphore visuelle

---

## Objectif de l'Infographie

Expliquer visuellement la différence entre les pratiques Legacy (.NET Framework) et Modernes (.NET 8) pour la gestion des secrets (mots de passe, API keys, connection strings).

**Message clé** : "Ne jamais commiter les secrets sur Git. Utiliser User Secrets en Dev, Variables d'Environnement en Production."

---

## Métaphore Visuelle : Le Coffre-Fort vs Le Paillasson

**Concept** : Comparer deux approches pour protéger les clés de la maison (secrets)

### Partie Gauche : ❌ AVANT (.NET Framework - Le Paillasson)

**Scène visuelle** :
- Une maison avec un paillasson devant la porte
- Sous le paillasson : une clé visible (étiquetée "Password=MotDePasseEnClair123!")
- À côté : un fichier `Web.config` ouvert avec du XML visible contenant `<add key="SmtpPassword" value="MotDePasseEnClair123!" />`
- Un logo GitHub avec une flèche pointant vers le fichier config
- Un personnage malveillant (hacker) qui trouve facilement la clé sous le paillasson

**Texte** :
- Titre : "❌ AVANT - .NET Framework"
- Sous-titre : "Secrets hardcodés dans Web.config"
- Problème 1 : "Mots de passe en clair dans le code source"
- Problème 2 : "Committé sur Git = accessible à tous"
- Problème 3 : "Impossible de changer sans recompiler"

**Icônes à afficher** :
- 🔓 Paillasson (non sécurisé)
- 📁 Web.config
- ⚠️ Danger
- 💻 Git (avec X rouge)

---

### Partie Droite : ✅ APRÈS (.NET 8 - Le Coffre-Fort)

**Scène visuelle divisée en 2 zones** :

#### Zone 1 : Développement (en haut)
- Un badge temporaire de chantier (User Secrets)
- Un fichier `secrets.json` dans un dossier `%AppData%\Microsoft\UserSecrets\` (hors du projet Git)
- Une commande terminal visible : `dotnet user-secrets set "Smtp:Password" "MonSecret"`
- Un dossier Git avec un `.gitignore` qui bloque `secrets.json`

**Texte Zone Dev** :
- "✅ Développement : User Secrets"
- "Stockés hors Git (%AppData%)"
- "Commande : `dotnet user-secrets init`"
- "Texte clair local uniquement"

#### Zone 2 : Production (en bas)
- Un coffre-fort biométrique (Azure Key Vault)
- Des variables d'environnement flottant dans un cloud
- Un serveur avec un cadenas
- Une injection directe en mémoire (RAM)

**Texte Zone Prod** :
- "✅ Production : Variables d'Environnement ou Key Vault"
- "Secrets injectés en mémoire"
- "Rotation automatique des clés"
- "Chiffrement asymétrique"

---

### Centre : Le Pattern Unifié IOptions<T>

**Zone centrale (pont entre Dev et Prod)** :
- Un code C# avec `IOptions<SmtpOptions>`
- Une flèche montrant que le même code fonctionne en Dev ET en Prod
- Texte : "Le code reste identique, seule la source change"

```csharp
public EmailService(IOptions<SmtpOptions> options)
{
    _password = options.Value.Password;
}
```

**Icônes** :
- 🔐 Coffre-fort
- 🔑 Clé sécurisée
- ☁️ Cloud (Azure Key Vault)
- 💾 Variables d'environnement
- ✅ Checkmark (sécurisé)

---

## Zones de Texte à Inclure

### Titre Principal (en haut)
"🔐 Gestion des Secrets : .NET Framework vs .NET 8"

### Sous-titre
"De la clé sous le paillasson au coffre-fort biométrique"

### Encadré "Règle d'Or" (en bas)
📌 **Règle d'Or** : Ne JAMAIS commiter un secret sur Git. Utiliser User Secrets (Dev) ou Key Vault (Prod).

### Encadré "Erreur Fréquente"
⚠️ **Piège** : User Secrets ne fonctionne qu'en mode Development. En production, utiliser des variables d'environnement ou Azure Key Vault.

---

## Palette de Couleurs Suggérée

- **Legacy (Gauche)** : Rouge / Orange (danger)
- **Moderne Dev (Droite Haut)** : Bleu clair (développement)
- **Moderne Prod (Droite Bas)** : Vert foncé (sécurité production)
- **Zone Centrale** : Gris neutre (code unifié)

---

## Éléments Visuels Clés

1. **Contraste visuel** : Gauche chaotique (paillasson, clé visible) vs Droite organisée (coffre-fort, cadenas)
2. **Flux de données** : Flèches montrant le chemin des secrets du stockage vers l'application
3. **Séparation claire** : Ligne verticale séparant Legacy et Moderne
4. **Hiérarchie visuelle** : Titre → Métaphore → Code → Règle d'Or

---

## Instructions pour NotebookLM

Créez une infographie en format **paysage (landscape)** qui :
1. Utilise la métaphore du paillasson (Legacy) vs coffre-fort (Moderne)
2. Montre visuellement les 3 zones : Legacy, Dev (.NET 8), Prod (.NET 8)
3. Illustre le pattern `IOptions<T>` comme pont unifié entre Dev et Prod
4. Inclut des icônes claires pour chaque concept (paillasson, coffre-fort, cloud, badge)
5. Utilise des couleurs distinctes pour chaque zone
6. Affiche des extraits de code et commandes clés
7. Met en évidence la règle d'or en bas

**Orientation** : Landscape (Paysage) obligatoire  
**Style** : Professionnel et éducatif avec métaphore visuelle forte  
**Public** : Développeurs .NET intermédiaires migrant du legacy vers .NET 8
