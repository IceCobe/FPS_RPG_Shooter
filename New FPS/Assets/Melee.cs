using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public float damage = 20f;
    public float knockback = 8000f;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Enemy" ) {
            Target enemy = other.GetComponent<Target>();
            Enemy_Chase en = other.GetComponent<Enemy_Chase>();
            if (enemy != null) {
                en.isStunned = true;
                enemy.TakeDamage(damage);
                Rigidbody enemybody = other.gameObject.GetComponent<Rigidbody>();
                enemybody.AddForce(this.transform.forward * knockback);
            }
        }
    }
}
