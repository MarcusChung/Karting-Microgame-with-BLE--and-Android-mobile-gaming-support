using UnityEngine;
using Android.BLE;
using Android.BLE.Commands;
using KartGame.KartSystems;

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
    public void ScanForDevices()
    {
        if (!_isScanning)
        {
            _isScanning = true;
            BleManager.Instance.QueueCommand(new DiscoverDevices(OnDeviceFound, _scanTime * 200));
        }
    }

    private void vTap()
    {
        Debug.Log("characteristic run 1");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, "1"));
    }
    private void vQuickTaps()
    { 
        Debug.Log("characteristic run 2");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, "2"));
    }
    private void vSingleLongVibrate()
    {
        Debug.Log("characteristic run 3");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, "3"));
    }

    private void vWarning()
    {
        Debug.Log("characteristic run 4");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, "4"));
    }

    private void vDyingHeartbeat()
    {
        Debug.Log("characteristic run 5");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, "5"));
    }

     private void vCloseWarning()
    {
        Debug.Log("characteristic run 7");
        BleManager.Instance.QueueCommand(new WriteToCharacteristic(deviceUuid, SERVICE_ADDRESS, CHARACTERISTIC_ADDRESS, "6"));
    }
   
     private void Update()
    {
        if(_isScanning)
        {
            _scanTimer += Time.deltaTime;
            if(_scanTimer > _scanTime)
            {
                _scanTimer = 0f;
                _isScanning = false;
            }
        }
        bool doneCommand = FindObjectOfType<ArcadeKart>().collisionDone;
        bool isColliding = FindObjectOfType<ArcadeKart>().vCollisionCheck;
        string collisionObjectName = FindObjectOfType<ArcadeKart>().collisionObjectName;
        float collisionSpeed = FindObjectOfType<ArcadeKart>().LocalSpeed();
        if (isColliding && !doneCommand && deviceUuid!= null){
            Debug.Log("Collision Detected at speed: " + collisionSpeed);
            FindObjectOfType<ArcadeKart>().collisionDone = true;

            if(collisionObjectName.StartsWith("CrashObject")){
                vQuickTaps();
            } else if (collisionObjectName.StartsWith("PhysicsBall")){
                vSingleLongVibrate();
            } else if (collisionObjectName.StartsWith("StoneRound")){
                vWarning();
            } else if (collisionObjectName.StartsWith("DeathRock") && collisionSpeed < 0.5f){
                vDyingHeartbeat();
            } else if (collisionObjectName.StartsWith("AheadWarningStone")){
                vCloseWarning();
            } else if (collisionObjectName.StartsWith("DeathRock") && collisionSpeed > 0.5f){
                vSingleLongVibrate();
            }
            else{
                vTap();
                FindObjectOfType<GameFlowManager>().Vibrate();
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