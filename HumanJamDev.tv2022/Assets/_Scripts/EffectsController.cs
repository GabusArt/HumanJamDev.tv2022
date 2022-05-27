using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EffectsController : MonoBehaviour
{
    [SerializeField] float parpadeoIntensity = 0.8f;
    [SerializeField] float parpadeoRange = 0.2f;
    [SerializeField] float openEyesRange = 0.4f;

    [SerializeField] Volume volume;
    Vignette vignette;

    bool isClosingEyes = true;
    public bool isSleeping = false;
    public bool isWakingUP = false;



    public bool parpadeo = false; //quitar public

    private void Start()
    {
        if (volume.profile.TryGet<Vignette>(out vignette))
        {
            vignette.intensity.value = 0;
        }

    }



    void FixedUpdate()
    {
        if (parpadeo)
        {
            if (isClosingEyes)
            {
                vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, parpadeoIntensity + 0.1f, parpadeoRange * Time.deltaTime);
                if (vignette.intensity.value >= parpadeoIntensity)
                {
                    isClosingEyes = false;
                }
            }
            if (!isClosingEyes)
            {
                vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0, openEyesRange * Time.deltaTime);
                if (vignette.intensity.value <= 0.2)
                {
                    vignette.intensity.value = 0;
                    ActiveParpadeo(false);
                    isClosingEyes = true;
                    return;
                }


            }

        }


        if (isSleeping)
        {
            SleepingEffect();
        }
    }


    public void SleepingEffect()
    {

        if (!isWakingUP)
        {
            vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, parpadeoIntensity + 0.1f, parpadeoRange * Time.deltaTime);
        }
        if (isWakingUP)
        {
            vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0, openEyesRange * Time.deltaTime);
            if (vignette.intensity.value <= 0.2)
            {
                vignette.intensity.value = 0;
                isSleeping = false;
                isWakingUP = false;
            }
        }
    }



    public void ActiveParpadeo(bool value)
    {
        parpadeo = value;

    }

}
