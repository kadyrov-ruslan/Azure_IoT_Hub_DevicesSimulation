using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IoTDevicesSimulator.Devices
{
    /// <summary>
    /// Model of IoT device that is stated in fredge at home
    /// </summary>
    public class FridgeDevice : IDevice
    {
        [JsonIgnore]
        public string ConnectionString { get; set; }

        [JsonIgnore]
        public int DelayInSec { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DeviceType Type { get; set; }
        public string DeviceName { get; set; }
        public long DeviceSendUnixTime { get; set; }
        public double MainChamberTemperature { get; set; }
        public double FreezeChamberTemperature { get; set; }

        public bool MainChamberIsOpen { get; set; }

        public bool FreezeChamberIsOpen { get; set; }

        private const double MinTemperature = -20;
        private const double MinHumidity = 60;

        // Async method to send simulated telemetry
        public async void SendDeviceToCloudMessagesAsync(DeviceClient client)
        {
            // Initial telemetry values
            var rand = new Random();
            while (true)
            {
                MainChamberTemperature = MinTemperature + rand.NextDouble() * 5;
                FreezeChamberTemperature = MinTemperature + rand.NextDouble() * 1;

                DeviceSendUnixTime = DateTime.UtcNow.Ticks;

                var messageString = JsonConvert.SerializeObject(this);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                // Add a custom application property to the message.
                // An IoT hub can filter on these properties without access to the message body.
                message.Properties.Add("mainChamberTemperatureAlert", MainChamberTemperature > 20 ? "true" : "false");
                message.Properties.Add("freezeTemperatureAlert", FreezeChamberTemperature > 0 ? "true" : "false");

                // Send the telemetry message
                await client.SendEventAsync(message);
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);
                await Task.Delay(DelayInSec * 1000);
            }
        }
    }
}
