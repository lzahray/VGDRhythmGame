using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;

public class BossCharacterDuel : MonoBehaviour
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
		arrowInformation = "7.354607999999999,7.61537723076923,up\n8.277683999999999,8.538453230769228,left\n9.200759999999999,9.461529230769228,down\n10.123835999999999,10.384605230769228,right\n14.739215999999999,14.999984999999999,up\n15.200753999999998,15.461522999999998,up\n15.662291999999999,15.923061230769228,right\n16.585367999999995,16.846137230769227,down\n17.508443999999997,17.76921323076923,down\n22.123823999999995,22.384593230769227,left\n23.046899999999997,23.30766923076923,right\n23.969975999999996,24.230745,left\n24.431513999999996,24.692282999999996,right\n24.893051999999997,25.15382123076923,up\n29.508431999999996,29.653816499999998,up\n29.739200999999994,29.884585499999996,left\n29.969969999999996,30.230738999999996,up\n30.431507999999994,30.692276999999997,left\n30.893045999999995,31.153814999999994,left\n31.354583999999996,31.615353230769227,down\n32.27766,32.53842923076923,down\n36.89303999999999,37.153808999999995,up\n37.354578,37.615347,down\n37.816115999999994,38.07688499999999,up\n38.27765399999999,38.538422999999995,down\n38.739191999999996,38.99996123076923,left\n39.662268,39.92303723076923,left\n44.27764799999999,44.538416999999995,left\n44.739186,44.999955,right\n45.200723999999994,45.46149299999999,left\n45.66226199999999,45.923030999999995,right\n46.123799999999996,46.384569,up\n46.58533799999999,46.84610699999999,down\n47.04687599999999,47.19226049999999,up\n47.27764499999999,47.4230295,down\n47.508413999999995,47.76918323076923,up\n51.66225599999999,51.92302523076923,up\n52.585331999999994,52.84610123076923,down\n53.508407999999996,53.76917723076923,right\n54.200714999999995,54.461484,left\n54.66225299999999,54.92302223076923,up\n59.04686399999999,59.307632999999996,right\n59.50840199999999,59.76917099999999,right\n59.969939999999994,60.23070923076923,down\n60.89301599999999,61.15378499999999,left\n61.35455399999999,61.61532299999999,left\n61.81609199999999,62.076861230769225,down\n66.43147199999999,66.69224123076921,up\n67.354548,67.61531723076922,up\n68.27762399999999,68.53839299999999,up\n68.739162,68.999931,up\n69.2007,69.461469,left\n69.66223799999999,69.92300723076922,left\n73.81607999999999,74.07684923076921,right\n74.739156,74.99992523076922,left\n75.66223199999999,75.92300123076922,up\n76.58530799999998,76.84607723076921,down";
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
