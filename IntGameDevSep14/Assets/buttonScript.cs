using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour
{
	public GameObject pan;
	public GameObject Arrow;
	public float thrust=10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col){
    	if(col.collider.tag==pan.tag){
    		print(col.collider.tag);
    		Arrow.GetComponent<Rigidbody2D>().AddForce(transform.up*thrust);
    		Destroy(this.gameObject);
    	}
    }
}
