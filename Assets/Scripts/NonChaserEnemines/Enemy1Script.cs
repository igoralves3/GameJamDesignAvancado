using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Enemy1Script : MonoBehaviour
{

    private float side = -1f;

    private float speed = 1f;

    public SpriteRenderer spr;

    public AudioClip clip;

    public Sprite[] sprites;
    private int spriteFrame = 0;
    private int spriteIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        side = -1f;
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += side * Vector3.right * speed * Time.deltaTime;
        UpdateSprite();
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Chaser" || col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy")
        {
            if (col.gameObject.tag == "Player")
            {
                SoundFXManager.instance.PlaySoundFXClip(clip, transform, 1f);
            }
            if (side < 0) {
                if (col.transform.position.x < transform.position.x)
                {
                 
                    side = -side;
                }
            }
            else if (side > 0)
            {
                if (col.transform.position.x > transform.position.x)
                {
                    
                    side = -side;
                }
            }
        }
       
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



        if (side < 0)
        {
            spr.flipX = false;
        }else if (side > 0)
        {
            spr.flipX = true;
        }
    }
}
