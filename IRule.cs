using System;
using System.Collections.Generic;
using System.Linq;

namespace generationxml
{
    public interface IRule
    {
        bool IsValid(string value);
        string ErrorMessage(string tagName, string value);
    }

    public class MandatoryRule : IRule
    {
        public bool IsValid(string value) => !string.IsNullOrEmpty(value);
        public string ErrorMessage(string tagName, string value) =>
            $"Value for '{tagName}' is mandatory and cannot be empty.";
    }

    public class MinLengthRule : IRule
    {
        private readonly int _minLength;
        public MinLengthRule(int minLength) { _minLength = minLength; }
        public bool IsValid(string value) => value != null && value.Length >= _minLength;
        public string ErrorMessage(string tagName, string value) =>
            $"Value '{value}' for '{tagName}' must be at least {_minLength} characters.";
    }

    public class MaxLengthRule : IRule
    {
        private readonly int _maxLength;
        public MaxLengthRule(int maxLength) { _maxLength = maxLength; }
        public bool IsValid(string value) => value == null || value.Length <= _maxLength;
        public string ErrorMessage(string tagName, string value) =>
            $"Value '{value}' for '{tagName}' must be at most {_maxLength} characters.";
    }

    public class ForbiddenCharsRule : IRule
    {
        private readonly List<char> _forbiddenChars;
        public ForbiddenCharsRule(IEnumerable<char> forbiddenChars)
        {
            _forbiddenChars = forbiddenChars.ToList();
        }
        public bool IsValid(string value) => value == null || !_forbiddenChars.Any(value.Contains);
        public string ErrorMessage(string tagName, string value) =>
            $"Value '{value}' for '{tagName}' contains forbidden characters: {string.Join(", ", _forbiddenChars.Where(value.Contains))}";
    }

    public class AuthorizedCharsRule : IRule
    {
        private readonly HashSet<char> _authorizedChars;
        public AuthorizedCharsRule(IEnumerable<char> authorizedChars)
        {
            _authorizedChars = new HashSet<char>(authorizedChars);
        }
        public bool IsValid(string value) => value == null || value.All(c => _authorizedChars.Contains(c));
        public string ErrorMessage(string tagName, string value) =>
            $"Value '{value}' for '{tagName}' contains unauthorized characters.";
    }

    // Add more rules as needed
}