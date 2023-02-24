using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopActivator : MonoBehaviour
{
public MSSceneControllerFree controlir;
public GameObject ButtonExcl;
public GameObject MoneyPanel; //панель денег
public GameObject ScoreInterface; //панель очков
public GameObject shop;

bool kostyl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {	
	//if (shop.activeSelf == true)
		//Time.timeScale = Mathf.Lerp (Time.timeScale, 0.0f, Time.fixedDeltaTime * 7.0f);
    }
    void OnTriggerStay(Collider other)
	{
	if (other.gameObject.tag == "Player")
		{
		MoneyPanel.SetActive(true);
		ScoreInterface.SetActive(false);
		ButtonExcl.SetActive(true);
		if (Input.GetButtonDown("Action"))
			{
			shop.SetActive(!shop.activeSelf);
			GetComponent<AudioSource>().Play();
			}
		}
	}
    void OnTriggerExit(Collider other)
	{
	if (other.gameObject.tag == "Player")
		{
		shop.SetActive(false);
		MoneyPanel.SetActive(false);
		ButtonExcl.SetActive(false);
		}
	}
   void KostyleRemove()
	{
	kostyl = true;
	}
}
