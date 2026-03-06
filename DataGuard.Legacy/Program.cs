// ============================================================
// DATAGUARD LEGACY - .NET Framework 4.8
// ============================================================
// OBJECTIF PÉDAGOGIQUE : Ce code illustre TOUS les problèmes
// d'une application Legacy typique que nous allons corriger
// progressivement durant la formation.
//
// LES 5 PROBLÈMES MAJEURS :
// ⚠️  #1 : SÉCURITÉ - Credentials hardcodés
// 🐌 #2 : PERFORMANCE - Code synchrone bloquant
// 💥 #3 : ROBUSTESSE - Aucune gestion d'erreurs
// 🔧 #4 : MAINTENABILITÉ - Couplage fort (impossible à tester)
// 📦 #5 : DÉPLOIEMENT - Windows uniquement
// ============================================================

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Xml.Serialization;

namespace DataGuard.Legacy
{
    class Program
    {
        static void Main(string[] args)
        {
            // ============================================================
            // ⚠️ PROBLÈME #1 : SÉCURITÉ - ConnectionString hardcodée
            // ============================================================
            // LIGNE 31-32 : ConnectionString SQL en clair avec password
            // CONSÉQUENCE : Si ce code est sur Git, le mot de passe est exposé
            // SOLUTION .NET 8 : Secret Manager + appsettings.json
            // ============================================================
            string connectionString = "Server=localhost\\SQLEXPRESS;Database=DataGuardDb;" +
                                     "User Id=sa;Password=SuperSecret123!;TrustServerCertificate=True;";

            // ============================================================
            // 🐌 PROBLÈME #2 : PERFORMANCE - Appel synchrone bloquant
            // ============================================================
            // LIGNE 38 : GetDataFromDb() est 100% synchrone
            // CONSÉQUENCE : Le thread est gelé pendant 50-200ms d'attente DB
            // CPU idle à 2% pendant que le thread attend la réponse
            // SOLUTION .NET 8 : async/await pour libérer le thread
            // ============================================================
            var data = GetDataFromDb(connectionString);

            // Définition des règles de validation en dur
            // 🔧 PROBLÈME #4 : Les règles sont instanciées directement (couplage fort)
            var rules = new List<TagRule>
            {
                new TagRule
                {
                    TagName = "Name",
                    Rules = new List<IRule>
                    {
                        new MandatoryRule(),
                        new MinLengthRule(3),
                        new MaxLengthRule(50),
                        new ForbiddenCharsRule(new[] { 'T', 'u' }),
                        new AuthorizedCharsRule("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ".ToCharArray())
                    }
                },
                new TagRule
                {
                    TagName = "Code",
                    Rules = new List<IRule>
                    {
                        new MandatoryRule(),
                        new MinLengthRule(2),
                        new MaxLengthRule(10),
                        new ForbiddenCharsRule(new[] { 'X', 'Y' }),
                        new AuthorizedCharsRule("0123456789ABCDEF".ToCharArray())
                    }
                }
            };

            var validModels = new List<MyXmlModel>();
            var invalidEntries = new List<string>();

            // Création et validation d'un modèle exemple
            var model = new MyXmlModel
            {
                Name = "DataGuard",
                Code = "DG001"
            };

            bool hasValid = false;
            ValidateObject(model, rules, invalidEntries, ref hasValid);

            if (hasValid)
            {
                validModels.Add(model);
            }

            // ============================================================
            // 💥 PROBLÈME #3 : ROBUSTESSE - Pas de gestion d'erreurs
            // ============================================================
            // LIGNE 95-96 : Si invalidEntries.Count > 0, on envoie un email
            // MAIS aucun try-catch si le serveur SMTP est down
            // CONSÉQUENCE : L'application crashe totalement
            // SOLUTION .NET 8 : try-catch + Polly retry policy + ILogger
            // ============================================================
            if (invalidEntries.Count > 0)
            {
                // ⚠️ PROBLÈME #1 : Credentials SMTP hardcodés
                SendEmail("admin@company.com", "Invalid XML Data",
                    "The following entries are invalid:\n" + string.Join("\n", invalidEntries));
            }

            // Sérialisation XML (synchrone)
            var serializer = new XmlSerializer(typeof(List<MyXmlModel>), new XmlRootAttribute("Root"));
            using (var writer = new StreamWriter("output.xml"))
            {
                serializer.Serialize(writer, validModels);
            }

            Console.WriteLine("Processing completed. Output saved to output.xml");
            Console.ReadKey();
        }

