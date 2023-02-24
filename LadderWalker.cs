using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderWalker : MonoBehaviour //небольшой скрипт для канатов, лестниц и т.д., работающий только в пешем режиме
{
public GameObject cont;
public GameObject ControlIcon;
bool rept = true;
    // Start is called before the first frame update 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)		
		{
		if (other.gameObject == cont)
		ControlIcon.SetActive(true);
		}
    void OnTriggerStay(Collider other)
	{
	if (other.gameObject == cont)
	{
	if (Input.GetButton("Action") && rept == true)
		{
		cont.GetComponent<CharacterController>().enabled = !cont.GetComponent<CharacterController>().enabled;
		rept = false;
		Invoke("RRT", 0.5f);
		}
	//cont.GetComponent<Rigidbody>().useGravity = false;
	if (Input.GetAxis("Vertical") < -0.2f)
		cont.GetComponent<Rigidbody>().velocity = Vector3.down * 3f;
	else if (Input.GetAxis("Vertical") > 0.2f)
		cont.GetComponent<Rigidbody>().velocity = Vector3.up * 3f;
	else if (Input.GetAxis("Horizontal") > 0.2f)
		cont.GetComponent<Rigidbody>().velocity = -transform.forward * 3f;
	else if (Input.GetAxis("Horizontal") < -0.2f)
		cont.GetComponent<Rigidbody>().velocity = transform.forward * 3f;
	else
		cont.GetComponent<Rigidbody>().velocity = Vector3.zero;
	}
	}
  void OnTriggerExit(Collider other)
	{
	if (other.gameObject == cont)
		{
		ControlIcon.SetActive(false);
		cont.GetComponent<CharacterController>().enabled = true;
		}
	}
 void RRT()
	{
	rept = true;
	}
}
