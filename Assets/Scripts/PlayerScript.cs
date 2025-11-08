using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using TMPro;
using System.Runtime.InteropServices;

public class PlayerScript : MonoBehaviour
{
    private bool swimming = false;

    public Rigidbody2D rb;
    public BoxCollider2D bc;

    public Camera camera;

    private float speed = 5f;
    private const float swimSpeed = 2f;

    private bool canJump = false;
    public float jumpHeight = 10f;

    private bool canSwim = true;

    public static int lifes = 3;

    public Canvas canvas;
    public Text text;

    public float moveSpeed = 5f;

    public SpriteRenderer spr;

    private float dir = 1f;
    public PhysicsMaterial2D pm;

    public bool collectedPoo = false;

    private RigidbodyType2D tipoOriginal;

    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();

        if (scene.name == "Stage3")
        {
           // rb.gravityScale = 1f;

            swimming = true;
            speed = swimSpeed;

            //bc.sharedMaterial = pm;
        }

        tipoOriginal = rb.bodyType;


        camera = GetComponent<Camera>();
        spr = GetComponent<SpriteRenderer>();

        dir = 1f;
    }

    // Update is called once per frame
    void Update()
    { 
    
    }


    void FixedUpdate()
    {
        if (collectedPoo)
        {
            StartCoroutine(CollectedPoo());
            return;
        }

        if (Input.GetKey("left"))
        {
            //transform.position += Vector3.left * speed * Time.deltaTime;

            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            dir = -1f;
        }
        else if (Input.GetKey("right"))
        {
            //transform.position -= Vector3.left * speed * Time.deltaTime;

            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            dir = 1f;
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        if (Input.GetKey("space"))
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
            UpdateUI();

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Ground")
        {
            if (col.transform.position.y + col.transform.lossyScale.y < transform.position.y + transform.lossyScale.y)
            {
                if (Mathf.Abs(transform.position.x - col.transform.position.x) <= col.transform.lossyScale.x) {
                    canJump = true;
                }
            }
        }
        if (col.gameObject.tag == "Chaser")
        {
            CaughtByChaser();
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {

        if (col.gameObject.tag == "Ground")
        {
            
                canJump = false;
            
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if (col.gameObject.tag == "Exit")
        {
            StageClear();
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
        if (canSwim) {
            if (rb.velocity.y < 5f) {
                canSwim = false;
                rb.AddForce(transform.up * 5f, ForceMode2D.Impulse);
            }
        }
    }

    void StageClear()
    {
        Scene scene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene("Stage0");

        switch (scene.name)
        {
            case "Stage0":
                SceneManager.LoadScene("Stage1");
                break;
            case "Stage1":
                SceneManager.LoadScene("Stage2");
                break;
            case "Stage2":
                SceneManager.LoadScene("Stage3");
                break;
            case "Stage3":
                SceneManager.LoadScene("Stage4");
                break;
            case "Stage4":
                SceneManager.LoadScene("Stage5");
                break;
            case "Stage5":
                SceneManager.LoadScene("Stage6");
                break;

            case "Stage6":
                SceneManager.LoadScene("VictoryScreen");
                break;
            default:
                break;

        }
    }

    void CaughtByChaser()
    {
        if (lifes == 1) {

            SceneManager.LoadScene("GameOverScreen");
        }
        else
        {
            lifes--;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void UpdateUI()
    {
        text.text =("Lifes : " + lifes.ToString());
    }

    void UpdateSprite()
    {
        if (dir > 0)
        {
            spr.flipX = false;
        }else if (dir < 0)
        {
            spr.flipX = true;
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

}