        // ============================================================
        // 🐌 PROBLÈME #2 : PERFORMANCE - Méthode synchrone bloquante
        // ============================================================
        // Cette méthode bloque le thread pendant toute la durée de la requête SQL
        // Le thread reste gelé même si le CPU n'a rien à faire (attente réseau)
        // ============================================================
        static Dictionary<string, string> GetDataFromDb(string connectionString)
        {
            var data = new Dictionary<string, string>();

            // ============================================================
            // 💥 PROBLÈME #3 : ROBUSTESSE - Aucun try-catch
            // ============================================================
            // LIGNE 132 : conn.Open() sans gestion d'erreur
            // Si la DB est down ou timeout, l'application crash totalement
            // Pas de retry, pas de logs, l'utilisateur ne sait pas pourquoi
            // ============================================================
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open(); // ❌ Appel synchrone bloquant + pas de gestion d'erreur

                using (var cmd = new SqlCommand("SELECT Tag, Value FROM XmlDataTable", conn))
                using (var reader = cmd.ExecuteReader()) // ❌ Synchrone bloquant
                {
                    while (reader.Read())
                    {
                        data[reader.GetString(0)] = reader.GetString(1);
                    }
                }
            }

            return data;
        }

        // ============================================================
        // ⚠️ PROBLÈME #1 + 💥 PROBLÈME #3 : SÉCURITÉ + ROBUSTESSE
        // ============================================================
        // Credentials SMTP hardcodés + aucune gestion d'erreur
        // ============================================================
        static void SendEmail(string to, string subject, string body)
        {
            // ============================================================
            // ⚠️ PROBLÈME #1 : SÉCURITÉ - Credentials email hardcodés
            // ============================================================
            // LIGNE 162-167 : Username et Password SMTP en clair
            // VIOLATION ISO 27001 : Impossible d'auditer qui a le mot de passe
            // SOLUTION .NET 8 : dotnet user-secrets set "Email:Password" "xxx"
            // ============================================================
            var message = new MailMessage("noreply@dataguard.com", to, subject, body);
            var client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("admin@company.com", "MyP@ssw0rd123!"),
                EnableSsl = true
            };

