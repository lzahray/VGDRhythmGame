using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDance : MonoBehaviour
{
	public Conductor conductorScript;
	public GameObject conductorObject;
	public BossCharacterDuel bossScript;
	private TimeArrowInfo timeGuy;
    private bool hitDanceMove;
    private bool hitCenterMove;


    // Start is called before the first frame update
    void Start()
    {
    	bossScript = GetComponent<BossCharacterDuel>();
    	conductorScript = conductorObject.GetComponent<Conductor>();
        timeGuy = bossScript.getNextTimesAndArrows();

		Debug.Log("Boss first time arrow is "+timeGuy.start_time + " " + timeGuy.end_time + " " + timeGuy.arrow);
		
        hitDanceMove = false;
        hitCenterMove = false;

    }

    // Update is called once per frame
    void Update()
    {
        if ((conductorScript.songPosition > timeGuy.end_time) && (hitCenterMove==false))// if we are passed the index, get the next so that way don't go out of bounds
        {
			timeGuy = bossScript.getNextTimesAndArrows();
			bossScript.MoveCharacter("center");
			//Debug.Log("done with this move, next is " + timeGuy.start_time + " " + timeGuy.end_time + " " + timeGuy.arrow);
            hitCenterMove = true;
            hitDanceMove = false;
		  
        } 

		else if ((conductorScript.songPosition > timeGuy.start_time) &&(conductorScript.songPosition < timeGuy.end_time) && (hitDanceMove == false))
        {
			bossScript.MoveCharacter(timeGuy.arrow);
			//Debug.Log("time to move to "+timeGuy.arrow);
            hitDanceMove = true;
            hitCenterMove = false;
        }
    }
}
