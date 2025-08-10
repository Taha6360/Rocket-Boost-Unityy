using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float crashDelay = 2f;
    [SerializeField] float loadDelay = 2f;
    [SerializeField] AudioClip roundFinish;
    [SerializeField] AudioClip crashed;
    [SerializeField] ParticleSystem successEffect;
    [SerializeField] ParticleSystem crashEffect;

    AudioSource audioSource;

    bool isControlable = true;
    bool isCollidable = true;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }
    private void Update()
    {
        RespondToDebugKeys();
    }

    private void OnCollisionEnter(Collision collision)
    {
    
    
        if (!isControlable || !isCollidable)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":             
                break;
                            
            case "Finish":               
                nextLevelLoadDelay();              
                break;

            default:           
                CrashDelay();
                
                break;
        }
    }
    void CrashDelay()
    {
        isControlable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashed);
        crashEffect.Play();
        GetComponent<MNovement>().enabled = false;
        Invoke("reload", crashDelay);
    }
    void nextLevelLoadDelay()
    {
        isControlable = false;
        audioSource.Stop();
        
        audioSource.PlayOneShot(roundFinish);
        successEffect.Play();
        GetComponent<MNovement>().enabled = false;
        Invoke("nextLevel", loadDelay);
    }
    void reload()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
    void nextLevel()
    {        
       int currentScene = SceneManager.GetActiveScene().buildIndex ;
       int nextScene = currentScene + 1;
       if (nextScene == SceneManager.sceneCountInBuildSettings)
            {
                nextScene = 0;
            }
            SceneManager.LoadScene(nextScene);
    }
    void RespondToDebugKeys()
    {
        if(Keyboard.current.lKey.wasPressedThisFrame)
        {
            nextLevel();
        }
        else if(Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }
    }

}

