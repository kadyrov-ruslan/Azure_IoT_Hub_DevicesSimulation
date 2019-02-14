using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IoTDevicesSimulator.Devices
{
    public class SimpleHomeDevice : IDevice
    {
        public string DeviceId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DeviceType Type { get; set; }
        public string DeviceName { get; set; }
        public ulong DeviceSendUnixTime { get; set; }

        [JsonIgnore] public int DelayInSec { get; set; } = 15;
        private double Temperature { get; set; }
        private double Humidity { get; set; }

        private const double MinTemperature = 20;
        private const double MinHumidity = 60;

        // Async method to send simulated telemetry
        public async void SendDeviceToCloudMessagesAsync(DeviceClient client)
        {
            // Initial telemetry values
            var rand = new Random();
            while (true)
            {
                Temperature = MinTemperature + rand.NextDouble() * 15;
                Humidity = MinHumidity + rand.NextDouble() * 20;

                var messageString = JsonConvert.SerializeObject(this);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                // Add a custom application property to the message.
                // An IoT hub can filter on these properties without access to the message body.
                message.Properties.Add("temperatureAlert", Temperature > 30 ? "true" : "false");

                // Send the telemetry message
                await client.SendEventAsync(message);
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);
                await Task.Delay(DelayInSec * 1000);
            }
        }
    }
}
