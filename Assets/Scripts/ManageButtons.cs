using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ManageButtons : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject conductorObject;

    public List<GameObject> RightButtons;
    public List<GameObject> CenterButtons;
    public List<GameObject> LeftButtons; 

    private List<SpriteRenderer> RightSRs;
    private List<SpriteRenderer> CenterSRs;
    private List<SpriteRenderer> LeftSRs; 
    
    private string arrowInfo;
    private List<TimeArrowInfo> allArrows;
    private List<List<double>> rightTimes;
    private List<List<double>> centerTimes;
    private List<List<double>> leftTimes;

    private List<int> rightTimesIndices;
    private List<int> centerTimesIndices;
    private List<int> leftTimesIndices;

    private Conductor conductorScript;


    void Start()
    {
    	//Duplicate code, we will fix all of this later i'm just so tired 
    	conductorScript = conductorObject.GetComponent<Conductor>();

    	arrowInfo = "7.384607999999999,left\n9.230759999999998,center\n11.076911999999998,right\n12.923063999999998,center\n14.769215999999998,right\n14.769215999999998,left\n16.615367999999997,center\n16.615367999999997,left\n18.461519999999997,right\n18.461519999999997,center\n20.307671999999997,right\n20.307671999999997,left\n22.153823999999997,left\n23.0769,right\n23.999975999999997,left\n24.923052,center\n25.846127999999997,right\n27.692279999999997,right\n29.538431999999997,left\n30.461507999999995,right\n31.384583999999997,left\n32.30766,center\n33.23073599999999,right\n35.076888,right\n36.92303999999999,center\n36.92303999999999,left\n37.846115999999995,right\n38.769192,left\n39.230729999999994,center\n39.692268,right\n40.61534399999999,left\n41.538419999999995,right\n42.461496,left\n43.384572,left\n43.846109999999996,left\n44.30764799999999,right\n44.30764799999999,center\n45.230723999999995,left\n46.1538,right\n46.615337999999994,center\n47.07687599999999,left\n47.99995199999999,right\n48.923027999999995,left\n49.846104,center\n50.76917999999999,left\n51.230717999999996,right\n51.69225599999999,left\n53.538408,right\n55.38455999999999,right\n55.38455999999999,left\n57.230712,center\n58.15378799999999,center\n59.07686399999999,right\n59.07686399999999,left\n60.92301599999999,right\n60.92301599999999,center\n62.76916799999999,right\n62.76916799999999,left\n64.61532,right\n65.07685799999999,left\n65.53839599999999,center\n65.999934,right\n66.46147199999999,left\n68.30762399999999,right\n70.153776,center\n71.999928,right\n73.84607999999999,center\n73.84607999999999,left\n74.769156,right\n75.69223199999999,center\n75.69223199999999,left\n76.61530799999998,right\n77.538384,left\n79.384536,center";
        
        allArrows = new List<TimeArrowInfo>();
		var result = arrowInfo.Split(new [] { '\r', '\n' });
		foreach (var line in result)
		{
			var values = line.Split(new [] {','});
			double center = Convert.ToDouble(values[0]);
			allArrows.Add(new TimeArrowInfo(center, center, center, values[1]));
		}

		RightSRs = new List<SpriteRenderer>();
		CenterSRs = new List<SpriteRenderer>();
		LeftSRs = new List<SpriteRenderer>();

		rightTimes = new List<List<double>>();
		centerTimes = new List<List<double>>();
		leftTimes = new List<List<double>>();

		rightTimesIndices = new List<int>();
		centerTimesIndices = new List<int>();
		leftTimesIndices = new List<int>();


        for (int i=0;i<RightButtons.Count;i++)
        {
        	RightSRs.Add(RightButtons[i].GetComponent<SpriteRenderer>());
        	rightTimesIndices.Add(0);
        	rightTimes.Add(new List<double>());
        	foreach (TimeArrowInfo timeArrow in allArrows)
        	{
        		if (timeArrow.arrow == "right")
        		{
        			rightTimes[i].Add(timeArrow.exact_time - 1/conductorScript.songBpm*60*i);
        		}
        	}
        }
        for (int i=0;i<CenterButtons.Count;i++)
        {
        	CenterSRs.Add(CenterButtons[i].GetComponent<SpriteRenderer>());
        	centerTimesIndices.Add(0);
        	centerTimes.Add(new List<double>());
        	foreach (TimeArrowInfo timeArrow in allArrows)
        	{
        		if (timeArrow.arrow == "center")
        		{
        			centerTimes[i].Add(timeArrow.exact_time - 1/conductorScript.songBpm*60*i);
        		}
        	}
        }
        for (int i=0;i<LeftButtons.Count;i++)
        {
        	LeftSRs.Add(LeftButtons[i].GetComponent<SpriteRenderer>());
        	leftTimesIndices.Add(0);
        	leftTimes.Add(new List<double>());
        	foreach (TimeArrowInfo timeArrow in allArrows)
        	{
        		if (timeArrow.arrow == "left")
        		{
        			leftTimes[i].Add(timeArrow.exact_time - 1/conductorScript.songBpm*60*i);
        		}
        	}
        }
    }

    // Update is called once per frame
    void Update()
    {
        //loop through all buttons, see if anyone is ready to rumble 
        List<int> rightActiveBeats = new List<int>();
        
        for(int i=0;i<rightTimes.Count;i++)
        {
        	//new beat to add
        	if (rightTimesIndices[i] < rightTimes[i].Count && conductorScript.songPosition >= rightTimes[i][rightTimesIndices[i]])
        	{
        		rightTimesIndices[i]+=1;
        		rightActiveBeats.Add(i);
        	}
        	//if it's still going
        	else if (rightTimesIndices[i] > 0 && conductorScript.songPosition < (rightTimes[i][rightTimesIndices[i]-1]+1/conductorScript.songBpm*60))
        	{
        		rightActiveBeats.Add(i);
        	}

        }
        ChangeBeat("right", rightActiveBeats);



        List<int> centerActiveBeats = new List<int>();
        
        for(int i=0;i<centerTimes.Count;i++)
        {
        	if (centerTimesIndices[i] < centerTimes[i].Count && conductorScript.songPosition >= centerTimes[i][centerTimesIndices[i]])
        	{
        		centerTimesIndices[i]+=1;
        		centerActiveBeats.Add(i);
        	}
        	//if it's still going
        	else if (centerTimesIndices[i] > 0 && conductorScript.songPosition < (centerTimes[i][centerTimesIndices[i]-1]+1/conductorScript.songBpm*60))
        	{
        		centerActiveBeats.Add(i);
        	}
        }
        ChangeBeat("center", centerActiveBeats);




       	List<int> leftActiveBeats = new List<int>();
        
        for(int i=0;i<leftTimes.Count;i++)
        {
        	if (leftTimesIndices[i] < leftTimes[i].Count && conductorScript.songPosition >= leftTimes[i][leftTimesIndices[i]])
        	{
        		leftTimesIndices[i]+=1;
        		leftActiveBeats.Add(i);
        	}
        	//if it's still going
        	else if (leftTimesIndices[i] > 0 && conductorScript.songPosition < (leftTimes[i][leftTimesIndices[i]-1]+1/conductorScript.songBpm*60))
        	{
        		leftActiveBeats.Add(i);
        	}
        }
        ChangeBeat("left", leftActiveBeats);






    }

    public void ChangeBeat(string direction, List<int> activeBeats)
    {
    	//Debug.Log("changing beat for " + direction + " with num active beats " + activeBeats.Count);
    	for (int i=0;i<3;i++) //loop through all possible beat stages 
    	{
    		if (direction=="right")
    		{
    			if (activeBeats.IndexOf(i) != -1)
    			{
    				//Debug.Log("right should be shiny");
    				RightSRs[i].enabled = true;
    			}
    			else
    			{
    				RightSRs[i].enabled = false;
    			}
    		}
    		else if (direction=="center") //for now, easier 
    		{
    			if (activeBeats.IndexOf(i) != -1)
    			{
    				//Debug.Log("center should be shiny");
    				CenterSRs[i].enabled = true;
    			}
    			else
    			{
    				CenterSRs[i].enabled = false;
    			}
    		}
    		else if (direction =="left")
    		{
    			
    			if (activeBeats.IndexOf(i) != -1)
    			{
    				//Debug.Log("left should be shiny");
    				LeftSRs[i].enabled = true;
    			}
    			else
    			{
    				LeftSRs[i].enabled = false;
    			}
    		}
    	}
    }
}
