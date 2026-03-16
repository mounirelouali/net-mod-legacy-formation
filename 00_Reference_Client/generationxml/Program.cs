using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Xml.Serialization;
using generationxml; // For models and rules

namespace generationxml
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "your_connection_string_here";

            var data = GetDataFromDb(connectionString); // Dictionary<string, string>

            // Example: rules could be loaded from DB/config
            var rules = new List<TagRule>
            {
                new TagRule
                {
                    TagName = "Name",
                    Rules = new List<IRule>
                    {
                        new MandatoryRule(),
                        new MinLengthRule(3),
                        new MaxLengthRule(10),
                        new ForbiddenCharsRule(new[] { 'T', 'u' }),
                        new AuthorizedCharsRule("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray())
                    }
                },
                new TagRule
                {
                    TagName = "Code",
                    Rules = new List<IRule>
                    {
                        new MandatoryRule(),
                        new MinLengthRule(2),
                        new MaxLengthRule(5),
                        new ForbiddenCharsRule(new[] { 'X', 'Y' }),
                        new AuthorizedCharsRule("0123456789".ToCharArray())
                    }
                }
            };

            var validModels = new List<MyXmlModel>();
            var invalidEntries = new List<string>();

            var model = new MyXmlModel();
            // TODO: Set properties on model and its children from your data as needed

            bool hasValid = false;
            ValidateObject(model, rules, invalidEntries, ref hasValid);

            if (hasValid)
                validModels.Add(model);

            if (invalidEntries.Count > 0)
            {
                SendEmail("admin@example.com", "Invalid XML Data",
                    "The following entries are invalid:\n" + string.Join("\n", invalidEntries));
            }

            var serializer = new XmlSerializer(typeof(List<MyXmlModel>), new XmlRootAttribute("Root"));
            using (var writer = new StreamWriter("output.xml"))
            {
                serializer.Serialize(writer, validModels);
            }
        }

        static Dictionary<string, string> GetDataFromDb(string connectionString)
        {
            var data = new Dictionary<string, string>();
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT Tag, Value FROM DataTable", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data[reader.GetString(0)] = reader.GetString(1);
                    }
                }
            }
            return data;
        }

        static void SendEmail(string to, string subject, string body)
        {
            var message = new MailMessage("noreply@example.com", to, subject, body);
            var client = new SmtpClient("smtp.example.com")
            {
                Credentials = new NetworkCredential("username", "password"),
                EnableSsl = true
            };
            client.Send(message);
        }

        // Recursive validation for model and child classes
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
                else if (prop.PropertyType.IsClass && prop.PropertyType != typeof(string) && !prop.PropertyType.FullName.StartsWith("System."))
                {
                    ValidateObject(value, rules, invalidEntries, ref hasValid);
                }
                else if (typeof(System.Collections.IEnumerable).IsAssignableFrom(prop.PropertyType) && prop.PropertyType != typeof(string))
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
}
