using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform){
        	child.position=new Vector3(child.position.x,child.position.y,transform.position.z);
        	print(child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
