using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventoryScript : MonoBehaviour
{
	public Image inventoryBox;
	public Canvas canvas;
	public List<GameObject> items=new List<GameObject>();
	public List<Image> images=new List<Image>();
	public GameObject player;

	public bool inventoryOn;
	public Sprite placeHolder;
	public GameObject egg;
	public Image img;
	public int selectedItem;
    // Start is called before the first frame update
    void Start()
    {
        inventoryOn=false;
        canvas.enabled=false;
        addItem(egg);
        player=GameObject.FindGameObjectsWithTag("Player")[0];
        selectedItem=0;
    }

    // Update is called once per frame
    void Update()
    {
        canvas.enabled=inventoryOn;
        if(Input.GetKeyDown(KeyCode.Escape)){
        	inventoryOn=!inventoryOn;
        }
        if(inventoryOn){
        	player.GetComponent<playerController>().frozen=true;
        	if(Input.GetKeyDown(KeyCode.A) && selectedItem>0){
        		selectedItem--;
        	}else if(Input.GetKeyDown(KeyCode.D) && selectedItem<images.Count-1){
        		selectedItem++;
        	}
        }else if(Input.GetKeyDown(KeyCode.Escape)){
			player.GetComponent<playerController>().frozen=false;
		}
        int i=0;
    	foreach(Transform child in canvas.gameObject.transform){
    		if (child.gameObject.tag=="box"){
	        	if(i==selectedItem){
	        		child.gameObject.GetComponent<Image>().enabled=true;
	        	}else{
	        		child.gameObject.GetComponent<Image>().enabled=false;
	        	}
	        	i++;
	        	if(i>=images.Count) break;
        	}
        }
    }

    public void addItem(GameObject item){
    	
    	GameObject g=new GameObject();
    	g.transform.parent=canvas.gameObject.transform;
    	Image img=g.AddComponent<Image>();
    	images.Add(img);
    	items.Add(item);
    	if(item.TryGetComponent(out SpriteRenderer sr)){
    		img.sprite=sr.sprite;
    	}else{
    		img.sprite=placeHolder;
    	}
    	img.SetNativeSize();
    	foreach(Transform child in canvas.gameObject.transform){
    		if (child.gameObject.tag=="box" && !child.gameObject.activeInHierarchy){
    			child.gameObject.SetActive(true);
    			g.transform.position=child.position;
    			float s=Mathf.Min((child.gameObject.GetComponent (typeof (RectTransform)) as RectTransform).sizeDelta.x/(g.GetComponent (typeof (RectTransform)) as RectTransform).sizeDelta.x,(child.gameObject.GetComponent (typeof (RectTransform)) as RectTransform).sizeDelta.y/(g.GetComponent (typeof (RectTransform)) as RectTransform).sizeDelta.y);
    			s=s*0.8f;
    			(g.GetComponent (typeof (RectTransform)) as RectTransform).localScale=new Vector3(s,s,1);
    			break;
    		}
    	}

    }
}
