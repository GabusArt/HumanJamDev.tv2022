using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGameButton : MonoBehaviour
{
    public void BackToMainMenu()
    {
        FindObjectOfType<GameManager>().StartToMainMenu();
    }
}
