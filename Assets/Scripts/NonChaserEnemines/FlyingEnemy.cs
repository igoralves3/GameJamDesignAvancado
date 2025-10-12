
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{

    private float side = -1f;

    private float speed = 1f;

    private Rigidbody2D rb;

    private float currentFramesForProjectile;
    private float framesForProjectile;

    private float currentFramesForChangeDir;
    private float framesForChangeDir;

    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentFramesForProjectile = 0;

        framesForProjectile = Random.Range(2,5);

        currentFramesForChangeDir = 0;

        framesForChangeDir = Random.Range(4, 10);
    }

    // Update is called once per frame
    void Update()
    {
        currentFramesForProjectile += Time.deltaTime;

        currentFramesForChangeDir += Time.deltaTime;
        if (currentFramesForProjectile >= framesForProjectile)
        {
            DropProjectile();
        }

        transform.position += Vector3.right * side * speed * Time.deltaTime;
        if (currentFramesForChangeDir >= framesForChangeDir)
        {
            currentFramesForChangeDir = 0;
            framesForChangeDir = Random.Range(4, 10);

            side = -side;
        }
    }

    void DropProjectile()
    {
        currentFramesForProjectile = 0;
        framesForProjectile = Random.Range(2, 5);

        var newX = transform.position.x;
        var newY = transform.position.y-0.5f;
        var newZ = transform.position.z;



        Instantiate(projectile, new Vector3(newX,newY,newZ), Quaternion.identity);
    }
}
