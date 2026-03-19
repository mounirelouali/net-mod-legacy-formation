# Prompt Infographie NotebookLM - Session 13h30 : Domain Isolation

**À coller dans NotebookLM** → Studio → Créer une infographie

---

## 📋 Instructions de Génération

**Type d'infographie** : Professional  
**Orientation** : Landscape (paysage)  
**Niveau de détail** : Standard  
**Style visuel** : Professional

---

## 🎯 Prompt Complet

```
Créer une infographie pédagogique pour expliquer l'isolation du Domain dans la Clean Architecture .NET 8.

OBJECTIF DE PERFORMANCE :
Les développeurs .NET doivent comprendre POURQUOI isoler le Domain (zéro dépendance externe) pour le rendre testable en 15 millisecondes sans infrastructure.

AUDIENCE :
Développeurs .NET intermédiaires (3-4 ans d'expérience) en formation présentielle.

CONTEXTE D'UTILISATION :
L'infographie sera affichée à l'écran pendant 10 minutes lors d'une session de formation.

CONTENU À VISUALISER :

1. MÉTAPHORE CENTRALE : "L'Île Stérile"
   - Le Domain est comme une île stérile qui ne doit jamais être contaminée par des dépendances externes
   - Visuellement : Une île isolée (Domain) vs un continent connecté (Infrastructure)

2. COMPARAISON AVANT/APRÈS :

   AVANT (Code Legacy) :
   - Program.cs monolithique couplé à SQL Server, SMTP, XML
   - Impossible de tester sans infrastructure
   - Temps de test : 10 minutes (lancer SQL + SMTP + insérer données)
   - Représentation visuelle : Un bloc monolithique avec des câbles partout

   APRÈS (Domain Isolé) :
   - Domain pur avec Client (record), IValidationRule, 3 règles de validation
   - Testable sans infrastructure
   - Temps de test : 15 millisecondes
   - Représentation visuelle : Un bloc Domain propre + flèche vers Infrastructure (sens unique)

3. PRINCIPE CLÉ : Inversion de Dépendances
   - Infrastructure dépend de Domain (flèche Infrastructure → Domain)
   - Domain NE dépend JAMAIS de Infrastructure (croix rouge sur flèche inverse)
   - Schéma : 2 cercles (Domain au centre, Infrastructure autour) avec flèches unidirectionnelles

4. GAIN MESURABLE :
   - Temps de feedback : 10 minutes → 15ms
   - Gain : ×40 000 plus rapide
   - Représentation : Graphique en barres (Legacy vs Domain) avec ratio visuel impressionnant

5. STRUCTURE DOMAIN :
   - 3 dossiers : Entities (Client record), Interfaces (IValidationRule), Rules (MinLength, MaxLength, Mandatory)
   - Diagramme de classes simplifié avec relations

PRINCIPES DE DESIGN À RESPECTER :

1. Contiguïté spatiale : Texte explicatif EXACTEMENT à côté de l'élément visuel correspondant
2. Signalisation : Utiliser flèches, cercles, contrastes de couleurs pour guider l'œil
3. Cohérence : Éliminer tout élément décoratif qui ne sert pas l'objectif pédagogique
4. Visuels instructifs : Schémas simples plutôt que photographies complexes
5. Espaces blancs : Laisser respirer le contenu
6. Alignement parfait : Texte et icônes bien centrés et synchronisés
7. Contraste élevé : Assurer la lisibilité

ÉLÉMENTS VISUELS À PRIVILÉGIER :
- Schémas organisationnels (arborescence des dossiers Domain)
- Diagrammes relationnels (flèches de dépendances)
- Graphiques quantitatifs (10 min vs 15ms)
- Codes couleur : Vert (Domain isolé ✅), Rouge (couplage Legacy ❌)

MESSAGES TEXTUELS CLÉS À INTÉGRER :
- "Domain = Île Stérile : Zéro Dépendance Externe"
- "Legacy : 10 minutes de test | Domain : 15 millisecondes"
- "Infrastructure → Domain (✅) | Domain → Infrastructure (❌)"
- "Testable sans SQL, sans SMTP, sans XML"

ÉVITER ABSOLUMENT :
- Images purement décoratives (photos de serveurs, d'ordinateurs)
- Texte en légendes séparées (causes attention divisée)
- Surcharge visuelle (trop d'informations)
- Design complexe ou réaliste (privilégier le schématique simple)
```

---

## 📝 Notes pour Toi

**Après génération** :
1. Télécharge l'infographie (PNG)
2. Renomme-la : `J1_S3_Infographie_Domain_Isolation.png`
3. Place-la dans : `05_Ressources_Visuelles/`
4. Partage-la avec moi (via chat ou dossier)

**Je l'intégrerai dans** :
- `03_Support_Quotidien/Jour_1_Fondations.md` (Section 13h30, après les diagrammes Mermaid)
- Format Markdown : `![Domain Isolation](../05_Ressources_Visuelles/J1_S3_Infographie_Domain_Isolation.png)`

---

**Fin du prompt**
