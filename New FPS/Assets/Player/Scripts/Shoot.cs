using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Animator animator;
    public Camera fpsCam;
    public GameObject bullet;
    public GameObject emitter;
    public GameObject meleeobject;
    public Ammo_Count ammo_counter;
    public AudioSource gun_shot;
    public AudioSource reload_sound;
    public AudioSource melee_sound;
    public ParticleSystem muzzle_flash;

    public float speed = 100f;
    public float max_ammo = 8f;
    public float cur_ammo = 8f;
    public float reload_time = 1f;
    public float fire_rate = .1f;
    public float spread = .05f;

    bool isReloading = false;
    bool isShooting = false;
    bool isMelee = false;

    void Update()
    {          
        // Logic for when I can shoot
        if (cur_ammo < max_ammo && isReloading == false && (Input.GetKeyDown(KeyCode.R) || !isShooting && cur_ammo <= 0 && isReloading == false)) {
            StartCoroutine(reload_now());
        } else if (!isReloading && !isMelee && !isShooting) {
            StartCoroutine(shoot());
            StartCoroutine(melee());
        } else if (!isReloading && !isMelee ) {
            StartCoroutine(melee());
        }

        // Update the ammo
        ammo_counter.changetext(cur_ammo+"/"+max_ammo);
    }

    // Reloading implementation
    IEnumerator reload_now() {
        isReloading = true;
        animator.SetBool("Anim_isReloading", true);
        
        yield return new WaitForSeconds(reload_time*.33f);
        reload_sound.Play();
        yield return new WaitForSeconds(reload_time*.66f);

        cur_ammo = max_ammo;
        isReloading = false;
        animator.SetBool("Anim_isReloading", false);
    }

    // Melee implementation
    IEnumerator melee() {
        if (Input.GetKeyDown(KeyCode.V)) {
            isMelee = true;
            animator.Play("melee");

            yield return new WaitForSeconds(13f/60f);

            melee_sound.Play();

            yield return new WaitForSeconds(25f/60f-13f/60f);

            meleeobject.SetActive(true);

            yield return new WaitForSeconds(1f/60f);

            meleeobject.SetActive(false);

            yield return new WaitForSeconds(.66f-25f/60f-1f/60f);

            isMelee = false;
        }
        
    }

    // Shooting implementation
    IEnumerator shoot() {
        if (Input.GetMouseButton(0)) {
            muzzle_flash.Play();
            animator.Play("shoot");
            gun_shot.Play();
            isShooting = true;
            
            RaycastHit hit;
            Vector3 direction = emitter.transform.forward;

            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit)) {
                Vector3 Target = hit.point;
                direction = (hit.point - emitter.transform.position).normalized;
                direction.x += Random.Range(-spread, spread);
                direction.y += Random.Range(-spread, spread);
            }

            GameObject instBullet = Instantiate(bullet, emitter.transform.position, Quaternion.LookRotation(direction));
            Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();

            instBulletRigidbody.AddForce(direction * speed);
            cur_ammo--;

            yield return new WaitForSeconds(fire_rate);

            isShooting = false;
        }
    }
}