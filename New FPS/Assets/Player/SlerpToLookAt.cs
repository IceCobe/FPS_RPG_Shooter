using UnityEngine;
 
 public class SlerpToLookAt: MonoBehaviour
 {
    public Camera fpsCam;
    Transform Target;

    public float RotationSpeed;
    private Quaternion _lookRotation;
    private Vector3 _direction;

    void Update()
    {
        RaycastHit hit;

        // find the vector pointing from our position to the target
        _direction = fpsCam.transform.forward;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit)) {
            Vector3 Target = hit.point;

            // find the vector pointing from our position to the target
            _direction = (Target - transform.position).normalized;
        }

        // create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(_direction);

        // rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
     }
 }