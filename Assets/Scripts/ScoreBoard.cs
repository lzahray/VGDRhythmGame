using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreBoard : MonoBehaviour
{
    public GameObject Pivot;
    private double health;


    void Start()
    {
        health = 0.5; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public double getHealth()
    {
    	return health;
    }

    public void updateHealth(double healthModifier)
    {
    	health = Math.Max(Math.Min(health + healthModifier, 1.0),0.0);
    	Debug.Log("health: " + health);
    	//Pivot.transform.Rotate(0.0f,0.0f,(float)((health-0.5)*90.0));
    	Pivot.transform.eulerAngles = new Vector3(0,0,(float)((health-0.5)*180.0));
    }
}
