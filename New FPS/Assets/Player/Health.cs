using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{   
    public HealthBarScript healthbar;
    public float max_health = 100f;
    public float current_health = 100f;
    
    void Start()
    {
        // Set the current_health to max_health
        current_health = max_health;

        // Tell healthbar to set it's max health
        healthbar.SetMaxHealth(max_health);
    }

    void Update()
    {
        // Tell healthbar to match the health here
        healthbar.SetHealth(current_health);

        // If I die then reload the room
        if (current_health <= 0) {
            SceneManager.LoadScene("First_Room");
        }

        // If I fall off the map reload the room
        if (transform.position.y <= -100) {
            SceneManager.LoadScene("First_Room");
        } 
    }

    // Take damage script
    void TakeDamage(float damage) {
        current_health -= damage;
    }

    // If an enemy collides with me destroy it and then take damage
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
