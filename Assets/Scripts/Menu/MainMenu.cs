using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /*************
     * FUNCTIONS *
     *************/

    /**Buttons**/
    public void PlayGame()
    {
        SceneManager.LoadScene("NielsScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
