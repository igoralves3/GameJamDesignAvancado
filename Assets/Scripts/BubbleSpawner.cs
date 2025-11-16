
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public float minX, maxX,minY,maxY;
    public GameObject bubble;

    public float timeLeft = 1f;

    // Start is called before the first frame update
    void Start()
    {
        minX = transform.position.x - 13.5f;
        maxX = transform.position.x + 13.5f;

        minY = transform.position.y + 1f;
        maxY = transform.position.y + 5f;

        timeLeft = Random.Range(5f, 10f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            timeLeft = Random.Range(1f,2f);
            var x = Random.Range(minX, maxX);
            var y = Random.Range(minY, maxY);
            var b = Instantiate(bubble,new Vector3(x,y,0f), Quaternion.identity);
        }
    }
}
