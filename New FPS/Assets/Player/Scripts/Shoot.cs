using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Animator animator;
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
            animator.SetBool("Anim_isReloading", true);
        } else if(isShooting) {
            animator.SetBool("Anim_isShooting", true);
        }
        
        // Logic for when I can shoot
        if (cur_ammo <= max_ammo && isReloading == false && (Input.GetKeyDown(KeyCode.R) || cur_ammo <= 0)) {
            recoveryspeed = 10f;
            animator.SetBool("Anim_isReloading", true);
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
        animator.SetBool("Anim_isReloading", false);
    }

    // Shooting implementation
    void shoot() {
        if (Input.GetMouseButtonDown(0)) {

            animator.SetBool("Anim_isShooting", true);
            
            isShooting = true;
            recoveryspeed = 60f;
            
            RaycastHit hit;
            Vector3 direction = emitter.transform.forward;

            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit)) {
                Vector3 Target = hit.point;
                direction = (hit.point - emitter.transform.position).normalized;
            }

            GameObject instBullet = Instantiate(bullet, emitter.transform.position, emitter.transform.rotation);
            Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
            
            instBulletRigidbody.AddForce(direction * speed);
            cur_ammo--;
        }
            Invoke("doneShooting",.25f);
    }

    void doneShooting() {
        isShooting = false;
        animator.SetBool("Anim_isShooting", false);
    }
}