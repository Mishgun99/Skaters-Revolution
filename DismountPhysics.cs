using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DismountPhysics : MonoBehaviour //��������� ����������� � ����� ������
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
        if (Input.GetButtonUp("HorCam")) //����� ���������� �� ������ z � ����� ������
	{
	//Players.transform.position = transform.position + transform.forward * 3f;
	Vector3 dir = GetComponent<MSFPSControllerFree>().cpf.forward;
	dir.y = 0f;
	Players.transform.position = transform.position + dir * 8f;
	Players.transform.rotation = Quaternion.Euler(dir.x, dir.y, dir.z);
	}	

        if (GetComponent<Rigidbody>().velocity.y < -35f && GetComponent<Rigidbody>().velocity.y < regit)
	regit = GetComponent<Rigidbody>().velocity.y; //��� ��������� ����� �� ������� �������������� ������������ �������
    }
	void OnTriggerEnter(Collider other)
	{
	if ((other.gameObject.tag == "Ground") && (regit != 0f) && (inv == false)) //������� � ������ ������ ������� ����
		{
		//player.GetComponent<WheelJump>().SkateAnimator.SetInteger("hurt", 1); //��� ��������� �������� ����� ����� �������
		Players.GetComponent<WheelJump>().hp -= (int)(regit / -1.5f); //���� �� ������� ������������� �� �������
		audiosource.clip = ouch;
		audiosource.Play();
		Players.GetComponent<WheelJump>().TrickWindowDisappear(); //��� ����������� ���� ����� �����������
		regit = 0f;	
		inv = true;
		Invoke("InvFade", 3f);			
		}
	}
	public void HazardDamage(int damg) //����� ������� ��� ��������� �����
		{
		Players.GetComponent<WheelJump>().TrickWindowDisappear();
				if (blockActive == false)
		Players.GetComponent<WheelJump>().hp -= damg; //���� �� �����������
				else
		Players.GetComponent<WheelJump>().hp -= (damg / 3); //��� ����� �������� �������� ������ ����� �����
		Debug.Log("����. ���� ��� ����� -" + damg + " ���� � ������" + (damg / 3));
		}
	void InvFade()
		{
		inv = false;
		}
	
}
