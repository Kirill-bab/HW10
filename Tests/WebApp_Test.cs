using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tests
{
    public static class WebApp_Test
    {
        private static HttpClient _httpClient;

        public static void Setup()
        {

            var connectionSettings = JsonSerializer.Deserialize<ConnectionSettings>(File.ReadAllText("connectionSettings.json"));

            if (!File.Exists("connectionSettings.json"))
            {
                Console.WriteLine("File with connection settings was not located!");
                throw new FileNotFoundException("connectionSettings.json");
            }

            Uri.TryCreate(connectionSettings.BaseAddress, UriKind.Absolute, out var uri);

            _httpClient = new HttpClient()
            {
                BaseAddress = uri
            };
        }

        public static void Should_Return_Program_Info_With_OK_StatusCode()
        {
            SeparatorLine();
            var response = Task.Run(async () =>
           {
               return await _httpClient.GetAsync("/Status/");
           }).Result;
            Console.WriteLine("Test get request Status/ performed!");
            var responseBody = Task.Run(async () =>
            {
                 return await response.Content.ReadAsStringAsync();
            }).Result;
            Console.WriteLine($"get request Status/ returned: {responseBody}\n with Status code: {response.StatusCode}");
            if (!string.IsNullOrEmpty(responseBody))
            {
                Console.WriteLine("get request Status/ was successfull!");
            }
            else
            {
                Console.WriteLine("get request Status/ has failed!");
            }
        }

        public static void Should_Return_OK_StatusCode_When_Number_Is_Prime()
        {
            SeparatorLine();
            var response = Task.Run(async () =>
            {
                return await _httpClient.GetAsync("/Primes/primes/7");
            }).Result;
            var responseBody = Task.Run(async () =>
            {
                return await response.Content.ReadAsStringAsync();
            }).Result;
            Console.WriteLine($"Test get request Primes/primes/7 performed!");
            Console.WriteLine($"get request Primes/primes/7 returned: {responseBody}\n with Status code: {response.StatusCode}");

            if (HttpStatusCode.OK == response.StatusCode)
            {
                Console.WriteLine("get request Primes/primes/7 was successfull!");
            }
            else
            {
                Console.WriteLine("get request Primes/primes/7 has failed!");
            }
        }

        public static void Should_Return_NotFound_StatusCode_When_Number_Is_Not_Prime()
        {
            SeparatorLine();
            var response = Task.Run(async () =>
            {
                return await _httpClient.GetAsync($"Primes/primes/4");
            }).Result;
            var responseBody = Task.Run(async () =>
            {
                return await response.Content.ReadAsStringAsync();
            }).Result;
            Console.WriteLine($"Test get request Primes/primes/4 performed!");
            Console.WriteLine($"get request Primes/primes/4 returned: {responseBody}\n with Status code: {response.StatusCode}");

            if (HttpStatusCode.NotFound == response.StatusCode)
            {
                Console.WriteLine("get request Primes/primes/4 was successfull!");
            }
            else
            {
                Console.WriteLine("get request Primes/primes/4 has failed!");
            }
        }

        public static void Should_Return_Not_Empty_Primes_List_With_StatusCode_OK()
        {
            SeparatorLine();
            var response = Task.Run(async () =>
            {
                return await _httpClient.GetAsync("Primes/primes?from=1&to=5");
            }).Result;
            Console.WriteLine("Test get request Primes/primes?from=1&to=5 performed!");

            var responseBody = Task.Run(async () =>
            {
                return await response.Content.ReadAsStringAsync();
            }).Result;
            Console.WriteLine($"get request Primes/primes?from=1&to=5 returned: {responseBody}\n with Status code: {response.StatusCode}");

            if (HttpStatusCode.OK == response.StatusCode && responseBody == "[2,3,5]")
            {
                Console.WriteLine("request Primes/primes?from=1&to=5 was successfull!");
            }
            else
            {
                Console.WriteLine("request Primes/primes?from=1&to=5 failed!");
            }
        }

        public static void Should_Return_Empty_Primes_List_With_StatusCode_OK()
        {
            SeparatorLine();
            var response = Task.Run(async () =>
            {
                return await _httpClient.GetAsync("Primes/primes?from=-5&to=1");
            }).Result;

            Console.WriteLine("Test get request Primes/primes?from=-5&to=1 performed!");

            var responseBody = Task.Run(async () =>
            {
                return await response.Content.ReadAsStringAsync();
            }).Result;
            Console.WriteLine($"get request Primes/primes?from=-5&to=1 returned: {responseBody}\n with Status code: {response.StatusCode}");

            if(HttpStatusCode.OK == response.StatusCode && responseBody == "[]")
            {
                Console.WriteLine("get request Primes/primes?from=-5&to=1 was successfull!");
            }
            else
            {
                Console.WriteLine("get request Primes/primes?from=-5&to=1 failed!");
            }
        }

        public static void Should_Return_Nothing_With_StatusCode_BadRequest()
        {
            SeparatorLine();
            var response = Task.Run(async () =>
            {
                return await _httpClient.GetAsync("Primes/primes?from=-5&to=abc");
            }).Result; 
            Console.WriteLine("Test get request Primes/primes?from=-5&to=abc performed!");

            var responseBody = Task.Run(async () =>
            {
                return await response.Content.ReadAsStringAsync();
            }).Result; 
            Console.WriteLine($"get request Primes/primes?from=-5&to=abc returned: {responseBody}\n with Status code: {response.StatusCode}");

            if(HttpStatusCode.BadRequest == response.StatusCode)
            {
                Console.WriteLine("get request Primes/primes?from=-5&to=abc was successfull!");
            }
            else
            {
                Console.WriteLine("get request Primes/primes?from=-5&to=abc failed!");
            }
        }

        private static void SeparatorLine()
        {
            Console.WriteLine("=======================================================");
        }
    }
}
