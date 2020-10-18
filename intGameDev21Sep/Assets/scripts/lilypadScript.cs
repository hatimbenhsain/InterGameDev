using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lilypadScript : MonoBehaviour
{
	public Sprite deadSprite;
	public int num;
    // Start is called before the first frame update
    void Start()
    {
    	gameObject.GetComponent<SpriteRenderer>().color=new Color(1f,1f,1f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Animator>().SetInteger("num",num);
    }

    void OnTriggerEnter2D(Collider2D col){
    	if(col.gameObject.tag=="Player"){
    		gameObject.GetComponent<Animator>().enabled=false;
    		GetComponent<SpriteRenderer>().sprite=deadSprite;
    	}
    }
    void OnTriggerExit2D(Collider2D col){
		if(col.gameObject.tag=="Player"){
    		gameObject.GetComponent<Animator>().enabled=true;
    	}
    }
}
