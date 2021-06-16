using Core.Dto.TasksDto;
using Core.Dto.UserDto;
using Core.Entities;
using Core.Enum;
using Core.Utils;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WindowsClient.Utils
{
    class MyRestClient
    {
        public static RestClient Client { get; set; } = new RestClient("http://user15336.realhost-free.net/api/");

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

        public static async Task<GetTokenResult> SignUpAsync(UserSignUp userSignOut)
        {

            var request = new RestRequest("account/sign-up", Method.POST, RestSharp.DataFormat.Json)
                .AddJsonBody(userSignOut);
            var response = await Client.ExecuteAsync<GetTokenResult>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Client.Authenticator = new JwtAuthenticator(response.Data.Token);
                return response.Data;
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var error = JsonConvert.DeserializeObject<FriendlyException>(response.Content);

                MessageBox.Show(error.Message);
                return null;
            }
            else
            {
                return null;
            }
        }

        public static async Task<Tasks> AddTask(CreateTaskDto task)
        {

            var request = new RestRequest("task/create-task", Method.POST, RestSharp.DataFormat.Json)
                .AddJsonBody(task);
            var response = await Client.ExecuteAsync<Tasks>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Data;
            }
            else
            {
                return null;
            }
        }

        public static async Task UpdateTaskStatusAsync(long taskId, bool status)
        {

            var request = new RestRequest($"task/{taskId}/update-status?status={status}", Method.PUT);
            var response = await Client.ExecuteAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Not updated status");
            }
        }

        public static async Task UpdateTaskFavoritesAsync(long taskId, bool favorite)
        {

            var request = new RestRequest($"task/{taskId}/update-favorite?favorite={favorite}", Method.PUT);
            var response = await Client.ExecuteAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Not updated favorites");
            }
        }

        public static async Task UpdateTaskNameAsync(long taskId, string name)
        {

            var request = new RestRequest($"task/{taskId}/update-name?name={name}", Method.PUT);
            var response = await Client.ExecuteAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Not updated name");
            }
        }

        public static async Task DeleteTask(long taskId)
        {

            var request = new RestRequest($"task/{taskId}/delete", Method.DELETE);
            var response = await Client.ExecuteAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Not found task");
            }
        }

        public static async Task DeleteTask(List<long> taskIds)
        {
            var request = new RestRequest($"task/delete", Method.DELETE, RestSharp.DataFormat.Json).AddJsonBody(taskIds);
            var response = await Client.ExecuteAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Not found task");
            }
        }
    }
}
