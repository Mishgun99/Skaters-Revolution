using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WheelJump : MonoBehaviour //������ �� ������� �����
{
public float jumpforce;
public bool jumpable;
public bool trickable;
public bool grindalt; //���������� ��� �������

public int score;
public int scorepool; //����������, ������������, ������� ����� ����� �������, ���� �������

public int money;
public int moneypool; //����������, ������������ ���������� �����.

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

public GameObject vignette; //�����, ������� ���������� ��� ����� �������� ������

public GameObject colhead; //������, ������� ������� �������� ������, ����� �� �����������

public bool skable; //����� �� ����� ������� ����� � ����� ����� ��� ���. 

public GameObject ScoreInterface; //����� ����� ����� ��������� ��� ������� ��� ���������� ����������� �����
public GameObject CoinInterface; //��������� �����

public GameObject GameOverScreen; //����� ���������
public AudioSource MUZ; //�������� ����������

public GameObject[] character; //����� ��������� ��� ������� ����
public int characterid;


private GameSavingDataContainer dataToSave = new GameSavingDataContainer();
private GameSavingDataContainer dataToLoad = new GameSavingDataContainer();
public MyGameSaveManager myGameSaveManager;  

    // Start is called before the first frame update
    public void SaveGameData() //�������� ������
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
if (myGameSaveManager.debugActive == false) //���� �� ������� ����� �������, �� ���������� �������� ������.
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

//�������� ���������, ��������� �� �� �� ����� ��� ���
if (GetComponent<MSVehicleControllerFree>().isInsideTheCar == true)
	{
	colhead.SetActive(true);
	}
else if (GetComponent<MSVehicleControllerFree>().isInsideTheCar == false)
	{
	colhead.SetActive(false);
	}

//�������� �����
cans.text = "" + spray;

//�������� ��������

hpscale.fillAmount = hp/100f;
hpscale2.fillAmount = hp/100f; 
hpscale.color = Color.Lerp(Color.red, Color.green, hp/100f);
if (hp <= 20)
	vignette.SetActive(true); //��� ������ ���-�� �������� ���������� ������� �����
else
	vignette.SetActive(false);
if (hp <= 0)
	{
	//�������� ������
	GameOver();
	}
if (hp > 100)
	hp = 100;

//����������

if (Input.GetButtonDown("ShiftDir") && grindalt == true) //����� ����������� �������
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
if ( jumpable == true) //����� ����� ������� � ����-����, ��������� � ��� ����������
	{
	//����
	}
if (Input.GetButtonDown("Jump") && jumpable == true && GetComponent<MSVehicleControllerFree>().isInsideTheCar == true) //������ �������
			{
			GetComponent<Rigidbody>().AddForce(transform.up * jumpforce, ForceMode.Impulse);
			jumpable = false;
			Invoke("BacktoJump", 3f);
			angleskater = transform.rotation.y;
			}   
if (jumpable == false) //������, ����������� �� ���� ��������, � ����� ��� ������� �����
	{
	if (Input.GetButtonUp("VerCam") && trickable == true) //���� 1
		{
		GetComponent<Rigidbody>().AddForce(transform.up * jumpforce, ForceMode.Impulse);
		trickable = false;
		score = Mathf.RoundToInt(score + 50 * combo);
		stattrickx.text = "" + trickname1;
		stattpoints.text = "50" + " x" + combo;
		Invoke("BacktoTrick", tricktime);
		TrickWindowAppear();
		}
	else if (Input.GetButtonUp("HorCam") && trickable == true) //���� 1
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
	jumpable = true; //����� ��� ����� ��������� �� ���������� �������
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
public void BonusCollected() //���� �������
	{
		stattrickx.text = "Bonus";
		score += scorepool;
		money += moneypool;
		stattpoints.text = "" + scorepool;
		if (moneypool != 0) //���� �� �������� �����, ��������� � �������� �� ��������
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
	combo = ((int)(combo * 100)) / 100f;//���������� �����
	stattrick.SetActive(true);
	Invoke("TrickWindowDisappear", 2.5f);
	if (CoinInterface.activeSelf == false) //���� ������ �� ������������, ���������� ����� �����
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
