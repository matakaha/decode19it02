using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Shared;
using System.Threading.Tasks;

namespace decode19it02sdklist
{
    class ListManagerSample
    {
        private string ListType = "";
        private string ResultList = "";
        private readonly RegistryManager _registryManager;

        public void SetListType(string listtype)
        {
            ListType = listtype;
        }
        public ListManagerSample(RegistryManager registryManager)
        {
            _registryManager = registryManager ?? throw new ArgumentNullException(nameof(registryManager));
        }

        public async Task RunSampleAsync()
        {
            await EnumerateTwinsAsync().ConfigureAwait(false);
        }

        public async Task EnumerateTwinsAsync()
        {
            Console.WriteLine("Querying devices:");
            string QueryString = "select * from devices";
            switch (ListType)
            {
                case "1":
                    QueryString += " where connectionState = 'Connected'";
                    break;
                case "2":
                    QueryString += " where connectionState = 'Disconnected'";
                    break;
            }
            var query = _registryManager.CreateQuery(QueryString);
            ResultList = "<table><tr><th>デバイスID</th><th>状態</th><th>最終アクティビティ</th><th>前回の状態の更新</th><th>認証の種類</th><th>クラウドからデバイスへのメッセ...</th></tr>";

            while (query.HasMoreResults)
            {
                IEnumerable<Twin> twins = await query.GetNextAsTwinAsync().ConfigureAwait(false);

                foreach (Twin twin in twins)
                {
                    ResultList += $"<tr><td>{twin.DeviceId}</td><td>{twin.Status}</td>";
                    if(twin.LastActivityTime == DateTime.Parse("1/1/0001 12:00:00 AM"))
                    {
                        ResultList += "<td></td>";
                    }
                    else
                    {
                        ResultList += $"<td>{twin.LastActivityTime}</td>";
                    }
                    // とりあえず省略
                    ResultList += "<td></td><td></td><td>0</td></tr>";

                }
            }
            ResultList += "</table>";

        }
        public string GetList()
        {
            return ResultList;
        }
    }
}


