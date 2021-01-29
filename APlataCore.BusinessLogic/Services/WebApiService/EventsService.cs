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
    interface IEventsService : IBaseService<Events> { }
    public class EventsService : ServiceBase, IEventsService
    {
        private string BaseUri { get; set; }
        private TokensInfo TokensInfo { get; set; }
        private HttpClient Client;

        public EventsService()
        {
            BaseUri = GetStoragePath();
            TokensInfo = GetTokenAsync("demo", "demo1").Result;

            Client = new HttpClient();
            Client.BaseAddress = new Uri(BaseUri);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public EventsService(string login, string password)
        {
            BaseUri = GetStoragePath();
            TokensInfo = GetTokenAsync(login, password).Result;

            Client = new HttpClient();
            Client.BaseAddress = new Uri(BaseUri);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Events>> GetAllAsync()
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("events?token=" + TokensInfo.Token);

            List<Events> events = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                events = JsonConvert.DeserializeObject<List<Events>>(json);
            }

            return events;

        }

        #region EventsServices
        public async Task<Events> GetAsync(int? id)
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("events/" + id.ToString() + "?token=" + TokensInfo.Token);

            Events events = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                events = JsonConvert.DeserializeObject<Events>(json);
            }

            return events;
        }
        public async Task<bool> CreateAsync(Events events)
        {
            try
            {
                var json = JsonConvert.SerializeObject(events);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage postTask = await Client.PostAsync("events?token=" + TokensInfo.Token, data);

                return postTask.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<bool> EditAsync(int? Id, Events events)
        {
            try
            {
                var json = JsonConvert.SerializeObject(events);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage putTask = await Client.PutAsync("events/" + Id.ToString() + "?token=" + TokensInfo.Token, data);

                //HttpResponseMessage putTask = await Client.PutAsJsonAsync<Events>("api/events/id=" + events.Id.ToString(), events);

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
                HttpResponseMessage deleteTask = await Client.DeleteAsync("events/" + id.ToString() + "?token=" + TokensInfo.Token);

                return deleteTask.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<List<Events>> SearchQueryAsync(string query)
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("events?machine_name=" + query + "&token=" + TokensInfo.Token);

            List<Events> events = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                events = JsonConvert.DeserializeObject<List<Events>>(json);
            }

            return events;
        }
        public async Task<List<Events>> SearchLinqAsync(string query)
        {
            List<Events> events = await GetAllAsync();

            return events
                //.Where(x =>
                //x.Name.ToLower().Contains(query.ToLower()) ||
                //x.MachineModel.ToLower().Contains(query.ToLower()) ||
                //x.Address.ToLower().Contains(query.ToLower()) ||
                //x.ModelID.ToString().ToLower().Contains(query.ToLower()) ||
                //x.OwnerID.ToString().ToLower().Contains(query.ToLower())
                //)
                .ToList();
        }      
        #endregion
    }
}
