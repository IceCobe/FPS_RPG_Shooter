using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float max_health = 100f;
    public float current_health = 100f;
    public HealthBarScript healthbar;
    // Start is called before the first frame update
    void Start()
    {
        current_health = max_health;
        healthbar.SetMaxHealth(max_health);
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.SetHealth(current_health);

        if (current_health <= 0) {
            SceneManager.LoadScene("First_Room");
        }

        if (transform.position.y <= -10) {
            SceneManager.LoadScene("First_Room");
        } 
    }

    void TakeDamage(float damage) {
        current_health -= damage;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Enemy" ) {
            Target enemy = other.GetComponent<Target>();
            if (enemy != null) {
                Destroy(other.gameObject);
                TakeDamage(enemy.damage);
            }
        }
    }
}
