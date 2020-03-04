using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeArrowInfo
{
	public double start_time;
	public double end_time; 
	public double exact_time;
	public string arrow;

	public TimeArrowInfo(double exact, double start, double end, string arrow_dir)
	{
		start_time = start;
		end_time = end;
		exact_time = exact;
		arrow = arrow_dir;
	}
}
