using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class QTEPhysics : MonoBehaviour //базовый триггер для всех QTE, кроме тех, что связаны с граффити
{
public float period; //интервал
public GameObject QTEB;
public GameObject[] CI; //строка состояний
public GameObject Players;

public UnityEvent m_AfterSuccess;
public UnityEvent m_AfterLosing;
public int turns;
public bool stopchar;
int n;
bool tr;
float timelimit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
if (tr == true)
{
timelimit -= Time.fixedDeltaTime;
if (stopchar = true)
Players.GetComponent<Rigidbody>().velocity = Vector3.zero; //останавливает персонажа при рисовании больших граффити
QTEB.GetComponent<Image>().fillAmount = (timelimit/period);
	if (CI[0].activeSelf == true && Input.GetAxis("Horizontal") < -0.2f) //успешное завершение рисования
	GraffitiMech();
	else if (CI[1].activeSelf == true && Input.GetAxis("Horizontal") > 0.2f)
	GraffitiMech();
	else if (CI[2].activeSelf == true && Input.GetAxis("Vertical") < -0.2f)
	GraffitiMech();
	else if (CI[3].activeSelf == true && Input.GetAxis("Vertical") > 0.2f)
	GraffitiMech();
if (timelimit <= 0f) 
	{
	tr = false;
	m_AfterLosing.Invoke();
	foreach (GameObject CE in CI)
		{
		CE.SetActive(false);
		}
	QTEB.SetActive(false);
	}       
    }
}
   public void QTEInit()
    {
	if (tr == false)
		{
		timelimit = period;
		n = 0;
		}
	tr = true;
		foreach (GameObject CE in CI)
		{
		CE.SetActive(false);
		}
		int c = Random.Range(0, 4);
		CI[c].SetActive(true);
		QTEB.SetActive(true);
    }
   public void GraffitiMech()
	{
	if (n == turns)
		{
		tr = false;
		foreach (GameObject CE in CI)
		{
		CE.SetActive(false);
		}
		QTEB.SetActive(false);
		m_AfterSuccess.Invoke();
		gameObject.SetActive(false);
		}
	else
		{
		n++;
		QTEInit();
		}
	
	}

   
}
