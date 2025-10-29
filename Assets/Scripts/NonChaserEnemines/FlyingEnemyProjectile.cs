using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Chaser" || col.gameObject.tag == "Player" || col.gameObject.tag == "Projectile")
        {
            Destroy(this.gameObject);
        }

    }
}
