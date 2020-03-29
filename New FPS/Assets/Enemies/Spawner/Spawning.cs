using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public GameObject enemy;
    public Round_Script round_Script;
    public int round = 0;

    void Update()
    {
        // Spawn an enemy
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
            round_Script.changetext("Round " + round);
            for (int i = 0; i < round; i++) {
                Vector3 position = new Vector3(Random.Range(-50, 50), 5, Random.Range(-50, 50));
                Instantiate(enemy, position, Quaternion.identity);
            }
            round++;
        }
    }
}
