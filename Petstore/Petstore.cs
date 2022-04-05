using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;


namespace Petstore_Swagger_Testing
{
    internal class Petstore
    {

        public static Task<RestResponse> SendRequest(string requestUrl, string method, string body)
        {
            string url = "https://petstore.swagger.io/v2";

            var client = new RestClient(url);

            RestRequest request = null;

            switch (method)   {
                case "GET":
                    request = new RestRequest(requestUrl, Method.Get);
                    break;
                case "POST":
                    request = new RestRequest(requestUrl, Method.Post);
                    break;
                case "PUT":
                    request = new RestRequest(requestUrl, Method.Put);
                    break;
                case "DELETE":
                    request = new RestRequest(requestUrl, Method.Delete);
                    break;
                default:
                    throw new ArgumentException("request variable is null");
            }
            
            request.AddOrUpdateHeader("Content-Type", "application/json");
            request.AddOrUpdateHeader("Accept", "application/json");
            request.AddOrUpdateParameter("application/json", body, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            var response = client.ExecuteAsync(request);

            return response;
        }
    }
}
