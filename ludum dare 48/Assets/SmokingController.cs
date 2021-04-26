using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokingController : MonoBehaviour
{
    public GameObject CigarettePack;
    public GameObject Cigarette;
    public GameObject MouthCigarette;
    public GameObject Lighter;

    public ParticleSystem LighterParticleSystem;
    public ParticleSystem CigaretteParticleSystem;
    public ParticleSystem MouthSmokeParticleSystem;

    private bool isEven = false;
    
    void SpawnCigarettePack()
    {
        CigarettePack.SetActive(true);
    }
    void SpawnCigarette()
    {
        Cigarette.SetActive(true);    
    }
    void CigaretteToMouth()
    {
        Cigarette.SetActive(false);        
        MouthCigarette.SetActive(true);        
    }
    void DespawnCigarettePack()
    {
        CigarettePack.SetActive(false);
    }
    void GetLighter()
    {
        Lighter.SetActive(true);
    }
    void LightCigarette()
    {
        LighterParticleSystem.Play();
    }
    void DespawnLighter()
    {
        LighterParticleSystem.Stop();
        Lighter.SetActive(false);
    }
    void CigaretteToHand()
    {
        Cigarette.SetActive(true);        
        MouthCigarette.SetActive(false);
    }

    void CigaretteBurst()
    {
        CigaretteParticleSystem.Play();
    }

    void CigaretteBurstStop()
    {
        CigaretteParticleSystem.Stop();
    }
    
    void MouthSmoke()
    {
        if (!isEven)
        {
            MouthSmokeParticleSystem.Play();    
        }

        isEven = !isEven;
    }

    void DropCigarette()
    {
        Cigarette.SetActive(false);
    }
    
}
