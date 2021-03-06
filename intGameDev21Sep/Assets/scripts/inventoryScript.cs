﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class inventoryScript : MonoBehaviour
{
	public Image inventoryBox;
	public Canvas canvas;
	public List<GameObject> items=new List<GameObject>();
	public List<Image> images=new List<Image>();
    public List<string[]> descriptions=new List<string[]>();
	public GameObject player;
    public Canvas textCanvas;
    public GameObject showBox;

	public bool inventoryOn;
	public Sprite placeHolder;
	public GameObject egg;
	public Image img;
	public int selectedItem;
    public GameObject selectedBox=null;
    public GameObject hoveredBox=null;
    public GameObject npcNearby=null;

    public AudioClip invOpen;
    public AudioClip invClose;
    public AudioClip itemPickup;

    public float greyingFactor=0.5f;
    public float hoverFactor=0.75f;

    public GraphicRaycaster raycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;

    Ray ray;

    string spokenMessage="";
    float lettersSpoken=0f;
    public float textSpeed=0.3f;
    // Start is called before the first frame update
    void Start()
    {
        inventoryOn=false;
        canvas.enabled=false;
        addItem(egg);
        player=GameObject.FindGameObjectsWithTag("Player")[0];
        selectedItem=-1;
        eventSystem=GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        bool showBoxSelected=false;
        int i;
        //ray=Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit2D mouseHit=Physics2D.Raycast(ray.origin,ray.direction);
        //print(mouseHit.collider.gameObject);
        pointerEventData=new PointerEventData(eventSystem);
        pointerEventData.position=Input.mousePosition;
        List<RaycastResult> results=new List<RaycastResult>();
        raycaster.Raycast(pointerEventData,results);
        GameObject box=null;

        (showBox.GetComponentInChildren(typeof(Text)) as Text).color=new Color(hoverFactor,hoverFactor,hoverFactor,hoverFactor);

        foreach(RaycastResult result in results){
            if(result.gameObject.tag=="box"){
                box=result.gameObject;
                break;
            }else if(result.gameObject.tag=="showBox"){
                (result.gameObject.GetComponentInChildren(typeof(Text)) as Text).color=new Color(1f,1f,1f,1f);
                showBoxSelected=true;
            }
        }
        i=0;
        if(box!=null){
            foreach(Transform child in canvas.gameObject.transform){
                if (child.gameObject.tag=="box"){
                    if (child.gameObject==box){
                        if(selectedBox!=child.gameObject){
                            hoveredBox=child.gameObject;
                            child.gameObject.GetComponent<Image>().color=new Color(hoverFactor,hoverFactor,hoverFactor,hoverFactor);
                        }else{
                            selectedItem=i;
                        }
                    }else{
                        if(selectedBox!=child.gameObject){
                            child.gameObject.GetComponent<Image>().color=new Color(greyingFactor,greyingFactor,greyingFactor,greyingFactor);
                            if(hoveredBox==child.gameObject){
                                hoveredBox=null;
                            }
                        }
                    }
                    child.GetChild(0).GetComponent<Image>().color=child.gameObject.GetComponent<Image>().color;
                    i++;
                    if(i>=images.Count) break;
                }
            }
        }else{
            hoveredBox=null;
            foreach(Transform child in canvas.gameObject.transform){
                if (child.gameObject.tag=="box"){
                    if(child.gameObject!=selectedBox){
                        child.gameObject.GetComponent<Image>().color=new Color(greyingFactor,greyingFactor,greyingFactor,greyingFactor);
                        child.GetChild(0).GetComponent<Image>().color=child.gameObject.GetComponent<Image>().color;
                    }
                    i++;
                    if(i>=images.Count) break;
                }
            }
        }

        if(Input.GetMouseButtonDown(0) && showBoxSelected && selectedBox!=null && npcNearby!=null){
            (npcNearby.GetComponentInChildren(typeof(textScript)) as textScript).itemUsed=items[selectedItem];
            lettersSpoken=0;
        }else if(Input.GetMouseButtonDown(0) && hoveredBox!=null && inventoryOn){
            lettersSpoken=0;
            selectedBox=hoveredBox;
            selectedBox.GetComponent<Image>().color=new Color(1f,1f,1f,1f);
            selectedBox.transform.GetChild(0).GetComponent<Image>().color=selectedBox.GetComponent<Image>().color;
        }else if(Input.GetMouseButtonDown(0) && inventoryOn){
            lettersSpoken=0;
            selectedBox=null;
            selectedItem=-1;
            bool outsideBox=true;
            foreach(RaycastResult result in results){
                if(result.gameObject.tag=="inventoryBox"){
                    outsideBox=false;
                }
            }
            if(outsideBox){
                inventoryOn=false;
                player.GetComponent<playerController>().frozen=false;
                textCanvas.enabled=false;
            }
        }


        canvas.enabled=inventoryOn;
        if(Input.GetKeyDown(KeyCode.Escape)){
            lettersSpoken=0;
            if((!inventoryOn && !textCanvas.enabled) || inventoryOn) inventoryOn=!inventoryOn;
            if(inventoryOn) gameObject.GetComponent<AudioSource>().clip=invOpen;
            else gameObject.GetComponent<AudioSource>().clip=invClose;
            gameObject.GetComponent<AudioSource>().Play();
        }
        if(inventoryOn){
        	player.GetComponent<playerController>().frozen=true;
            if(selectedItem>=0){
                textCanvas.enabled=true;
                lettersSpoken+=textSpeed;
                spokenMessage=descriptions[selectedItem][0].Substring(0,Mathf.Min(descriptions[selectedItem][0].Length,(int)Mathf.Ceil(lettersSpoken)));
                (textCanvas.GetComponentInChildren(typeof(Text)) as Text).text=spokenMessage;
            }else{
                (textCanvas.GetComponentInChildren(typeof(Text)) as Text).text="";
            }
            if(npcNearby!=null){
                showBox.SetActive(true);
            }else{
                showBox.SetActive(false);
            }
        	// if(Input.GetKeyDown(KeyCode.A) && selectedItem>0){
        	// 	selectedItem--;
        	// }else if(Input.GetKeyDown(KeyCode.D) && selectedItem<images.Count-1){
        	// 	selectedItem++;
        	// }
        }else if(Input.GetKeyDown(KeyCode.Escape) && canvas.enabled){
			player.GetComponent<playerController>().frozen=false;
            textCanvas.enabled=false;
		}
        i=0;
    	// foreach(Transform child in canvas.gameObject.transform){
    	// 	if (child.gameObject.tag=="box"){
	    //     	if(i==selectedItem){
	    //     		child.gameObject.GetComponent<Image>().enabled=true;
	    //     	}else{
	    //     		child.gameObject.GetComponent<Image>().enabled=false;
	    //     	}
	    //     	i++;
	    //     	if(i>=images.Count) break;
     //    	}
     //    }
    }

    public void addItem(GameObject item){
        if(Time.time>0){
        	gameObject.GetComponent<AudioSource>().clip=itemPickup;
            gameObject.GetComponent<AudioSource>().Play();
        }
    	GameObject g=new GameObject();
    	g.transform.parent=canvas.gameObject.transform;
    	Image img=g.AddComponent<Image>();
    	images.Add(img);
    	items.Add(item);
        string[] t;
        itemScript txt=item.GetComponentInChildren(typeof(itemScript)) as itemScript;
        if(txt!=null && txt.description!=null){
            t=txt.description;
            print(t[0]);
        }else{
            t=new string[1];
            t[0]="an indescripable item";
        }
        descriptions.Add(t);
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
                g.transform.parent=child;
    			float s=Mathf.Min((child.gameObject.GetComponent (typeof (RectTransform)) as RectTransform).sizeDelta.x/(g.GetComponent (typeof (RectTransform)) as RectTransform).sizeDelta.x,(child.gameObject.GetComponent (typeof (RectTransform)) as RectTransform).sizeDelta.y/(g.GetComponent (typeof (RectTransform)) as RectTransform).sizeDelta.y);
    			s=s*0.8f;
    			(g.GetComponent (typeof (RectTransform)) as RectTransform).localScale=new Vector3(s,s,1);
    			break;
    		}
    	}

    }

    public void deleteItem(GameObject item){
        int i=getIndex(item);
        if(i>=0){
            // foreach(Transform child in canvas.gameObject.transform){
            //     if (child.gameObject.tag=="box" && child.gameObject.activeInHierarchy && child.child.gameObject==item){
            //         child.gameObject.SetActive(false);
            //         g.transform.position=child.position;
            //         g.transform.parent=child;
            //         break;
            //     }
            // }
            img=images[i];
            images.RemoveAt(i);
            img.gameObject.transform.parent.gameObject.SetActive(false);
            Destroy(img.gameObject);
            descriptions.RemoveAt(i);
            items.RemoveAt(i);
            print(i);
            //some sort of code to move the images back and delete this image
        }
    }

    public void reorganize(){
        List<Transform> emptyBoxes=new List<Transform>();
        foreach(Transform child in canvas.gameObject.transform){
            if (child.gameObject.tag=="box" && !child.gameObject.activeInHierarchy){
                emptyBoxes.Add(child);
            }else if(emptyBoxes.Count>0 && child.gameObject.tag=="box" && child.gameObject.activeInHierarchy){
                child.GetChild(0).position=emptyBoxes[0].position;
                child.GetChild(0).parent=emptyBoxes[0];
                emptyBoxes[0].gameObject.SetActive(true);
                child.gameObject.SetActive(false);
                emptyBoxes.RemoveAt(0);
            }
        }
        selectedItem=-1;
    }

    public int getIndex(GameObject item){
        int i=0;
        foreach(GameObject currentItem in items){
            if(currentItem==item){
                return i;
            }
            i++;
        }
        return -1;     
    }
}
