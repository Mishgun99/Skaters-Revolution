using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraffitiMechanics : MonoBehaviour //механика для малых граффити
{
public GameObject Players;
public GameObject ControlIcon;
public GameObject Arrow;
public AudioSource audiosource;
public Animator animator;
bool ga;
public bool painted;
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
	if (other.gameObject.tag == "Player" && Players.GetComponent<WheelJump>().spray>0)
		{
		ControlIcon.SetActive(true);
		}
	}
    void OnTriggerStay(Collider other)
	{
	if (other.gameObject.tag == "Player" && Input.GetButton("Action") && Players.GetComponent<WheelJump>().spray>0 && ga == false)
		{
		ga = true; //нарисовано. Это нужно для связи с граффити-триггером
		audiosource.Play();
		animator.SetBool("paint", true);
		Arrow.SetActive(false);
		Players.GetComponent<WheelJump>().spray--;
		Players.GetComponent<WheelJump>().GraffitiTrick();
		ControlIcon.SetActive(false);
		painted = true;
		}
	}
    void OnTriggerExit(Collider other)
	{
	if (other.gameObject.tag == "Player")
		ControlIcon.SetActive(false); //при выезде из области подсказка пропадает
	}

}
