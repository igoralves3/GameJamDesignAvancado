using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyProjectile : MonoBehaviour
{
    public GameObject chaser;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        chaser = GameObject.FindGameObjectsWithTag("Chaser")[0];
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Chaser" || col.gameObject.tag == "Player" || col.gameObject.tag == "Projectile")
        {
            if (col.gameObject.tag == "Chaser")
            {
                var c = chaser.GetComponent<ChaserScript>();
                c.collectedPoo = true;
            }
            if (col.gameObject.tag == "Player")
            {
                var p = player.GetComponent<PlayerScript>();
                p.collectedPoo = true;
            }
            Destroy(this.gameObject);
        }

    }
}
