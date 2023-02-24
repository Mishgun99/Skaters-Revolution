using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DismountPhysics : MonoBehaviour //обработка повреждений в пешем режиме
{
public GameObject Players;
public AudioSource audiosource;
public AudioClip ouch;
public float regit;
public GameObject chars;
public bool blockActive;
bool inv;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("HorCam")) //вызов скейтборда на кнопку z в пешем режиме
	{
	//Players.transform.position = transform.position + transform.forward * 3f;
	Vector3 dir = GetComponent<MSFPSControllerFree>().cpf.forward;
	dir.y = 0f;
	Players.transform.position = transform.position + dir * 8f;
	Players.transform.rotation = Quaternion.Euler(dir.x, dir.y, dir.z);
	}	

        if (GetComponent<Rigidbody>().velocity.y < -35f && GetComponent<Rigidbody>().velocity.y < regit)
	regit = GetComponent<Rigidbody>().velocity.y; //при нанесении урона от падения регистрируется максимальное падение
    }
	void OnTriggerEnter(Collider other)
	{
	if ((other.gameObject.tag == "Ground") && (regit != 0f) && (inv == false)) //падение с высоты пешком наносит урон
		{
		//player.GetComponent<WheelJump>().SkateAnimator.SetInteger("hurt", 1); //при получении большого урона будет рэгдолл
		Players.GetComponent<WheelJump>().hp -= (int)(regit / -1.5f); //урон от падения расчитывается по формуле
		audiosource.clip = ouch;
		audiosource.Play();
		Players.GetComponent<WheelJump>().TrickWindowDisappear(); //при повреждении цепь комбо прерывается
		regit = 0f;	
		inv = true;
		Invoke("InvFade", 3f);			
		}
	}
	public void HazardDamage(int damg) //общий триггер для нанесения урона
		{
		Players.GetComponent<WheelJump>().TrickWindowDisappear();
				if (blockActive == false)
		Players.GetComponent<WheelJump>().hp -= damg; //урон от препятствия
				else
		Players.GetComponent<WheelJump>().hp -= (damg / 3); //при блоке персонаж получает только треть урона
		Debug.Log("БЛОК. Урон без блока -" + damg + " Урон с блоком" + (damg / 3));
		}
	void InvFade()
		{
		inv = false;
		}
	
}
