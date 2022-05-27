using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamePicker : MonoBehaviour
{
    [SerializeField] List<string> femaleNames;
    [SerializeField] List<string> malesNames;


    public string PickRandomFemaleName()
    {
        string newName =  femaleNames[Random.Range(0,femaleNames.Count)];
        return newName;
    }

        public string PickRandomMalesName()
    {
        string newName =  malesNames[Random.Range(0,malesNames.Count)];
        return newName;
    }

    
}
