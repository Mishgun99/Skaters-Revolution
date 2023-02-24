using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDriveCameraSense : MonoBehaviour //скрипт, позволяющий настраивать чувствительность камеры во время езды на скейте/машине
{
public int CameraSensibility = 5;
public float[] CameraETS;
public float[] CameraFPS;
public float[] CameraORB;
public MSVehicleControllerFree MSVC;
public GameObject gose;

private AkshayDhotre.GraphicSettingsMenu.GraphicSettingDataContainer dataToLoad = new AkshayDhotre.GraphicSettingsMenu.GraphicSettingDataContainer();
private AkshayDhotre.GraphicSettingsMenu.GraphicSettingSaveManager graphicSettingSaveManager;
    // Start is called before the first frame update
    void Start()
    {
 graphicSettingSaveManager = gose.GetComponent<AkshayDhotre.GraphicSettingsMenu.GraphicSettingSaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        graphicSettingSaveManager.LoadSettings(out dataToLoad);
		CameraSensibility = dataToLoad.CameraSenseOnDrive;
		MSVC._cameras.cameraSettings.ETS_StyleCamera.sensibility = CameraETS[CameraSensibility];
		MSVC._cameras.cameraSettings.firstPersonCamera.sensibility = CameraFPS[CameraSensibility];
		MSVC._cameras.cameraSettings.orbitalCamera.sensibility = CameraORB[CameraSensibility];
    }
}
