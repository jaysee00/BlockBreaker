using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    private SceneLoader sceneLoader;

    // Config
    [SerializeField] private Text scoreText;
    [SerializeField] private int pointsPerBlock = 10;
    [SerializeField, Range(0.1f, 10f)] private float gameSpeed = 1f;
    [SerializeField] private AudioClip winSound;
    
    private int blocksRemaining = 0;
    private Ball ball;
    [SerializeField] private int score = 0; // Serialised for debugging purposes

    private void Awake()
    {
        Level[] otherLevels = FindObjectsOfType<Level>();
        int count = otherLevels.Length;
        if (count > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        ball = FindObjectOfType<Ball>();

        scoreText.text = score.ToString();
    }

    public void OnCreateBlock()
    {
        blocksRemaining++;
        Debug.Log("Registering Block in level: " + this.blocksRemaining);
    }

    public void OnBreakBlock()
    {
        score += pointsPerBlock;
        blocksRemaining--;
        Debug.Log("Block broken. Remaining: " + this.blocksRemaining);
        scoreText.text = score.ToString();
        if (blocksRemaining <= 0)
        {
            AudioSource.PlayClipAtPoint(winSound, Camera.main.transform.position);
            ball.Freeze();
            Debug.Log("You Won! Loading Next Level!");
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel() {
        yield return new WaitForSeconds(2);
        sceneLoader.LoadNextScene();
    }

    private void Update()
    {
        Time.timeScale = gameSpeed;
    }
}
