
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{

    private float side = -1f;

    private float speed = 1f;

    private Rigidbody2D rb;

    private float currentFramesForProjectile;
    private float framesForProjectile;

    private float currentFramesForChangeDir;
    private float framesForChangeDir;

    public GameObject projectile;

    public SpriteRenderer spr;

    public Sprite[] sprites;
    private int spriteFrame = 0;
    private int spriteIndex = 0;

    private float dir = -1f;

    public AudioClip clip;

    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentFramesForProjectile = 0;

        framesForProjectile = Random.Range(2,5);

        currentFramesForChangeDir = 0;

        framesForChangeDir = Random.Range(4, 10);

        spr = GetComponent<SpriteRenderer>();

        dir = -1f;

        var p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            playerTransform = p.transform;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentFramesForProjectile += Time.deltaTime;

        currentFramesForChangeDir += Time.deltaTime;
        if (currentFramesForProjectile >= framesForProjectile)
        {
            DropProjectile();
        }

        transform.position += Vector3.right * side * speed * Time.deltaTime;
        if (currentFramesForChangeDir >= framesForChangeDir)
        {
            currentFramesForChangeDir = 0;
            framesForChangeDir = Random.Range(4, 10);

            side = -side;
            dir = side;
        }

        UpdateSprite();
    }

    void DropProjectile()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) < 10)
        {
            SoundFXManager.instance.PlaySoundFXClip(clip, transform, 1f);
        }
        currentFramesForProjectile = 0;
        framesForProjectile = Random.Range(2, 5);

        var newX = transform.position.x;
        var newY = transform.position.y-0.5f;
        var newZ = transform.position.z;



        Instantiate(projectile, new Vector3(newX,newY,newZ), Quaternion.identity);
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

        if (dir > 0)
        {
            spr.flipX = true;
        }
        else if (dir < 0)
        {
            spr.flipX = false;
        }
    }
}
