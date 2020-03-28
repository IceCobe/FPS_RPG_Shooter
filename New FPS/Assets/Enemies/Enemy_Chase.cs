 using UnityEngine;
 
 public class Enemy_Chase: MonoBehaviour
 {
     GameObject targ;
     public bool isStunned = false;
     public float speed = 5f;
     public Rigidbody rb;
     // Update is called once per frame
     void Start() {
        targ = GameObject.FindGameObjectWithTag("Player");
     }
     void Update()
     {
        if (isStunned) {
            rb.isKinematic = false;
            rb.detectCollisions = true;
        } else {
            Vector3 direction = (targ.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f );
            transform.position = Vector3.MoveTowards(transform.position, targ.transform.position, speed * Time.deltaTime);
        }
     }
 }