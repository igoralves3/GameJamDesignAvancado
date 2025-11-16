using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using UnityEngine;
using UnityEngine.SceneManagement;

public class ChaserScript : MonoBehaviour
{
    private bool swimming = false;
    public GameObject player;

    public Rigidbody2D rb;
    public BoxCollider2D bc;

    private const float minSpeed= 5f;
    private const float maxSpeed=15f;

    private const float swimSpeed = 2f;

    public float speed = minSpeed;

    private bool canJump = false;
    private float jumpHeight = 10f;

    private bool canSwim = true;

    public float moveSpeed = 5f;

    public bool stopped = false;
    public int stoppedFrames = 0;

    public float tempoParado = 2f;
    private RigidbodyType2D tipoOriginal;

    public SpriteRenderer spr;

    public Sprite[] sprites;
    private int spriteFrame = 0;
    private int spriteIndex = 0;

    private float dir = 1f;

    public bool collectedPoo = false;

    private bool gettingReady;
    private int readyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Stage3")
        {
            swimming = true;
            speed = swimSpeed;
        }

        gameObject.layer = LayerMask.NameToLayer("Chaser");

        player = GameObject.FindGameObjectWithTag("Player");

        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();

        tipoOriginal = rb.bodyType;

        stopped = false;

        spr = GetComponent<SpriteRenderer>();

        dir = 1f;

        gettingReady = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (stopped)
        {
            StartCoroutine(PararTemporariamente());
            return;
        }
        if (collectedPoo)
        {
            StartCoroutine(CollectedPoo());
            return;
        }

        if (gettingReady)
        {
            readyCount++;
            if (readyCount >= 60)
            {
                gettingReady = false;
            }
            return;
        }

        if (!swimming)
        {
            
            var distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance > 7.5f)
            {
                moveSpeed = maxSpeed;
            }
            else
            {
                moveSpeed = minSpeed;
            }
        }

        if (player.transform.position.x > transform.position.x)
        {
            //transform.position -= Vector3.left * speed * Time.deltaTime;

            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            dir = 1f;
        }
        else if (player.transform.position.x < transform.position.x)
        {
            //transform.position += Vector3.left * speed * Time.deltaTime;

            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            dir = -1f;
        }

        int layerMask = ~(1 << LayerMask.NameToLayer("Chaser")); // Exclude the "Player" layer

        Vector3 direction = (player.transform.position-transform.position);
        direction.Normalize();
        RaycastHit2D hit = Physics2D.Raycast(transform.position,direction,Mathf.Infinity,layerMask);


        if (hit)
        {
            Debug.Log(hit.collider.gameObject.tag);
            if (hit.collider.gameObject.tag == "Ground" || hit.collider.gameObject.tag == "Enemy" || hit.collider.gameObject.tag == "Projectile")
            {
                var distance = Math.Abs(transform.position.x - hit.collider.transform.position.x);// Vector3.Distance(transform.position, hit.collider.transform.position);

                if (distance < 7.5f)
                {
                    if (!swimming)
                    {
                        Jump();
                    }
                    else
                    {
                        Swim();
                    }
                }
            }
            else if(hit.collider.gameObject.tag == "Player")
            {
                var distance = Math.Abs(transform.position.x- hit.collider.transform.position.x);// Vector3.Distance(transform.position, hit.collider.transform.position);

                if (distance < 7.5f)
                {
                    var deltaY = 0f;
                    if (!swimming)
                    {
                        deltaY = 0.5f;
                    }


                    if (player.transform.position.y > transform.position.y + deltaY)
                    {
                        if (!swimming)
                        {
                            Jump();
                        }
                        else
                        {
                            Swim();
                        }
                    }
                }
            }
        }

        if (swimming)
        {
            if (rb.velocity.y <= 0)
            {
                canSwim = true;
            }
        }
        else
        {
            if (rb.velocity.y == 0)
            {
                canJump = true;
            }
        }
        UpdateSprite();
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Ground")
        {
            if (col.transform.position.y + col.transform.localScale.y < transform.position.y + transform.localScale.y)
            {
                if (Mathf.Abs(transform.position.x - col.transform.position.x) <= col.transform.lossyScale.x)
                {
                    canJump = true;
                }
                
            }
        }
        if (col.gameObject.tag == "Player")
        {

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
            //rb.AddForce(transform.up * jumpHeight,ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }

    void Swim()
    {
        if (canSwim)
        {
            if (rb.velocity.y < 5f)
            {
                canSwim = false;
                rb.AddForce(transform.up * 5f, ForceMode2D.Impulse);
            }
        }
    }

    public IEnumerator CollectedPoo()
    {
        // 1️⃣ Desativa o movimento e colisões
        rb.bodyType = RigidbodyType2D.Static; // congela totalmente o corpo
        //bc.enabled = false; // desativa colisões

        // 2️⃣ Espera o tempo desejado
        yield return new WaitForSeconds(1);

        // 3️⃣ Reativa física e colisões
        rb.bodyType = tipoOriginal;
        //bc.enabled = true;

        collectedPoo = false;
    }

   public  IEnumerator PararTemporariamente()
    {
        // 1️⃣ Desativa o movimento e colisões
        rb.bodyType = RigidbodyType2D.Static; // congela totalmente o corpo
        bc.enabled = false; // desativa colisões

        // 2️⃣ Espera o tempo desejado
        yield return new WaitForSeconds(tempoParado);

        // 3️⃣ Reativa física e colisões
        rb.bodyType = tipoOriginal;
        bc.enabled = true;

        stopped = false;
    }

    void UpdateSprite()
    {
        if (rb.velocity.x != 0)
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
        }
        else
        {
            spriteFrame = 0;
        }

        spr.sprite = sprites[spriteIndex];

        if (dir > 0)
        {
            spr.flipX = false;
        }
        else if (dir < 0)
        {
            spr.flipX = true;
        }
    }

}
