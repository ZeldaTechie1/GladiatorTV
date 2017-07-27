using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mediator : MonoBehaviour {

    bool credentialsSet = false;

    private void Awake()
    {
        credentialsSet = AllCredentialsObtained();
    }

    public bool GetCredentialsSet()
    {
        return credentialsSet;
    }

    public void setCredentials()
    {
        credentialsSet = true;
    }

    public bool AllCredentialsObtained()
    {
        if (!PlayerPrefs.HasKey("Username"))
        {
            Debug.LogError("Username is not entered, PUT IT IN BITCH!!!");
            return false;
        }
        if (!PlayerPrefs.HasKey("Token"))
        {
            Debug.LogError("Token has not been provided, PUT IT IN SON!");
            return false;
        }
        return true;
    }
}
