using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Azure.Devices;

namespace WebApplication1.Helpers
{
    static public class IoTHubHelper
    {
        public static async Task<int> SendDirectMethod(int seconds)
        {
            try
            {
                var iothubconnection = ConfigurationManager.AppSettings["IoTHubConnection"];

                using (var serviceClient = ServiceClient.CreateFromConnectionString(iothubconnection))
                {
                    var methodInvocation = new CloudToDeviceMethod("spinmotor") { ResponseTimeout = TimeSpan.FromSeconds(30) };

                    methodInvocation.SetPayloadJson(seconds.ToString());

                    // Invoke the direct method asynchronously and get the response from the simulated device.
                    var response = await serviceClient.InvokeDeviceMethodAsync("feeder", methodInvocation);
                    return response.Status;
                }
            }
            catch (ConfigurationErrorsException ex)
            {
                Console.WriteLine($"Error reading app settings {ex.Message}");
                Trace.TraceError(ex.Message);
                throw ex;
            }
        }

    }

 

}