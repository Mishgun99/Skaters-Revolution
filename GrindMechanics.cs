using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GrindMechanics : MonoBehaviour
{
	public float grindspeed; //нужно для замедления игрока на трассе 
	GameObject other2;
	public GameObject Skater;     
	public float j;
	public AudioSource audiosource;
    // Start is called before the first frame update
    void Start()
    {
	audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GravityEnder()
	{
		Skater.GetComponent<Rigidbody>().useGravity = true;
	}
    void OnTriggerExit(Collider other)
	{
	Invoke("GravityEnder", 0.2f); //заглушка для того, чтобы гравитация не прерывалась в момент смены рельсы
	CancelInvoke("GrindSlower");	
	other2 = null;
	grindspeed = 0;
	if (other.gameObject.tag == "Ally")
		{
		//other.GetComponent<NavMeshAgent>().enabled = true;
		//other.GetComponent<Rigidbody>().useGravity = true;
		}
	audiosource.Stop();
	}
    void OnTriggerEnter(Collider other)
	{
	if (other.gameObject.tag == "Ally")
		{
		//other.GetComponent<NavMeshAgent>().enabled = false;
		//other.GetComponent<Rigidbody>().useGravity = false;
		//grindspeed = 12f;
		//other.GetComponent<Rigidbody>().velocity = transform.forward * grindspeed + other.transform.up * 0.25f;
		audiosource.Play();
		}
	if (other.gameObject.tag == "Player" && other.GetComponent<Rigidbody>().velocity.magnitude > 3f)
	{
		CancelInvoke("GravityEnder");	
		Skater.GetComponent<Rigidbody>().useGravity = false;
		other2 = other.gameObject;
		InvokeRepeating("GrindSlower", 0.5f, 1f);
		//grindspeed = other.GetComponent<MSVehicleControllerFree>().KMh / 2f;
		grindspeed = 18f;
		audiosource.Play();
	}
}
   void OnTriggerStay(Collider other)
	{
	if (other.gameObject.tag == "Player")
		{
		Skater.GetComponent<Rigidbody>().useGravity = false;
		if (other.GetComponent<WheelJump>().grindalt == false)
		other.GetComponent<Rigidbody>().velocity = transform.forward * grindspeed + other.transform.up * 0.2f; //заглушка для разрешения проблемы с гравитац
		else
		other.GetComponent<Rigidbody>().velocity = -transform.forward * grindspeed + other.transform.up * 0.2f;
		if (Input.GetButtonDown("Jump") && Input.GetAxis("Horizontal") < -0.5f) //отскок влево
			{
			other.transform.position += Vector3.up * 1f;
			other.GetComponent<Rigidbody>().AddForce(other.transform.up * other.GetComponent<WheelJump>().jumpforce / 2, ForceMode.Impulse);
			other.GetComponent<Rigidbody>().AddForce(transform.right * other.GetComponent<WheelJump>().jumpforce, ForceMode.Impulse);
			OnTriggerExit(null);
			}
		if (Input.GetButtonDown("Jump") && Input.GetAxis("Horizontal") > 0.5f) //отскок влево
			{
			other.transform.position += Vector3.up * 1f;
			other.GetComponent<Rigidbody>().AddForce(other.transform.up * other.GetComponent<WheelJump>().jumpforce / 2, ForceMode.Impulse);
			other.GetComponent<Rigidbody>().AddForce(transform.right * other.GetComponent<WheelJump>().jumpforce, ForceMode.Impulse);
			OnTriggerExit(null);
			}
		else if (Input.GetButtonDown("Jump")) //прыжок объекта
			{
			other.transform.position += Vector3.up * 1f;
			other.GetComponent<Rigidbody>().AddForce(other.transform.up * other.GetComponent<WheelJump>().jumpforce, ForceMode.Impulse);
			OnTriggerExit(null);
			}
		if (Input.GetButton("Action"))
			{
			grindspeed = 18f;
			Skater.GetComponent<Rigidbody>().useGravity = false;
			}
	}
	}
  void GrindSlower()
	{
	other2.GetComponent<WheelJump>().GrindTrick();
	if (grindspeed - 0.1f > 0)
	grindspeed = grindspeed;
	else
	{
		grindspeed = 0.2f;
		other2.GetComponent<Rigidbody>().AddForce(other2.transform.up * other2.GetComponent<WheelJump>().jumpforce, ForceMode.Impulse);
	}
	}
}
