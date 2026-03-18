// ==========================================================================================
// FICHIER : ValidFlow.Domain/ValueObjects/MinLengthRule.cs
// ==========================================================================================

namespace ValidFlow.Domain.ValueObjects;

using ValidFlow.Domain.Interfaces;

/* Un "record" en C# (depuis C# 9) est une classe spéciale optimisée pour représenter des données.
 * Il est IMMUABLE par défaut : une fois créé, on ne peut plus modifier "MinLength".
 * La syntaxe "(int MinLength)" est un constructeur "positionnel".
 * Le compilateur va automatiquement créer une propriété "MinLength" en lecture seule.
 * Le ":" signifie que ce record implémente l'interface "IValidationRule".
 */
public record MinLengthRule(int MinLength) : IValidationRule
{
    /* Implémentation de la méthode IsValid de l'interface.
     * On utilise ici une expression "switch" (introduite dans C# 8), qui est 
     * beaucoup plus puissante et concise qu'une suite de "if / else if / else".
     * L'opérateur "=>" (expression-bodied member) signifie "cette méthode retourne le résultat de ce switch".
     */
    public bool IsValid(string? value) => value switch
    {
        // CAS 1 : Si la chaîne de caractères est 'null' OU si elle est vide ("").
        // On retourne immédiatement 'false' (la règle n'est pas respectée).
        null or "" => false,

        // CAS 2 : Le "Property Pattern Matching" (Filtrage par motif de propriété).
        // Si 'value' n'est pas null, on va regarder sa propriété "Length".
        // "{ Length: var len }" veut dire : "Prends la valeur de value.Length et mets-la dans une variable temporaire 'len'".
        // "when len >= MinLength" est une condition supplémentaire (une garde) : "...et vérifie que 'len' est supérieur ou égal au minimum requis".
        // Si tout ça est vrai, on retourne 'true'.
        { Length: var len } when len >= MinLength => true,

        // CAS 3 : Le cas par défaut (le "default" d'un switch classique).
        // L'underscore "_" (discard) signifie "pour tout ce qui n'a pas été intercepté par les cas précédents".
        // Ici, cela capture les chaînes de caractères qui sont valides (ni nulles, ni vides) mais dont la longueur est inférieure à MinLength.
        _ => false
    };
    
    /* Le signe '$' avant les guillemets permet de faire de l'"Interpolation de chaîne".
     * Cela permet d'insérer directement des variables entre accolades { } dans le texte, 
     * au lieu de faire de la concaténation "texte" + variable + "texte".
     */
    public string GetErrorMessage(string fieldName) => 
        $"Le champ '{fieldName}' doit contenir au moins {MinLength} caractères.";
}