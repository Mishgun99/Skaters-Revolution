using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIRaceRival : MonoBehaviour
{
public Transform[] checkpoints;
public int n;
public GameObject Player;
public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
if (n != checkpoints.Length)
agent.destination = checkpoints[n].position;
else
agent.destination = checkpoints[n-1].position;
if (Vector3.Distance(transform.position, agent.destination) < 1.5f)
n++;	
if (n == checkpoints.Length)
	Player.GetComponent<WheelJump>().GameOver();        
    }
}
