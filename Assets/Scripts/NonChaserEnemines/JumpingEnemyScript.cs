using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class JumpingEnemyScript : MonoBehaviour
{

    private Rigidbody2D rb;

    private bool canJump = false;
    private float jumpHeight = 10f;

    public AudioClip clip;

    public Transform playerTransform; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        var p= GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            playerTransform = p.transform;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Jump();
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Ground")
        {
            canJump = true;
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
            if (Vector3.Distance(transform.position,playerTransform.position) < 10) {
                SoundFXManager.instance.PlaySoundFXClip(clip, transform, 1f);
            }
            rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
        }
    }
}
