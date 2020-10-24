using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class priestessScript : MonoBehaviour
{
	public GameObject goddessStatue;
	public textScript ts;
	public string[] finalMessages;
    public bool disappearing=false;
    public GameObject endObj;

    Color sr;
    // Start is called before the first frame update
    void Start()
    {
        sr=transform.parent.gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if(goddessStatue.activeSelf && ts.inZone && ts.canvas.enabled){
        	ts.messages=finalMessages;
        	ts.basicMessages=finalMessages;
        }else if(ts.inZone && !ts.canvas.enabled && ts.messages==finalMessages && !disappearing){
        	disappearing=true;
        	ts.resolved=true;
        }
        if(disappearing && sr.a>=0f){
            sr.a-=0.005f;
            transform.parent.gameObject.GetComponent<SpriteRenderer>().color=sr;
        }else if(disappearing){
        	endObj.SetActive(true);
            this.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
