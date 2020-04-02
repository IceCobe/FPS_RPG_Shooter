 using UnityEngine;
 
 public class Enemy_Chase: MonoBehaviour
 {
    public Rigidbody rb;
    public Target self;
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
        if (!self.isDead) {
            // Flatten the Vector3
            flatten = transform.position + (direction * speed * Time.deltaTime);
            direction = (targ.transform.position - transform.position).normalized;
            Vector3 flat_dir = new Vector3(flatten.x,transform.position.y,flatten.z);
            
            // Variable Calculation
            lookRotation = Quaternion.LookRotation(direction);
            Quaternion newRot = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2.5f );
            
            // Rotation and position movemnet
            rb.MovePosition(flat_dir);
            rb.MoveRotation(newRot);
            
        }
     }
 }