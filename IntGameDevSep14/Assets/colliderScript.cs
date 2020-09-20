using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderScript : MonoBehaviour
{
	camManager cm;
	int index;
    // Start is called before the first frame update

    public void Initialize(camManager l, int i){
    	cm=l;
    	index=i;
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D col){
        if(this.gameObject.tag!=col.collider.gameObject.tag){
        	cm.Switch(index);
        }
    }
}
