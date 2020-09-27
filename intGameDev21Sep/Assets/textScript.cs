using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 public class messageList
 {
     public string[] subMessages;
 }

public class textScript : MonoBehaviour
{
	public Canvas canvas;
	public bool inZone; 
	public string[] messages;
	public string[] basicMessages;
	public string[] confusionMessages;
	public int currentMessage; 
	public GameObject player;
	public inventoryScript inventory;

    void Start()
    {
        canvas.enabled=false;
        inZone=false;
        currentMessage=0;
        player=GameObject.FindGameObjectsWithTag("Player")[0];
        inventory=GameObject.FindGameObjectsWithTag("inventory")[0].GetComponent<inventoryScript>();
        int i=0;
        basicMessages=new string[messages.Length];
        foreach(string m in messages){
        	basicMessages[i]=m;
        	i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    public void UpdateText(){
    	if(inZone && Input.GetKeyDown(KeyCode.Space)){
    		if(currentMessage==0 && inventory.inventoryOn){
    			GameObject item=inventory.items[inventory.selectedItem];
    			altMessage[] alts=this.gameObject.GetComponents<altMessage>();
    			foreach(altMessage a in alts){
    				if(item.tag==a.item.tag){
    					messages=a.messages;
    					break;
    				}
    			}
    			if(messages==basicMessages){
    				messages=confusionMessages;
    			}
    			inventory.inventoryOn=false;

    		}

        	currentMessage++;
        	if(currentMessage<=messages.Length){
        		canvas.enabled=true;
        		player.GetComponent<playerController>().frozen=true;

        	}
        	else{
        		player.GetComponent<playerController>().frozen=false;
        		canvas.enabled=false;
        		currentMessage=0;
        		messages=basicMessages;
        	}
        	
        }
        // if(!inZone){
        // 	canvas.enabled=false;
        // }
        if(messages.Length>0 && inZone && currentMessage>0){
        	(canvas.GetComponentInChildren(typeof(Text)) as Text).text=messages[currentMessage-1];
        }


    }

    void OnTriggerStay2D(Collider2D other){
    	if(other.CompareTag("Player")){
    		inZone=true;
    	}
    }

    void OnTriggerExit2D(Collider2D other){
    	if(other.CompareTag("Player")){
    		inZone=false;
    	}
    }
}
