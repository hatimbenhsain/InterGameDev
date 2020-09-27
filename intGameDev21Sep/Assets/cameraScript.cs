using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
	public Transform target;
	public float smoothTime=0.3f;

	Vector3 vel=Vector3.zero;
	public bool inDeadZone;

	public float maxTime=1f;
	public float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(inDeadZone){
        	currentTime=maxTime;
        }
        

        Vector3 targetPosition=target.TransformPoint(new Vector3(0,0,-10f));

        transform.position=Vector3.SmoothDamp(transform.position, targetPosition, ref vel,smoothTime);

    }

    void OnTriggerStay2D(Collider2D other){
    	if(other.CompareTag("Player")){
    		inDeadZone=true;
    	}
    }

    void OnTriggerExit2D(Collider2D other){
    	if(other.CompareTag("Player")){
    		inDeadZone=false;
    	}
    }
}
