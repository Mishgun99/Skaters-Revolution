using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NeutralCarGenerator : MonoBehaviour
{
public GameObject[] prefabNPC; //префабы-NPC
public int limit; //лимит на NPC. ≈сли лимит равен 0, то спавн проходит бесконечно
public Transform[] dots;
int k;
public float delay; //задержка

public bool notveh; //машина или человек?

    // Start is called before the first frame update
    void Start()
    {
InvokeRepeating("CarSpawn", 2f, delay);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   void CarSpawn()
    {
	if (limit == 0 || k < limit)
		{
		GameObject NewCar = Instantiate(prefabNPC[UnityEngine.Random.Range(0, prefabNPC.Length)], transform.position, transform.rotation, transform.parent); //создание новой машины
		if (notveh == false)		
		Array.Resize(ref NewCar.GetComponent<NeutralCarAI>().dot, dots.Length);
		else
		Array.Resize(ref NewCar.GetComponent<NeutralNPCWalkerAI>().dot, dots.Length);
			for (int i = 0; i < dots.Length; i++)
				{
				if (notveh == false)
				 NewCar.GetComponent<NeutralCarAI>().dot[i] = dots[i];
				else
				NewCar.GetComponent<NeutralNPCWalkerAI>().dot[i] = dots[i];
				}
		if (limit != 0)
		k++;
		}
	else
		{
		CancelInvoke("CarSpawn");
		}
	delay += UnityEngine.Random.Range(-2f, 2f);
    }
}
