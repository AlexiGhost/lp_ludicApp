using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalHub : MonoBehaviour {

    public string levelName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (SceneUtility.GetBuildIndexByScenePath(levelName) != -1)
                SceneManager.LoadSceneAsync(levelName);
            else
                SceneManager.LoadSceneAsync("MainMenu");
        }
    }
}
