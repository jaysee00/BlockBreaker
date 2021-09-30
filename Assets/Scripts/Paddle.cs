using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private int minX = 1;
    private int maxX = 15;

    [SerializeField] private float screenWidthInUnits;

    // Update is called once per frame
    void Update()
    {
        float mousePosX = Input.mousePosition.x / Screen.width * this.screenWidthInUnits;
        Vector2 paddlePos = new Vector2(transform.position.y, transform.position.y);
        paddlePos.x = Mathf.Clamp(mousePosX, minX, maxX);
        transform.position = paddlePos;    
    }
}
