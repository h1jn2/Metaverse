using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SugarManager SugarManager;
    private bool isMoving = false;

    private Rigidbody2D rb;

    void Start()
    {
        SugarManager = GameObject.FindGameObjectWithTag("SugarManager").GetComponent<SugarManager>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isMoving = true;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;
        }
        else
        {
            if (isMoving)
            {
                SugarManager.GameOver();
            }
            isMoving = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        SugarManager.GameOver();
    }
}
