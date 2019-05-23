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

namespace decode19it02sdkregist
{
    public static class Function1
    {
        private static string s_connectionString = "HostName=～～.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=～～=";

        [FunctionName("decode19it02sdkregist")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            RegistryManager registryManager = RegistryManager.CreateFromConnectionString(s_connectionString);

            var sample = new RegistryManagerSample(registryManager);
            sample.SetRegistId(name);
            sample.RunSampleAsync().GetAwaiter().GetResult();


            return name != null
                ? (ActionResult)new OkObjectResult($"【{name}】を登録しました")
                : new BadRequestObjectResult("デバイスIDを入力後、登録ボタンを押してください");

        }
    }
}
