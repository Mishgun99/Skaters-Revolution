using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviourGangRival : MonoBehaviour
{
public GameObject Skater;
public GameObject Dismountskater;
public NavMeshAgent agent;
public float laziness; //���� ��������� �� ���������� ��� ��������, ����������� �� 0.00 �� 1.00
public bool escaper; //����� �� �� ������ �� ��� ����� ��������� �������� �����
public float porogchance; //�������� �� 0 �� 1 - ���������� � ������
public int porog;
public Transform hole; //�����, ���� �� �������
public float hittime = 5f; //���������������� ����
public bool provoked = true; //������������� �� �������� �� ����� ��� ���
public string enemyid; //�������� �����, ����� ��� �������� �� ��������

//������ ������������� ��� ����������

public GameObject hitbox; //����� ��������� ��� ��������
public bool beaten; //���� �������, ���������� ��������� ������
public float distanceForAttack; //��������� ��� �����

public GameObject[] seekallies; //���� ����� ����������� ��������, �� � ����������� ������ �� �������� �� ��� ������ ���

public int reward; //���� ����� ����� ���������

public bool ranged; //���� ���� ������� ���
public bool thrower; //���� ��������

bool de;
bool t;

public float reloadTime; //����� ����������� ��� ������������ ������
	
//����� ������� ������������� ��� ����������

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

NavMeshPath charpath = new NavMeshPath(); //�������� �� ���� �� ��������� ��� ���
	if (Dismountskater == null)
    Dismountskater = GameObject.Find("MSFPSController");
if (escaper == true)
	{
	hitbox.SetActive(false);
	CancelInvoke("AutoHit"); 
	de = false;
	agent.destination = hole.position;
	if (Vector3.Distance(transform.position, agent.destination) < 12f)
		Destroy(gameObject); //� ������ ��������� ������ ������ ��������
	} 
else if (Skater.GetComponent<MSVehicleControllerFree>().isInsideTheCar == true && provoked == true) //���� �������� �� �����
agent.destination = Skater.transform.position;
else if (Skater.GetComponent<MSVehicleControllerFree>().isInsideTheCar == false && provoked == true) //���� �������� ��������
agent.destination = Dismountskater.transform.position;   

//���� ���� � ��������� ����������, �� �� �������� ������ ������ ������� � ���� ����� ��������, ����� � �.�.  

//������ ������������� ��� ����������

if (Vector3.Distance(transform.position, agent.destination) < distanceForAttack && beaten == false && de == false && escaper == false && provoked == true) //����� �������� ��� � ���������
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
	beaten = true; //�������� ���������
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

//����� ������� ������������� ��� ��������
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
