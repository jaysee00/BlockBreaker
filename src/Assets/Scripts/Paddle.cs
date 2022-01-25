using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private int minX = 1;
    private int maxX = 15;

    [SerializeField] private float screenWidthInUnits;

    // Cached references
    private Level level;
    private Ball ball;


    private void Start()
    {
        level = FindObjectOfType<Level>();
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePos = new Vector2(transform.position.y, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform.position = paddlePos;    
    }

    private float GetXPos()
    {
        if (level.IsAutoPlayEnabled())
        {
            // Auto controlled
            return ball.transform.position.x;
        }
        else
        {
            // Player controlled
            return Input.mousePosition.x / Screen.width * this.screenWidthInUnits;
        }
    }
}
