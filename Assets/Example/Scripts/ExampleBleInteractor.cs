using UnityEngine;
using Android.BLE;
using Android.BLE.Commands;
using KartGame.KartSystems;
using System.Text;
using TMPro;

public class ExampleBleInteractor : MonoBehaviour
{
    [SerializeField] private GameObject deviceButton;
    [SerializeField] private Transform deviceList;
    [SerializeField] private int scanTime = 10;
    private float scanTimer = 0f;
    private bool isScanning = false;
    public string deviceUuid;
    private const string SERVICE_ADDRESS = "180a";
    private const string CHARACTERISTIC_ADDRESS = "2a57";
    private const string CHARACTERISTIC_DATA_0 = "0";
    private const string CHARACTERISTIC_DATA_1 = "1";
    private const string CHARACTERISTIC_DATA_2 = "2";
    private const string CHARACTERISTIC_DATA_3 = "3";
    private const string CHARACTERISTIC_DATA_4 = "4";
    private const string CHARACTERISTIC_DATA_5 = "5";
    private const string CHARACTERISTIC_DATA_6 = "6";
    private const string CHARACTERISTIC_DATA_7 = "7";
    private const string CHARACTERISTIC_DATA_8 = "8";
    private const string CHARACTERISTIC_DATA_9 = "9";
    private const string CHARACTERISTIC_DATA_10 = ":";
    private const string CHARACTERISTIC_DATA_11 = ";";
    private string charValue;
    public void ScanForDevices()
    {
        if (!isScanning)
        {
            isScanning = true;
            BleManager.Instance.QueueCommand(new DiscoverDevices(OnDeviceFound, scanTime * 200));
        }
    }

    public void Vtest()
    {
        Debug.Log("characteristic run 0");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, CHARACTERISTIC_DATA_0));
    }
    public void Vtap()
    {
        Debug.Log("characteristic run 1");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, CHARACTERISTIC_DATA_1));
    }
    private void VstoneWallCrash()
    {
        Debug.Log("characteristic run 2");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, CHARACTERISTIC_DATA_2));
    }
    private void VsingleLongVibrate()
    {
        Debug.Log("characteristic run 3");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, CHARACTERISTIC_DATA_3));
    }

    public void Vwarning()
    {
        Debug.Log("characteristic run 4");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, CHARACTERISTIC_DATA_4));
    }

    private void VdyingHeartBeat()
    {
        Debug.Log("characteristic run 5");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, CHARACTERISTIC_DATA_5));
    }

    private void VcloseWarning()
    {
        Debug.Log("characteristic run 6");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, CHARACTERISTIC_DATA_6));
    }

    public void VstrongTap()
    {
        Debug.Log("characteristic run 7");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, CHARACTERISTIC_DATA_7));
    }

    private void VlightTap()
    {
        Debug.Log("characteristic run 8");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, CHARACTERISTIC_DATA_8));
    }

    public void VquickTap()
    {
        Debug.Log("characteristic run 9");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, CHARACTERISTIC_DATA_9));
    }

    public void Vgoal()
    {
        Debug.Log("characteristic run 10");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, CHARACTERISTIC_DATA_10));
    }
    public void VallVariations()
    {
        Debug.Log("characteristic run 11");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, CHARACTERISTIC_DATA_11));
    }


    // public void ReadFromCharacteristic(){
    //     Debug.Log("characteristic read");
    //     BleManager.Instance.QueueCommand(new ReadFromCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, (byte[] value) =>
    //     {
    //         charValue = Encoding.UTF8.GetString(value);
    //     }));
    //     BleManager.Instance.QueueCommand(_readFromCharacteristic);

    // }

    // call vLoudEngine depending on the local speed of the kart.

        // collisionText.text = charValue;
        // if (localSpeed > 0.8f && !isColliding && engineCommandCalled == false) 
        // {
        //     VquietEngine();
        // } else if(charValue == "7" && localSpeed > 0.8f){
        //     engineCommandCalled = true;
        // }
    private void Update()
    {
        if (isScanning)
        {
            scanTimer += Time.deltaTime;
            if (scanTimer > scanTime)
            {
                scanTimer = 0f;
                isScanning = false;
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
                // FindObjectOfType<GameFlowManager>().QuickVibrate();
            }
        }
    }

    private void OnDeviceFound(string name, string device)
    {
        // Instantiate new device buttons
        DeviceButton button = Instantiate(deviceButton, deviceList).GetComponent<DeviceButton>();
        button.Show(name, device);
    }
}