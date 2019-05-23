using System;
using Microsoft.Azure.Devices;

namespace decode19it02sdkconsoleregist
{
    class Program
    {
        private static string s_connectionString = "HostName=～～.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=～～=";
        static void Main(string[] args)
        {
            Console.WriteLine("一括登録を開始します");
            RegistryManager registryManager = RegistryManager.CreateFromConnectionString(s_connectionString);
            var sample = new ServiceClientSample(registryManager);
            sample.AddDeviceAsync();
            Console.WriteLine("登録が完了しました");
            Console.WriteLine("何かキーを押してください...");
            Console.ReadKey();
        }
    }
}
