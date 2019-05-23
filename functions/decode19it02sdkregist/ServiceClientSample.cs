using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Shared;
using System.Threading.Tasks;

namespace decode19it02sdkregist
{
    class RegistryManagerSample
    {
        private string DeviceId = "";

        private readonly RegistryManager _registryManager;

        public void SetRegistId(string deviceid)
        {
            DeviceId = deviceid;
        }
        public RegistryManagerSample(RegistryManager registryManager)
        {
            _registryManager = registryManager ?? throw new ArgumentNullException(nameof(registryManager));
        }

        public async Task RunSampleAsync()
        {
            await AddDeviceAsync(DeviceId).ConfigureAwait(false);
        }

        public async Task AddDeviceAsync(string deviceId)
        {
            Console.Write($"Adding device '{deviceId}' with default authentication . . . ");
            await _registryManager.AddDeviceAsync(new Device(deviceId)).ConfigureAwait(false);
            Console.WriteLine("DONE");
        }

    }
}



