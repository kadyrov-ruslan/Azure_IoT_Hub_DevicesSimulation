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
                        DeviceId = "id",
                        DeviceName = "Simple home IoT device"
                    };
                    break;
                case DeviceType.Chiller:
                    device = new SimpleHomeDevice
                    {
                        DeviceId = "id"
                    };
                    break;
                case DeviceType.WashingMachine:
                    device = new SimpleHomeDevice
                    {
                        DeviceId = "id"
                    };
                    break;
            }

            return device;
        }
    }
}
