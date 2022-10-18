using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    #region Singleton
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }
    #endregion

    [Header("Shop variables")]
    [Tooltip("The x amount of coins you have")]
    [SerializeField] private int Coins;
    [SerializeField] private TMPro.TextMeshProUGUI p_CoinText;
    [Tooltip("The different cars")]
    [SerializeField] private GameObject p_CarRed, p_CarGreen, p_CarBlue, p_CarYellow, p_CarBlack;
    [Tooltip("The current car")]
    [SerializeField] private GameObject p_CurrentCar;

    // If it is shown yes or no
    private bool p_Shown;
    // Checks if its already bought
    private bool p_AlreadyBoughtRed, p_AlreadyBoughtGreen, p_AlreadyBoughtBlue, p_AlreadyBoughtYellow, p_AlreadyBoughtBlack;

    private void Start()
    {
        Coins = PlayerPrefs.GetInt("Coin");
        //Changes the cointext on startup
        p_CoinText.text = Coins.ToString();
    }

    #region Coin Changer
    /// <summary>
    /// Will change the coin amount when is called on
    /// Will also change the text when is called on
    /// </summary>
    /// <param name="p_Change"> The amount the coins int will change when it is called on </param>
    public void CoinsChanger(int p_Change)
    {
        Coins = Coins + p_Change;
        p_CoinText.text = Coins.ToString();
        PlayerPrefs.SetInt("Coin", Coins);
    }
    #endregion

    #region CarChange
    public void ButtonDetect(string name)
    {
        if (Coins < 40) return;

        if (name == "Red")
        {
            if (p_AlreadyBoughtRed == false && PlayerPrefs.GetInt("CarRed") == 0)
            {
                Coins -= 40;
                p_CoinText.text = Coins.ToString();
                p_AlreadyBoughtRed = true;
                PlayerPrefs.SetInt("Coin", Coins);
            }
            if (p_CurrentCar == p_CarRed == false)
            {
                p_CarRed.SetActive(true);
                p_CarRed.transform.position = p_CurrentCar.transform.position;
                p_CurrentCar.SetActive(false);
                p_CurrentCar = p_CarRed;
                PlayerPrefs.SetInt("CarRed", 1);
                PlayerPrefs.Save();
            }
        }
        else if (name == "Green")
        {
            if (p_AlreadyBoughtGreen == false && PlayerPrefs.GetInt("CarGreen") == 0)
            {
                Coins -= 40;
                p_CoinText.text = Coins.ToString();
                p_AlreadyBoughtGreen = true;
                PlayerPrefs.SetInt("Coin", Coins);
            }
            if (p_CurrentCar == p_CarGreen == false)
            {
                p_CarGreen.SetActive(true);
                p_CarGreen.transform.position = p_CurrentCar.transform.position;
                p_CurrentCar.SetActive(false);
                p_CurrentCar = p_CarGreen;
                PlayerPrefs.SetInt("CarGreen", 1);
                PlayerPrefs.Save();
            }
        }
        else if (name == "Yellow")
        {
            if (p_AlreadyBoughtYellow == false && PlayerPrefs.GetInt("CarYellow") == 0)
            {
                Coins -= 40;
                p_CoinText.text = Coins.ToString();
                p_AlreadyBoughtYellow = true;
                PlayerPrefs.SetInt("Coin", Coins);
            }
            if (p_CurrentCar == p_CarYellow == false)
            {
                p_CarYellow.SetActive(true);
                p_CarYellow.transform.position = p_CurrentCar.transform.position;
                p_CurrentCar.SetActive(false);
                p_CurrentCar = p_CarYellow;
                PlayerPrefs.SetInt("CarYellow", 1);
                PlayerPrefs.Save();
            }
        }
        else if (name == "Blue")
        {
            if (p_AlreadyBoughtBlue == false && PlayerPrefs.GetInt("CarBlue") == 0)
            {
                Coins -= 40;
                p_CoinText.text = Coins.ToString();
                p_AlreadyBoughtBlue = true;
                PlayerPrefs.SetInt("Coin", Coins);
            }
            if (p_CurrentCar == p_CarBlue == false)
            {
                p_CarBlue.SetActive(true);
                p_CarBlue.transform.position = p_CurrentCar.transform.position;
                p_CurrentCar.SetActive(false);
                p_CurrentCar = p_CarBlue;
                PlayerPrefs.SetInt("CarBlue", 1);
                PlayerPrefs.Save();
            }
        }
        else if (name == "Black")
        {
            if (p_AlreadyBoughtBlack == false && PlayerPrefs.GetInt("CarBlack") == 0)
            {
                Coins -= 40;
                p_CoinText.text = Coins.ToString();
                p_AlreadyBoughtBlack = true;
                PlayerPrefs.SetInt("Coin", Coins);
            }
            if (p_CurrentCar == p_CarBlack == false)
            {
                p_CarBlack.SetActive(true);
                p_CarBlack.transform.position = p_CurrentCar.transform.position;
                p_CurrentCar.SetActive(false);
                p_CurrentCar = p_CarBlack;
                PlayerPrefs.SetInt("CarBlack", 1);
                PlayerPrefs.Save();
            }
        }
        else return;

    }
    #endregion
}
