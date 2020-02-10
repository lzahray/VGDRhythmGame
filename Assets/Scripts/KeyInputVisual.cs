using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInputVisual : MonoBehaviour
{
	private SpriteRenderer theSR;
	public Sprite defaultImage;
	public Sprite[] pressedImages;
	public KeyCode[] keyCodes;
    public GameObject otherScriptObject;
    //public int myVelocity;
	//public KeyCode keyToPress;
    //public TextAsset text = Resources.Load("YourFilePath") as TextAsset;
    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        //myNewScript = otherScriptObject.GetComponent<LisaScript>();
        //keyCodes = new KeyCode[] {KeyCode.UpArrow,  KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow};
    }

    // Update is called once per frame
    void Update()
    {
        //myVelocity = myNewScript.velocity;ydi
        for (int i = 0; i < keyCodes.Length; i++)
        {
        	if(Input.GetKeyDown(keyCodes[i]))
    		{
    			theSR.sprite = pressedImages[i];
    		}
        

        	if(Input.GetKeyUp(keyCodes[i]))
	    	{
	    		theSR.sprite = defaultImage;
	    	}

        }
        //Debug.Log(myVelocity);
	        
    }
}
