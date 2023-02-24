using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NeutralNPCWalkerAI : MonoBehaviour
{
public Material[] Body;
public GameObject CharBody;
public Material HairColor;
public GameObject[] Headwear;
public Material[] Head;
public GameObject CharHead;

public int b; //тело
public int h; //волосы
public int hc; //цвет волос

public Transform[] dot;
public bool repeatable; //если такого нет, NPC пропадает после последней точки. Если есть, то он идёт по кругу.

public NavMeshAgent agent; //ИИ для персонажа

int t;

    // Start is called before the first frame update
    void Start()
    {
	CharBody.GetComponent<Renderer>().material = Body[Random.Range(0, Body.Length)]; //смена телосложений и волос
	CharHead.GetComponent<Renderer>().material = Head[Random.Range(0, Head.Length)];
	//Headwear[h].GetComponent<Renderer>().material = Head[0];
	Headwear[Random.Range(0, Headwear.Length)].SetActive(true);
	        
    }

    // Update is called once per frame

    void Update()
    			{
			agent.destination = dot[t].transform.position; //выбор ИИ новой точки
			if (Vector3.Distance(agent.destination, transform.position) < 2f && t < dot.Length)
			t++;	
			if (t == dot.Length && repeatable == false)
			Destroy(gameObject);
			else if (t == dot.Length && repeatable == true)
			t = 0;
			} 
}
