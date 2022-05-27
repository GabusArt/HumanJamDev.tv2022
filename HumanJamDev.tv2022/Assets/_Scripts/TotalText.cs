using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalText : MonoBehaviour
{
    TextMeshProUGUI myT;

    private void Start()
    {
        myT = GetComponent<TextMeshProUGUI>();
        myT.text = string.Format("{0:0}", FindObjectOfType<GameManager>().GetMaxScore());
    }
}
