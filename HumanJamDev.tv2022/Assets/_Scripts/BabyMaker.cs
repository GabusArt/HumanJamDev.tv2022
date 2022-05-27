using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyMaker : MonoBehaviour
{
    int fertility = 1;
    [SerializeField] int adultFertilityValue = 2;
    [SerializeField] int oldFertilityValue = 1;
    [SerializeField] GameObject reproductionParticles;


    HumanMovement humanMovement;
    Spawner spawner;
    AudioManager audioManager;

    private void Awake()
    {

        humanMovement = GetComponentInParent<HumanMovement>();
        spawner = FindObjectOfType<Spawner>();
        audioManager = FindObjectOfType<AudioManager>();
    }


    // fase 3 adulta

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponentInParent<HumanMovement>().IsMale()) return;
        if (humanMovement.CanHumanHaveBabies() && other.gameObject.GetComponentInParent<HumanMovement>().CanHumanHaveBabies())
        {

            NewBabiesCount();
            StartCoroutine(NewBorn());
            audioManager.PlayReproductionSound();
            Instantiate(reproductionParticles, transform);
            FindObjectOfType<UICount>().ActivateNewBornEvent();
            var textEvent = FindObjectOfType<newBornDisplay>(); // quizas quitar dejar solo new humans created. REVISAR
            if(textEvent != null)
            {
                textEvent.PrintNewBabiesCount(fertility);
            }
           
        }

    }

    IEnumerator NewBorn()
    {
        for (int i = 0; i <= fertility; i++)
        {
            yield return new WaitForSeconds(0.4f);
            spawner.SpawnNewBorn();
            yield return new WaitForSeconds(0.1f);
        }


    }



    private void NewBabiesCount()
    {

        int age = humanMovement.HumanFase();
        if (age == 3)
        {
            fertility = adultFertilityValue;
        }
        else
        {
            fertility = oldFertilityValue;
        }

    }
}




