using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class standScript : MonoBehaviour
{
	public textScript txt;
	public inventoryScript inventory;
	public string[] newMessages;
	public GameObject objectToCompare;
	public int minimumRequired;
	public GameObject replacement;

	public bool changed=false;
    // Start is called before the first frame update
    void Start()
    {
        txt=GetComponent<textScript>();
        inventory=txt.inventory;
    }

    // Update is called once per frame
    void Update()
    {
        if(!changed && newMessages!=null && minimumRequired>1 && objectToCompare!=null && inventory.items!=null){
        	int count=0;
        	foreach(GameObject item in inventory.items){
        		if(item.tag==objectToCompare.tag){
        			count++;
        		}
        	}
        	if(count>=minimumRequired){
        		altMessage[] alts=GetComponents<altMessage>();
        		foreach(altMessage a in alts){
        			foreach(GameObject i in a.item){
        				if(i.tag==objectToCompare.tag){
        					a.messages=newMessages;
        					changed=true;
        					return;
        				}
        			}
        		}
        	}
        }
        //some sort of code to replace the current game object with a new one
    }
}
