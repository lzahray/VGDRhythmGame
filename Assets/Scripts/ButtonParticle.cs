using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonParticle : MonoBehaviour
{
	public ParticleSystem particleSystemPerfect;
	public ParticleSystem particleSystemGood;
	public ParticleSystem particleSystemWrong;
	public ParticleSystem particleSystemMiss;

	private Dictionary<string, ParticleSystem> hitValue_to_particle_system;
    // Start is called before the first frame update

	void Start()
	{
		hitValue_to_particle_system = new Dictionary<string,ParticleSystem> () {{"perfect",particleSystemPerfect}, {"good",particleSystemGood},{"miss",particleSystemMiss},{"wrong",particleSystemWrong}};

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
}
