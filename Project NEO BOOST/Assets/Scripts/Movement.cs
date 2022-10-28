using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
   [SerializeField]  float mainThrust = 100f;
   [SerializeField] float rotationThrust = 100f;
   [SerializeField] public AudioClip mainEngine;
   
   [SerializeField] public ParticleSystem MainEngineParticles;
   [SerializeField] ParticleSystem LeftThrusterParticles;
   [SerializeField] ParticleSystem RightThrusterParticles;
    Rigidbody rb;
   public AudioSource audioSourceMovement;
   public static Movement mfs;
    // Start is called before the first frame update
    void Start()
    {  
       mfs = this;
       rb = GetComponent<Rigidbody>();
       audioSourceMovement = GetComponent<AudioSource>();
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
            StartThrusting();
            FuelSystem.tfs.setRocketThrust(true);
        }
        else
        {   
            StopThrusting();
            FuelSystem.tfs.setRocketThrust(false);
        }
    }
    void RocketRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }

    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSourceMovement.isPlaying)
        {
            audioSourceMovement.PlayOneShot(mainEngine);
        }
        if (!MainEngineParticles.isPlaying)
        {
            MainEngineParticles.Play();
        }
    }
    
    public void StopThrusting()
    {
        audioSourceMovement.Stop();
        MainEngineParticles.Stop();
    }
    void RotateLeft()
    {
        ApplyRotation(rotationThrust);

        if (!RightThrusterParticles.isPlaying)
        {
            RightThrusterParticles.Play();
        }
    }
    void RotateRight()
    {
        ApplyRotation(-rotationThrust);

        if (!LeftThrusterParticles.isPlaying)
        {
            LeftThrusterParticles.Play();
        }
    }

    void StopRotating()
    {
        RightThrusterParticles.Stop();
        LeftThrusterParticles.Stop();
    }


    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;  // Freezing rotation to manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation= false; // unfreezing rotation so physics system can take over
    }
    
}
