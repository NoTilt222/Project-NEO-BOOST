using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FuelSystem : MonoBehaviour
{   
    public float CurrentFuel = 100f;
    public TextMeshProUGUI DisplayFuel;
    [SerializeField] public Slider fuelIndicator;
    public float baseInterval = 1f;
    public float Counter;

    [SerializeField] float FuelExpenditure = 1f;
    [SerializeField] AudioClip EmptyfuelWarningSound;

    bool RocketIsMoving;
  
    AudioSource FuelAudioSource;
    public static FuelSystem tfs;
    // Start is called before the first frame update
    void Start()
    {   
        tfs= this;
        FuelAudioSource = GetComponent<AudioSource>();
        Counter = baseInterval;
    }

    // Update is called once per frame
    void Update()
    {    
       RocketFuel();
       EmptyFuel();
    }
    public void setRocketThrust(bool YesOrNo)
    {
        RocketIsMoving = YesOrNo;
    }

    public void RocketFuel()
    {    
        fuelIndicator.value= CurrentFuel;
         if(RocketIsMoving)
        {
            if(Counter > 0)
            {
                Counter -= Time.deltaTime;
            }
            else
            {
                Counter = baseInterval;
                CurrentFuel-= FuelExpenditure;
            }
        }
        DisplayFuel.text = "Fuel: " + CurrentFuel + "/100";
        
        
    }

     public void EmptyFuel()
    {
         if(CurrentFuel<= 0)
        {    
             DisplayFuel.text = "Fuel: 0/100";
             Movement.mfs.MainEngineParticles.Stop();
             GetComponent<Movement>().enabled = false;
        }
    }
}
   