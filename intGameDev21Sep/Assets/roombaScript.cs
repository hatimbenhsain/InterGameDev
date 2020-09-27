using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roombaScript : MonoBehaviour
{
	public bool jobOffered;
	public textScript ts;
	public altMessage targetMessage;
    // Start is called before the first frame update
    void Start()
    {
        jobOffered=false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ts.messages==targetMessage.messages) jobOffered=true;
        if(jobOffered && !ts.canvas.enabled) this.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
