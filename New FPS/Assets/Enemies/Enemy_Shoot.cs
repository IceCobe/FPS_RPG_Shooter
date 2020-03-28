using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot : MonoBehaviour
{

    public GameObject bullet;
    public float speed = 100f;
    private float nextActionTime = 0.0f;
    public float period = 0.1f;
    public GameObject emitter;
    
    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime ) {
        nextActionTime += period;
        GameObject instBullet = Instantiate(bullet, transform.position , transform.rotation) as GameObject;
        Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
           instBulletRigidbody.AddForce(emitter.transform.forward * speed);
         // execute block of code here
        }
    }
}