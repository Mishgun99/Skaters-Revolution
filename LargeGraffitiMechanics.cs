using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LargeGraffitiMechanics : MonoBehaviour //�������� ��� ������� ��������
{
public GameObject Players;
public GameObject ControlIcon;
public GameObject[] CI;
public GameObject QTEB;
public GameObject Arrow;
public AudioSource audiosource;
public GameObject cont;
public int turns; //���������� ��� ��� ���������
public int n;
bool tr;
float timelimit;
public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
n = 0;        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
if (tr == true)
{
Players.GetComponent<Rigidbody>().velocity = Vector3.zero; //������������� ��������� ��� ��������� ������� ��������
cont.GetComponent<CharacterController>().enabled = false;
timelimit -= Time.fixedDeltaTime;
QTEB.GetComponent<Image>().fillAmount = (timelimit/2f);
	if (CI[0].activeSelf == true && Input.GetAxis("Horizontal") < -0.2f) //�������� ���������� ���������
	GraffitiMech();
	else if (CI[1].activeSelf == true && Input.GetAxis("Horizontal") > 0.2f)
	GraffitiMech();
	else if (CI[2].activeSelf == true && Input.GetAxis("Vertical") < -0.2f)
	GraffitiMech();
	else if (CI[3].activeSelf == true && Input.GetAxis("Vertical") > 0.2f)
	GraffitiMech();

}
if (tr == true && (timelimit <= 0f || Players.GetComponent<WheelJump>().spray<=0))
{
tr = false;
cont.GetComponent<CharacterController>().enabled = true;
if (Players.GetComponent<WheelJump>().spray > 0)
ControlIcon.SetActive(true);
foreach (GameObject CE in CI)
	{
	CE.SetActive(false);
	}
QTEB.SetActive(false);
} 
}
    void OnTriggerEnter(Collider other)
	{
	if (other.gameObject.tag == "Player" && Players.GetComponent<WheelJump>().spray>0 && n < turns)
		{
		ControlIcon.SetActive(true);
		}
	}
    void OnTriggerStay(Collider other)
	{
	if (other.gameObject.tag == "Player" && Input.GetButton("Action") && Players.GetComponent<WheelJump>().spray >0 && n < turns && tr == false)
		{
		GraffitiInit();
		}
	}
    void OnTriggerExit(Collider other)
	{
	if (other.gameObject.tag == "Player")
		ControlIcon.SetActive(false); //��� ������ �� ������� ��������� ���������
	}
    public void GraffitiMech() //��� ���������� ��� �������� ���������� ��������� ��������
		{
		audiosource.Play();
		n++;
		animator.SetInteger("paint", n);
		Players.GetComponent<WheelJump>().spray--;
		Players.GetComponent<WheelJump>().GraffitiTrick();
		if (n == turns)
		{
		cont.GetComponent<CharacterController>().enabled = true;
		tr = false;
		Arrow.SetActive(false);
			foreach (GameObject CE in CI)
			{
			CE.SetActive(false);
			}
		QTEB.SetActive(false);
		}
		else
		GraffitiInit();
		}
    public void GraffitiInit() //���������� ��������
		{
		foreach (GameObject CE in CI)
			{
			CE.SetActive(false);
			}
		timelimit = 2f;
		tr = true;
		ControlIcon.SetActive(false);
		int c = Random.Range(0, 4);
		CI[c].SetActive(true);
		QTEB.SetActive(true);
		}
}