            // ============================================================
            // 💥 PROBLÈME #3 + 🐌 PROBLÈME #2 : ROBUSTESSE + PERFORMANCE
            // ============================================================
            // LIGNE 176 : client.Send() sans try-catch (crash si SMTP down)
            // ET méthode synchrone (bloque le thread pendant 500ms-2s)
            // SOLUTION .NET 8 : try-catch + SendAsync() + Polly retry
            // ============================================================
            client.Send(message); // ❌ Synchrone bloquant + pas de gestion d'erreur
        }

        // ============================================================
        // 🔧 PROBLÈME #4 : MAINTENABILITÉ - Validation par réflexion
        // ============================================================
        // Cette méthode utilise la réflexion pour valider récursivement
        // Problèmes :
        // - Synchrone (pas de parallélisation possible)
        // - Difficile à tester (dépend de la structure des objets)
        // - Pas de séparation des responsabilités
        // ============================================================
        private static void ValidateObject(
            object obj,
            List<TagRule> rules,
            List<string> invalidEntries,
            ref bool hasValid)
        {
            if (obj == null) return;

            var type = obj.GetType();
            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var value = prop.GetValue(obj);

                if (prop.PropertyType == typeof(string))
                {
                    var tagRule = rules.FirstOrDefault(r => r.TagName == prop.Name);
                    var strValue = value as string;
                    bool isValid = true;

                    if (tagRule != null)
                    {
                        foreach (var rule in tagRule.Rules)
                        {
                            if (!rule.IsValid(strValue))
                            {
                                invalidEntries.Add(rule.ErrorMessage(prop.Name, strValue));
                                isValid = false;
                                break;
                            }
                        }
                    }

                    if (isValid && strValue != null)
                        hasValid = true;
                }
                else if (prop.PropertyType.IsClass && prop.PropertyType != typeof(string) &&
                         !prop.PropertyType.FullName.StartsWith("System."))
                {
                    ValidateObject(value, rules, invalidEntries, ref hasValid);
                }
                else if (typeof(System.Collections.IEnumerable).IsAssignableFrom(prop.PropertyType) &&
                         prop.PropertyType != typeof(string))
                {
                    var enumerable = value as System.Collections.IEnumerable;
                    if (enumerable != null)
                    {
                        foreach (var item in enumerable)
                        {
                            if (item != null && item.GetType().IsClass && item.GetType() != typeof(string))
                                ValidateObject(item, rules, invalidEntries, ref hasValid);
                        }
                    }
                }
            }
        }
    }

    // ============================================================
    // INTERFACES ET CLASSES DE VALIDATION
    // ============================================================
    // Ces classes seront déplacées vers le projet Domain en .NET 8
    // ============================================================

    public interface IRule
    {
        bool IsValid(string value);
        string ErrorMessage(string tagName, string value);
    }

    public class TagRule
    {
        public string TagName { get; set; }
        public List<IRule> Rules { get; set; } = new List<IRule>();
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

        public MinLengthRule(int minLength)
        {
            _minLength = minLength;
        }

        public bool IsValid(string value) => value != null && value.Length >= _minLength;

        public string ErrorMessage(string tagName, string value) =>
            $"Value '{value}' for '{tagName}' must be at least {_minLength} characters.";
    }

    public class MaxLengthRule : IRule
    {
        private readonly int _maxLength;

        public MaxLengthRule(int maxLength)
        {
            _maxLength = maxLength;
        }

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
            $"Value '{value}' for '{tagName}' contains forbidden characters: " +
            $"{string.Join(", ", _forbiddenChars.Where(value.Contains))}";
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

    // ============================================================
    // MODÈLE DE DONNÉES POUR SÉRIALISATION XML
    // ============================================================
    // Cette classe sera déplacée vers le projet Domain en .NET 8
    // et transformée en record avec syntaxe C# 12
    // ============================================================

    [XmlRoot("MyXmlModel")]
    public class MyXmlModel
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Code")]
        public string Code { get; set; }
    }
}

// ============================================================
// RÉCAPITULATIF DES PROBLÈMES À CORRIGER
// ============================================================
//
// ⚠️ SÉCURITÉ (Lignes 31-32, 162-167)
// - ConnectionString hardcodée avec password
// - Credentials SMTP hardcodés
// → Solution : appsettings.json + Secret Manager
//
// 🐌 PERFORMANCE (Lignes 38, 122-149, 176)
// - GetDataFromDb() synchrone bloquant
// - SendEmail() synchrone bloquant
// → Solution : async/await partout
//
// 💥 ROBUSTESSE (Lignes 132, 176)
// - conn.Open() sans try-catch
// - client.Send() sans try-catch
// - Aucun logging
// → Solution : try-catch + Polly + ILogger
//
// 🔧 MAINTENABILITÉ (Tout le fichier)
// - Code monolithique (1 fichier, 350 lignes)
// - Instanciations directes (new SmtpClient, new SqlConnection)
// - Impossible à tester sans vraie DB + vraie SMTP
// → Solution : Architecture 5 projets + DI
//
// 📦 DÉPLOIEMENT
// - .NET Framework 4.8 (Windows uniquement)
// - Pas de conteneurisation possible
// → Solution : .NET 8 + Docker Linux
//
// ============================================================
