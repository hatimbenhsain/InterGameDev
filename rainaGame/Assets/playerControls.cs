using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControls : MonoBehaviour
{
	public float acc=0.25f;
	public float vx=0f;
	public float vy=0f;

	public float maxSpeed=2.5f;

	private Animator anim;

	public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
  		anim=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    	transform.position=new Vector3(transform.position.x,transform.position.y,transform.position.y);

        if(Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)){
        	vx-=acc;
        	if(transform.localScale.x==-1){
        		transform.localScale+=new Vector3(2,0,0);
        	}
        }else if(!Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)){
        	vx+=acc;
        	if(transform.localScale.x==1){
        		transform.localScale+=new Vector3(-2,0,0);
        	}
        }else{
        	if(vx>0){
        		vx-=acc*2;
        		if(vx<0) vx=0;
        	}else if(vx<0){
        		vx+=acc*2;
        		if(vx>0) vx=0;
        	}
        }
        if(Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow)){
        	vy+=acc;
        }else if(Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow)){
        	vy-=acc;
        }else{
        	if(vy>0){
        		vy-=acc*2;
        		if(vy<0) vy=0;
        	}else if(vy<0){
        		vy+=acc*2;
        		if(vy>0) vy=0;
        	}
        }

        if(vx!=0){
        	anim.SetBool("run", true);
        	anim.SetBool("idle", false);
        	anim.SetInteger("facing",1);
        }else if(vy!=0){
        	anim.SetBool("run", true);
        	anim.SetBool("idle", false);
        	if(vy>0){
        		anim.SetInteger("facing",2);
        	}else if(vy<0){
        		anim.SetInteger("facing",0);
        	}
        }else{
        	anim.SetBool("idle", true);
        	anim.SetBool("run", false);
        }

        vx=Mathf.Clamp(vx,-maxSpeed,maxSpeed);
        vy=Mathf.Clamp(vy,-maxSpeed,maxSpeed);

        transform.Translate(vx*Time.deltaTime,0,0);
        transform.Translate(0,vy*Time.deltaTime,0);
        
        cam.GetComponent<Transform>().position=Vector3.Lerp(cam.GetComponent<Transform>().position,new Vector3(transform.position.x,transform.position.y,cam.GetComponent<Transform>().position.z),0.2f);
    }
}
