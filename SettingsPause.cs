using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPause : MonoBehaviour
{
public AkshayDhotre.GraphicSettingsMenu.MenuToggler sds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
if (sds.menuCanvasComponent.enabled == true)
	Time.timeScale = Mathf.Lerp (Time.timeScale, 0.0f, Time.fixedDeltaTime * 5.0f);        
    }
}
