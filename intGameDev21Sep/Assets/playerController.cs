using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
	public SpriteRenderer sprite;
	public Rigidbody2D bod;
	public float force=10f;
	public Animator anim;
	public bool frozen=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A) && !frozen){
        	bod.AddForce(Vector2.left*force*Time.fixedDeltaTime,ForceMode2D.Impulse);
        	//if(anim.GetBool("walking")==false){
        		anim.SetInteger("direction",1);
        	//}
        	anim.SetBool("walking",true);
        }else if(Input.GetKey(KeyCode.D) && !frozen){
        	bod.AddForce(Vector2.right*force*Time.fixedDeltaTime,ForceMode2D.Impulse);
        	//if(anim.GetBool("walking")==false){
        		anim.SetInteger("direction",3);
        	//}
        	anim.SetBool("walking",true);
        }

        if(Input.GetKey(KeyCode.W) && !frozen){
        	bod.AddForce(Vector2.up*force*Time.fixedDeltaTime,ForceMode2D.Impulse);
        	//if(anim.GetBool("walking")==false){
        		anim.SetInteger("direction",2);
        	//}
        	anim.SetBool("walking",true);
        }else if(Input.GetKey(KeyCode.S) && !frozen){
        	bod.AddForce(Vector2.down*force*Time.fixedDeltaTime,ForceMode2D.Impulse);
        	//if(anim.GetBool("walking")==false){
        		anim.SetInteger("direction",0);
        	//}
        	anim.SetBool("walking",true);
        }

        if(frozen || (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))){
        	anim.SetBool("walking",false);
        }

    }
}
