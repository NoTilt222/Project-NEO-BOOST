using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
   [SerializeField]  float mainThrust = 100f;
   [SerializeField] float rotationThrust = 100f;
    Rigidbody rb;
  
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {    
        RocketThrust();
        RocketRotation();
    }

    void RocketThrust()
    {
     if(Input.GetKey(KeyCode.Space))
     {
       rb.AddRelativeForce(Vector3.up * mainThrust* Time.deltaTime);
     }  
    }
    
    void RocketRotation()
    {
     if(Input.GetKey(KeyCode.A))
     {
        ApplyRotation(rotationThrust);
     }
    else if(Input.GetKey(KeyCode.D))
     {
        ApplyRotation(-rotationThrust);
     }
    
    }
       void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;  // Freezing rotation to manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation= false; // unfreezing rotation so physics system can take over
    }
}
