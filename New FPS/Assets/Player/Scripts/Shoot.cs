using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public Camera fpsCam;
    public GameObject bullet;
    public GameObject emitter;
    public Ammo_Count ammo_counter;

    public float speed = 100f;
    public float max_ammo = 8f;
    public float cur_ammo = 8f;
    public float reload_time = 1f;

    bool isReloading = false;
    bool isShooting = false;
    float recoveryspeed = 10f;

    void Update()
    {   
        // tried to implement reloading animation
        if (isReloading) {
            emitter.transform.localPosition = Vector3.Lerp(emitter.transform.localPosition,new Vector3(1.426f, -1.44f, 2.554f), 10f*Time.deltaTime);
        } else if(isShooting) {
            emitter.transform.localPosition = Vector3.Lerp(emitter.transform.localPosition,new Vector3(1.426f, -0.44f, 2.4f), 30f*Time.deltaTime);
        } else {
            emitter.transform.localPosition = Vector3.Lerp(emitter.transform.localPosition,new Vector3(1.426f, -0.44f, 2.554f), recoveryspeed*Time.deltaTime);
        }
        
        // Logic for when I can shoot
        if (cur_ammo <= max_ammo && isReloading == false && (Input.GetKeyDown(KeyCode.R) || cur_ammo <= 0)) {
            isReloading = true;
            recoveryspeed = 10f;
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
    void shoot() {
        if (Input.GetMouseButtonDown(0)) {
            isShooting = true;
            recoveryspeed = 60f;
            
            RaycastHit hit;

            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit)) {
                Vector3 Target = hit.point;
            }

            // create the rotation we need to be in to look at the target
            GameObject instBullet = Instantiate(bullet, emitter.transform.position , emitter.transform.rotation);
            Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
            Vector3 direction = (hit.point - emitter.transform.position).normalized;
            instBulletRigidbody.AddForce(direction * speed);
            cur_ammo--;
        }
            Invoke("doneShooting",.25f);
    }

    void doneShooting() {
        isShooting = false;
    }
}