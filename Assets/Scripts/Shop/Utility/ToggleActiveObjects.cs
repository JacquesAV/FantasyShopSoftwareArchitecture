using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script that allows for quick toggling of objects given through the inspector
//Faster and cleaner than manually inputting SetActive(false/true) to objects in the inspectors button functionality
public class ToggleActiveObjects : MonoBehaviour
{
    [SerializeField] private GameObject[] toggleObjectsOn = null, toggleObjectsOff = null;
    public void ToggleObjects()
    {
        //Toggle given objects off
        foreach (GameObject singleObject in toggleObjectsOff)
        {
            //Null check
            if (singleObject != null)
            {
                singleObject.SetActive(false);
            }
        }
        //Toggle given objects on
        foreach (GameObject singleObject in toggleObjectsOn)
        {
            //Null check
            if (singleObject != null)
            {
                singleObject.SetActive(true);
            }
        }
    }
}
