// ==========================================================================================
// FICHIER : ValidFlow.Domain/ValueObjects/MandatoryRule.cs
// ==========================================================================================

namespace ValidFlow.Domain.ValueObjects;

using ValidFlow.Domain.Interfaces;

/* Règle de champ obligatoire - Démonstration de l'opérateur 'is not' de C# 9
 * Cette règle est plus simple car elle n'a pas de paramètre (pas de longueur minimum à gérer).
 */
public record MandatoryRule : IValidationRule
{
    // L'opérateur 'is not' permet d'inverser la logique de pattern matching.
    // Ici : "la valeur est valide SI elle n'est PAS (null ou vide)".
    public bool IsValid(string? value) => value is not (null or "");
    
    public string GetErrorMessage(string fieldName) => 
        $"Le champ '{fieldName}' est obligatoire.";
}