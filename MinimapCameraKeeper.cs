using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraKeeper : MonoBehaviour
{
public float[] heightaccs;
public GameObject CameraVehicle;
public GameObject Skater;
public GameObject Dismounted;
public int k;
private AkshayDhotre.GraphicSettingsMenu.GraphicSettingDataContainer dataToLoad = new AkshayDhotre.GraphicSettingsMenu.GraphicSettingDataContainer();
public AkshayDhotre.GraphicSettingsMenu.GraphicSettingSaveManager graphicSettingSaveManager;
public AkshayDhotre.GraphicSettingsMenu.GraphicMenuManager graphicMenuManager;
private AkshayDhotre.GraphicSettingsMenu.GraphicSettingDataContainer dataToSave = new AkshayDhotre.GraphicSettingsMenu.GraphicSettingDataContainer();
    // Start is called before the first frame update
    void Start()
    { 
graphicSettingSaveManager = GetComponent<AkshayDhotre.GraphicSettingsMenu.GraphicSettingSaveManager>();
//k =  dataToLoad.minimapDistance; зум
    }

    // Update is called once per frame
    void Update()
    {
CameraVehicle.transform.localEulerAngles = new Vector3(90, 0 ,0); //она всегда остаётся наверху, как бы она не поворачивалась
	 if (Skater.GetComponent<MSVehicleControllerFree>().isInsideTheCar == true)
		CameraVehicle.transform.position = Skater.transform.position + (Vector3.up * heightaccs[k]);
	else if (Skater.GetComponent<MSVehicleControllerFree>().isInsideTheCar == false)
		CameraVehicle.transform.position = Dismounted.transform.position + (Vector3.up * heightaccs[k]);
	//дальше будет включена функция масштабирования камеры
	if (Input.GetButtonUp("Resize")) 
		{
			if (k <= 0)
				                k = (heightaccs.Length - 1);
			else
					k--;
            			graphicMenuManager.Save();
			graphicMenuManager.Load();
		}
	else if (Input.GetButtonUp("ResizeTwo")) 
		{
			if (k >= (heightaccs.Length - 1))
				            k = 0;
			else
				           k++;
            			graphicMenuManager.Save();
			graphicMenuManager.Load();
		}      
    }
}
