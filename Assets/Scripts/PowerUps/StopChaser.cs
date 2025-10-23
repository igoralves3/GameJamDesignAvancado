using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class StopChaser : MonoBehaviour
{
    public GameObject chaser;

    // Start is called before the first frame update
    void Start()
    {
        chaser = GameObject.FindGameObjectsWithTag("Chaser")[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if (col.gameObject.tag == "Player")
        {
            ParaChaser();
        }
    }

    void ParaChaser()
    {
        var c = chaser.GetComponent<ChaserScript>();
        c.stopped = true;
        Destroy(this.gameObject);
    }
}
