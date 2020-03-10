using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeController : MonoBehaviour
{
    // Start is called before the first frame update
    public Conductor conductor;
    public int beatOffsetNumber;
    public List<GameObject> smokeObjects;
    public int beatsPerChange;
    public int changesOfSilence;

    private List<SpriteRenderer> smokeSRs;
    private int nextSmokeIndex; 
    private int nextBeat;

    void Awake()
    {
        nextBeat = beatOffsetNumber;
        smokeSRs = new List<SpriteRenderer>();
        for (int i=0; i<smokeObjects.Count;i++)
        {
            smokeSRs.Add(smokeObjects[i].GetComponent<SpriteRenderer>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("UPDATE " + conductor.songPositionInBeats);
        if (conductor.musicSource.isPlaying)
        {
            if (conductor.songPositionInBeats >= nextBeat) //a little early
            {
                makeSmoke(nextSmokeIndex);
                nextBeat += beatsPerChange;
                nextSmokeIndex = (nextSmokeIndex+1)%(smokeObjects.Count+changesOfSilence);
                // Debug.Log("nextSmokeIndex "+ nextSmokeIndex);
                // Debug.Log("conductor song pos in beats "+ conductor.songPositionInBeats);
                // Debug.Log("and nextBeat "+ nextBeat +"\n");
            }
        }

    }

    private void makeSmoke(int smokeIndex)
    {
    	
    	if (smokeIndex < smokeObjects.Count)
    	{
    		smokeSRs[smokeIndex].enabled = true; 
    	}
        else 
        {
            for (int i=0; i<smokeObjects.Count;i++)
            {
                smokeSRs[i].enabled=false;
            }
        }
    }
}
