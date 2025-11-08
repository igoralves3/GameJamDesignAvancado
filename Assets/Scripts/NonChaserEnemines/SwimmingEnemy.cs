
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SwimmingEnemy : MonoBehaviour
{

    private Rigidbody2D rb;

    private float speed = 1f;

    private float frame;
    private float dirDuration;

    public SpriteRenderer spr;

    private float dir = -1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        spr = GetComponent<SpriteRenderer>();

        dir = -1f;

        frame = 0;
        dirDuration=Random.Range(5, 10);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        frame += Time.deltaTime;
        if (frame >= dirDuration)
        {
            frame = 0;
            dirDuration = Random.Range(5,10);
            speed = -speed;

            dir = speed;
        }
        UpdateSprite();
    }

    void UpdateSprite()
    {
        if (speed > 0)
        {
            spr.flipX = true;
        }
        else if (speed < 0)
        {
            spr.flipX = false;
        }
    }
}
