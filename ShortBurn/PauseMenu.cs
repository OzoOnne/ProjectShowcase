using System.Collections;
using Cinemachine;
using Control_and_Input;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool _gameIsPaused = false;
    
    [SerializeField] private GameObject pauseMenuUiCold;
    [SerializeField] private GameObject pauseMenuUiWarm;
    
    [SerializeField] private InputManager inputManager ;
    private CinemachinePOV pov;


    [SerializeField] private GameObject pauzeMenu;
    [SerializeField] private Animator summerAnimator;
    [SerializeField] private Animator winterAnimator;
    

    public bool isWinter;

    private void Start()
    {
        var cvCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (cvCamera != null)
        {
            pov = cvCamera.GetCinemachineComponent<CinemachinePOV>();
        }
    }

    void Update()
    {
        //game is paused yes or no?
        if (inputManager.Escape())
        {
            if (_gameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    //No? then it will continue on this code:
    public void Resume()
    {
        StartCoroutine(ResumeCoroutine());
    }
    
    IEnumerator ResumeCoroutine()
    {
        winterAnimator.SetBool("Up", true);
                summerAnimator.SetBool("Up",true);
                _gameIsPaused = false;
                pov.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                yield return new WaitForSeconds(1.8f);
                pauseMenuUiCold.SetActive(false);
                pauseMenuUiWarm.SetActive(false);
                pauzeMenu.SetActive(false);
    }
    //Yes? then it will pause on this code:
    public void Pause()
    {
        pauzeMenu.SetActive(true);
        if (isWinter)
        {
            pauseMenuUiCold.SetActive(true);
            winterAnimator.SetBool("Up",false);
        }
        else
        {
            pauseMenuUiWarm.SetActive(true);
            summerAnimator.SetBool("Up",false);
        }

        pov.enabled = false;
        _gameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    //Put in the scene name in the button to get it momving to the scene.
    //Remember to put your scene als in the build settings other wise it wont work
    public void LoadScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }

    //Exit game when is clicked on Exit
    public void ExitGame()
    {
        Application.Quit();
    }
    
}
