using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicBehaviour : MonoBehaviour
{

    Animator myCamAnimator;

    private void Awake()
    {
        myCamAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        FocusGreenScreen();
    }


    public void FocusGreenScreen()
    {
        myCamAnimator.SetInteger("ChangeCameraTo", 4);
    }

    public void FocusBlueScreen()
    {
        myCamAnimator.SetInteger("ChangeCameraTo", 3);

    }
    public void FocusStartScreen()
    {
        myCamAnimator.SetInteger("ChangeCameraTo", 1);
    }

    /*    public void StartGame()
       {
           StartCoroutine(Tutorial());
       } */
    /* 
        IEnumerator Tutorial()
        {
            yield return new WaitForEndOfFrame;
        } */
}
