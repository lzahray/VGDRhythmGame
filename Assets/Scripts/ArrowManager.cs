using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//I think this should just be constructed by the main character file 

public class ArrowManager 
{
    public Conductor conductorScript;

	private LinkedList<TimeArrowInfo> activeArrows;
	private List<TimeArrowInfo> allArrows;
	private int nextArrowIndex;

	private double thresholdGood;
	private double thresholdPerfect; 

    // Start is called before the first frame update

	public ArrowManager(string arrowInfo, Conductor conductor)
	{
		//instantiate allArrows

		activeArrows = new LinkedList<TimeArrowInfo >();
		thresholdGood = 0.09;
		thresholdPerfect = 0.05;
		nextArrowIndex = 0;
		conductorScript = conductor;

		allArrows = new List<TimeArrowInfo>();
		var result = arrowInfo.Split(new [] { '\r', '\n' });
		foreach (var line in result)
		{
			var values = line.Split(new [] {','});
			double center = Convert.ToDouble(values[0]);
			allArrows.Add(new TimeArrowInfo(center, center-thresholdGood, center+thresholdGood, values[1]));
		}
		//Debug.Log("allArrows.Count"+allArrows.Count);
	}

    public List<string> updateActiveAndGetMisses()
    {
    	//Add arrows to activeArrows
    	List<string> missList = new List<string>();
    	if (nextArrowIndex < allArrows.Count)
    	{
    		//while the current song position is later than the next arrow index
    		//Account for potential index errors at the end of the song 
    		bool keepGoingActive = false;
    		if (conductorScript.songPosition >= allArrows[nextArrowIndex].start_time)
    		{
    			keepGoingActive = true;
    			activeArrows.AddFirst(allArrows[nextArrowIndex]);
    			nextArrowIndex += 1;
    			//Debug.Log("added an arrow");
    		}
    		while (keepGoingActive && nextArrowIndex < allArrows.Count)
    		{
    			if (conductorScript.songPosition >= allArrows[nextArrowIndex].start_time)
    			{
    				keepGoingActive = true;
    				activeArrows.AddFirst(allArrows[nextArrowIndex]);
    				nextArrowIndex += 1;
    				//Debug.Log("added an arrow");
    			}
    			else
    			{
    				keepGoingActive = false;
    			}
    		}
    	}
    	//Check if any arrows have been missed
    	//Remove them from ActiveArrows
    	//return an integer for how many misses 
    	//while there are still arrows that are active, and while we're still on the hunt
    	int missCount = 0; 
    	if (activeArrows.Count > 0)
    	{
    		bool keepGoingMiss = false;
    		if (conductorScript.songPosition > activeArrows.Last.Value.end_time)
    		{
    			keepGoingMiss = true;
    			missCount++;
    			missList.Add(activeArrows.Last.Value.arrow);
    			activeArrows.RemoveLast();
    		}
    		while (activeArrows.Count > 0 && keepGoingMiss) 
    		{
    			if (conductorScript.songPosition > activeArrows.Last.Value.end_time)
	    		{
	    			keepGoingMiss = true;
	    			missCount++;
	    			missList.Add(activeArrows.Last.Value.arrow);
	    			activeArrows.RemoveLast();
	    		}
	    		else
	    		{
	    			keepGoingMiss = false;
	    		}
    		}
    	}

    	return missList;
    	
    }
    	

    	

    public string checkArrowHit(string direction)
    {
    	//Check whether an arrow was successfully hit, mark it off the linked list
    	//return a string for "wrong", "perfect", "great"
		LinkedListNode<TimeArrowInfo> nextArrow = activeArrows.Last;
		Debug.Log("activeArrows.Last "+activeArrows.Last);
		while (nextArrow != null)
		{
			Debug.Log("nextArrow.Value.arrow "+nextArrow.Value.arrow+ "direction "+direction);
			if (nextArrow.Value.arrow == direction)
			{
				//yay good hit! 
				double error = Math.Abs(nextArrow.Value.exact_time-conductorScript.songPosition);
				activeArrows.Remove(nextArrow);
				if (error > thresholdPerfect)
				{
					return "good";
				}
				else
				{
					return "perfect";
				}
				//remove from activeArrows
			}
			nextArrow = nextArrow.Previous;
		}
		return "wrong";
    }
}
