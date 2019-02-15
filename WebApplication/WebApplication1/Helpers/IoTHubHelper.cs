using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Azure.Devices;

namespace WebApplication1.Helpers
{
    static public class IoTHubHelper
    {
        private static ServiceClient s_serviceClient;

        static public bool SendDirectMethod(int seconds)
        {
            try {
            
                var iothubconnection = ReadSetting("IoTHubConnection");
                if (iothubconnection == "Not found") return false;

                s_serviceClient = ServiceClient.CreateFromConnectionString(iothubconnection);
                InvokeMethod().GetAwaiter().GetResult();
                return true;

            }
            catch 
            {
                return false;
            }
        }

        private static async Task InvokeMethod()
        {
            var methodInvocation = new CloudToDeviceMethod("spinmotor") { ResponseTimeout = TimeSpan.FromSeconds(30) };

            methodInvocation.SetPayloadJson("10");

            // Invoke the direct method asynchronously and get the response from the simulated device.
            var response = await s_serviceClient.InvokeDeviceMethodAsync("feeder", methodInvocation);

            Console.WriteLine("Response status: {0}, payload:", response.Status);
            Console.WriteLine(response.GetPayloadAsJson());
        }

        static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                Console.WriteLine(result);
                return result;
            }
            catch (ConfigurationErrorsException ex)
            {
                Console.WriteLine("Error reading app settings");
                throw ex;
            }
        }
    }

 

}