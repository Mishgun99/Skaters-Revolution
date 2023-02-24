using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostPhysics : MonoBehaviour //скрипт, отвечающий за спидометр и рывок
{

public int action; //какое действие выполняет персонаж, 0 - рывок, 1 - мегапрыжок, 2 - щит, 3 - автореген, 4 - автокраска, 5 - волна, 6 - взрыв, 7 - невидимость, 8 - слоумо
public int[] tier; //уровень вашего навыка. Если он равен 0, навык недоступен
public GameObject[] gaugeText; //текст навыка

public float gauge = 1f;
public GameObject collise; //при рывке персонаж временно неуязвим
public GameObject BoostEffect; //красивый шлейф, к которому привязана коллизия для атаки врагов
public GameObject BoostEffect2; //тот же шлейф, но для мегапрыжка
public GameObject ShieldEffect;
public float power;
public float jumppower; //высота при мегапрыжке
public GameObject QTEB;
public float[] delay; //задержка между бустами
public Text MPH;
public Text KMH;
public GameObject sp;

public Transform mussle; //дуло для атак
public GameObject wave; //атака волной
public bool stealth; //если true, то персонаж невидимый для врагов

public GameObject BlastEffect;

private GameSavingDataContainer dataToLoad = new GameSavingDataContainer();
private GameSavingDataContainer dataToSave = new GameSavingDataContainer();
public MyGameSaveManager myGameSaveManager;  


    public void SaveGameData() //загрузка данных
    {
	myGameSaveManager.LoadSettings(out dataToLoad);
	dataToSave = dataToLoad;
	dataToSave.tier = tier;
	myGameSaveManager.SaveSettings(dataToSave);
    }
    // Start is called before the first frame update
    void Start()
    {
        //загрузка сохранения
if (myGameSaveManager.debugActive == false) //если не включен режим отладки, то происходит загрузка данных.
	{
	myGameSaveManager.LoadSettings(out dataToLoad);
	tier = dataToLoad.tier;
	}
       //конец загрузки
        delay[4] -= tier[4]; //в ходе прокачки способности восстановления краски задержка уменьшается
    }

    // Update is called once per frame
    void Update()
    {

       foreach (GameObject qe in gaugeText)
		qe.SetActive(false);
       gaugeText[action].SetActive(true); 
       if (Input.GetButtonDown("ActionChoose") || (Input.mouseScrollDelta != Vector2.zero && Input.GetMouseButton(2)))
	ChooseAct();
       if (GetComponent<MSVehicleControllerFree>().isInsideTheCar == true) //включение-выключение спидометра и датчиков при уходе из пешего режима
	sp.SetActive(true);
       else
	sp.SetActive(false);
       KMH.text = "" + GetComponent<MSVehicleControllerFree>().KMh;
       MPH.text = "" + GetComponent<MSVehicleControllerFree>().KMh / 1.6f;
       if (gauge < 1f)
	gauge += Time.fixedDeltaTime / delay[action];
       QTEB.GetComponent<Image>().fillAmount = gauge;
       if (GetComponent<MSVehicleControllerFree>().isInsideTheCar == true && gauge >= 1f && Input.GetButtonUp("Roll") && (action == 0)) //рывок
	{
		gauge = 0f;
		collise.SetActive(false);
		BoostEffect.SetActive(true);
		GetComponent<Rigidbody>().velocity += transform.forward * power * (0.5f + tier[0] / 2);
		Invoke("DD", 1f);	
	}
       else if (GetComponent<MSVehicleControllerFree>().isInsideTheCar == true && gauge >= 1f && Input.GetButtonUp("Roll") && (action == 1)) //мегапрыжок
	{
		gauge = 0f;
		collise.SetActive(false);
		BoostEffect2.SetActive(true);
		GetComponent<Rigidbody>().velocity += Vector3.up * jumppower * (0.5f + tier[1] / 2);
		Invoke("DD", 1f);	
	}
       else if (GetComponent<MSVehicleControllerFree>().isInsideTheCar == true && gauge >= 1f && Input.GetButtonUp("Roll") && (action == 2)) //щит
	{
		gauge = 0f;
		collise.SetActive(false);
		ShieldEffect.SetActive(true);
		Invoke("DD", (2f + tier[2]));
	}
      else if (GetComponent<MSVehicleControllerFree>().isInsideTheCar == true && gauge >= 1f && (action == 3)) //автовосстановление
	{
		gauge = 0f;
		GetComponent<WheelJump>().hp += 5 + (tier[3] * 2) - 2; //восстановление здоровья		
	}
     else if (GetComponent<MSVehicleControllerFree>().isInsideTheCar == true && gauge >= 1f && (action == 4)) //автобанки
	{
		gauge = 0f;
		if (GetComponent<WheelJump>().spray < GetComponent<WheelJump>().SprayMax)
			GetComponent<WheelJump>().spray++; //восстановление банок		
	}
     else if (GetComponent<MSVehicleControllerFree>().isInsideTheCar == true && gauge >= 0.25f && Input.GetButtonUp("Roll") && (action == 5)) //атака волной
	{
		gauge -= 0.25f;
		GameObject WV = Instantiate(wave, mussle.position, transform.rotation);
		WV.GetComponent<Rigidbody>().velocity = transform.forward * 60f;
		WV.GetComponent<PlayerAttackPhysics>().damage += 10 * (tier[5] - 1); //с каждым уровнем повышается урон
	}
     else if (GetComponent<MSVehicleControllerFree>().isInsideTheCar == true && gauge >= 1f && Input.GetButtonUp("Roll") && (action == 6)) //атака ближнего боя
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
    public void ChooseAct() //выбор действия
	{
	gauge = 0f; //после смены режима ползунок требует заполнения обратно
	if (Input.GetAxis("ActionChoose") < 0f || Input.mouseScrollDelta == Vector2.up) //прокрутка влево
		{
		for (; ;)
			{
			if (action > 0)
				action--;
			else
				action = tier.Length - 1;
			if (tier[action] > 0)
				break; //если есть действие не с 0-м уровнем, цикл прерывается
			}
		}
	else if (Input.GetAxis("ActionChoose") > 0f || Input.mouseScrollDelta == Vector2.down) //прокрутка вправо
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
