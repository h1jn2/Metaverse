using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private GameManager gameManager;
    private bool isMoving = false;
    private bool gameStarted = false;

    private Vector2 startPosition;
    private float passedCheckpointsCnt = 0;

    private Rigidbody2D rb;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    void Update()
    {
        if (gameManager.isStart)
        {
            if (Input.GetMouseButton(0))
            {
                if (!gameStarted)
                {
                    startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    gameStarted = true;
                }

                isMoving = true;
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = mousePosition;

                Debug.Log("Passed Checkpoints: " + passedCheckpointsCnt);
            }
            else
            {
                if (isMoving)
                {
                    gameManager.GameOver();
                }

                isMoving = false;
            }

            if (gameStarted && passedCheckpointsCnt >= 4 && Vector2.Distance(transform.position, startPosition) < 15f)
            {
                gameManager.GameClear();
            }
        }
    }
 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            passedCheckpointsCnt++;
        }

        if (other.CompareTag("Wall"))
        {
            gameManager.GameOver();
        }
        
    }
}
