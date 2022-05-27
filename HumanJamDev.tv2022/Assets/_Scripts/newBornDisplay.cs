using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class newBornDisplay : MonoBehaviour
{
    TextMeshProUGUI newBornText;


    private void Awake()
    {
        newBornText = GetComponent<TextMeshProUGUI>();
    }


     public void PrintNewBabiesCount(int value)
    {

        newBornText.text = "New Humans Created";
    }


}
