using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JobPortalBlazor.Client.Services
{
    public class Uploader
    {
        private readonly HttpClient httpClient;

        public Uploader(HttpClient httpClient) => this.httpClient = httpClient;

        public async Task<String> UploadFile(IBrowserFile file)
        {
            var content = new MultipartFormDataContent();
            content.Add(content: new StreamContent(file.OpenReadStream()),
                        name: "file",
                        fileName: file.Name);
            var response = await httpClient.PostAsync("/api/Upload", content);
            try { return await response.Content.ReadAsStringAsync(); }
            catch (Exception e) { Console.WriteLine(e); return ""; }
        }
    }
}
