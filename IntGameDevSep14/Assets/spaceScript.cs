using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceScript : MonoBehaviour
{
	public Sprite greyBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space)){
        	GetComponent<SpriteRenderer>().sprite=greyBar;
        }else if(Input.GetKeyUp(KeyCode.Space)){
        	Destroy(this.gameObject);
        }
    }
}
