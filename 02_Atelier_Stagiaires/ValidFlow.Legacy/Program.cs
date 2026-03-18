using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Xml.Linq;

namespace ValidFlow.Legacy
{
    /// <summary>
    /// Batch ValidFlow Legacy - Code monolithique à moderniser
    /// Anti-patterns volontaires pour exercice de refactoring
    /// </summary>
    class Program
    {
        // ⚠️ ANTI-PATTERN #1 : Secrets en dur dans le code
        private static string connectionString = "Server=localhost;Database=ValidFlowDB;User Id=admin;Password=admin123;";
        private static string smtpServer = "smtp.company.local";
        private static string smtpUser = "batch@company.com";
        private static string smtpPassword = "SmtpP@ss2024!";

        static void Main(string[] args)
        {
            Console.WriteLine("=== ValidFlow Batch - Démarrage ===");
            
            try
            {
                // ⚠️ ANTI-PATTERN #2 : Tout dans une seule méthode
                var data = GetDataFromDatabase();
                var errors = ValidateData(data);
                
                if (errors.Count > 0)
                {
                    SendAlertEmail(errors);
                }
                else
                {
                    GenerateXmlOutput(data);
                }
            }
            catch (Exception ex)
            {
                // ⚠️ ANTI-PATTERN #3 : Gestion d'erreur générique
                Console.WriteLine("ERREUR: " + ex.Message);
            }
        }

        // ⚠️ ANTI-PATTERN #4 : Couplage fort à SQL Server
        private static Dictionary<string, string> GetDataFromDatabase()
        {
            var result = new Dictionary<string, string>();
            
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT Id, Name, Email FROM Clients", connection);
                
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(reader["Id"].ToString(), 
                            reader["Name"].ToString() + "|" + reader["Email"].ToString());
                    }
                }
            }
            
            return result;
        }

        // ⚠️ ANTI-PATTERN #5 : Règles métier mélangées avec l'infrastructure
        private static List<string> ValidateData(Dictionary<string, string> data)
        {
            var errors = new List<string>();
            var rules = new List<IRule>
            {
                new MandatoryRule(),
                new MinLengthRule(2),
                new MaxLengthRule(100)
            };

            foreach (var item in data)
            {
                var parts = item.Value.Split('|');
                var name = parts[0];
                var email = parts.Length > 1 ? parts[1] : null;

                foreach (var rule in rules)
                {
                    // ⚠️ ANTI-PATTERN #6 : Pas de gestion du null
                    if (!rule.IsValid(name))
                    {
                        errors.Add($"Client {item.Key}: Name - {rule.GetErrorMessage("Name")}");
                    }
                    if (!rule.IsValid(email))
                    {
                        errors.Add($"Client {item.Key}: Email - {rule.GetErrorMessage("Email")}");
                    }
                }
            }

            return errors;
        }

        // ⚠️ ANTI-PATTERN #7 : Couplage fort à SMTP
        private static void SendAlertEmail(List<string> errors)
        {
            using (var client = new SmtpClient(smtpServer))
            {
                client.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPassword);
                
                var message = new MailMessage(
                    smtpUser,
                    "admin@company.com",
                    "ValidFlow - Erreurs de validation",
                    string.Join(Environment.NewLine, errors)
                );
                
                client.Send(message);
            }
            
            Console.WriteLine($"Email envoyé avec {errors.Count} erreurs.");
        }

        private static void GenerateXmlOutput(Dictionary<string, string> data)
        {
            var xml = new XElement("Clients");
            
            foreach (var item in data)
            {
                var parts = item.Value.Split('|');
                xml.Add(new XElement("Client",
                    new XAttribute("Id", item.Key),
                    new XElement("Name", parts[0]),
                    new XElement("Email", parts.Length > 1 ? parts[1] : "")
                ));
            }
            
            xml.Save("output.xml");
            Console.WriteLine("XML généré: output.xml");
        }
    }

    // ========================================
    // RÈGLES DE VALIDATION (à extraire vers Domain)
    // ========================================
    
    public interface IRule
    {
        bool IsValid(string value);
        string GetErrorMessage(string fieldName);
    }

    public class MandatoryRule : IRule
    {
        public bool IsValid(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public string GetErrorMessage(string fieldName)
        {
            return $"Le champ {fieldName} est obligatoire.";
        }
    }

    public class MinLengthRule : IRule
    {
        private readonly int _minLength;

        public MinLengthRule(int minLength)
        {
            _minLength = minLength;
        }

        public bool IsValid(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            return value.Length >= _minLength;
        }

        public string GetErrorMessage(string fieldName)
        {
            return $"Le champ {fieldName} doit contenir au moins {_minLength} caractères.";
        }
    }

    public class MaxLengthRule : IRule
    {
        private readonly int _maxLength;

        public MaxLengthRule(int maxLength)
        {
            _maxLength = maxLength;
        }

        public bool IsValid(string value)
        {
            if (string.IsNullOrEmpty(value)) return true;
            return value.Length <= _maxLength;
        }

        public string GetErrorMessage(string fieldName)
        {
            return $"Le champ {fieldName} ne peut pas dépasser {_maxLength} caractères.";
        }
    }
}
