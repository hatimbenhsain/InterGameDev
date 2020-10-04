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
    public Vector2 direction;
    public float maxDistance=1f;
    public GameObject[] textBoxes;
    public inventoryScript inventory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("walking",false);
        
        if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !frozen){
        	bod.AddForce(Vector2.left*force*Time.fixedDeltaTime,ForceMode2D.Impulse);
        	//if(anim.GetBool("walking")==false){
        	anim.SetInteger("direction",1);
            direction=Vector2.left;
        	//}
        	anim.SetBool("walking",true);
        }else if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !frozen){
        	bod.AddForce(Vector2.right*force*Time.fixedDeltaTime,ForceMode2D.Impulse);
        	//if(anim.GetBool("walking")==false){
        	anim.SetInteger("direction",3);
        	direction=Vector2.right;
            //}
        	anim.SetBool("walking",true);
        }

        if(Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !frozen){
        	bod.AddForce(Vector2.up*force*Time.fixedDeltaTime,ForceMode2D.Impulse);
        	//if(anim.GetBool("walking")==false){
        		anim.SetInteger("direction",2);
            direction=Vector2.up;
        	//}
        	anim.SetBool("walking",true);
        }else if(Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !frozen){
        	bod.AddForce(Vector2.down*force*Time.fixedDeltaTime,ForceMode2D.Impulse);
        	//if(anim.GetBool("walking")==false){
        	anim.SetInteger("direction",0);
        	//}
            direction=Vector2.down;
        	anim.SetBool("walking",true);
        }

        if(frozen || (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))){
        	anim.SetBool("walking",false);
            bod.velocity=Vector2.zero;
        }

        Ray ray=new Ray(GetComponent<BoxCollider2D>().bounds.center,direction);
        Debug.DrawRay(ray.origin,ray.direction*maxDistance,Color.red);

        RaycastHit2D[] hits=Physics2D.RaycastAll(ray.origin,ray.direction,maxDistance);

        textBoxes=GameObject.FindGameObjectsWithTag("textBox");

        foreach(GameObject t in textBoxes){
            t.GetComponent<textScript>().inZone=false;
        }
        inventory.npcNearby=null;

        foreach(RaycastHit2D hit in hits){
            if(hit.collider.isTrigger && hit.collider.gameObject.tag=="textBox"){
                hit.collider.gameObject.GetComponent<textScript>().inZone=true;
                if(hit.collider.gameObject.GetComponent<textScript>().isNpc){
                    inventory.npcNearby=hit.collider.gameObject;
                }
            }
        }

    }
}
