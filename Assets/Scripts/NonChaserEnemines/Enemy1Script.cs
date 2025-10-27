using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Enemy1Script : MonoBehaviour
{

    private float side = -1f;

    private float speed = 1f;



    // Start is called before the first frame update
    void Start()
    {
        side = -1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += side * Vector3.right * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Ground")
        {
            if (side < 0) {
                if (col.transform.position.x < transform.position.x)
                {
                    side = -side;
                }
            }
            else
            {
                if (col.transform.position.x > transform.position.x)
                {
                    side = -side;
                }
            }
        }
       
    }
}
