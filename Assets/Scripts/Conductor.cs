﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{

    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public float songBpm;

    //The number of seconds for each song beat
    public float secPerBeat;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    //How many seconds have passed since the song started
    public float dspSongTime;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    public float songStartTime;

    private bool started; 

    // Start is called before the first frame update

    void Awake()
    {
        started = false;
        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        //Record the time when the music starts
        
        
        songPosition = 0;
        songPositionInBeats = 0;

    }
    void Start()
    {
        
        //Start the music
        musicSource.Play();
        dspSongTime = (float)AudioSettings.dspTime;
        songStartTime = (float)AudioSettings.dspTime;

        started = true;

        songPosition = (float)(AudioSettings.dspTime - dspSongTime);

            //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (started)
        {
            //determine how many seconds since the song started
            songPosition = (float)(AudioSettings.dspTime - dspSongTime);

            //determine how many beats since the song started
            songPositionInBeats = songPosition / secPerBeat;

        }
        
    }
}
