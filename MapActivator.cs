using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapActivator : MonoBehaviour //небольшой скрипт, активирующий глобальную карту
{
public GameObject map;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
			if (Input.GetButtonUp("Map"))
					{
					map.SetActive(!map.activeSelf);
					}
        			if (map.activeSelf == true) {
				Time.timeScale = Mathf.Lerp (Time.timeScale, 0.0f, Time.fixedDeltaTime * 5.0f);
			} else {
				//Time.timeScale = Mathf.Lerp (Time.timeScale, 1.0f, Time.fixedDeltaTime * 5.0f);
			}
    }
}
