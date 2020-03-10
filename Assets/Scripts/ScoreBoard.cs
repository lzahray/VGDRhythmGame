using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreBoard : MonoBehaviour
{
    public GameObject Pivot;
    public List<Sprite> colorSectors;
    public GameObject colorSectorObject;
    private double health;
    private SpriteRenderer theSR; 


    void Start()
    {
        health = 0.5; 
        theSR = colorSectorObject.GetComponent<SpriteRenderer>();
        updateHealth(0);
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
    	//Debug.Log("health: " + health);
    	//Pivot.transform.Rotate(0.0f,0.0f,(float)((health-0.5)*90.0));
    	Pivot.transform.eulerAngles = new Vector3(0,0,(float)((health-0.5)*180.0));

        if (health < 0.083333)
        {
            theSR.sprite = colorSectors[0];
        }
        else if (health < 0.32222)
        {
            theSR.sprite = colorSectors[1];
        }
        else if (health < 0.63889)
        {
            theSR.sprite = colorSectors[2];
        }
        else if (health < 0.91667)
        {
            theSR.sprite = colorSectors[3];
        }
        else
        {
            theSR.sprite = colorSectors[4];
        }
    }

 
}
