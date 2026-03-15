# Mounir's Technical Training Framework (V2)

## 1. Philosophie Pédagogique
1. **Zéro Meta-Texte :** Les supports générés ne doivent contenir aucune mention "Note Formateur cachée". Tout doit être rédigé sous forme de rubriques pédagogiques intégrées.
2. **Vocabulaire Professionnel :** Utiliser les termes de l'ingénierie (Questionnement ciblé, Scaffolding, Charge cognitive, Difficulté désirable). BANNIR le terme "Maïeutique".
3. **Validation Itérative (Le Challenge Permanent) :** L'ingénieur pédagogique doit toujours valider la cohérence des demandes avec le contexte global. Ne jamais appliquer une demande si elle contredit un principe E-learning fondamental.

## 2. Architecture et Logistique Git
4. **Mise sur les Rails (Single Branch) :** Onboarder les stagiaires avec `git clone --single-branch --branch [nom-du-jour]`. Ils ne font plus de `git pull` de la journée pour éviter les conflits (Merge Conflicts).
5. **Standardisation de la Nomenclature :** Tous les fichiers d'exercice doivent suivre le format `Type_Heure_Sujet.md` (ex: `Workbook_10h40_Architecture.md`, `Solution_10h40_Architecture.md`).
6. **Le "Golden Master" :** La branche `main` contient la vérité absolue (Code, Workbooks, Solutions). La branche stagiaire ne contient QUE le code de départ et les Workbooks.
7. **Gestion des Dossiers Vides :** Toujours placer un fichier `.gitkeep` dans les dossiers devant rester vides (ex: `01_Demo_Formateur/`).

## 3. Sécurité Cognitive et Anti-Triche
8. **Cécité IA (Zero AI Meta-References) :** STRICTEMENT INTERDIT de mentionner des outils d'IA (NotebookLM, Windsurf, Gemini, prompts) dans les supports, le code ou les commits.
9. **Authenticité du Code (Verbatim) :** Le code du dossier Legacy (`00_Reference_Client/`) doit être le code exact fourni par le client. Le fichier `.sln` doit être renommé en `.sln.legacy` pour sécuriser la compilation.
10. **Sanctuarisation des Solutions :** 
    - Ne JAMAIS placer les dossiers de solution en local chez les stagiaires.
    - Ne JAMAIS mettre de lien vers la solution à l'intérieur du Workbook stagiaire (cela détruit la difficulté désirable).
    - Les solutions sont des URL Web (`main` sur GitHub) que le formateur partage manuellement dans le chat UNIQUEMENT à la fin du temps imparti.
