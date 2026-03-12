using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Xml.Serialization;

namespace generationxml
{
    // CODE LEGACY - FIL ROUGE FORMATION .NET 8
    // Ce code contient DÉLIBÉRÉMENT 5 anti-patterns pour démonstration pédagogique
    // NE PAS utiliser en production - Exemple de mauvaises pratiques
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Batch de Génération XML - Version Legacy ===\n");

            // ❌ ANTI-PATTERN #1 : CREDENTIALS HARDCODÉS (Lignes 24-25)
            // Problème : Secrets en clair dans le code source
            // Impact : Risque de fuite si commit sur Git, impossible de changer sans recompiler
            string connectionString = "Server=prod-sql.company.local;Database=GenerationXml;User Id=sa;Password=Prod2024!;";
            string smtpPassword = "MyP@ssw0rd123!";

            // ❌ ANTI-PATTERN #2 : APPELS SYNCHRONES BLOQUANTS (Ligne 30)
            // Problème : Thread bloqué pendant les I/O, CPU idle
            // Impact : Scalabilité limitée, waste de ressources serveur
            var data = GetDataFromDatabase(connectionString);

            // ❌ ANTI-PATTERN #5 : LOGIQUE MÉTIER MÉLANGÉE AVEC ACCÈS DONNÉES (Lignes 35-45)
            // Problème : Validation codée en dur dans le flux principal
            // Impact : Impossible à tester unitairement, couplage fort
            var validRecords = new List<XmlRecord>();
            foreach (var record in data)
            {
                // Règles métier hardcodées
                if (!string.IsNullOrEmpty(record.Name) && 
                    record.Name.Length >= 3 && 
                    record.Code.Length == 5 &&
                    !record.Code.Contains("X"))
                {
                    validRecords.Add(record);
                }
            }

            Console.WriteLine($"Enregistrements valides : {validRecords.Count}/{data.Count}");

            // ❌ ANTI-PATTERN #4 : INSTANCIATION DIRECTE AVEC 'new' (Ligne 53)
            // Problème : Couplage fort, impossible d'injecter un mock pour tests
            // Impact : Code non testable, violation du principe d'inversion de dépendances
            var serializer = new XmlSerializer(typeof(List<XmlRecord>));
            
            using (var writer = new StreamWriter("output.xml"))
            {
                serializer.Serialize(writer, validRecords);
            }

            Console.WriteLine("✓ Fichier XML généré : output.xml");

            // ❌ ANTI-PATTERN #1 + #4 : Credentials hardcodés + Instanciation directe (Lignes 64-66)
            SendEmailNotification(
                "admin@company.com", 
                smtpPassword, 
                $"Export XML terminé : {validRecords.Count} enregistrements"
            );

            Console.WriteLine("✓ Email de notification envoyé");
        }

        // ❌ ANTI-PATTERN #2 + #3 : Appel synchrone + Aucune gestion d'erreurs (Lignes 73-89)
        // Problème : Pas de try-catch, crash si connexion échoue
        // Impact : Application arrêtée brutalement, pas de logs d'erreur
        static List<XmlRecord> GetDataFromDatabase(string connectionString)
        {
            var records = new List<XmlRecord>();
            
            // ⚠️ Aucun try-catch : Si connexion échoue, crash total
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open(); // ⚠️ Synchrone : Thread bloqué
                
                var command = new SqlCommand("SELECT Name, Code, Value FROM XmlData WHERE IsActive = 1", connection);
                using (var reader = command.ExecuteReader()) // ⚠️ Synchrone : Thread bloqué
                {
                    while (reader.Read())
                    {
                        records.Add(new XmlRecord
                        {
                            Name = reader.GetString(0),
                            Code = reader.GetString(1),
                            Value = reader.GetDecimal(2)
                        });
                    }
                }
            }

            return records;
        }

        // ❌ ANTI-PATTERN #3 + #4 : Aucun try-catch + Instanciation directe SmtpClient (Lignes 103-114)
        static void SendEmailNotification(string to, string password, string message)
        {
            // ⚠️ Aucun try-catch : Si SMTP échoue, crash total
            var mail = new MailMessage("noreply@company.com", to)
            {
                Subject = "Export XML - Notification",
                Body = message
            };

            // ⚠️ Instanciation directe : Impossible de mocker pour tests
            var smtp = new SmtpClient("smtp.company.com", 587)
            {
                Credentials = new NetworkCredential("admin@company.com", password),
                EnableSsl = true
            };

            smtp.Send(mail); // ⚠️ Synchrone : Thread bloqué
        }
    }

    // Modèle de données simple
    [Serializable]
    public class XmlRecord
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Value { get; set; }
    }
}
