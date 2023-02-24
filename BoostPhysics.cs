using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostPhysics : MonoBehaviour //������, ���������� �� ��������� � �����
{

public int action; //����� �������� ��������� ��������, 0 - �����, 1 - ����������, 2 - ���, 3 - ���������, 4 - ����������, 5 - �����, 6 - �����, 7 - �����������, 8 - ������
public int[] tier; //������� ������ ������. ���� �� ����� 0, ����� ����������
public GameObject[] gaugeText; //����� ������

public float gauge = 1f;
public GameObject collise; //��� ����� �������� �������� ��������
public GameObject BoostEffect; //�������� �����, � �������� ��������� �������� ��� ����� ������
public GameObject BoostEffect2; //��� �� �����, �� ��� ����������
public GameObject ShieldEffect;
public float power;
public float jumppower; //������ ��� ����������
public GameObject QTEB;
public float[] delay; //�������� ����� �������
public Text MPH;
public Text KMH;
public GameObject sp;

public Transform mussle; //���� ��� ����
public GameObject wave; //����� ������
public bool stealth; //���� true, �� �������� ��������� ��� ������

public GameObject BlastEffect;

private GameSavingDataContainer dataToLoad = new GameSavingDataContainer();
private GameSavingDataContainer dataToSave = new GameSavingDataContainer();
public MyGameSaveManager myGameSaveManager;  


    public void SaveGameData() //�������� ������
    {
	myGameSaveManager.LoadSettings(out dataToLoad);
	dataToSave = dataToLoad;
	dataToSave.tier = tier;
	myGameSaveManager.SaveSettings(dataToSave);
    }
    // Start is called before the first frame update
    void Start()
    {
        //�������� ����������
if (myGameSaveManager.debugActive == false) //���� �� ������� ����� �������, �� ���������� �������� ������.
	{
	myGameSaveManager.LoadSettings(out dataToLoad);
	tier = dataToLoad.tier;
	}
       //����� ��������
        delay[4] -= tier[4]; //� ���� �������� ����������� �������������� ������ �������� �����������
    }

    // Update is called once per frame
    void Update()
    {

       foreach (GameObject qe in gaugeText)
		qe.SetActive(false);
       gaugeText[action].SetActive(true); 
       if (Input.GetButtonDown("ActionChoose") || (Input.mouseScrollDelta != Vector2.zero && Input.GetMouseButton(2)))
	ChooseAct();
       if (GetComponent<MSVehicleControllerFree>().isInsideTheCar == true) //���������-���������� ���������� � �������� ��� ����� �� ������ ������
	sp.SetActive(true);
       else
	sp.SetActive(false);
       KMH.text = "" + GetComponent<MSVehicleControllerFree>().KMh;
       MPH.text = "" + GetComponent<MSVehicleControllerFree>().KMh / 1.6f;
       if (gauge < 1f)
	gauge += Time.fixedDeltaTime / delay[action];
       QTEB.GetComponent<Image>().fillAmount = gauge;
       if (GetComponent<MSVehicleControllerFree>().isInsideTheCar == true && gauge >= 1f && Input.GetButtonUp("Roll") && (action == 0)) //�����
	{
		gauge = 0f;
		collise.SetActive(false);
		BoostEffect.SetActive(true);
		GetComponent<Rigidbody>().velocity += transform.forward * power * (0.5f + tier[0] / 2);
		Invoke("DD", 1f);	
	}
       else if (GetComponent<MSVehicleControllerFree>().isInsideTheCar == true && gauge >= 1f && Input.GetButtonUp("Roll") && (action == 1)) //����������
	{
		gauge = 0f;
		collise.SetActive(false);
		BoostEffect2.SetActive(true);
		GetComponent<Rigidbody>().velocity += Vector3.up * jumppower * (0.5f + tier[1] / 2);
		Invoke("DD", 1f);	
	}
       else if (GetComponent<MSVehicleControllerFree>().isInsideTheCar == true && gauge >= 1f && Input.GetButtonUp("Roll") && (action == 2)) //���
	{
		gauge = 0f;
		collise.SetActive(false);
		ShieldEffect.SetActive(true);
		Invoke("DD", (2f + tier[2]));
	}
      else if (GetComponent<MSVehicleControllerFree>().isInsideTheCar == true && gauge >= 1f && (action == 3)) //������������������
	{
		gauge = 0f;
		GetComponent<WheelJump>().hp += 5 + (tier[3] * 2) - 2; //�������������� ��������		
	}
     else if (GetComponent<MSVehicleControllerFree>().isInsideTheCar == true && gauge >= 1f && (action == 4)) //���������
	{
		gauge = 0f;
		if (GetComponent<WheelJump>().spray < GetComponent<WheelJump>().SprayMax)
			GetComponent<WheelJump>().spray++; //�������������� �����		
	}
     else if (GetComponent<MSVehicleControllerFree>().isInsideTheCar == true && gauge >= 0.25f && Input.GetButtonUp("Roll") && (action == 5)) //����� ������
	{
		gauge -= 0.25f;
		GameObject WV = Instantiate(wave, mussle.position, transform.rotation);
		WV.GetComponent<Rigidbody>().velocity = transform.forward * 60f;
		WV.GetComponent<PlayerAttackPhysics>().damage += 10 * (tier[5] - 1); //� ������ ������� ���������� ����
	}
     else if (GetComponent<MSVehicleControllerFree>().isInsideTheCar == true && gauge >= 1f && Input.GetButtonUp("Roll") && (action == 6)) //����� �������� ���
	{
		gauge = 0f;
		collise.SetActive(false);
		BlastEffect.SetActive(true);
		Invoke("DD", 2f);
	}
    }
    void DD()
	{
	collise.SetActive(true);
	BoostEffect.SetActive(false);
	BoostEffect2.SetActive(false);
	ShieldEffect.SetActive(false);
	BlastEffect.SetActive(false);
	stealth = false;
	}
    public void ChooseAct() //����� ��������
	{
	gauge = 0f; //����� ����� ������ �������� ������� ���������� �������
	if (Input.GetAxis("ActionChoose") < 0f || Input.mouseScrollDelta == Vector2.up) //��������� �����
		{
		for (; ;)
			{
			if (action > 0)
				action--;
			else
				action = tier.Length - 1;
			if (tier[action] > 0)
				break; //���� ���� �������� �� � 0-� �������, ���� �����������
			}
		}
	else if (Input.GetAxis("ActionChoose") > 0f || Input.mouseScrollDelta == Vector2.down) //��������� ������
		{
		for (; ;)
			{
			if (action < tier.Length - 1)
				action++;
			else
				action = 0;
			if (tier[action] > 0)
				break;
			}
		}
	Debug.Log(action + "act");
	}
}
