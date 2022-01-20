using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ball test = collision.attachedRigidbody.gameObject.GetComponent<Ball>();
        Debug.Log("Found ball to destroy called " + test.name);
        Destroy(test);

        Ball[] remainingBalls = FindObjectsOfType<Ball>();
        if (remainingBalls.Length <= 1)
        {
            Debug.Log("No balls left");
            GameOver();
        }
        else
        {
            Debug.Log(remainingBalls.Length + " remaining");
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
