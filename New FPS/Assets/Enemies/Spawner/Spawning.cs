using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public GameObject enemy;
    public Round_Script round_Script;
    public int round = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
