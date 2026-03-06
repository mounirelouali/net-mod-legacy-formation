// ============================================================
// VALIDFLOW LEGACY - .NET Framework 4.8
// ============================================================
// PROJET ATELIER : Code Legacy pour les apprenants
// 
// MISSION : Analysez ce code et identifiez les 5 problèmes majeurs
// Créez ensuite l'architecture TO-BE en 5 projets pour moderniser
// cette application vers .NET 8
// ============================================================

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ValidFlow.Legacy
{
    class Program
    {
        static void Main(string[] args)
        {
            // ConnectionString pour la base de données clients
            string connectionString = "Server=localhost\\SQLEXPRESS;Database=ValidFlowDb;" +
                                     "User Id=admin;Password=P@ssw0rd123;TrustServerCertificate=True;";

            // Récupération des clients depuis la base de données
            var clients = GetClientsFromDb(connectionString);

            // Définition des règles de validation pour les clients
            var clientRules = new List<ValidationRule>
            {
                new EmailValidationRule(),
                new PhoneValidationRule(),
                new MandatoryFieldRule("FirstName"),
                new MandatoryFieldRule("LastName"),
                new MandatoryFieldRule("Email")
            };

            var validClients = new List<Client>();
            var invalidClients = new List<string>();

            // Validation de chaque client
            foreach (var client in clients)
            {
                bool isValid = true;

                foreach (var rule in clientRules)
                {
                    if (!rule.Validate(client))
                    {
                        invalidClients.Add($"Client {client.Id}: {rule.GetErrorMessage(client)}");
                        isValid = false;
                        break;
                    }
                }

                if (isValid)
                {
                    validClients.Add(client);
                }
            }

            // Si des clients invalides, envoi d'un rapport par email
            if (invalidClients.Count > 0)
            {
                SendInvalidClientsReport("sales@company.com", invalidClients);
            }

            // Export des clients valides en CSV
            ExportToCSV(validClients, "valid_clients.csv");

            // Export des clients valides en JSON
            ExportToJSON(validClients, "valid_clients.json");

            Console.WriteLine($"Processing completed.");
            Console.WriteLine($"Valid clients: {validClients.Count}");
            Console.WriteLine($"Invalid clients: {invalidClients.Count}");
            Console.WriteLine($"Exports saved to valid_clients.csv and valid_clients.json");
            Console.ReadKey();
        }

        // Récupération des clients depuis la base de données
        static List<Client> GetClientsFromDb(string connectionString)
        {
            var clients = new List<Client>();

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                var query = @"SELECT Id, FirstName, LastName, Email, Phone, Address, City, PostalCode 
                             FROM Clients WHERE IsActive = 1";

                using (var cmd = new SqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clients.Add(new Client
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.IsDBNull(1) ? null : reader.GetString(1),
                            LastName = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Phone = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Address = reader.IsDBNull(5) ? null : reader.GetString(5),
                            City = reader.IsDBNull(6) ? null : reader.GetString(6),
                            PostalCode = reader.IsDBNull(7) ? null : reader.GetString(7)
                        });
                    }
                }
            }

            return clients;
        }

        // Envoi du rapport des clients invalides par email
        static void SendInvalidClientsReport(string to, List<string> invalidClients)
        {
            var subject = "ValidFlow - Invalid Clients Report";
            var body = new StringBuilder();
            body.AppendLine("The following clients have validation errors:");
            body.AppendLine();
            foreach (var error in invalidClients)
            {
                body.AppendLine(error);
            }

            var message = new MailMessage("noreply@validflow.com", to, subject, body.ToString());
            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("sales@company.com", "MyEmailP@ss!"),
                EnableSsl = true
            };

            smtp.Send(message);
        }

        // Export des clients en CSV
        static void ExportToCSV(List<Client> clients, string filename)
        {
            using (var writer = new StreamWriter(filename))
            {
                // Header
                writer.WriteLine("Id,FirstName,LastName,Email,Phone,Address,City,PostalCode");

                // Data
                foreach (var client in clients)
                {
                    writer.WriteLine($"{client.Id},{client.FirstName},{client.LastName}," +
                                   $"{client.Email},{client.Phone},{client.Address}," +
                                   $"{client.City},{client.PostalCode}");
                }
            }
        }

        // Export des clients en JSON (simple)
        static void ExportToJSON(List<Client> clients, string filename)
        {
            var json = new StringBuilder();
            json.AppendLine("[");

            for (int i = 0; i < clients.Count; i++)
            {
                var client = clients[i];
                json.AppendLine("  {");
                json.AppendLine($"    \"id\": {client.Id},");
                json.AppendLine($"    \"firstName\": \"{client.FirstName}\",");
                json.AppendLine($"    \"lastName\": \"{client.LastName}\",");
                json.AppendLine($"    \"email\": \"{client.Email}\",");
                json.AppendLine($"    \"phone\": \"{client.Phone}\",");
                json.AppendLine($"    \"address\": \"{client.Address}\",");
                json.AppendLine($"    \"city\": \"{client.City}\",");
                json.AppendLine($"    \"postalCode\": \"{client.PostalCode}\"");
                json.Append("  }");
                if (i < clients.Count - 1)
                    json.AppendLine(",");
                else
                    json.AppendLine();
            }

            json.AppendLine("]");

            File.WriteAllText(filename, json.ToString());
        }
    }

    // ============================================================
    // MODÈLE CLIENT
    // ============================================================

    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }

    // ============================================================
    // RÈGLES DE VALIDATION
    // ============================================================

    public interface IValidationRule
    {
        bool Validate(Client client);
        string GetErrorMessage(Client client);
    }

    public class ValidationRule : IValidationRule
    {
        public virtual bool Validate(Client client) => true;
        public virtual string GetErrorMessage(Client client) => string.Empty;
    }

    public class EmailValidationRule : ValidationRule
    {
        public override bool Validate(Client client)
        {
            if (string.IsNullOrEmpty(client.Email))
                return false;

            return client.Email.Contains("@") && client.Email.Contains(".");
        }

        public override string GetErrorMessage(Client client) =>
            $"Email '{client.Email}' is not valid.";
    }

    public class PhoneValidationRule : ValidationRule
    {
        public override bool Validate(Client client)
        {
            if (string.IsNullOrEmpty(client.Phone))
                return false;

            // Validation simple : au moins 10 chiffres
            var digits = client.Phone.Count(char.IsDigit);
            return digits >= 10;
        }

        public override string GetErrorMessage(Client client) =>
            $"Phone '{client.Phone}' must contain at least 10 digits.";
    }

    public class MandatoryFieldRule : ValidationRule
    {
        private readonly string _fieldName;

        public MandatoryFieldRule(string fieldName)
        {
            _fieldName = fieldName;
        }

        public override bool Validate(Client client)
        {
            var property = typeof(Client).GetProperty(_fieldName);
            if (property == null)
                return false;

            var value = property.GetValue(client) as string;
            return !string.IsNullOrEmpty(value);
        }

        public override string GetErrorMessage(Client client) =>
            $"Field '{_fieldName}' is mandatory.";
    }
}
