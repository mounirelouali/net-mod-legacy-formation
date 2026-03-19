# Task Tracking - Formation .NET Legacy → .NET 8

**Version** : 1.0  
**Date Démarrage** : 19 mars 2026  
**Dernière Mise à Jour** : 19 mars 2026  
**Statut Global** : 🔄 En cours - Sprint 1

---

## 📊 Vue d'Ensemble

**Objectif MVP (V1)** : Générer Jour 1 complet (4 sessions)

**Progression Globale** :
- ✅ Phase BRIEF : Complète
- ✅ Phase MANAGE : Complète
- ✅ Phase ARCHITECT : Complète
- 🔄 Phase DEVELOP : En cours (Sprint 1/4)

---

## 🎯 Sprint 1 - Story FR001 : Session 09h00 - Analyse Legacy

**Statut** : 🔄 En cours  
**Responsable** : Agent BMAD  
**Durée Estimée** : 2h  
**Dépendances** : Aucune (première story)

### Objectif

Créer la première session avec scaffolding complet, permettant au formateur de donner une session fluide de détection d'anti-patterns dans le code legacy.

### Tâches

#### 1. Génération Support Quotidien (Git)

**Fichier** : `03_Support_Quotidien/Jour_1_Fondations.md`

**Sections à créer** :

- [ ] **En-tête document** : Titre Jour 1, durée, objectif
- [ ] **Session 1 - 09h00 : Analyse du Batch Legacy**
  - [ ] Section 🧠 **Concepts Fondamentaux**
    - [ ] Définition dette technique
    - [ ] Les 5 catégories anti-patterns (Sécurité, Performance, Robustesse, Maintenabilité, Déploiement)
    - [ ] Tableau avec "Question clé" et "Impact Business"
  - [ ] Diagramme Mermaid : Workflow Legacy (AS-IS)
    - [ ] Flux : InputDB → Program.cs → Validation → OutputEmail/XML
    - [ ] Style : Code monolithique en rouge
  - [ ] Section 💡 **L'Astuce Pratique**
    - [ ] Principe SOLID comme détecteur
    - [ ] SRP (Single Responsibility Principle) expliqué
  - [ ] Section 💬 **Analyse Collective**
    - [ ] Question : "Temps pour être certain qu'une modif ne casse rien ?"
    - [ ] Instruction formateur (silence 5-8s)
    - [ ] Réponse attendue
  - [ ] Section ⚙️ **Défi d'Application**
    - [ ] Contexte : Héritage batch ValidFlow
    - [ ] Mission : Identifier 5 problèmes (1 par catégorie)
    - [ ] Durée : 15 minutes
    - [ ] Format de réponse fourni
    - [ ] Critères de succès
  - [ ] Section 💡 **Pistes de Réflexion** (SCAFFOLDING)
    - [ ] Sécurité : "Cherchez mots de passe (lignes 15-20)"
    - [ ] Performance : "Appels SQL async ?"
    - [ ] Robustesse : "Si SQL Server plante ?"
    - [ ] Maintenabilité : "Tester ValidateData() sans SQL ?"
    - [ ] Déploiement : "Chemin 'output.xml' sur Linux ?"
  - [ ] Section 🔗 **Lien vers la Solution**
    - [ ] Phrase standard
    - [ ] Lien Drive vers solution
  - [ ] Scripts Téléprompter 🎤
    - [ ] Script Ouverture (2 min)
    - [ ] Script Lancement Exercice (1 min)
  - [ ] Timing Détaillé ⏱️
    - [ ] Tableau horaires avec cumul
    - [ ] Total = 1h00

#### 2. Génération Solution Drive

**Fichier** : `G:\Drive partagés\wetic-s\modules\net-mod-legacy\net-mod-legacy_master_documents\Jour_1_Fondations\Solutions_A_Partager\J1_S1_Solution_09h00_Analyse.md`

**Contenu à créer** :

- [ ] En-tête solution (Jour, Session, Horaire, Durée)
- [ ] Objectif de l'exercice (rappel)
- [ ] **Problème 1 : Sécurité**
  - [ ] Localisation lignes exactes
  - [ ] Code problématique
  - [ ] Impact business chiffré
  - [ ] Solution moderne (code)
  - [ ] Explication
- [ ] **Problème 2 : Performance**
  - [ ] (idem structure)
- [ ] **Problème 3 : Robustesse**
  - [ ] (idem structure)
- [ ] **Problème 4 : Maintenabilité**
  - [ ] (idem structure)
- [ ] **Problème 5 : Déploiement**
  - [ ] (idem structure)
- [ ] Tableau Synthèse
  - [ ] 5 problèmes avec catégorie, ligne, impact, coût
  - [ ] Total coût dette : 85k€-550k€
- [ ] Diagramme Mermaid Architecture Cible (TO-BE)
- [ ] Section "Pour Aller Plus Loin" (optionnel)

#### 3. Validation Qualité

**Checklist Support Quotidien** :

- [ ] Zéro mention IA (scan grep)
- [ ] Toutes icônes présentes (🧠💡💬⚙️🔗)
- [ ] Scaffolding présent (💡 Pistes)
- [ ] Scripts téléprompter 🎤 (minimum 2)
- [ ] Timing documenté (tableau ⏱️)
- [ ] Diagramme Mermaid valide
- [ ] Markdown syntaxe valide
- [ ] Langue française uniquement

**Checklist Solution Drive** :

- [ ] 5 problèmes documentés
- [ ] Numéros de ligne exacts
- [ ] Impact business chiffré
- [ ] Code solution fourni
- [ ] Tableau synthèse présent
- [ ] Diagramme architecture cible

**Tests Techniques** :

