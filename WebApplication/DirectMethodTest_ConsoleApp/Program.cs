using System;

// For samples see: https://github.com/Azure/azure-iot-sdk-csharp/tree/master/iothub/service
using System.Threading.Tasks;
using Microsoft.Azure.Devices;

namespace DirectMethodTest_ConsoleApp
{
    class Program
    {

        private static ServiceClient s_serviceClient;

        // Connection string for your IoT Hub
        // az iot hub show-connection-string --hub-name {your iot hub name}
        private readonly static string s_connectionString = "{connection-string}";


        // Invoke the direct method on the device
        private static async Task InvokeMethod()
        {
            var methodInvocation = new CloudToDeviceMethod("spinmotor") { ResponseTimeout = TimeSpan.FromSeconds(30) };
            methodInvocation.SetPayloadJson("10"); //num of seconds

            // Invoke the direct method asynchronously and get the response from the simulated device.
            var response = await s_serviceClient.InvokeDeviceMethodAsync("feeder", methodInvocation);

            Console.WriteLine("Response status: {0}, payload:", response.Status);
            Console.WriteLine(response.GetPayloadAsJson());
        }

        static void Main(string[] args)
        {
            Console.WriteLine("IoT Hub Method Invocation.\n Rabbit Feeder Test #1");

            // Create a ServiceClient to communicate with service-facing endpoint on your hub.
            s_serviceClient = ServiceClient.CreateFromConnectionString(s_connectionString);

            while (true)  //I want to send several C2D messages (or method invocations)
            {
                InvokeMethod().GetAwaiter().GetResult();
                Console.WriteLine("Press Enter to send another msg!!");
                Console.ReadLine();
            }
        }
    }
}
