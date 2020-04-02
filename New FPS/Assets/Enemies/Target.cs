using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Animator animator;
    public float health = 50f;
    public float damage = 30f;
    public bool isDead = false;

    public void TakeDamage(float amount) {
        health -= amount;
        if (health <= 0f) {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die() {
        isDead = true;
        animator.SetBool("IsDead", true);

        yield return new WaitForSeconds(.5f);

        Destroy(gameObject);
    }
    void Start() {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (transform.position.y <= -10) {
            StartCoroutine(Die());
        }
    }
}
