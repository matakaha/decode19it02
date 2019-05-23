using Microsoft.Azure.Devices;
using System;
using System.Collections.Generic;
using System.Text;

namespace decode19it02sdkconsoleregist
{
    class ServiceClientSample
    {
        private string DeviceId = "device";

        private readonly RegistryManager _registryManager;

        public ServiceClientSample(RegistryManager registryManager)
        {
            _registryManager = registryManager ?? throw new ArgumentNullException(nameof(registryManager));
        }

        public void AddDeviceAsync()
        {
            string deviceid = DeviceId;
            for (int i = 1; i <= 20; i++)
            {
                deviceid = string.Concat(DeviceId,(i).ToString("D4"));
                _registryManager.AddDeviceAsync(new Device(deviceid)).ConfigureAwait(false);
                Console.WriteLine($"{deviceid}を登録しました");
            }

        }

    }
}



