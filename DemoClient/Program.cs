using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var login = "SC9PE3U5PNMGMBSDYMQMZ4OEVX57UQBC";
            var password = "na9K8qk6wIWJPx3UwtarI4szWZ0FtBhHN0UHqaWygdnQcVlBELLFbykUo2B/" +
                            "JxMHhi1qGxwVizoozGVRa8hCJDmIkOxTW4hHD32gSDNeIUNU9d5fe4+WDSx" +
                            "EmGBqvBc2v8HdXJ6+4p487Y+Vbx+lkBowpRpnNP5EmIYW4m42Lo1rlZaUZ/jD/zGmuG0ldDuU";

            Console.WriteLine("Gerando novo token...");
            // Obter token            
            var clientToken = new RestClient("http://localhost/segurancaapi/"); // Local de onde a API para geração do token está publicada
            var requestToken = new RestRequest("/api/v1.0/security/token", Method.POST);
            requestToken.AddParameter("content-type", "application/x-www-form-urlencoded", ParameterType.HttpHeader);            
            requestToken.AddParameter("login", login, ParameterType.HttpHeader);
            requestToken.AddParameter("password", password, ParameterType.HttpHeader);            
            //Fixo 'password'
            string encodedBody = "grant_type=password";  
            requestToken.AddParameter("application/x-www-form-urlencoded", encodedBody, ParameterType.RequestBody);

            var response = clientToken.Execute<Token>(requestToken).Content;
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(response);
            Console.WriteLine("");
            Console.WriteLine(string.Format("Token gerado: {0}", result.access_token));

            Console.WriteLine("");
            Console.WriteLine("Consumindo API passando o token gerado...");
            // Consumir API passando o token de autorização
            var client = new RestClient("http://localhost:13232");
            var request = new RestRequest("/api/demo", Method.GET);
            request.AddParameter("Authorization", string.Format("bearer {0}", result.access_token), ParameterType.HttpHeader);
            var responseClient = client.Execute(request).Content;
            
            Console.WriteLine(string.Format("resultado da API: {0}", responseClient));
            Console.ReadKey();
        }
    }

    class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
    }
}
