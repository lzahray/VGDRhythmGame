using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTheBeatMainLevel : MonoBehaviour
{
	public GameObject conductorObject;
   // public KeyInputVisual inputScript;
    
    public MainCharacterDuel characterScript;
    public GameObject leftButton;
    public GameObject centerButton;
    public GameObject rightButton;
    // public keyCode currentInput;

    public double points; 

    private Conductor conductorScript;
    private ScoreBoard scoreBoardScript;
    private Dictionary<string, KeyCode> keyCodesDict;
    private ArrowManager arrowManager;

    private ButtonParticle leftButtonParticle;
    private ButtonParticle centerButtonParticle;
    private ButtonParticle rightButtonParticle;

    private Dictionary<string, ButtonParticle> directionToButtonParticle;
    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        conductorScript = conductorObject.GetComponent<Conductor>(); //how to link... do myNewScript.public stuff
        leftButtonParticle = leftButton.GetComponent<ButtonParticle>();
        centerButtonParticle = centerButton.GetComponent<ButtonParticle>();
        rightButtonParticle = rightButton.GetComponent<ButtonParticle>();

        string arrowInfo = "7.384607999999999,left\n9.230759999999998,center\n11.076911999999998,right\n12.923063999999998,center\n14.769215999999998,right\n14.769215999999998,left\n16.615367999999997,center\n16.615367999999997,left\n18.461519999999997,right\n18.461519999999997,center\n20.307671999999997,right\n20.307671999999997,left\n22.153823999999997,left\n23.0769,right\n23.999975999999997,left\n24.923052,center\n25.846127999999997,right\n27.692279999999997,right\n29.538431999999997,left\n30.461507999999995,right\n31.384583999999997,left\n32.30766,center\n33.23073599999999,right\n35.076888,right\n36.92303999999999,center\n36.92303999999999,left\n37.846115999999995,right\n38.769192,left\n39.230729999999994,center\n39.692268,right\n40.61534399999999,left\n41.538419999999995,right\n42.461496,left\n43.384572,left\n43.846109999999996,left\n44.30764799999999,right\n44.30764799999999,center\n45.230723999999995,left\n46.1538,right\n46.615337999999994,center\n47.07687599999999,left\n47.99995199999999,right\n48.923027999999995,left\n49.846104,center\n50.76917999999999,left\n51.230717999999996,right\n51.69225599999999,left\n53.538408,right\n55.38455999999999,right\n55.38455999999999,left\n57.230712,center\n58.15378799999999,center\n59.07686399999999,right\n59.07686399999999,left\n60.92301599999999,right\n60.92301599999999,center\n62.76916799999999,right\n62.76916799999999,left\n64.61532,right\n65.07685799999999,left\n65.53839599999999,center\n65.999934,right\n66.46147199999999,left\n68.30762399999999,right\n70.153776,center\n71.999928,right\n73.84607999999999,center\n73.84607999999999,left\n74.769156,right\n75.69223199999999,center\n75.69223199999999,left\n76.61530799999998,right\n77.538384,left\n79.384536,center";
        Debug.Log(arrowInfo);
        
        arrowManager = new ArrowManager(arrowInfo, conductorScript);
        keyCodesDict = new Dictionary<string, KeyCode> {{"left",KeyCode.J},  {"center", KeyCode.K}, {"right", KeyCode.L}};
        
        //inputScript = objectWithScript.GetComponent<KeyInputVisual>();
        characterScript = GetComponent<MainCharacterDuel>();

        directionToButtonParticle = new Dictionary<string,ButtonParticle> {{"left",leftButtonParticle},{"center",centerButtonParticle}, {"right",rightButtonParticle}};
        //characterScript.MoveCharacter(timeGuy.arrow);
    }

    // Update is called once per frame
    void Update()
    {
        List<string> misses = arrowManager.updateActiveAndGetMisses();
        foreach(string missDir in misses)
        {
        	directionToButtonParticle[missDir].EmitParticles("miss");
            //characterScript.EmitParticles("miss");
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
                    //characterScript.EmitParticles("wrong");
                    characterScript.MoveCharacter("confused");
                    directionToButtonParticle[entry.Key].EmitParticles(hitValue);
                    Debug.Log("Wrong! " + conductorScript.songPosition);
                }
                else if (hitValue == "good")
                {
                    if (entry.Key == "center")
                    {
                    	characterScript.MoveCharacter("down"); //it's weird sorry
                    } 
                    else
                    {
                    	characterScript.MoveCharacter(entry.Key);
                    }
                    directionToButtonParticle[entry.Key].EmitParticles(hitValue);
                    //characterScript.EmitParticles("good");
                    Debug.Log("good!");
                }
                else if (hitValue == "perfect")
                {
                    //characterScript.EmitParticles("perfect");
                    if (entry.Key == "center")
                    {
                    	characterScript.MoveCharacter("down"); //it's weird sorry
                    } 
                    else
                    {
                    	characterScript.MoveCharacter(entry.Key);
                    }
                    directionToButtonParticle[entry.Key].EmitParticles(hitValue);
                    //characterScript.EmitParticles("perfect");
                    
                    Debug.Log("perfect!");
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

