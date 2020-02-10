using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTheBeat : MonoBehaviour

{
    public GameObject conductorObject;
   // public KeyInputVisual inputScript;
    public Conductor conductorScript;
    public MainCharacterDuel characterScript;
    // public keyCode currentInput;

    public float points; 
    public TimeArrowInfo timeGuy;

    private Dictionary<string, KeyCode> keyCodesDict;
    private bool hitCorrectArrow;
    private bool canStillHitArrow;
    private bool checkedNextWindow;

    // Start is called before the first frame update
    void Start()
    {
        hitCorrectArrow = false;
        canStillHitArrow = true;
        checkedNextWindow = false;
        keyCodesDict = new Dictionary<string, KeyCode> {{"up",KeyCode.UpArrow},  {"down", KeyCode.DownArrow}, {"left", KeyCode.LeftArrow}, {"right", KeyCode.RightArrow}};
        conductorScript = conductorObject.GetComponent<Conductor>(); //how to link... do myNewScript.public stuff
        //inputScript = objectWithScript.GetComponent<KeyInputVisual>();
        characterScript = GetComponent<MainCharacterDuel>();
        timeGuy = characterScript.getNextTimesAndArrows();
        //characterScript.MoveCharacter(timeGuy.arrow);
        Debug.Log("first time and arrows " + timeGuy.start_time + " " + timeGuy.end_time + " " + timeGuy.arrow);

    }

    // Update is called once per frame
    void Update()
    {
        //always check if we're hitting a key 
        foreach(KeyValuePair<string, KeyCode> entry in keyCodesDict)
        {
            if(Input.GetKeyDown(entry.Value))
            {
                //if we're in a hit window
                if ((timeGuy.start_time <= conductorScript.songPosition) && (conductorScript.songPosition < timeGuy.end_time))
                {
                    //if it's the correct key
                    if ((entry.Key == timeGuy.arrow) && (canStillHitArrow == true)) //if it's the right button
                    {
                        characterScript.MoveCharacter(timeGuy.arrow);
                        canStillHitArrow = false; //already hit the arrow, we're good for this window (no bonus points for multi hits!!)
                        Debug.Log("Correct!");
                        hitCorrectArrow = true;
                    } 
                    else if (canStillHitArrow == true)
                    {
                        characterScript.MoveCharacter("confused");
                        canStillHitArrow = false;
                        Debug.Log("Incorrect key!");
                    }

                }

                else
                {
                    //key input but not in key window, need to be confused 
                    characterScript.MoveCharacter("confused");
                    Debug.Log("Wrong time to hit! " + conductorScript.songPosition);
                }
            
            }

            if(Input.GetKeyUp(entry.Value))
                {
                    Debug.Log("key up");
                    characterScript.MoveCharacter("center");
                }
        }


        //we need to get the next time guy
        if (timeGuy.end_time <= conductorScript.songPosition)
        {
            //Debug.Log("songPosition"+conductorScript.songPosition+"end_time"+timeGuy.end_time);
            timeGuy = characterScript.getNextTimesAndArrows();
            hitCorrectArrow = false;
            canStillHitArrow = true;
        }

    }
}
