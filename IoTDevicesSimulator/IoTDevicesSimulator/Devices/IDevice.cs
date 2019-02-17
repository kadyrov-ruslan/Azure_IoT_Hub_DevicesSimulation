using Microsoft.Azure.Devices.Client;

namespace IoTDevicesSimulator.Devices
{
    public interface IDevice
    {
        string ConnectionString { get; set; }

        DeviceType Type { get; set; }

        string DeviceName { get; set; }

        long DeviceSendUnixTime { get; set; }

        int DelayInSec { get; set; }

        void SendDeviceToCloudMessagesAsync(DeviceClient client);
    }
}
