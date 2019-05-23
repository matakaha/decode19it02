using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Devices;

namespace decode19it02sdklist
{
    public static class Function1
    {
        private static string s_connectionString = "HostName=～～.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=～～=";

        [FunctionName("decode19it02sdklist")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string listtype = req.Query["listtype"];

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            listtype = listtype ?? data?.listtype;

            RegistryManager registryManager = RegistryManager.CreateFromConnectionString(s_connectionString);

            var sample = new ListManagerSample(registryManager);
            sample.SetListType(listtype);
            sample.RunSampleAsync().GetAwaiter().GetResult();

            return (ActionResult)new OkObjectResult(sample.GetList());

        }
    }
}
