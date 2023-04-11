using Android.BLE;
using Android.BLE.Commands;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DeviceButton : MonoBehaviour
{
    private string _deviceUuid = string.Empty;
    private string _deviceName = string.Empty;

    [SerializeField]
    private Text _deviceUuidText;
    [SerializeField]
    private Text _deviceNameText;

    [SerializeField]
    private Image _deviceButtonImage;
    [SerializeField]
    private Text _deviceButtonText;

    [SerializeField]
    private Color _onConnectedColor;
    private Color _previousColor;

    private bool _isConnected = false;

    private ConnectToDevice _connectCommand;
    private ReadFromCharacteristic _readFromCharacteristic;

    private WriteToCharacteristic _writeToCharacteristic;

    public void Show(string uuid, string name)
    {
        _deviceButtonText.text = "Connect";

        _deviceUuid = uuid;
        _deviceName = name;

        _deviceUuidText.text = uuid;
        _deviceNameText.text = name;
    }

    public void Connect()
    {
        if (!_isConnected)
        {
            _connectCommand = new ConnectToDevice(_deviceUuid, OnConnected, OnDisconnected);
            BleManager.Instance.QueueCommand(_connectCommand);
        }
        else
        {
            _connectCommand.Disconnect();
        }
    }


    public void SubscribeToExampleService()
    {
        //Replace these Characteristics with YOUR device's characteristics
        _readFromCharacteristic = new ReadFromCharacteristic(_deviceUuid, "180a", "2a57", (byte[] value) =>
        {
            Debug.Log("hello there: " + Encoding.UTF8.GetString(value));
        });
        BleManager.Instance.QueueCommand(_readFromCharacteristic);

        Debug.Log("characteristic run " + _deviceUuid);
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(_deviceUuid, "180a", "2a57", "2"));


    }

    private void OnConnected(string deviceUuid)
    {
        _previousColor = _deviceButtonImage.color;
        _deviceButtonImage.color = _onConnectedColor;

        _isConnected = true;
        _deviceButtonText.text = "Disconnect";
        FindObjectOfType<ExampleBleInteractor>().deviceUuid = _deviceUuid;
        // SubscribeToExampleService();
    }

    private void OnDisconnected(string deviceUuid)
    {
        _deviceButtonImage.color = _previousColor;

        _isConnected = false;
        _deviceButtonText.text = "Connect";
    }


    // public void OnButtonPress2()
    // {
    //     Debug.Log("characteristic run");
    //     BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceAddress, "180a", "2a57", "3"));
    // }

}
