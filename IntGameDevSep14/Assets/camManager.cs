using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camManager : MonoBehaviour
{
	public Camera[] cameras;   
	public Rigidbody2D[] triggers;
	public int currentCam=0;
	public Collider2D[] colliders;

    void Start()
    {
        currentCam=0;
        foreach(Camera c in cameras){
            c.enabled=false;
        }
        cameras[currentCam].enabled=true;
    	colliders=new Collider2D[triggers.Length];
        for(int i=0;i<triggers.Length;i++){
        	colliders[i]=triggers[i].gameObject.GetComponent<Collider2D>();
        	print(colliders[i]);
        	colliderScript cs=colliders[i].gameObject.AddComponent<colliderScript>();
        	cs.Initialize(this,i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Switch(int i){
    	if(i==currentCam){
    		cameras[currentCam].enabled=false;
    		cameras[currentCam+1].enabled=true;
            if(currentCam!=0){
                
            }
    		currentCam++;
    	}
    }
}
