using UnityEngine;
using Android.BLE;
using Android.BLE.Commands;
using KartGame.KartSystems;
using System.Text;
using TMPro;

public class ExampleBleInteractor : MonoBehaviour
{
    [SerializeField] private GameObject _deviceButton;
    [SerializeField] private Transform _deviceList;

    [SerializeField] private int _scanTime = 10;

    private float _scanTimer = 0f;

    private bool _isScanning = false;
    public string deviceUuid;
    private const string SERVICE_ADDRESS = "180a";
    private const string CHARACTERISTIC_ADDRESS = "2a57";
   

    private string charValue;


    public void ScanForDevices()
    {
        if (!_isScanning)
        {
            Debug.Log("Scanning for devices");
            _isScanning = true;
            BleManager.Instance.QueueCommand(new DiscoverDevices(OnDeviceFound, _scanTime * 200));
        }
    }

    public void Vtap()
    {
        Debug.Log("characteristic run 1");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, "1"));
    }
    private void VstoneWallCrash()
    {
        Debug.Log("characteristic run 2");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, "2"));
    }
    private void VsingleLongVibrate()
    {
        Debug.Log("characteristic run 3");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, "3"));
    }

    public void Vwarning()
    {
        Debug.Log("characteristic run 4");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, "4"));
    }

    private void VdyingHeartBeat()
    {
        Debug.Log("characteristic run 5");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, "5"));
    }

    private void VcloseWarning()
    {
        Debug.Log("characteristic run 6");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, "6"));
    }

    public void VstrongTap()
    {
        Debug.Log("characteristic run 7");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, "7"));
    }

    private void VlightTap()
    {
        Debug.Log("characteristic run 8");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, "8"));
    }

    public void VquickTap()
    {
        Debug.Log("characteristic run 9");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, "9"));
    }

    public void VallVariations()
    {
        Debug.Log("characteristic run 10");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, "0"));
    }

    public void Vgoal()
    {
        Debug.Log("characteristic run 11");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, ":"));
    }

    // public void ReadFromCharacteristic(){
    //     Debug.Log("characteristic read");
    //     BleManager.Instance.QueueCommand(new ReadFromCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, (byte[] value) =>
    //     {
    //         charValue = Encoding.UTF8.GetString(value);
    //     }));
    //     BleManager.Instance.QueueCommand(_readFromCharacteristic);

    // }
    private void Update()
    {
        if (_isScanning)
        {
            _scanTimer += Time.deltaTime;
            if (_scanTimer > _scanTime)
            {
                _scanTimer = 0f;
                _isScanning = false;
            }
        }
        bool doneCommand = FindObjectOfType<ArcadeKart>().collisionDone;
        bool isColliding = FindObjectOfType<ArcadeKart>().vCollisionCheck;
        string collisionObjectName = FindObjectOfType<ArcadeKart>().collisionObjectName;
        float localSpeed = FindObjectOfType<ArcadeKart>().LocalSpeed();
        bool kartInAir = FindObjectOfType<ArcadeKart>().m_InAir;

        if (kartInAir == true)
        {
            Debug.Log("Kart in air");
        }
        // call vLoudEngine depending on the local speed of the kart.

        // collisionText.text = charValue;
        // if (localSpeed > 0.8f && !isColliding && engineCommandCalled == false) 
        // {
        //     VquietEngine();
        // } else if(charValue == "7" && localSpeed > 0.8f){
        //     engineCommandCalled = true;
        // }

        if (isColliding && !doneCommand && deviceUuid != null)
        {
            // Debug.Log("Collision Detected at speed: " + localSpeed);
            FindObjectOfType<ArcadeKart>().collisionDone = true;

            if (collisionObjectName.StartsWith("CrashObject"))
            {
                VquickTap();
            }
            else if (collisionObjectName.StartsWith("PhysicsBall"))
            {
                Vtap();
            }
            else if (collisionObjectName.StartsWith("StoneRound"))
            {
                Vwarning();
            }
            else if (collisionObjectName.StartsWith("DeathRock"))
            {
                VsingleLongVibrate();
            }
            else if (collisionObjectName.StartsWith("AheadWarningStone"))
            {
                VcloseWarning();
            }
            else if (collisionObjectName.StartsWith("GameOverRock"))
            {
                VdyingHeartBeat();
            }
            else if (collisionObjectName.StartsWith("StoneWall"))
            {
                VstoneWallCrash();
            }
            else
            {
                VlightTap();
                //FindObjectOfType<GameFlowManager>().QuickVibrate();
            }
        }
    }

    private void OnDeviceFound(string name, string device)
    {
        // Instantiate new device buttons
        DeviceButton button = Instantiate(_deviceButton, _deviceList).GetComponent<DeviceButton>();
        button.Show(name, device);
    }
}