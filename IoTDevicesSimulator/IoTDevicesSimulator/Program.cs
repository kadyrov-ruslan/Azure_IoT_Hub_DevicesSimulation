using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;

namespace IoTDevicesSimulator
{
    public class Program
    {
        private static DeviceClient _deviceClient;

        private static int _telemetryInterval = 1; // Seconds

        private static void Main(string[] args)
        {
            args = new[] { "Fridge", "Http1" };
            var arg0Val = args[0];
            var deviceType = (DeviceType)Enum.Parse(typeof(DeviceType), arg0Val, true);
            var simulatingDevice = DeviceFactory.GetDevice(deviceType);
            _telemetryInterval = simulatingDevice.DelayInSec;

            var arg1Val = args[1];
            var protocol = (TransportType)Enum.Parse(typeof(TransportType), arg1Val);

            Console.WriteLine("IoT Hub Simulated device. Ctrl-C to exit.\n");
            // Connect to the IoT hub using the args protocol
            _deviceClient = DeviceClient.CreateFromConnectionString(simulatingDevice.ConnectionString, protocol);

            // Create a handler for the direct method call
            _deviceClient.SetMethodHandlerAsync("SetTelemetryInterval", SetTelemetryInterval, null).Wait();
            simulatingDevice.SendDeviceToCloudMessagesAsync(_deviceClient);
            Console.ReadLine();
        }

        private static Task<MethodResponse> SetTelemetryInterval(MethodRequest methodRequest, object userContext)
        {
            var data = Encoding.UTF8.GetString(methodRequest.Data);

            // Check the payload is a single integer value
            if (int.TryParse(data, out _telemetryInterval))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Telemetry interval set to {0} seconds", data);
                Console.ResetColor();

                // Acknowlege the direct method call with a 200 success message
                var result = "{\"result\":\"Executed direct method: " + methodRequest.Name + "\"}";
                return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(result), 200));
            }
            else
            {
                // Acknowlege the direct method call with a 400 error message
                var result = "{\"result\":\"Invalid parameter\"}";
                return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(result), 400));
            }
        }
    }
}
