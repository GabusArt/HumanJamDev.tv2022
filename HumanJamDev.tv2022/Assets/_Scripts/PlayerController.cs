using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{


    [SerializeField] float coolDown = 3f;
    [SerializeField] float timeKilling = 2f;
    [SerializeField] int wakeUpTaps = 6;
    [SerializeField] GameObject tapSprite = null;

    bool canPlay = false;
    public bool isSleeping = false; //quitar public
    float timer = Mathf.Infinity;
    public int tapCount = 0; //quitar public
    float tapTimer = 0;


    Vector2 moveInput;
    Spawner spawner;
    CameraShake cameraShake;
    Animator myCamAnimator;
    ParticlesKill particlesKill;
    EffectsController effectsController;


    //1 start, 2 sleep, 3 blueScreen, 4 greenScreen.


    private void Awake()
    {

        spawner = FindObjectOfType<Spawner>();
        cameraShake = FindObjectOfType<CameraShake>();
        myCamAnimator = GetComponent<Animator>();
        particlesKill = FindObjectOfType<ParticlesKill>();
        effectsController = FindObjectOfType<EffectsController>();
    }

    private void Update()
    {
        PlayingActions();
        if (!canPlay) return;
        timer += Time.deltaTime;
        if (timer > coolDown && !particlesKill.AreParticlesActive())
        {
            particlesKill.PlayKillParticles();
        }

    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void PlayingActions()
    {
        if (isSleeping)
        {
            tapTimer += Time.deltaTime;
            if (tapCount > wakeUpTaps)
            {
                WakeUpActions();

            }
            if (tapTimer >= 20)
            {
                tapSprite.SetActive(true);
            }
        }
        if (!isSleeping)
        {
            if (moveInput.y != 0) return;
            if (moveInput.x > 0)
            {
                FocusBlueScreen();
            }
            if (moveInput.x < 0)
            {
                FocusGreenScreen();
            }
        }




    }

    void WakeUpActions()
    {
        isSleeping = false;
        effectsController.isWakingUP = true;
        FocusBlueScreen();
        tapCount = 0;
    }



    public IEnumerator SleepActions()
    {
        effectsController.ActiveParpadeo(true);
        yield return new WaitForSecondsRealtime(3f);
        effectsController.isSleeping = true;
        isSleeping = true;
        myCamAnimator.SetInteger("ChangeCameraTo", 2);
    }

    void OnWakeUp(InputValue value)
    {

        if (value.isPressed)
        {
            if (isSleeping)
            {
                print("Tap Tap!");
                tapCount++;
                if (tapSprite.activeInHierarchy)
                {
                    tapSprite.SetActive(false);
                    tapTimer = 0;
                }
            }

        }

    }


    private void FocusGreenScreen()
    {
        myCamAnimator.SetInteger("ChangeCameraTo", 4);
    }

    public void FocusBlueScreen()
    {
        myCamAnimator.SetInteger("ChangeCameraTo", 3);

    }
    void FocusStartCamera()
    {
        myCamAnimator.SetInteger("ChangeCameraTo", 1);
    }
    public void FinishingGamePlayer()
    {
        canPlay = false; 
        FocusStartCamera();
    }

  

    /*  if (moveInput.x != 0) return;
        if (moveInput.y > 0)
        {
            myCamAnimator.SetInteger("ChangeCameraTo", 2);
        }
        if (moveInput.y < 0)
        {
            ;
        } */



    //press E
    void OnKill(InputValue value)
    {
        if (!canPlay) return;
        if (isSleeping) return;
        if (!CanSeeBlueScreenCanvas()) return;
        if (timer < coolDown) return;
        if (value.isPressed)
        {
            cameraShake.PlayShake();
            FindObjectOfType<AudioManager>().PlayKillSound();
            StartCoroutine(PlayerKilling());
            particlesKill.StopKillParticles();
            timer = 0;
        }
    }


    IEnumerator PlayerKilling()
    {
        FindObjectOfType<DeadActivator>().KillHumans();
        yield return new WaitForSecondsRealtime(timeKilling);
        print("nuevos humanos");
        spawner.SpawnSameHuman();
    }

    public void ActivateControls(bool value)
    {
        canPlay = value;
    }

    public bool CanSeeBlueScreenCanvas()
    {
        if (myCamAnimator.GetInteger("ChangeCameraTo") == 3)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

}
