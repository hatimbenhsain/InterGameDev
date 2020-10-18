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
    public float speedFactor=2f;

    Vector2 forceVec;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //anim.SetBool("walking",false);
        
        bool wasdUp=false;
        if(Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)) wasdUp=true;

        if((Input.GetKeyDown(KeyCode.A) || (Input.GetKey(KeyCode.A) && wasdUp)) && !Input.GetKey(KeyCode.D) && !frozen){
        	forceVec=Vector2.left*force;
        	anim.SetInteger("direction",1);
            direction=Vector2.left;
            anim.SetBool("walking",true);
            gameObject.GetComponent<Animator>().speed=1;
        }else if((Input.GetKeyDown(KeyCode.D) || (Input.GetKey(KeyCode.D) && wasdUp)) && !Input.GetKey(KeyCode.A) && !frozen){
        	forceVec=Vector2.right*force;
        	anim.SetInteger("direction",3);
        	direction=Vector2.right;
            anim.SetBool("walking",true);
            gameObject.GetComponent<Animator>().speed=1;
        }else if((Input.GetKeyDown(KeyCode.W) || (Input.GetKey(KeyCode.W) && wasdUp)) && !Input.GetKey(KeyCode.S) && !frozen){
        	forceVec=Vector2.up*force;
        	anim.SetInteger("direction",2);
            direction=Vector2.up;
            anim.SetBool("walking",true);
            gameObject.GetComponent<Animator>().speed=1;
        }else if((Input.GetKeyDown(KeyCode.S) || (Input.GetKey(KeyCode.S) && wasdUp)) && !Input.GetKey(KeyCode.W) && !frozen){
        	forceVec=Vector2.down*force;
        	anim.SetInteger("direction",0);
            direction=Vector2.down;
            anim.SetBool("walking",true);
            gameObject.GetComponent<Animator>().speed=1;
        }else if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)){
            anim.SetBool("walking",false);
            forceVec=Vector2.zero;
            bod.velocity=Vector2.zero;
        }

        if(Input.GetKey(KeyCode.LeftShift) && gameObject.GetComponent<Animator>().speed==1){
            gameObject.GetComponent<Animator>().speed=speedFactor;
            forceVec=forceVec*speedFactor;
        }else if(!Input.GetKey(KeyCode.LeftShift) && gameObject.GetComponent<Animator>().speed==speedFactor){
            gameObject.GetComponent<Animator>().speed=1;
            forceVec=forceVec/speedFactor;
        }

        bod.AddForce(forceVec*Time.fixedDeltaTime,ForceMode2D.Impulse);

        if(frozen || (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))){
            forceVec=Vector2.zero;
            bod.velocity=Vector2.zero;
        }

        if(bod.velocity==Vector2.zero){
            anim.SetBool("walking",false);
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

    void OnCollisionStay2D(Collision2D col){
        Ray ray=new Ray(GetComponent<BoxCollider2D>().bounds.center,direction);
        //Debug.DrawRay(ray.origin,ray.direction*maxDistance,Color.red);
        RaycastHit2D[] hits=Physics2D.BoxCastAll(ray.origin,this.gameObject.GetComponent<BoxCollider2D>().size,0,ray.direction,maxDistance);
        bool blocked=false;
        foreach(RaycastHit2D hit in hits){
            if(hit.collider==col.collider){
                blocked=true;
                break;
            }
        }

        if(blocked){
            anim.SetBool("walking",false);
        }
    }
}
