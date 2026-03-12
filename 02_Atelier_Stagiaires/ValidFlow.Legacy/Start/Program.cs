using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace ValidFlow.Legacy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ValidFlow - Export Clients ===\n");

            string connectionString = "Server=prod-db.company.local;Database=ClientsDB;User Id=admin;Password=ClientPass2024!;";
            string smtpPassword = "SmtpSecret456!";

            var clients = LoadClientsFromDatabase(connectionString);

            var validClients = new List<Client>();
            foreach (var client in clients)
            {
                if (!string.IsNullOrEmpty(client.Email) && 
                    client.Email.Contains("@") &&
                    client.CompanyName.Length > 2 &&
                    client.Revenue > 0)
                {
                    validClients.Add(client);
                }
            }

            Console.WriteLine($"Clients valides : {validClients.Count}/{clients.Count}");

            ExportToCsv(validClients, "clients_export.csv");
            Console.WriteLine("✓ Export CSV terminé");

            SendNotificationEmail(
                "manager@company.com",
                smtpPassword,
                $"Export clients terminé : {validClients.Count} clients exportés"
            );

            Console.WriteLine("✓ Email envoyé");
        }

        static List<Client> LoadClientsFromDatabase(string connectionString)
        {
            var clients = new List<Client>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var command = new SqlCommand(
                    "SELECT CompanyName, Email, Revenue, Country FROM Clients WHERE IsActive = 1", 
                    connection
                );

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clients.Add(new Client
                        {
                            CompanyName = reader.GetString(0),
                            Email = reader.GetString(1),
                            Revenue = reader.GetDecimal(2),
                            Country = reader.GetString(3)
                        });
                    }
                }
            }

            return clients;
        }

        static void ExportToCsv(List<Client> clients, string filename)
        {
            var writer = new StreamWriter(filename);
            
            writer.WriteLine("CompanyName;Email;Revenue;Country");

            foreach (var client in clients)
            {
                writer.WriteLine($"{client.CompanyName};{client.Email};{client.Revenue};{client.Country}");
            }

            writer.Close();
        }

        static void SendNotificationEmail(string to, string password, string message)
        {
            var mail = new MailMessage("noreply@validflow.com", to)
            {
                Subject = "ValidFlow - Export terminé",
                Body = message
            };

            var smtp = new SmtpClient("smtp.validflow.com", 587)
            {
                Credentials = new NetworkCredential("notifications@validflow.com", password),
                EnableSsl = true
            };

            smtp.Send(mail);
        }
    }

    public class Client
    {
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public decimal Revenue { get; set; }
        public string Country { get; set; }
    }
}
