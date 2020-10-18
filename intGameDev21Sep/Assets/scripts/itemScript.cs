using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemScript : textScript
{
	public string[] description;
    public AudioSource itemPickupSound;

    void Update(){
    	isNpc=false;
    	UpdateText();
    	if(inZone && Input.GetKeyDown(KeyCode.Space) && currentMessage==0){
    		inventory.addItem(this.transform.parent.gameObject);
    		this.transform.parent.gameObject.SetActive(false);

    	}
    }

    // void OnTriggerStay2D(Collider2D other){
    // 	if(other.CompareTag("Player")){
    // 		inZone=true;
    // 	}
    // }

    // void OnTriggerExit2D(Collider2D other){
    // 	if(other.CompareTag("Player")){
    // 		inZone=false;
    // 	}
    // }
}
