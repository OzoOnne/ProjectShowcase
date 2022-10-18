using UnityEngine;
using TMPro;


    public enum GameState
    {
        Game,
        Paused,
        Start,
        Lost,
        Teleporting
    }
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Tooltip("The current state of the game")]
    [SerializeField] private GameState gameState;
    [Tooltip("How fast the fps refreshes")]
    [SerializeField] private float fpsRefreshRate = 0.5f;
    [Tooltip("The fps text")]
    [SerializeField] private TextMeshProUGUI fpsText;

    private InterfaceManager interfaceManager;
    private float timer;
    private bool updateFPSText;

    public bool UpdateFPSText { get { return updateFPSText; } set { updateFPSText = value; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;

        //SetGameState(GameState.Start);
    }

    private void Start()
    {
        interfaceManager = InterfaceManager.instance;
    }

    private void Update()
    {
        if (gameState == GameState.Game || gameState == GameState.Paused)
            CheckPause();

        if (updateFPSText)
            UpdateFPS();
    }

    /// <summary>
    /// This updates your fps
    /// </summary>
    private void UpdateFPS()
    {
        if (Time.unscaledTime > timer)
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            fpsText.text = fps.ToString() + " FPS";
            timer = Time.unscaledTime + fpsRefreshRate;
        }
    }

    /// <summary>
    /// Enable gameover
    /// </summary>
    public void SetGameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        SetGameState(GameState.Lost);
        interfaceManager.ShowGameOver();
    }

    /// <summary>
    /// Get the current game state
    /// </summary>
    /// <returns></returns>
    public GameState GetGameState()
    {
        return gameState;
    }

    /// <summary>
    /// Set the game state with enum
    /// </summary>
    /// <param name="state">What state it needs to be</param>
    public void SetGameState(GameState state)
    {
        gameState = state;
    }

    /// <summary>
    /// Set the game state with int for buttons
    /// </summary>
    /// <param name="state">The state number</param>
    public void SetGameState(int state)
    {
        gameState = (GameState)state;
    }

    /// <summary>
    /// Checks if the game is paused or not
    /// </summary>
    private void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameState != GameState.Lost && gameState != GameState.Start && gameState != GameState.Teleporting)
        {
            Cursor.lockState = CursorLockMode.None;

            if (gameState == GameState.Paused)
            {
                interfaceManager.ShowPauseMenu(false);
                SetGameState(GameState.Game);
            }
            else if (gameState == GameState.Game)
            {
                interfaceManager.ShowPauseMenu(true);
                SetGameState(GameState.Paused);
            }
        }
    }
}
