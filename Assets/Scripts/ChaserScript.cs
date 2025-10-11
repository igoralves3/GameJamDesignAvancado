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

    private const float minSpeed= 5f;
    private const float maxSpeed=15f;

    private const float swimSpeed = 2f;

    public float speed = minSpeed;

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

        gameObject.layer = LayerMask.NameToLayer("Chaser");

        player = GameObject.FindGameObjectWithTag("Player");

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!swimming)
        {
            
            var distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance > 8f)
            {
                speed = maxSpeed;
            }
            else
            {
                speed = minSpeed;
            }
        }

        if (player.transform.position.x > transform.position.x)
        {
            transform.position -= Vector3.left * speed * Time.deltaTime;
        }
        else if (player.transform.position.x < transform.position.x)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        int layerMask = ~(1 << LayerMask.NameToLayer("Chaser")); // Exclude the "Player" layer

        Vector3 direction = (player.transform.position-transform.position);
        direction.Normalize();
        RaycastHit2D hit = Physics2D.Raycast(transform.position,direction,Mathf.Infinity,layerMask);


        if (hit)
        {
            Debug.Log(hit.collider.gameObject.tag);
            if (hit.collider.gameObject.tag == "Ground" || hit.collider.gameObject.tag == "Enemy")
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

                if (distance < 7f)
                {
                    var deltaY = 0f;
                    if (!swimming)
                    {
                        deltaY = 2f;
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

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Ground")
        {
            if (col.transform.position.y + col.transform.localScale.y < transform.position.y + transform.localScale.y)
            {
                canJump = true;
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
            rb.AddForce(transform.up * jumpHeight,ForceMode2D.Impulse);
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


}
