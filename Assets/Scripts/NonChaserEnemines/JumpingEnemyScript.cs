using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemyScript : MonoBehaviour
{

    private Rigidbody2D rb;

    private bool canJump = false;
    private float jumpHeight = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Jump();
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Ground")
        {
            canJump = true;
        }

    }

    void OnCollisionExit2D(Collision2D col)
    {

        if (col.gameObject.tag == "Ground")
        {
            canJump = false;
        }

    }

    void Jump()
    {
        if (canJump)
        {
            canJump = false;
            rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
        }
    }
}
