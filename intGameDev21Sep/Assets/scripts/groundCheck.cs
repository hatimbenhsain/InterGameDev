using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheck : MonoBehaviour
{
	public bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D other){
    	print("in trig");
    	isGrounded=true;
    }

    public void OnTriggerExit2D(Collider2D other){
    	isGrounded=false;
    }
}
