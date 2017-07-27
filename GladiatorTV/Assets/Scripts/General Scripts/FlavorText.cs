using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlavorText : MonoBehaviour {

    [SerializeField]
    Text flavorTextHolder;

    private void Awake()
    {
        if(flavorTextHolder != null)
        {
            flavorTextHolder.enabled = false;
        }
    }

    /*private void Update()//for testing purposes
    {
        if(Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(showFlavorText("I am working!"));
        }
    }*/

    public IEnumerator showFlavorText(string flavorTexts)
    {
        flavorTextHolder.enabled = true;
        flavorTextHolder.text = flavorTexts;
        yield return new WaitForSeconds(3f);
        flavorTextHolder.enabled = false;
    }

}
