using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class MNovement : MonoBehaviour
{
    [SerializeField] float mainThrustStrength;
    [SerializeField] InputAction mainThrust;
    [SerializeField] float rotationStrength;
    [SerializeField] InputAction Rotation;
    [SerializeField] ParticleSystem mainThrustEffect;
    [SerializeField] ParticleSystem leftThrustEffect;
    [SerializeField] ParticleSystem rightThrustEffect;
    AudioSource audioSource;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        mainThrust.Enable();
        Rotation.Enable();
    }

    private void FixedUpdate()
    {
        MainThrusting();
        RotationThrust();
    }
    
    private void MainThrusting()
    {
        if (mainThrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * mainThrustStrength * Time.fixedDeltaTime);
 
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            if(!mainThrustEffect.isPlaying)
            {
                mainThrustEffect.Play();
            }
        }
        else
        {
            audioSource.Stop();
            mainThrustEffect.Stop();
        }    

        
    }
    private void RotationThrust()
    {
        float rotationvalue = Rotation.ReadValue<float>();
        rb.freezeRotation = true;
        if (rotationvalue < 0)
        {
            transform.Rotate(Vector3.forward* rotationStrength * Time.fixedDeltaTime);
            if (!leftThrustEffect.isPlaying)
            {
                rightThrustEffect.Stop();
                leftThrustEffect.Play();
            }
        }
        else if (rotationvalue > 0)
        {
            transform.Rotate(-Vector3.forward*rotationStrength*Time.fixedDeltaTime);
            if (!rightThrustEffect.isPlaying)
            {
                leftThrustEffect.Stop(); 
                rightThrustEffect.Play();
            }

        }
        else
        {
            leftThrustEffect.Stop();
            rightThrustEffect.Stop();
        }
            rb.freezeRotation = false;
    }

    
}
