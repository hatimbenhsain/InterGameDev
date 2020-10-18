using UnityEngine;

public class PhysicsController : MonoBehaviour
{
	public SpriteRenderer sprite;
	public Rigidbody2D bod;
	public float force=10f;
	public float jumpForce=10f;
	public groundCheck gc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A)){
        	bod.AddForce(Vector2.left*force*Time.fixedDeltaTime,ForceMode2D.Impulse);
        	if(!sprite.flipX){
        		sprite.flipX=true;
        	}
        }
        if(Input.GetKey(KeyCode.D)){
        	bod.AddForce(Vector2.right*force*Time.fixedDeltaTime,ForceMode2D.Impulse);
        	if(sprite.flipX){
        		sprite.flipX=false;
        	}
        }

        if(Input.GetKeyDown(KeyCode.Space) && gc.isGrounded){
        	bod.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
        }
    }
}
