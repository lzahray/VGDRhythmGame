using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;

public class BackupHeart : MonoBehaviour
{
	public Sprite[] up_down_left_right_center_sprites;
	//public string info_path;
	private string arrowInformation; 
	private int next_time_index;
	private List<double> start_times;
	private List<double> end_times;
	private List<string> arrows;
	private SpriteRenderer theSR;
	private Dictionary<string, Sprite> direction_to_sprite;
	private List<string> directions;

	void Awake()
	{
		//yeah yeah this is awful, but for now it's fiiiiine
		arrowInformation = "7.354607999999999,7.61537723076923,left\n9.200759999999999,9.461529230769228,down\n11.046911999999999,11.307681230769228,right\n12.893063999999999,13.153833230769228,down\n14.739215999999999,14.769215999999998,right\n14.739215999999999,14.999985230769228,left\n16.585367999999995,16.615367999999997,down\n16.585367999999995,16.846137230769227,left\n18.431519999999995,18.461519999999997,right\n18.431519999999995,18.692289230769227,down\n20.277671999999995,20.307671999999997,right\n20.277671999999995,20.538441230769227,left\n22.123823999999995,22.384593230769227,left\n23.046899999999997,23.30766923076923,right\n23.969975999999996,24.230745230769227,left\n24.893051999999997,25.15382123076923,down\n25.816127999999996,26.076897230769227,right\n27.662279999999996,27.923049230769227,right\n29.508431999999996,29.769201230769227,left\n30.431507999999994,30.692277230769225,right\n31.354583999999996,31.615353230769227,left\n32.27766,32.53842923076923,down\n33.20073599999999,33.46150523076923,right\n35.046887999999996,35.30765723076923,right\n36.89303999999999,36.92303999999999,down\n36.89303999999999,37.15380923076923,left\n37.816115999999994,38.07688523076923,right\n38.739191999999996,38.999961,left\n39.20072999999999,39.461498999999996,down\n39.662268,39.92303723076923,right\n40.58534399999999,40.84611323076923,left\n41.508419999999994,41.76918923076923,right\n42.431495999999996,42.69226523076923,left\n43.354572,43.615341,left\n43.816109999999995,44.07687899999999,left\n44.27764799999999,44.30764799999999,right\n44.27764799999999,44.53841723076923,down\n45.200723999999994,45.46149323076923,left\n46.123799999999996,46.384569,right\n46.58533799999999,46.84610699999999,down\n47.04687599999999,47.307645230769225,left\n47.96995199999999,48.23072123076923,right\n48.893027999999994,49.15379723076923,left\n49.816103999999996,50.07687323076923,down\n50.73917999999999,50.999948999999994,left\n51.200717999999995,51.46148699999999,right\n51.66225599999999,51.92302523076923,left\n53.508407999999996,53.76917723076923,right\n55.35455999999999,55.38455999999999,right\n55.35455999999999,55.61532923076923,left\n57.200711999999996,57.46148123076923,down\n58.12378799999999,58.384557230769225,down\n59.04686399999999,59.07686399999999,right\n59.04686399999999,59.30763323076923,left\n60.89301599999999,60.92301599999999,right\n60.89301599999999,61.15378523076922,down\n62.73916799999999,62.76916799999999,right\n62.73916799999999,62.99993723076923,left\n64.58532,64.84608899999999,right\n65.04685799999999,65.307627,left\n65.50839599999999,65.76916499999999,down\n65.969934,66.23070299999999,right\n66.43147199999999,66.69224123076921,left\n68.27762399999999,68.53839323076922,right\n70.12377599999999,70.38454523076922,down\n71.969928,72.23069723076922,right\n73.81607999999999,73.84607999999999,down\n73.81607999999999,74.07684923076921,left\n74.739156,74.99992523076922,right\n75.66223199999999,75.69223199999999,down\n75.66223199999999,75.92300123076922,left\n76.58530799999998,76.84607723076921,right\n77.50838399999999,77.76915323076922,left\n79.354536,79.61530523076922,down";
		start_times = new List<double> {};
		end_times = new List<double> {};
		arrows = new List<string> {};
		direction_to_sprite = new Dictionary<string, Sprite> {}; 
		next_time_index = 0; 
		var result = arrowInformation.Split(new [] { '\n' });
		foreach (var line in result)
		{
			//Debug.Log("line " + line);
			var values = line.Split(new [] {','});
         	start_times.Add(Convert.ToDouble(values[0]));
         	end_times.Add(Convert.ToDouble(values[1]));
         	arrows.Add(values[2]);
		}
		// StreamReader inp_stm = new StreamReader(info_path);
		// while (!inp_stm.EndOfStream)
  //       {
  //           var line = inp_stm.ReadLine();
  //           var values = line.Split(',');

  //           start_times.Add(Convert.ToDouble(values[0]));
  //           end_times.Add(Convert.ToDouble(values[1]));
  //           arrows.Add(values[2]);
  //       }

        theSR = GetComponent<SpriteRenderer>();
        directions = new List<string> {"up","down","left","right","center"};
        for (int i=0; i<up_down_left_right_center_sprites.Length; i++)
        {
        	direction_to_sprite[directions[i]] = up_down_left_right_center_sprites[i];
        }
	}

	void Start()
	{
		//TimeArrowInfo timeArrowObject = getNextTimesAndArrows();
		// Debug.Log("first time and arrows "+ timeArrowObject.start_time+ " "+ timeArrowObject.end_time+" "+ timeArrowObject.arrow);
		// MoveCharacter(timeArrowObject.arrow);
	}

	public TimeArrowInfo getNextTimesAndArrows()
	{
		TimeArrowInfo to_return = new TimeArrowInfo(start_times[next_time_index], start_times[next_time_index], end_times[next_time_index], arrows[next_time_index]);
		if (next_time_index < (start_times.Count-1))
		{
			next_time_index++;
		}
		return to_return;
	}

	public void MoveCharacter(string position)
	//position should either be "up", "down", "left", "right", or "center"
	{
		theSR.sprite = direction_to_sprite[position];
	}
}

