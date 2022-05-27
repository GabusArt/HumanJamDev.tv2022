using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] Animator myAnimator;
   

    public void StartButtonGame()
    {
        myAnimator.SetTrigger("Start");
    }

    public void LoadingGame()
    {
        FindObjectOfType<GameManager>().LoadGame();
    }

}
