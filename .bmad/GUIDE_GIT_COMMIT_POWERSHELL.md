# 🔧 Guide : Résolution Problème Git Commit Bloquant sur PowerShell

## 🚨 Problème Identifié

**Symptôme** : Lors d'un `git commit -m "..."` avec un message long/multi-lignes, PowerShell se bloque et attend une entrée manuelle (appui sur Enter).

**Cause** : Les messages de commit trop longs avec sauts de ligne `\n` créent une chaîne multi-lignes dans PowerShell qui n'est pas fermée correctement, provoquant une attente de confirmation.

---

## ✅ Solutions Testées et Validées

### **Solution 1 : Messages Courts (1 ligne titre + lignes séparées)**

**Format recommandé** :
```powershell
git commit -m "feat(J3): Titre court descriptif" -m "Détail 1" -m "Détail 2" -m "Détail 3"
```

**Avantages** :
- ✅ Pas de blocage
- ✅ Format Git conventionnel respecté
- ✅ Compatible PowerShell natif

**Limites** :
- ❌ Moins lisible pour messages très longs
- ❌ Nécessite plusieurs `-m`

---

### **Solution 2 : Fichier Temporaire pour Messages Longs (RECOMMANDÉE)**

**Implémentation** :
```powershell
# 1. Créer le message dans une variable here-string
$commitMessage = @"
feat(J3): Ajout Démonstrations Live + Guides Infographies

DÉMONSTRATIONS LIVE (4 sessions ajoutées) :
- Session 1 (IOptions) : EmailNotificationService → IOptions (15 min)
- Session 2 (User Secrets) : Hardcodé → User Secrets (20 min)

GUIDES PRÉSENTATION :
- 3 guides créés avec scripts détaillés
"@

# 2. Écrire dans fichier temporaire
$commitMessage | Out-File -Encoding utf8 -FilePath "commit-msg.txt"

# 3. Commit avec fichier
git commit -F "commit-msg.txt"

# 4. Nettoyer
Remove-Item "commit-msg.txt"
```

**Avantages** :
- ✅ Aucun blocage
- ✅ Messages aussi longs que nécessaire
- ✅ Format multi-lignes préservé
- ✅ Automatisable dans scripts PowerShell

**Limites** :
- ❌ Fichier temporaire à nettoyer

---

### **Solution 3 : Here-String Direct (Alternative)**

```powershell
git commit -m @"
feat(J3): Titre

Détails ligne 1
Détails ligne 2
"@
```

**Statut** : ⚠️ Peut encore bloquer selon versions PowerShell

---

## 📋 Règle Automatique pour Agent Cascade

**RÈGLE OBLIGATOIRE** : Lors de commits Git via PowerShell, utiliser **Solution 2** (fichier temporaire) pour tous messages > 100 caractères.

**Implémentation automatique** :
```powershell
function Safe-GitCommit {
    param([string]$Message)
    
    if ($Message.Length -gt 100) {
        # Message long : fichier temporaire
        $Message | Out-File -Encoding utf8 "commit-msg.txt"
        git commit -F "commit-msg.txt"
        Remove-Item "commit-msg.txt"
    } else {
        # Message court : direct
        git commit -m $Message
    }
}
```

---

## ✅ Validation

**Test réussi** :
```powershell
$msg = @"
feat(J3): Test commit long
Ligne 1
Ligne 2
Ligne 3
"@
$msg | Out-File -Encoding utf8 commit-msg.txt
git commit -F commit-msg.txt
Remove-Item commit-msg.txt
```

**Résultat** : ✅ Commit passé sans blocage, message complet préservé.

---

## 📝 Conclusion

**Problème résolu** : Utiliser `git commit -F fichier` pour messages longs au lieu de `-m "..."` direct.

**Impact** : Agent Cascade peut maintenant faire des commits longs sans intervention utilisateur manuelle.
