using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesKill : MonoBehaviour
{
    [SerializeField] GameObject eyeParticles;
    [SerializeField] GameObject eyeParticlesShine;

    [SerializeField] GameObject deadZonePArticles;
    [SerializeField] GameObject killPoint;


    public void StopKillParticles()
    {
        if(eyeParticles == null) return;
        eyeParticles.SetActive(false);
        eyeParticlesShine.SetActive(false);
        Instantiate(deadZonePArticles, killPoint.transform);
    }
        public void PlayKillParticles()
    {
        if(eyeParticles == null) return;
        eyeParticles.SetActive(true);
        eyeParticlesShine.SetActive(true);
    }
    public bool AreParticlesActive()
    {
        return eyeParticles.activeInHierarchy == true;
        
    }




}
