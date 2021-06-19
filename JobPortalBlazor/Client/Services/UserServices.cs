using JobPortalBlazor.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace JobPortalBlazor.Client.Services
{
    public class UserServices
    {
        private readonly HttpClient httpClient;
        public UserServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<Models.User> GetUserByID(String id)
        {
            return null;
        }
    }
}
