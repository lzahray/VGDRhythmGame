using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spotlightOnOff : MonoBehaviour
{
    // Start is called before the first frame update

    public float onIntensity;
    public float offIntensity;
    public Conductor conductor;
    public int songStartBeat;
    public int beatsPerCharacter;
    public bool goesFirst;

    private Light light; 
    private int nextBeat; 
    private bool nextIsOn;

    void Start()
    {
        light = GetComponent<Light>();
        nextBeat = songStartBeat;
        if (goesFirst)
        {
        	nextIsOn = true;
        }
        else
        {
        	nextIsOn = false;
        }
        light.intensity = offIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (!conductor.musicSource.isPlaying)
            {
                if (!nextIsOn)
                {
                    changeLightIntensity(offIntensity);
                }
                nextIsOn = true;
            }
        else
        {
            if (conductor.songPositionInBeats >= nextBeat-.1) //a little early
            {
                //time to start
                if (nextIsOn)
                {
                    changeLightIntensity(onIntensity);
                }
                else
                {
                    changeLightIntensity(offIntensity);
                }
                nextBeat += beatsPerCharacter;
                nextIsOn = !nextIsOn;
            }
        }
        
    }

    public void changeLightIntensity(float intensity)
    {
    	light.intensity = intensity;
    }
}
