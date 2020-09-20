using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shelfScript : MonoBehaviour
{
	public GameObject arrow;
	public float duration=5f;
	private Animator anim;
	private float startTime;
	private bool onFire=false;
    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        if(onFire && Time.time>=startTime+duration){
        	Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col){
    	if(col.collider.tag==arrow.tag){
    		anim.SetBool("onFire",true);
    		onFire=true;
    		startTime=Time.time;
    	}
    }
}
