using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private GameManager gameManager;
    private bool isMoving = false;
    private Vector2 initialPosition;
    private int checkpointCount = 0;

    private Rigidbody2D rb;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            initialPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMoving = true;
        }

        if (isMoving)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;

            if (checkpointCount >= 4 && IsBackToStart())
            {
                Debug.Log(checkpointCount);
                Debug.Log(transform.position);
                gameManager.GameClear();
            }
            else
            {
                if (isMoving)
                {
                    gameManager.GameOver();
                }

                isMoving = false;

            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            checkpointCount++;
        }
        else
        {
            gameManager.GameOver();
    }
}

    bool IsBackToStart()
    {
        return Vector2.Distance(initialPosition, transform.position) < 1f;
    }
}
