using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviourGangRival : MonoBehaviour
{
public GameObject Skater;
public GameObject Dismountskater;
public NavMeshAgent agent;
public float laziness; //лень следовать за персонажем вне асфальта, варьируется от 0.00 до 1.00
public bool escaper; //будет ли он бежать от вас после получения большого урона
public float porogchance; //величина от 0 до 1 - склонность к побегу
public int porog;
public Transform hole; //место, куда он убегает
public float hittime = 5f; //скорострельность типа
public bool provoked = true; //спровоцирован ли персонаж на атаку или нет
public string enemyid; //название врага, нужно для скриптов на триггеры

//скрипт исключительно для противника

public GameObject hitbox; //число предметов для здоровья
public bool beaten; //если побеждён, неспособен атаковать следом
public float distanceForAttack; //дистанция для атаки

public GameObject[] seekallies; //если рядом оказываются союзники, то с определённым шансом он нападает на них вместо вас

public int reward; //счёт очков после поражения

public bool ranged; //если ведёт дальний бой
public bool thrower; //если метатель

bool de;
bool t;

public float reloadTime; //время перезарядки для дальнобойных юнитов
	
//конец скрипта исключительно для противника

    // Start is called before the first frame update
    void Start()
    {
        SeekAlly();

	if (Skater == null)
	Skater = GameObject.Find("Vehicle5(skatebig)");    
    }

    // Update is called once per frame
    void Update()
    {

NavMeshPath charpath = new NavMeshPath(); //доступен ли путь до персонажа или нет
	if (Dismountskater == null)
    Dismountskater = GameObject.Find("MSFPSController");
if (escaper == true)
	{
	hitbox.SetActive(false);
	CancelInvoke("AutoHit"); 
	de = false;
	agent.destination = hole.position;
	if (Vector3.Distance(transform.position, agent.destination) < 12f)
		Destroy(gameObject); //в случае успешного побега объект исчезает
	} 
else if (Skater.GetComponent<MSVehicleControllerFree>().isInsideTheCar == true && provoked == true) //если персонаж на доске
agent.destination = Skater.transform.position;
else if (Skater.GetComponent<MSVehicleControllerFree>().isInsideTheCar == false && provoked == true) //если персонаж спешился
agent.destination = Dismountskater.transform.position;   

//если путь к персонажу недоступен, то он начинает искать способ попасть к нему через лестницы, тросы и т.д.  

//скрипт исключительно для противника

if (Vector3.Distance(transform.position, agent.destination) < distanceForAttack && beaten == false && de == false && escaper == false && provoked == true) //атака ближнего боя у оппонента
	{
	if (thrower == true)
		{
		Invoke("xe", 0.5f);		
		}
	if (ranged == true)
		{
		de = true; 
		InvokeRepeating("xf", 0.5f, reloadTime);	
		}
	InvokeRepeating("AutoHit", 0f, hittime); 
	de = true; 
	hitbox.GetComponent<HazardPhysics>().SkaterColl = GameObject.Find("CollisionHead");
	hitbox.GetComponent<HazardPhysics>().Skater = GameObject.Find("Vehicle5(skatebig)");    
	}
else if (Vector3.Distance(transform.position, agent.destination) >= distanceForAttack)
	{
	hitbox.SetActive(false);
	CancelInvoke("AutoHit"); 
	CancelInvoke("xf");
	de = false;
	}

if (GetComponent<AIDamageRS>().hp<= 0)
	{
	hitbox.SetActive(false);
	CancelInvoke("AutoHit"); 
	beaten = true; //анимация поражения
	agent.speed = 0f;
	Invoke("KillTime", 4f);
	}
if (GetComponent<AIDamageRS>().hp < porog && t == false)
	{
	t = true;
	float k = Random.Range(0, 1);
	if (k < porogchance)
		escaper = true;
	}

//конец скрипта исключительно для союзника
    }

void KillTime()
	{
	Skater.GetComponent<WheelJump>().KnockTrick(reward);
	Destroy(gameObject);
	}

void AutoHit()
	{
	hitbox.SetActive(true);
	Invoke("StopHit", 2.5f);
	}
void StopHit()
	{
	hitbox.SetActive(false);
	}
void SeekAlly()
	{
	seekallies = GameObject.FindGameObjectsWithTag("Ally");
	}
void xe()
	{
	GetComponent<ExplEnemyRunner>().Throw();
	}
void xf()
	{
	GetComponent<RangedAttacker>().RangedAttack();
	}
}
