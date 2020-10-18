using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class janitorScript : MonoBehaviour
{
	public roombaScript roomba;
	public textScript ts;
	public string[] finalMessages;
    public bool disappearing=false;
    public Tilemap tilemapToDelete;
    public BoxCollider2D wallToDelete;
    public GameObject lilypads;

    Color sr;
    // Start is called before the first frame update
    void Start()
    {
        sr=transform.parent.gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if(roomba.jobOffered && ts.inZone && ts.canvas.enabled){
        	ts.messages=finalMessages;
        	ts.basicMessages=finalMessages;
        }else if(ts.inZone && !ts.canvas.enabled && ts.messages==finalMessages && !disappearing){
        	disappearing=true;
        }
        if(disappearing && sr.a>=0f){
            sr.a-=0.005f;
            transform.parent.gameObject.GetComponent<SpriteRenderer>().color=sr;
            tilemapToDelete.color=sr;
            for(int i=0;i<lilypads.GetComponent<Transform>().childCount;i++){
                lilypads.GetComponent<Transform>().GetChild(i).gameObject.GetComponent<SpriteRenderer>().color=new Color(1f,1f,1f,1f-sr.a);
            }
            print(sr.a);
        }else if(disappearing){
            this.gameObject.transform.parent.gameObject.SetActive(false);
            wallToDelete.enabled=false;
        }
    }
}
