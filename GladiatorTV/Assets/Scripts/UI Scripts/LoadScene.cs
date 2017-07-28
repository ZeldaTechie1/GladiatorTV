using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    bool credentialsEntered;
    [SerializeField]
    Mediator mediator;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
    }

    public void LoadByIndex(int sceneIndex)
    {
        approveCredentials();
        if (credentialsEntered)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogError("BITCH YOU FORGOT THE CREDENTIALS");
        }
    }

    public void RestartScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void approveCredentials()
    {
        if (mediator != null)
            credentialsEntered = mediator.GetCredentialsSet();
    }
}
