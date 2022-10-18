using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("Timer system")]

    [Tooltip("The score based on your timer")]
    [SerializeField] private int p_TimerScore;
    [Tooltip("The score text needed for the text update")]
    [SerializeField] private TMPro.TextMeshProUGUI p_ScoreText;
    [Tooltip("Time for which a point is given for example 0.5")]
    [SerializeField] private float p_PointTime;


    [Header("Distance Score")]

    [Tooltip("Time for the formula")]
    [SerializeField] private float p_Time;
    [Tooltip("Speed for the formula")]
    [SerializeField] private float p_speed;
    [Tooltip("Distance the solution of the formula")]
    [SerializeField] private float p_Distance;
    [Tooltip("Highscore in distance")]
    [SerializeField] private int p_HighScore;
    [Tooltip("The text for the distance")]
    [SerializeField] private TMPro.TextMeshProUGUI p_DistanceText;
    [Tooltip("The highscore text needed for the text update")]
    [SerializeField] private TMPro.TextMeshProUGUI p_HighScoreText;

    public float Distance { get { return p_Distance; } }

    // The int needed for the distance score
    private int p_DistanceInt;

    // The timer to keep track how long you are playing
    private float p_Timer;

    #region Singleton
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }
    #endregion

    private void Start()
    {
        // Displays the highscore on the screen
        p_HighScore = PlayerPrefs.GetInt("HighScore");
        p_HighScoreText.text = p_HighScore.ToString();
    }

    private void Update()
    {
        if (p_Time < 3 && GameManager.instance.GetGameState() == GameState.Lost)
            AchievementManager.instance.EarnAchievement("WATCH THE ROAD");

        if (GameManager.instance.GetGameState() != GameState.Game)
            return;

        // Will add time to the 2 timers
        p_Timer += Time.deltaTime;
        p_Time += Time.deltaTime;

        // Formula used: Distance = speed x time so we will get the distance
        p_Distance = p_speed * p_Time;

        // Change the time when it is above the given change
        if (p_Timer > p_PointTime)
            TimeChanger();

        // Change the highscore when it is above it
        if (p_DistanceInt > p_HighScore)
            HighScoreChanger();

        //change the distance when its above 0
        if (p_Distance > 0.05)
            DistanceChanger();

        if (p_Distance > 100)
            AchievementManager.instance.EarnAchievement("Drive");
        if (p_Distance > 1000)
            AchievementManager.instance.EarnAchievement("To the horizon");
    }

    #region Time Changer
    private void TimeChanger()
    {
        // adds a point to the score
        p_TimerScore++;

        PlayerPrefs.SetInt("Time", p_TimerScore);
        PlayerPrefs.Save();

        // change the score
        ChangeTimeText();

        // resets the timer
        p_Timer = 0f;
    }

    // change the Time text when it is summoned
    private void ChangeTimeText()
    {
        p_ScoreText.text = p_TimerScore.ToString();
    }
    #endregion

    #region Distance and HighScore Changer

    /// <summary>
    /// Will change the distance given in the update to an int
    /// So it will be used in the score shown on the screen
    /// </summary>
    private void DistanceChanger()
    {
        p_DistanceInt = Mathf.RoundToInt(p_Distance);

        PlayerPrefs.SetInt("CurrentDistance", p_DistanceInt);
        PlayerPrefs.Save();

        p_DistanceText.text = p_DistanceInt + "m";
    }

    /// <summary>
    /// Will change the highscore if the distance is higher.
    /// Will also update when the highscore is beaten
    /// Will also save the highscore in playerprefs.
    /// </summary>
    private void HighScoreChanger()
    {
        p_HighScore = p_DistanceInt;
        p_HighScoreText.text = p_DistanceInt + "m";

        PlayerPrefs.SetInt("HighScore", p_HighScore);
        PlayerPrefs.Save();
    }
    #endregion

    /// <summary>
    /// Call this to load the player data
    /// </summary>
    public void LoadData()
    {
        p_DistanceInt = PlayerPrefs.GetInt("CurrentDistance");
        p_TimerScore = PlayerPrefs.GetInt("Time");
    }
}
