using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WheelJump : MonoBehaviour //скрипт на будущие трюки
{
public float jumpforce;
public bool jumpable;
public bool trickable;
public bool grindalt; //управление дл€ грайнда

public int score;
public int scorepool; //переменна€, определ€юща€, сколько очков игрок получит, вз€в предмет

public int money;
public int moneypool; //переменна€, определ€юща€ количество денег.

public Text scorepoints;
public Text moneypoints;
public GameObject stattrick;
public Text stattrickx;
public Text stattpoints;

public float tricktime;
public float tricktime2;
public string trickname1;
public string trickname2;

float angleskater;
bool flipable1;
bool flipable2;
bool spable;

public float combo;

public int hp;
public int spray;
public Image hpscale;
public Image hpscale2;
public Text cans;
public int SprayMax;

public GameObject vignette; //рамка, котора€ по€вл€етс€ при малом здоровье игрока

public GameObject colhead; //объект, который убирает коллизию игрока, когда он спешиваетс€

public bool skable; //можно ли будет вызвать скейт в любое врем€ или нет. 

public GameObject ScoreInterface; //экран очков будет пропадать при простое или замен€тьс€ индикатором монет
public GameObject CoinInterface; //индикатор монет

public GameObject GameOverScreen; //экран геймовера
public AudioSource MUZ; //сценарий аудиосурса

public GameObject[] character; //выбор персонажа при запуске игры
public int characterid;


private GameSavingDataContainer dataToSave = new GameSavingDataContainer();
private GameSavingDataContainer dataToLoad = new GameSavingDataContainer();
public MyGameSaveManager myGameSaveManager;  

    // Start is called before the first frame update
    public void SaveGameData() //загрузка данных
    {
	myGameSaveManager.LoadSettings(out dataToLoad);
	dataToSave = dataToLoad;

	dataToSave.cans = spray;
	dataToSave.money = money;
	dataToSave.score = score;
	dataToSave.character = characterid;
	myGameSaveManager.SaveSettings(dataToSave);
    }
    void Start()
    {
if (myGameSaveManager.debugActive == false) //если не включен режим отладки, то происходит загрузка данных.
	{
	myGameSaveManager.LoadSettings(out dataToLoad);
	characterid = dataToLoad.character;
	spray = dataToLoad.cans;
	money = dataToLoad.money;
	score = dataToLoad.score;
	}
foreach (GameObject charid in character)
	charid.SetActive(false);
character[characterid].SetActive(true);
combo = 1f;        
    }

    // Update is called once per frame
    void Update()
    {

//контроль персонажа, находитс€ ли он на сцене или нет
if (GetComponent<MSVehicleControllerFree>().isInsideTheCar == true)
	{
	colhead.SetActive(true);
	}
else if (GetComponent<MSVehicleControllerFree>().isInsideTheCar == false)
	{
	colhead.SetActive(false);
	}

//контроль банок
cans.text = "" + spray;

//контроль здоровь€

hpscale.fillAmount = hp/100f;
hpscale2.fillAmount = hp/100f; 
hpscale.color = Color.Lerp(Color.red, Color.green, hp/100f);
if (hp <= 20)
	vignette.SetActive(true); //при низком кол-ве здоровь€ по€вл€етс€ красна€ рамка
else
	vignette.SetActive(false);
if (hp <= 0)
	{
	//јнимаци€ смЁрти
	GameOver();
	}
if (hp > 100)
	hp = 100;

//управление

if (Input.GetButtonDown("ShiftDir") && grindalt == true) //смена направлени€ грайнда
grindalt = false;
else if (Input.GetButtonDown("ShiftDir") && grindalt == false)
grindalt = true;
scorepoints.text = "" + score;
moneypoints.text = "" + money;
if ((GetComponent<MSVehicleControllerFree>().KMh > 70f) && (spable == true))
	{
	spable = false;
	InvokeRepeating("UltraSpeedBonus", 0f, 1f);
	}
else if (GetComponent<MSVehicleControllerFree>().KMh < 70f)
	{
	spable = true;
	CancelInvoke("UltraSpeedBonus");
	}
if ( jumpable == true) //здесь будет мэньюал и мини-игра, св€занна€ с его удержанием
	{
	//трюк
	}
if (Input.GetButtonDown("Jump") && jumpable == true && GetComponent<MSVehicleControllerFree>().isInsideTheCar == true) //прыжок объекта
			{
			GetComponent<Rigidbody>().AddForce(transform.up * jumpforce, ForceMode.Impulse);
			jumpable = false;
			Invoke("BacktoJump", 3f);
			angleskater = transform.rotation.y;
			}   
if (jumpable == false) //скрипт, реагирующий на угол поворота, а также дл€ системы комбо
	{
	if (Input.GetButtonUp("VerCam") && trickable == true) //трюк 1
		{
		GetComponent<Rigidbody>().AddForce(transform.up * jumpforce, ForceMode.Impulse);
		trickable = false;
		score = Mathf.RoundToInt(score + 50 * combo);
		stattrickx.text = "" + trickname1;
		stattpoints.text = "50" + " x" + combo;
		Invoke("BacktoTrick", tricktime);
		TrickWindowAppear();
		}
	else if (Input.GetButtonUp("HorCam") && trickable == true) //трюк 1
		{
		GetComponent<Rigidbody>().AddForce(transform.up * jumpforce, ForceMode.Impulse);
		trickable = false;
		score = Mathf.RoundToInt(score + 50 * combo);
		stattrickx.text = "" + trickname2;
		stattpoints.text = "50" + " x" + combo;
		Invoke("BacktoTrick", tricktime2);
		TrickWindowAppear();
		} 
	if ((transform.rotation.y >= (angleskater + 0.25f) || transform.rotation.y <= (angleskater - 0.25f)) && flipable1 == false )
		{
		stattrickx.text = "180 Flip";
		score = Mathf.RoundToInt(score + 75 * combo);
		stattpoints.text = "75" + " x" + combo;
		flipable1 = true;
		TrickWindowAppear();
		}   
	else if ((transform.rotation.y >= (angleskater + 0.5f) || transform.rotation.y <= (angleskater - 0.5f)) && flipable2 == false )
		{
		stattrickx.text = "360 Flip";
		score = Mathf.RoundToInt(score + 150 * combo);
		stattpoints.text = "150" + " x" + combo;
		flipable2 = true;
		TrickWindowAppear();
		
		}   
}
}
void BacktoJump()
	{
	jumpable = true; //потом эта срака изменитс€ на нормальный вариант
	flipable1 = false;
	flipable2 = false;
	}
void UltraSpeedBonus()
	{
		stattrickx.text = "Ultra Speed Bonus";
		score = Mathf.RoundToInt(score + 5 * combo);
		stattpoints.text = "5" + " x" + combo;
		TrickWindowAppear();
	}
public void BonusCollected() //сбор монеток
	{
		stattrickx.text = "Bonus";
		score += scorepool;
		money += moneypool;
		stattpoints.text = "" + scorepool;
		if (moneypool != 0) //если не выпадает денег, интерфейс с деньгами не выпадает
		{
		CoinInterface.SetActive(true);
		ScoreInterface.SetActive(false);
		CancelInvoke("ScoreWindowDisappear");
		}
		TrickWindowAppear();
	}
void BacktoTrick()
	{
	trickable = true;
	}
public void TrickWindowAppear()
	{
	CancelInvoke("TrickWindowDisappear");
	combo += 0.2f;
	combo = ((int)(combo * 100)) / 100f;//округление комбо
	stattrick.SetActive(true);
	Invoke("TrickWindowDisappear", 2.5f);
	if (CoinInterface.activeSelf == false) //если монеты не отображаютс€, включаетс€ показ очков
		ScoreInterface.SetActive(true);
	CancelInvoke("ScoreWindowDisappear");
	Invoke("ScoreWindowDisappear", 4f);
	}
public void TrickWindowDisappear()
	{
	combo = 1f;
	stattrick.SetActive(false);
	}
public void ScoreWindowDisappear()
	{
	CoinInterface.SetActive(false);
	ScoreInterface.SetActive(false);
	}
public void GrindTrick()
	{
		stattrickx.text = "Grind";
		score = Mathf.RoundToInt(score + 10 * combo);
		stattpoints.text = "10" + " x" + combo;
		TrickWindowAppear();
	}
public void GraffitiTrick()
	{
		stattrickx.text = "Graffiti";
		score = Mathf.RoundToInt(score + 100 * combo);
		stattpoints.text = "100" + " x" + combo;
		TrickWindowAppear();
	}
public void HumilTrick()
	{
		stattrickx.text = "Oops!";
		score =  score + 800;
		stattpoints.text = "800";
		TrickWindowAppear();
	}
public void KnockTrick(int enemyvalue)
	{
		stattrickx.text = "Knocked out";
		score =  score + enemyvalue;
		score = Mathf.RoundToInt(score + enemyvalue * combo);
		stattpoints.text = "100" + " x" + combo;
		TrickWindowAppear();
	}
public void GameOver()
	{
		MUZ.Stop();
		GameOverScreen.SetActive(true);
	}
}
