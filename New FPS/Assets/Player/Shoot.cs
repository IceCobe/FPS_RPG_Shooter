using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public GameObject bullet;
    public float speed = 100f;

    public GameObject emitter;

    public float max_ammo = 8f;
    public float cur_ammo = 8f;

    public float reload_time = 1f;

    public Ammo_Count ammo_counter;
    bool isReloading = false;    
    // Update is called once per frame
    void Start() {
    }
    void Update()
    {   
        if (isReloading) {
            emitter.transform.localPosition = Vector3.Lerp(emitter.transform.localPosition,new Vector3(1.426f, -1.44f, 2.554f), 10f*Time.deltaTime);
        } else {
            // emitter.transform.localPosition = Vector3.Lerp(emitter.transform.localPosition,new Vector3(1.426f, -0.44f, 2.554f), 10f*Time.deltaTime);
        }
        
        if (cur_ammo <= max_ammo && isReloading == false && (Input.GetKeyDown(KeyCode.R) || cur_ammo <= 0)) {
            isReloading = true;
            Invoke("reload_now", reload_time);
        } else if (isReloading == false) {
            shoot();
        }

        ammo_counter.changetext(cur_ammo+"/"+max_ammo);
    }

    void reload_now() {
        cur_ammo = max_ammo;
        emitter.transform.localPosition = new Vector3(1.426f, -0.44f, 2.554f);
        isReloading = false;
    }

    void shoot(){
        if (Input.GetMouseButtonDown(0)) {
            GameObject instBullet = Instantiate(bullet, transform.position , transform.rotation) as GameObject;
            Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
            instBulletRigidbody.AddForce(emitter.transform.forward * speed);
            cur_ammo--;
        }
    }
}