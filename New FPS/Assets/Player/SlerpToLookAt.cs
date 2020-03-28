using UnityEngine;
 
 public class SlerpToLookAt: MonoBehaviour
 {
     //values that will be set in the Inspector
     Transform Target;
     public Camera fpsCam;
     public float RotationSpeed;
 
     //values for internal use
     private Quaternion _lookRotation;
     private Vector3 _direction;
     
     // Update is called once per frame 
     void Update()
     {
        RaycastHit hit;

        //find the vector pointing from our position to the target
        _direction = fpsCam.transform.forward;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit)) {

            Vector3 Target = hit.point;
            //find the vector pointing from our position to the target
            _direction = (Target - transform.position).normalized;
        }
        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(_direction);
        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
     }
 }