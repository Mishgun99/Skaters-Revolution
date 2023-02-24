using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRaycastTest : MonoBehaviour //тест попадания, потом он понадобится для стрельбы по разным целям
{
Camera cam;
public float MaxDistance;
public bool OnRide;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	   cam = Camera.main;
	   if (Input.GetButtonDown("AttackStandard"))
	   {
	   Collider raycastHitCollider = null;
       RaycastHit raycastHit;
		if(Physics.Raycast(transform.position, cam.gameObject.transform.forward, out raycastHit, MaxDistance) && OnRide == false){
		raycastHitCollider = raycastHit.collider;
		}
		else if(Physics.Raycast(transform.position, cam.gameObject.transform.forward + new Vector3(0f, 0.3f, 0f), out raycastHit, MaxDistance) && OnRide == true){
		raycastHitCollider = raycastHit.collider;
		}
		if (raycastHitCollider.gameObject.tag == "Enemy"){
		Debug.Log("ПОПАЛ!");
		}
		}	 
    }
}

