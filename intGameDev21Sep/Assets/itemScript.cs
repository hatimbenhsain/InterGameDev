using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemScript : textScript
{

    void Update(){
    	UpdateText();
    	if(inZone && Input.GetKeyDown(KeyCode.Space) && currentMessage==0){
    		inventory.addItem(this.transform.parent.gameObject);
    		this.transform.parent.gameObject.SetActive(false);
    	}
    }
}
