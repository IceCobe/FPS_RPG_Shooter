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
    public float fire_rate = .1f;

    bool isReloading = false;
    bool isShooting = false;

    void Update()
    {          
        // Logic for when I can shoot
        if (cur_ammo <= max_ammo && isReloading == false && (Input.GetKeyDown(KeyCode.R) || cur_ammo <= 0)) {
            StartCoroutine(reload_now());
        } else if (isReloading == false && isShooting == false) {
            StartCoroutine(shoot());
        }

        // Update the ammo
        ammo_counter.changetext(cur_ammo+"/"+max_ammo);
    }

    // Reloading implementation
    IEnumerator reload_now() {
        isReloading = true;
        animator.SetBool("Anim_isReloading", true);

        yield return new WaitForSeconds(reload_time);

        cur_ammo = max_ammo;
        isReloading = false;
        animator.SetBool("Anim_isReloading", false);
    }

    // Shooting implementation
    IEnumerator shoot() {
        if (Input.GetMouseButton(0)) {
            isShooting = true;
            animator.SetBool("Anim_isShooting", true);
            
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

            yield return new WaitForSeconds(fire_rate);

            isShooting = false;
            animator.SetBool("Anim_isShooting", false);
        }
    }
}