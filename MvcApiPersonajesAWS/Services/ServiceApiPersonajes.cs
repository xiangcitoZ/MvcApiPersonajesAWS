﻿using MvcApiPersonajesAWS.Models;
using System.Net.Http.Headers;
using System.Net.Security;

namespace MvcApiPersonajesAWS.Services
{
    public class ServiceApiPersonajes
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiPersonajes(IConfiguration configuration)
        {
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiPersonajes");
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<string> TestApiAsync()
        {
            string request = "/api/personajes";
            //UTILIZAMOS UN MANEJADOR PARA LA PETICION DEL HttpClient
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback =
                   (message, cert, chain, SslPolicyErrors) =>
                   {
                       return true;
                   };
            HttpClient client = new HttpClient(handler);

            client.BaseAddress = new Uri(this.UrlApi);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(this.Header);
            HttpResponseMessage response = await client.GetAsync(request);
            return "Respuesta: " + response.StatusCode;

        }





        private async Task<T> CallApiAsync<T>(string request)
        {   

            using(HttpClientHandler handler = new HttpClientHandler()) 
            {
                //INDICAMOS AL MANEJADOR COMO SE COMPORTARA AL RECIBIR PETICIONES
                handler.ServerCertificateCustomValidationCallback =
                    (message, cert, chain, SslPolicyErrors) =>
                    {
                        return true;
                    };

                using (HttpClient client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(this.UrlApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.Header);
                    HttpResponseMessage response =
                        await client.GetAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        T data = await response.Content.ReadAsAsync<T>();
                        return data;
                    }
                    else
                    {
                        return default(T);
                    }
                }

            }

            
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "/api/personajes";
            List<Personaje> personajes =
                await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }




    }
}
