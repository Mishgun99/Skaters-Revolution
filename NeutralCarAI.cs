using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NeutralCarAI : MonoBehaviour
{
public Transform[] dot;
public bool repeatable; //���� ������ ���, NPC ��������� ����� ��������� �����. ���� ����, �� �� ��� �� �����.
public int t;

public NavMeshAgent agent; //�� ��� ���������

    // Start is called before the first frame update
    void Start()
    {
	        
    }

    // Update is called once per frame
    void Update()
    			{
			agent.destination = dot[t].transform.position; //����� �� ����� �����
			if (Vector3.Distance(agent.destination, transform.position) < 2f && t < dot.Length)
			t++;	
			if (t == dot.Length && repeatable == false)
			Destroy(gameObject);
			else if (t == dot.Length && repeatable == true)
			t = 0;
			} 
	
	

}