
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SwimmingEnemy : MonoBehaviour
{

    private Rigidbody2D rb;

    private float speed = -1f;

    private float frame;
    private float dirDuration;

    public SpriteRenderer spr;
    public Sprite[] sprites;
    private int spriteFrame = 0;
    private int spriteIndex = 0;

    private float dir = -1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        spr = GetComponent<SpriteRenderer>();

        dir = 1f;

        frame = 0;
        dirDuration=Random.Range(5, 10);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        frame += Time.deltaTime;
        if (frame >= dirDuration)
        {
            frame = 0;
            dirDuration = Random.Range(5,10);
            speed = -speed;

            dir = speed;
        }
        transform.position += Vector3.left * speed * Time.deltaTime;
        UpdateSprite();
    }

    void UpdateSprite()
    {

        spriteFrame++;
        if (spriteFrame >= 10)
        {
            spriteFrame = 0;
            spriteIndex++;
            if (spriteIndex > 1)
            {
                spriteIndex = 0;
            }
        }



        spr.sprite = sprites[spriteIndex];


        if (speed > 0)
        {
            spr.flipX = false;
        }
        else if (speed < 0)
        {
            spr.flipX = true;
        }
    }
}
