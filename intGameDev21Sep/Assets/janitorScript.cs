using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class janitorScript : MonoBehaviour
{
	public roombaScript roomba;
	public textScript ts;
	public string[] finalMessages;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(roomba.jobOffered && ts.inZone && ts.canvas.enabled){
        	ts.messages=finalMessages;
        	ts.basicMessages=finalMessages;
        }else if(ts.inZone && !ts.canvas.enabled && ts.messages==finalMessages){
        	this.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
