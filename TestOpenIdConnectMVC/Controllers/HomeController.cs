using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestOpenIdConnect.Models;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace TestOpenIdConnect.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {


        private string _filePath;

        private IDictionary<string, string> _settings = new Dictionary<string, string>();
        private IDictionary<string, string> _settingComments = new Dictionary<string, string>();

        private UTF8Encoding _encoding = new UTF8Encoding();

        private const char SPLIT_CHAR = '=';


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // test security   

        public void Save()
        {
            using (FileStream stream = new FileStream(_filePath, FileMode.Create))
            {
                byte[] data = ToByteArray();

                stream.Write(data, 0, data.Length);
            }
        }

        private byte[] ToByteArray()
        {
            StringBuilder builder = new StringBuilder();

            foreach (var pair in _settings)
            {
                if (_settingComments.ContainsKey(pair.Key))
                {
                    builder.Append(_settingComments[pair.Key]);
                    builder.AppendLine();
                }

                builder.AppendFormat("{0}={1}", pair.Key, pair.Value);
                builder.AppendLine();
            }

            return _encoding.GetBytes(builder.ToString());
        }

            public string UpdateCustomerPassword(string txtUsername, string txtPassword)
            {
                string userName = txtUsername;
                string password = txtPassword;

                Regex testPassword = new Regex(userName);
                Match match = testPassword.Match(password);
                if (match.Success)
                {
                    return "Do not include name in password.";
                }
                else
                {
                    return "Good password.";
                }
            }

        }
    }
 