- [ ] Scan IA négatif : `grep -i -E "(IA|AI|ChatGPT)" 03_Support_Quotidien/Jour_1_Fondations.md`
- [ ] Code legacy `ValidFlow.Legacy/Program.cs` existe et accessible
- [ ] Markdown preview rendu correct (VS Code)

#### 4. Commit Git

- [ ] `git add 03_Support_Quotidien/Jour_1_Fondations.md`
- [ ] `git add` fichier solution Drive
- [ ] Commit message conventionnel :
  ```
  feat(jour1-s1): Session 09h00 Analyse Legacy avec scaffolding complet
  
  Story FR001 - Validation USER requise
  ```

#### 5. Validation USER

- [ ] Cocher cette tâche dans TASK_TRACKING.md
- [ ] **PAUSE** - Attendre feedback USER
- [ ] Si GO → Passer Sprint 2 (FR002)
- [ ] Si NOGO → Appliquer corrections

---

## 🎯 Sprint 2 - Story FR002 : Session 10h40 - Scaffolding Architecture

**Statut** : ⏸️ En attente (dépend de FR001)  
**Durée Estimée** : 2h30

### Tâches (À développer après validation FR001)

- [ ] Générer section Session 10h40 dans `Jour_1_Fondations.md`
- [ ] Créer `J1_S2_Solution_10h40_Architecture.md`
- [ ] Validation qualité
- [ ] Commit Git
- [ ] Validation USER

---

## 🎯 Sprint 3 - Story FR003 : Session 13h30 - Implémentation Domain

**Statut** : ⏸️ En attente (dépend de FR002)  
**Durée Estimée** : 3h

### Tâches (À développer après validation FR002)

- [ ] Générer section Session 13h30
- [ ] Créer solution Drive
- [ ] Validation qualité
- [ ] Commit Git
- [ ] Validation USER

---

## 🎯 Sprint 4 - Story FR004 : Session 15h10 - Modernisation C# 12

**Statut** : ⏸️ En attente (dépend de FR003)  
**Durée Estimée** : 2h

### Tâches (À développer après validation FR003)

- [ ] Générer section Session 15h10
- [ ] Créer solution Drive
- [ ] Validation qualité finale Jour 1
- [ ] Commit Git
- [ ] Validation USER → GO/NOGO pour V2 (Jours 2-3)

---

## 📊 Métriques de Progression

### Sprints Complétés

| Sprint | Story | Session | Statut | Date Complété |
|--------|-------|---------|--------|---------------|
| 1 | FR001 | Jour 1 - 09h00 | 🔄 En cours | - |
| 2 | FR002 | Jour 1 - 10h40 | ⏸️ En attente | - |
| 3 | FR003 | Jour 1 - 13h30 | ⏸️ En attente | - |
| 4 | FR004 | Jour 1 - 15h10 | ⏸️ En attente | - |

### Temps Estimé vs Réel

| Sprint | Estimé | Réel | Écart |
|--------|--------|------|-------|
| 1 | 2h | - | - |
| 2 | 2h30 | - | - |
| 3 | 3h | - | - |
| 4 | 2h | - | - |
| **Total V1** | **9h30** | - | - |

---

## 🚨 Blocages & Risques

### Blocages Actuels

**Aucun blocage actif**

### Risques Identifiés

| Risque | Probabilité | Impact | Mitigation |
|--------|-------------|--------|------------|
| USER demande refonte structure après Sprint 2 | Moyen | Élevé | Template validé avant Sprint 1 |
| Scan IA positif en validation | Faible | Moyen | Review manuelle avant commit |
| Code solution ne compile pas | Faible | Élevé | Test local avant commit |

---

## 📝 Notes de Session

### 19 mars 2026 - Démarrage BMAD

**Décisions** :
- ✅ Option A validée (BMAD complet vs correction rapide)
- ✅ PRD créé avec 5 douleurs client + 3 personas
- ✅ 20 Stories découpées (Epic 1-5)
- ✅ Architecture documentaire définie
- ✅ Project Context créé (conventions + stack)

**Prochaine Action** :
- 🔄 Générer Session 09h00 (Sprint 1)

---

## 🎯 Définition of Done (DoD)

### Sprint 1 (FR001) Considéré DONE Si :

**Livrables** :
1. ✅ Fichier `Jour_1_Fondations.md` créé avec Session 09h00
2. ✅ Fichier `J1_S1_Solution_09h00_Analyse.md` créé sur Drive

**Qualité** :
3. ✅ Toutes sections obligatoires présentes (🧠💡💬⚙️🔗🎤⏱️)
4. ✅ Scaffolding complet (💡 Pistes avec 5 indices)
5. ✅ Scripts téléprompter (2 minimum)
6. ✅ Timing = 1h00 exactement
7. ✅ Diagramme Mermaid valide
8. ✅ Zéro mention IA (scan grep négatif)

**Technique** :
9. ✅ Solution Drive testée (5 problèmes documentés)
10. ✅ Code legacy `Program.cs` accessible
11. ✅ Markdown valide

**Validation** :
12. ✅ USER a validé (feedback GO reçu)

---

## 🔄 Workflow de Mise à Jour

**Ce fichier est mis à jour** :
- ✅ Avant chaque sprint (ajout tâches détaillées)
- ✅ Après chaque tâche complétée (cocher checkbox)
- ✅ Après validation USER (màj statuts)
- ✅ En cas de blocage (ajout section Blocages)

**Format Commit** :
```bash
git add .bmad/05_TASK_TRACKING.md
git commit -m "chore(bmad): Task Tracking - Sprint 1 démarré"
```

---

**Fin Task Tracking - Prêt pour Sprint 1**
