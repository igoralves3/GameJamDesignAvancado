
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float timeLeft = 20f;
    private float dir = 0;

    private float pos=-1f;
    private float maxPos = 1f;


    // Start is called before the first frame update
    void Start()
    {
        timeLeft = Random.Range(10f, 20f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            Destroy(this.gameObject);
        }
        pos += Time.deltaTime;
        if (pos >= 1) {
            pos = -1;
        }

        var dir = new Vector3(pos,1f,0f);

        transform.position += dir*Time.deltaTime;
    }
}
