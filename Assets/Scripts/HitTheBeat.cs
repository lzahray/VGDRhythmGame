using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTheBeat : MonoBehaviour
{
    public GameObject conductorObject;
   // public KeyInputVisual inputScript;
    
    public MainCharacterDuel characterScript;
    public GameObject scoreBoardObject;

    // public keyCode currentInput;

    public double points; 

    private Conductor conductorScript;
    private ScoreBoard scoreBoardScript;
    private Dictionary<string, KeyCode> keyCodesDict;
    private ArrowManager arrowManager;
    private 
    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        string arrowInfo = "11.076911999999998,up\n11.999987999999998,left\n12.923063999999998,down\n13.846139999999998,right\n18.461519999999997,up\n18.923057999999997,up\n19.384596,right\n20.307671999999997,down\n21.230748,down\n25.846127999999997,left\n26.769204,right\n27.692279999999997,left\n28.153817999999998,right\n28.615356,up\n33.23073599999999,up\n33.461504999999995,left\n33.692274,up\n34.153811999999995,left\n34.61535,left\n35.076888,down\n35.999964,down\n40.61534399999999,up\n41.076882,down\n41.538419999999995,up\n41.99995799999999,down\n42.461496,left\n43.384572,left\n47.99995199999999,left\n48.46149,right\n48.923027999999995,left\n49.38456599999999,right\n49.846104,up\n50.307641999999994,down\n50.76917999999999,up\n50.999948999999994,down\n51.230717999999996,up\n55.38455999999999,up\n56.307635999999995,down\n57.230712,right\n57.923019,left\n58.384556999999994,up\n62.76916799999999,right\n63.23070599999999,right\n63.692243999999995,down\n64.61532,left\n65.07685799999999,left\n65.53839599999999,down\n70.153776,up\n71.07685199999999,up\n71.999928,up\n72.46146599999999,up\n72.92300399999999,left\n73.384542,left\n77.538384,right\n78.46145999999999,left\n79.384536,up\n80.30761199999999,down";
        Debug.Log(arrowInfo);
        conductorScript = conductorObject.GetComponent<Conductor>(); //how to link... do myNewScript.public stuff
        scoreBoardScript = scoreBoardObject.GetComponent<ScoreBoard>();
        arrowManager = new ArrowManager(arrowInfo, conductorScript);
        keyCodesDict = new Dictionary<string, KeyCode> {{"up",KeyCode.UpArrow},  {"down", KeyCode.DownArrow}, {"left", KeyCode.LeftArrow}, {"right", KeyCode.RightArrow}};
        
        //inputScript = objectWithScript.GetComponent<KeyInputVisual>();
        characterScript = GetComponent<MainCharacterDuel>();
        //characterScript.MoveCharacter(timeGuy.arrow);
    }

    // Update is called once per frame
    void Update()
    {
        List<string> misses = arrowManager.updateActiveAndGetMisses();
        if (misses.Count > 0)
        {
            characterScript.EmitParticles("miss");
            scoreBoardScript.updateHealth(-0.05);
        }
        //always check if we're hitting a key 
        foreach(KeyValuePair<string, KeyCode> entry in keyCodesDict)
        {
            if (Input.GetKeyDown(entry.Value))
            {
                
                //Debug.Log("Particle! "+conductorScript.songPosition);
                string hitValue = arrowManager.checkArrowHit(entry.Key);
                if (hitValue == "wrong")
                {
                    characterScript.EmitParticles("wrong");
                    characterScript.MoveCharacter("confused");
                    
                    Debug.Log("Wrong! " + conductorScript.songPosition);
                    //scoreBoardScript.updateHealth(-0.05);
                }
                else if (hitValue == "good")
                {
                    characterScript.MoveCharacter(entry.Key);
                    characterScript.EmitParticles("good");
                    Debug.Log("good!");
                    scoreBoardScript.updateHealth(0.025);
                }
                else if (hitValue == "perfect")
                {
                    //characterScript.EmitParticles("perfect");
                    characterScript.MoveCharacter(entry.Key);
                    characterScript.EmitParticles("perfect");
                    
                    Debug.Log("perfect!");
                    scoreBoardScript.updateHealth(0.04);
                }

            }

            if (Input.GetKeyUp(entry.Value))
                {
                    //Debug.Log("key up");
                    characterScript.MoveCharacter("center");
                }

         }

         // if (scoreBoardScript.getHealth() == 0)
         // {
         //    Debug.Log("GAME OVER");
         // }

    }
}
