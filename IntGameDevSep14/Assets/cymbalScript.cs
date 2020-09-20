using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cymbalScript : MonoBehaviour
{
	public AudioClip[] cymbals;
	public bool played=false;
	public AudioSource src;
	public GameObject egg;
	private float timeSincePlayed=0f;
	private float pauseTime=5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time-timeSincePlayed>=pauseTime && played){
        	played=false;
        	timeSincePlayed=Time.time;
        }
    }

    void OnCollisionEnter2D(Collision2D col){
    	if(!played && (col.collider.gameObject.tag==this.gameObject.tag || col.collider.gameObject.tag==egg.tag)){
    		src.clip=cymbals[(int) Mathf.Floor(Random.Range(0f,4.99f))];
    		src.Play();
    		played=true;
    		timeSincePlayed=Time.time;
    	}
    }
}
