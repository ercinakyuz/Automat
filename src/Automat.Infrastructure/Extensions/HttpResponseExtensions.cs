using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Automat.Infrastructure.Extensions
{
    public static class HttpResponseExtensions
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer()
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        public static void WriteJson<T>(this HttpResponse response, T obj, string contentType = null)
        {
            response.ContentType = contentType ?? "application/json";
            using (HttpResponseStreamWriter responseStreamWriter = new HttpResponseStreamWriter(response.Body, Encoding.UTF8))
            {
                using (JsonTextWriter jsonTextWriter = new JsonTextWriter(responseStreamWriter))
                {
                    jsonTextWriter.CloseOutput = false;
                    Serializer.Serialize(jsonTextWriter, obj);
                }
            }
        }
    }
}