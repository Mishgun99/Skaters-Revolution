using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBillboardOptiimize : MonoBehaviour //скрипт для оптимизации деревьев
{
public GameObject[] tree;
public GameObject[] treeboard;
public GameObject Skater;
public GameObject DismountSkater;
    // Start is called before the first frame update
    void Awake()
    {
tree = GameObject.FindGameObjectsWithTag("Tree"); 
treeboard = GameObject.FindGameObjectsWithTag("Treeboard");   
    }

    // Update is called once per frame
    void Update()
    {
if (Skater.GetComponent<MSVehicleControllerFree>().isInsideTheCar == true) 
	{
	foreach(GameObject te in tree)
		{
		if (Vector3.Distance(te.transform.position, Skater.transform.position) < 300f)
		te.SetActive(true);
		else 
		te.SetActive(false);	
		}
	foreach(GameObject te in treeboard)
		{
		if (Vector3.Distance(te.transform.position, Skater.transform.position) >= 300f)
		te.SetActive(true);
		else 
		te.SetActive(false);		
		}
	}
else
	{
	foreach(GameObject te in tree)
		{
		if (Vector3.Distance(te.transform.position, DismountSkater.transform.position) < 300f)
		te.SetActive(true);
		else 
		te.SetActive(false);	
		}
	foreach(GameObject te in treeboard)
		{
		if (Vector3.Distance(te.transform.position, DismountSkater.transform.position) >= 300f)
		te.SetActive(true);
		else 
		te.SetActive(false);	
		}
	}              
    }
}
