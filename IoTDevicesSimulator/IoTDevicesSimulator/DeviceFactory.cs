using IoTDevicesSimulator.Devices;

namespace IoTDevicesSimulator
{
    public class DeviceFactory
    {
        /// <summary>
        /// Factory method
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IDevice GetDevice(DeviceType type)
        {
            IDevice device = null;
            switch (type)
            {
                case DeviceType.SimpleHome:
                    device = new SimpleHomeDevice
                    {
                        DeviceName = "Simple home IoT device",
                        ConnectionString = "HostName=IoT-DDOS-ML-Hub.azure-devices.net;DeviceId=SimpleHomeDevice;SharedAccessKey=OjMlZCdvC2piwWDL1/IMDYnQwh8L6ehTAQSPh+23esg=",
                        DelayInSec = 5
                    };
                    break;
                case DeviceType.Fridge:
                    device = new FridgeDevice
                    {
                        DeviceName = "Fridge IoT device",
                        ConnectionString = "HostName=IoT-DDOS-ML-Hub.azure-devices.net;DeviceId=SimpleHomeDevice;SharedAccessKey=OjMlZCdvC2piwWDL1/IMDYnQwh8L6ehTAQSPh+23esg=",
                        DelayInSec = 10
                    };
                    break;
                case DeviceType.WashingMachine:
                    device = new WashingMachineDevice
                    {
                        DeviceId = "id"
                    };
                    break;
            }

            return device;
        }
    }
}
