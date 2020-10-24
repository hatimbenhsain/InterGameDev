using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class roombaScript : MonoBehaviour
{
	public bool jobOffered;
	public textScript ts;
	public altMessage targetMessage;
    public inventoryScript inventory;
    public GameObject objectToCompare;
    // Start is called before the first frame update
    void Start()
    {
        jobOffered=false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ts.messages==targetMessage.messages) jobOffered=true;
        if(jobOffered && !ts.canvas.enabled && !ts.resolved){
            foreach(GameObject item in inventory.items){
                if(item.tag==objectToCompare.tag){
                    inventory.deleteItem(item);
                    break;
                }
            }

            inventory.reorganize();
            ts.resolved=true;
            ts.messages=new string[1];
            ts.basicMessages=new string[1];
            ts.basicMessages[0]=targetMessage.messages[targetMessage.messages.Length-1];
            ts.messages=ts.basicMessages;
            foreach(altMessage alt in gameObject.GetComponents(typeof(altMessage))){
                alt.enabled=false;
            }

            //this.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
