using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefendingObjectScript : MonoBehaviour
{
public MissionGraffitiTargets mgt;
public GameObject hpscale;
public Animator animator;
public Text mgtext;
int hpmax;
    // Start is called before the first frame update
    void Awake()
    {
hpscale.SetActive(true);
hpmax = GetComponent<AIDamageRS>().hp; 
mgtext.text = "Graffiti"; 
Debug.Log(hpmax);      
    }

    // Update is called once per frame
    void Update()
    {
animator.SetInteger("health", GetComponent<AIDamageRS>().hp);
if (GetComponent<AIDamageRS>().hp < 0)
	mgt.LosingMissionTrigger(); 
hpscale.GetComponent<Image>().fillAmount = 1f * GetComponent<AIDamageRS>().hp/hpmax * 1f;
if (mgt.VictoryMessage.activeSelf == true)
	  hpscale.SetActive(false);
    }
}
