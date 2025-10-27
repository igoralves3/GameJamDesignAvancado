
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingEnemy2 : MonoBehaviour
{

    private Rigidbody2D rb;

    private float speed = 1f;

    private float frame;
    private float dirDuration;

    private bool canJump = false;

    private float jumpHeight = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        frame = 0;
        dirDuration = Random.Range(1, 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position += Vector3.left * speed * Time.deltaTime;
        frame += Time.deltaTime;
        if (frame >= dirDuration)
        {

            //rb.AddForce(transform.up * 10f, ForceMode2D.Impulse);

            canJump = true;

            frame = 0;
            dirDuration = Random.Range(1, 2);
            speed = -speed;
        }

        Jump();
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Ground")
        {
            if (col.transform.position.y + col.transform.localScale.y < transform.position.y + transform.localScale.y) {
                canJump = true;
            }
        }

    }

    void Jump()
    {
        if (canJump)
        {
            canJump = false;
            rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);

            frame = 0;
            dirDuration = Random.Range(1,2);
        }
    }
}
