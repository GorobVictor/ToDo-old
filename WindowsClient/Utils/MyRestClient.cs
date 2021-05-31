using Core.Dto.UserDto;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WindowsClient.Utils
{
    class MyRestClient
    {
        public static RestClient Client { get; set; } = new RestClient("https://localhost:44327/api");
        public static async Task<GetTokenResult> LoginAsync(UserAuth userAuth)
        {

            var request = new RestRequest("account/sign-in", Method.POST, RestSharp.DataFormat.Json)
                .AddJsonBody(userAuth);
            var response = await Client.ExecuteAsync<GetTokenResult>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Client.Authenticator = new JwtAuthenticator(response.Data.Token);
                return response.Data;
            }
            else
            {
                return null;
            }
        }
    }
}
