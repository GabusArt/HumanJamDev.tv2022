using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadActivator : MonoBehaviour
{

    [SerializeField] List<GameObject> humansToKill;
    

    Spawner spawner;

    private void Start()
    {
        spawner = FindObjectOfType<Spawner>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        humansToKill.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        humansToKill.Remove(other.gameObject);
    }

    public void KillHumans()
    {
        {
             for (int i = humansToKill.Count - 1; i >= 0; i--)
            {
                humansToKill[i].SetActive(false);
            }
           
        }
     
    }
}
