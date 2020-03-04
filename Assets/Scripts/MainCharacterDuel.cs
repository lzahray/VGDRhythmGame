using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;

public class MainCharacterDuel : MonoBehaviour
{
	public Sprite[] up_down_left_right_center_confused_sprites;
	public ParticleSystem particleSystemPerfect;
	public ParticleSystem particleSystemGood;
	public ParticleSystem particleSystemWrong;
	public ParticleSystem particleSystemMiss;

	private SpriteRenderer theSR;
	private Dictionary<string, Sprite> direction_to_sprite;
	private List<string> directions;
	private Dictionary<string, ParticleSystem> hitValue_to_particle_system;


	void Awake()
	{

        // ps.Stop();
        // var main = ps.main;
        // main.duration = 1;
		// particleSystem.enableEmission = false;
		// particleSystem.Play();
		direction_to_sprite = new Dictionary<string, Sprite> {}; 

        theSR = GetComponent<SpriteRenderer>();

        directions = new List<string> {"up","down","left","right","center","confused"};
        for (int i=0; i<up_down_left_right_center_confused_sprites.Length; i++)
        {
        	direction_to_sprite[directions[i]] = up_down_left_right_center_confused_sprites[i];
        }

        hitValue_to_particle_system = new Dictionary<string,ParticleSystem> () {{"perfect",particleSystemPerfect}, {"good",particleSystemGood},{"miss",particleSystemMiss},{"wrong",particleSystemWrong}};

        // var em = ps.emission;
        // em.enabled = true;
        // em.rateOverTime = 0;

        // em.SetBursts(
        //     new ParticleSystem.Burst[]
        //     {
        //         new ParticleSystem.Burst(0.0f, 1),
        //     });
	}

	void Update()
	{
	}


	public void MoveCharacter(string position)
	//position should either be "up", "down", "left", "right", "center", or "confused"
	{
		//Debug.Log("Moving character to "+  position + " sprite "+direction_to_sprite[position]);
		theSR.sprite = direction_to_sprite[position];
	}

	public void EmitParticles(string hitValue)
	{
		if (hitValue=="wrong" | hitValue =="miss")
		{
			hitValue_to_particle_system[hitValue].Emit(20);
		}
		else
		{
			hitValue_to_particle_system[hitValue].Emit(30);
		}
	}
	// IEnumerator emitParticles()
	// {
	// 	yield return new WaitForSeconds (0.1f);

	// 	particleSystem.enableEmission = false;
	// }

}
