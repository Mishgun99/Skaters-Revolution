using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBehaviour : MonoBehaviour //и
{
public GameObject player;
public AudioSource audiosource;
public AudioClip ouch;
    // Start is called before the first frame update
	void OnTriggerEnter(Collider other)
	{
	if ((other.gameObject.tag == "Ground") && (player.GetComponent<Rigidbody>().velocity.y < -4f)) //если скорость падения равна 3 и игрок бьётся башкой, то игрок получает урон от падения
		{
		//player.GetComponent<WheelJump>().SkateAnimator.SetInteger("hurt", 1); //при получении большого урона будет рэгдолл
		int DMG = (int)player.GetComponent<Rigidbody>().velocity.y;
		player.GetComponent<WheelJump>().hp -= (DMG * -2); //урон от падения расчитывается по формуле
		audiosource.clip = ouch;
		audiosource.Play();
		player.GetComponent<WheelJump>().TrickWindowDisappear(); //при повреждении цепь комбо прерывается
		Invoke("Restore", 1.5f); 					
		}
	}
	void Restore() //восстановление после удара
	{
	if (player.GetComponent<WheelJump>().hp > 0)
	{
	//player.GetComponent<SkateJump>().SkateAnimator.SetInteger("hurt", 0);
	player.transform.eulerAngles = new Vector3(0, 0, 0);
	}
	}

	public void HazardDamage(int damg) //общий триггер для нанесения урона
		{
		player.GetComponent<WheelJump>().TrickWindowDisappear();
		player.GetComponent<WheelJump>().hp -= damg; //урон от препятствия
		}
}
