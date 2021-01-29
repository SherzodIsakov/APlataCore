using APlataCore.BusinessLogic.Infrastructure;
using APlataCore.Domain.Infrastructure;
using APlataCore.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace APlataCore.BusinessLogic.Services.WebApiService
{
    interface IUsersService : IBaseService<Users> { }
    public class UsersService : ServiceBase, IUsersService
    {
        private string BaseUri { get; set; }
        private TokensInfo TokensInfo { get; set; }
        private HttpClient Client;

        public UsersService()
        {
            BaseUri = GetStoragePath();
            TokensInfo = GetTokenAsync("demo", "demo1").Result;

            Client = new HttpClient();
            Client.BaseAddress = new Uri(BaseUri);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public UsersService(string login, string password)
        {
            BaseUri = GetStoragePath();
            TokensInfo = GetTokenAsync(login, password).Result;

            Client = new HttpClient();
            Client.BaseAddress = new Uri(BaseUri);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Users>> GetAllAsync()
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("users?token=" + TokensInfo.Token);

            List<Users> users = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<Users>>(json);
            }

            return users;
        }
        public async Task<Users> GetAsync(int? id)
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("users/" + id.ToString() + "?token=" + TokensInfo.Token);

            Users users = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<Users>(json);
            }

            return users;
        }

        #region UsersServices
        public async Task<bool> CreateAsync(Users users)
        {
            try
            {
                var json = JsonConvert.SerializeObject(users);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage postTask = await Client.PostAsync("users?token=" + TokensInfo.Token, data);

                return postTask.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<bool> EditAsync(int? Id, Users users)
        {
            try
            {
                var json = JsonConvert.SerializeObject(users);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage putTask = await Client.PutAsync("users/" + Id.ToString() + "?token=" + TokensInfo.Token, data);

                //HttpResponseMessage putTask = await Client.PutAsJsonAsync<Users>("api/users/id=" + users.Id.ToString(), users);

                return putTask.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<bool> DeleteAsync(int? id)
        {
            try
            {
                HttpResponseMessage deleteTask = await Client.DeleteAsync("users/" + id.ToString() + "?token=" + TokensInfo.Token);

                return deleteTask.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<List<Users>> SearchQueryAsync(string query)
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("users?machine_name=" + query + "&token=" + TokensInfo.Token);

            List<Users> users = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<Users>>(json);
            }

            return users;
        }
        public async Task<List<Users>> SearchLinqAsync(string query)
        {
            List<Users> users = await GetAllAsync();

            return users
                .Where(x =>
                x.Name.ToLower().Contains(query.ToLower()) ||
                x.Login.ToString().ToLower().Contains(query.ToLower())
                )
                .ToList();
        }
        #endregion
    }
}