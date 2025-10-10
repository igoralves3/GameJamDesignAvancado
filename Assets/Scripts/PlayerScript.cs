using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

    public Rigidbody2D rb;

    public Camera camera;

    private float speed = 5f;

    private bool canJump = false;
    private float jumpHeight = 10f;

    // Start is called before the first frame update
    void Start()
    {
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

        if (Input.GetKey("space"))
        {
            Jump();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Ground")
        {
            if (col.transform.position.y < transform.position.y)
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
                default:
                    break;

            }
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
