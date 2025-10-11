using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private bool swimming = false;

    public Rigidbody2D rb;

    public Camera camera;

    private float speed = 5f;
    private const float swimSpeed = 2f;

    private bool canJump = false;
    private float jumpHeight = 10f;

    private bool canSwim = true;

    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Stage3")
        {
            swimming = true;
            speed = swimSpeed;
        }


        rb = GetComponent<Rigidbody2D>();

        camera = GetComponent<Camera>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("left"))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }else if (Input.GetKey("right"))
        {
            transform.position -= Vector3.left * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown("space"))
        {
            if (!swimming) {
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
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Ground")
        {
            if (col.transform.position.y+col.transform.localScale.y < transform.position.y + transform.localScale.y)
            {
                canJump = true;
            }
        }
        if (col.gameObject.tag == "Chaser")
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
            rb.AddForce(transform.up * jumpHeight,ForceMode2D.Impulse);
        }
    }

    void Swim()
    {
        if (rb.velocity.y < 5f) {
            rb.AddForce(transform.up * 5f, ForceMode2D.Impulse);
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
                //SceneManager.LoadScene("Stage4");
                break;

            default:
                break;

        }
    }

}
