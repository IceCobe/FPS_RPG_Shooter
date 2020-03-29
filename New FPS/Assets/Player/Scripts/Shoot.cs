using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public GameObject bullet;
    public GameObject emitter;
    public Ammo_Count ammo_counter;

    public float speed = 100f;
    public float max_ammo = 8f;
    public float cur_ammo = 8f;
    public float reload_time = 1f;

    bool isReloading = false;    

    void Update()
    {   
        // tried to implement reloading animation
        if (isReloading) {
            emitter.transform.localPosition = Vector3.Lerp(emitter.transform.localPosition,new Vector3(1.426f, -1.44f, 2.554f), 10f*Time.deltaTime);
        } else {
            emitter.transform.localPosition = Vector3.Lerp(emitter.transform.localPosition,new Vector3(1.426f, -0.44f, 2.554f), 10f*Time.deltaTime);
        }
        
        // Logic for when I can shoot
        if (cur_ammo <= max_ammo && isReloading == false && (Input.GetKeyDown(KeyCode.R) || cur_ammo <= 0)) {
            isReloading = true;
            Invoke("reload_now", reload_time);
        } else if (isReloading == false) {
            shoot();
        }

        // Update the ammo
        ammo_counter.changetext(cur_ammo+"/"+max_ammo);
    }

    // Reloading implementation
    void reload_now() {
        cur_ammo = max_ammo;
        isReloading = false;
    }

    // Shooting implementation
    void shoot(){
        if (Input.GetMouseButtonDown(0)) {
            GameObject instBullet = Instantiate(bullet, transform.position , transform.rotation) as GameObject;
            Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
            instBulletRigidbody.AddForce(emitter.transform.forward * speed);
            cur_ammo--;
        }
    }
}