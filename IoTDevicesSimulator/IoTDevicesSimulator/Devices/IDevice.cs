using Microsoft.Azure.Devices.Client;

namespace IoTDevicesSimulator.Devices
{
    public interface IDevice
    {
        string DeviceId { get; set; }

        DeviceType Type { get; set; }

        string DeviceName { get; set; }

        ulong DeviceSendUnixTime { get; set; }

        int DelayInSec { get; set; }

        void SendDeviceToCloudMessagesAsync(DeviceClient client);
    }
}
