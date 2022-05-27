using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCloser : MonoBehaviour
{
   public void Deactivate()
   {
       gameObject.SetActive(false);
   }
}
