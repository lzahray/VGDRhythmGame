using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmitter : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem ps;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ShootParticles()
    {
        ps.Emit(10);
    }
}
