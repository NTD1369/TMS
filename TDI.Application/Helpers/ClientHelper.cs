using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TDI.API.Helpers
{
    public class ClientHelper
    {
        public HttpClient _client = new HttpClient();
        public HashCode Crypt = new HashCode();

        public void conect()
        {
            
            string localhostString = "https://localhost:7060/";

            _client.Timeout = TimeSpan.Parse("00:25:00");
            _client.BaseAddress = new Uri(localhostString);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //_client.Dispose();
        }
    }
}
