using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour
{
    // the trigger for the death of the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameState.Instance.YouLose();
            Debug.Log("EndGame: " + collision.gameObject.name + " " + gameObject.name);
            SceneManager.LoadScene("Lose_scene");
        }
    }
}
