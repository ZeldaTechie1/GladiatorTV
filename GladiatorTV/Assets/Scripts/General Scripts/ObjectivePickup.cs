using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePickup : MonoBehaviour {

    [SerializeField]
    string flavorText;

    public string getFlavorText()
    {
        return flavorText;
    }
}
