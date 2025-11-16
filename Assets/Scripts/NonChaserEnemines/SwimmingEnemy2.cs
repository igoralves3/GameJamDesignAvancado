
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

    public SpriteRenderer spr;
    public Sprite[] sprites;
    private int spriteFrame = 0;
    private int spriteIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();

        frame = 0;
        dirDuration = Random.Range(1, 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position += Vector3.left * speed * Time.deltaTime;
       // frame += Time.deltaTime;
       // if (frame >= dirDuration)
       // {

            //rb.AddForce(transform.up * 10f, ForceMode2D.Impulse);

         //   canJump = true;

          //  frame = 0;
         //   dirDuration = Random.Range(1, 2);
         //   speed = -speed;
       // }

        Jump();
        UpdateSprite();
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

    void UpdateSprite()
    {
        if (rb.velocity.y > 0)
        {

            spr.sprite = sprites[1];
        }
        else
        {
            spr.sprite = sprites[0];
        }
    }
}
