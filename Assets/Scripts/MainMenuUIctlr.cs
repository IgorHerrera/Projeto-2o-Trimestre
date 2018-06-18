using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIctlr : MonoBehaviour {

	public void LoaadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
