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
    public GameObject itemUsed=null;

    public AudioClip speechSound=null;
    AudioClip typingSound;

    public bool isNpc=true;
    string spokenMessage="";
    float lettersSpoken=0f;
    float textSpeed;

    void Start()
    {
        canvas.enabled=false;
        inZone=false;
        currentMessage=0;
        player=GameObject.FindGameObjectsWithTag("Player")[0];
        inventory=GameObject.FindGameObjectsWithTag("inventory")[0].GetComponent<inventoryScript>();
        textSpeed=inventory.textSpeed;
        int i=0;
        basicMessages=new string[messages.Length];
        foreach(string m in messages){
        	basicMessages[i]=m;
        	i++;
        }
        typingSound=canvas.GetComponent<AudioSource>().clip;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    public void UpdateText(){
    	if(inZone && ((Input.GetKeyDown(KeyCode.Space) && !inventory.inventoryOn) || (itemUsed!=null && isNpc))){
    		if(itemUsed!=null && isNpc){
    			GameObject item=itemUsed;
    			altMessage[] alts=this.gameObject.GetComponents<altMessage>();
    			foreach(altMessage a in alts){
                    foreach(GameObject i in a.item){
        				if(item.tag==i.tag){
        					messages=a.messages;
                            print(a.messages[0]);
        					break;
        				}
                    }
    			}
    			if(messages==basicMessages){
    				messages=confusionMessages;
    			}
    			inventory.inventoryOn=false;
                print(itemUsed);
                itemUsed=null;
    		}

        	currentMessage++;
            lettersSpoken=0;

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

        if(messages.Length>0 && inZone && currentMessage>0){
            lettersSpoken+=textSpeed;
            if(lettersSpoken<messages[currentMessage-1].Length && !canvas.GetComponent<AudioSource>().isPlaying){
                if(speechSound!=null){
                    canvas.GetComponent<AudioSource>().clip=speechSound;
                }else{
                    canvas.GetComponent<AudioSource>().clip=typingSound;
                }
                canvas.GetComponent<AudioSource>().Play();
                Camera.main.gameObject.GetComponent<AudioSource>().volume=0.5f;
            }else if(lettersSpoken>messages[currentMessage-1].Length && canvas.GetComponent<AudioSource>().isPlaying){
                canvas.GetComponent<AudioSource>().Stop();
                Camera.main.gameObject.GetComponent<AudioSource>().volume=1f;
            }
            spokenMessage=messages[currentMessage-1].Substring(0,Mathf.Min(messages[currentMessage-1].Length,(int)Mathf.Ceil(lettersSpoken)));
        	(canvas.GetComponentInChildren(typeof(Text)) as Text).text=spokenMessage;
        }


    }

}
