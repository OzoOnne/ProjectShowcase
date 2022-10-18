using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //Put in the scene name in the button to get it momving to the scene.
    //Remember to put your scene als in the build settings other wise it wont work
    public void LoadScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }

    //Exit game when is clicked on Exit
    public void ExitGame()
    {
        Debug.Log("Test");
        Application.Quit();
    }
}
