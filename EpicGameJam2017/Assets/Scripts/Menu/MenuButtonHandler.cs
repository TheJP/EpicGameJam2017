using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonHandler : MonoBehaviour {

    public void loadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void loadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
	
}
