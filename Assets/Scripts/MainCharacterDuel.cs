using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;

public class MainCharacterDuel : MonoBehaviour
{
	public Sprite[] up_down_left_right_center_confused_sprites;
	public string info_path;

	private int next_time_index;
	private List<double> start_times;
	private List<double> end_times; 
	private List<string> arrows;
	private SpriteRenderer theSR;
	private Dictionary<string, Sprite> direction_to_sprite;
	private List<string> directions;

	void Awake()
	{
		start_times = new List<double> {};
		end_times = new List<double> {};
		arrows = new List<string> {};
		direction_to_sprite = new Dictionary<string, Sprite> {}; 
		next_time_index = 0; 
		StreamReader inp_stm = new StreamReader(info_path);
		while (!inp_stm.EndOfStream)
        {
            var line = inp_stm.ReadLine();
            var values = line.Split(',');

            start_times.Add(Convert.ToDouble(values[0]));
            end_times.Add(Convert.ToDouble(values[1]));
            arrows.Add(values[2]);
        }

        theSR = GetComponent<SpriteRenderer>();
        directions = new List<string> {"up","down","left","right","center","confused"};
        for (int i=0; i<up_down_left_right_center_confused_sprites.Length; i++)
        {
        	direction_to_sprite[directions[i]] = up_down_left_right_center_confused_sprites[i];
        }
	}

	void Start()
	{
		// TimeArrowInfo timeArrowObject = getNextTimesAndArrows();
		// Debug.Log("first time and arrows "+ timeArrowObject.start_time+ " "+ timeArrowObject.end_time+" "+ timeArrowObject.arrow);
		// MoveCharacter(timeArrowObject.arrow);
	}

	public TimeArrowInfo getNextTimesAndArrows()
	{
		//Debug.Log("next_time_index "+ next_time_index);
		TimeArrowInfo to_return = new TimeArrowInfo(start_times[next_time_index], end_times[next_time_index], arrows[next_time_index]);
		if (next_time_index < (start_times.Count-1))
		{
			next_time_index++;
		}
		
		return to_return;
	}

	public void MoveCharacter(string position)
	//position should either be "up", "down", "left", "right", "center", or "confused"
	{
		//Debug.Log("Moving character to "+  position + " sprite "+direction_to_sprite[position]);
		theSR.sprite = direction_to_sprite[position];
	}

}
