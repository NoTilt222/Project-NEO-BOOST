using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float LoadLevelDelay = 1f;
    [SerializeField] AudioClip DeathSound;
    [SerializeField] AudioClip SuccessSound;
    
    [SerializeField] ParticleSystem DeathParticles;
    [SerializeField] ParticleSystem  SuccessParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;
    public static CollisionHandler cfs;
      void Start()
    {
       cfs = this;
       audioSource = GetComponent<AudioSource>();
    }
     void Update() 
    {
        DebugCheats();
    }
      void DebugCheats()
     {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
           collisionDisabled = !collisionDisabled; // toggle collision
        }
     }
     
    public void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || collisionDisabled){return;}
        
        switch(other.gameObject.tag)
        {
            case "Friendly":
            Debug.Log("This thing is friendly");
            break;
            case "Finish":
            StartSuccessSequence();
            break;
            case "powerup":
             Debug.Log("Power picked up");
             break;
            default:
            StartCrashSequence();
            break;
        }
    }
    
    void StartSuccessSequence()
    {      
           isTransitioning = true;
           SuccessParticles.Play();
           audioSource.Stop();
           audioSource.PlayOneShot(SuccessSound);
           GetComponent<Movement>().enabled = false;
           Invoke("LoadNextLevel", LoadLevelDelay);
           
    }
    void StartCrashSequence()
    {      
           isTransitioning= true;
           DeathParticles.Play();
           audioSource.Stop();
           audioSource.PlayOneShot(DeathSound);
           GetComponent<Movement>().enabled = false;
           Invoke("ReloadLevel", LoadLevelDelay);
           
    }
   void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
   void ReloadLevel()
   {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
   }
}
