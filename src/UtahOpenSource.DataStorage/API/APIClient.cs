using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UtahOpenSource.DataObjects.UTOSDataModels;

namespace UtahOpenSource.DataStorage.API
{
    public class APIClient
    {
        public async Task<IEnumerable<Talk>> GetAllSessions()
        {
            var resultStream = await GetHttpResultStream(Resources.SessionsURL);
            if (resultStream != null)
            {
                using (var reader = new JsonTextReader(new StreamReader(resultStream)))
                    return JsonSerializer.Create().Deserialize<IEnumerable<Talk>>(reader);
            }
            return null;
        }
        protected async Task<Stream> GetHttpResultStream(string url)
        {
            HttpClient client = new HttpClient();
            var result = await client.GetAsync(url);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
                return await result.Content.ReadAsStreamAsync();
            else
                return null;
        }
    }
}
