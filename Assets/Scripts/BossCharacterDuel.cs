using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;

public class BossCharacterDuel : MonoBehaviour
{
	public Sprite[] up_down_left_right_center_sprites;
	public string info_path;

	private int next_time_index;
	private List<double> times;
	private List<string> arrows;
	private SpriteRenderer theSR;
	private Dictionary<string, Sprite> direction_to_sprite;
	private List<string> directions;

	void Awake()
	{
		times = new List<double> {};
		arrows = new List<string> {};
		direction_to_sprite = new Dictionary<string, Sprite> {}; 
		next_time_index = 0; 
		StreamReader inp_stm = new StreamReader(info_path);
		while (!inp_stm.EndOfStream)
        {
            var line = inp_stm.ReadLine();
            var values = line.Split(',');

            times.Add(Convert.ToDouble(values[0]));
            arrows.Add(values[2]);
        }

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
		TimeArrowInfo to_return = new TimeArrowInfo(times[next_time_index], times[next_time_index]+.15, arrows[next_time_index]);
		if (next_time_index < (times.Count-1))
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
