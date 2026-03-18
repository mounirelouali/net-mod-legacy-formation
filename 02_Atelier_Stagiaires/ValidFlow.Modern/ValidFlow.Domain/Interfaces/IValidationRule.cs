// ==========================================================================================
// FICHIER : ValidFlow.Domain/Interfaces/IValidationRule.cs
// ==========================================================================================

// Déclaration de l'espace de noms (namespace) selon la syntaxe moderne (C# 10+).
// Tout le code qui suit dans ce fichier appartiendra à "ValidFlow.Domain.Interfaces".
namespace ValidFlow.Domain.Interfaces;

/* Le "Contrat" de base. Toute règle de validation devra obligatoirement 
 * implémenter ces deux méthodes pour être considérée comme une "IValidationRule".
 */
public interface IValidationRule
{
    // Le '?' après 'string' est une fonctionnalité de C# 8 (Nullable Reference Types).
    // Il indique explicitement que la variable 'value' a le droit d'être 'null'.
    // Cela force le développeur qui code la règle à gérer le cas où la valeur n'existe pas.
    bool IsValid(string? value);
    
    // Méthode qui renverra le message d'erreur si IsValid a retourné 'false'.
    string GetErrorMessage(string fieldName);
}