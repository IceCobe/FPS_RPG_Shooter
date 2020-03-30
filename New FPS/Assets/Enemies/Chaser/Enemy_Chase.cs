 using UnityEngine;
 
 public class Enemy_Chase: MonoBehaviour
 {
    public Rigidbody rb;
    private Vector3 direction;
    private Vector3 flatten;
    private Quaternion lookRotation;
    GameObject targ;

    public bool isStunned = false;
    public float speed = 5f;

    void Start() {
        rb = this.GetComponent<Rigidbody>();
        targ = GameObject.FindGameObjectWithTag("Player");
    }
    void FixedUpdate()
    {
        if (isStunned) {
            Invoke("RemoveStun", .25f);
        } else {
            // Variable Gathering
            direction = (targ.transform.position - transform.position).normalized;
            lookRotation = Quaternion.LookRotation(direction);
            
            // rigidbody moving
            flatten = transform.position + (direction * speed * Time.deltaTime);
            rb.MovePosition(new Vector3(flatten.x,transform.position.y,flatten.z));
            
            //rigidbody rotation
            Quaternion newRot = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2.5f );
            rb.MoveRotation(newRot);
            
        }
     }

    void RemoveStun() {
        isStunned = false;
    }
 }