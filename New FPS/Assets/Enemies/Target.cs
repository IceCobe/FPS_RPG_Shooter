using UnityEngine;

public class Target : MonoBehaviour
{
    public Animator animator;
    public float health = 50f;
    public float damage = 30f;
    public void TakeDamage(float amount) {
        health -= amount;
        if (health <= 0f) {
            Die();
        }
    }

    void Die() {
        animator.SetBool("IsDead", true);
        Invoke("Actual_Death", .5f);
    }
    void Start() {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (transform.position.y <= -10) {
            Die();
        }
    }

    void Actual_Death() {
        Destroy(gameObject);
    }
}
