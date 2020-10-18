using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSprite : MonoBehaviour
{
	public Sprite[] sprites;
	public int index;
    // Start is called before the first frame update
    void Start()
    {
    	if(sprites!=null){
        	index=Random.Range(0,sprites.Length);
        	GetComponent<SpriteRenderer>().sprite=sprites[index];
    	}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
