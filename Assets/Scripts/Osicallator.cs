using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 endPosition;
    [SerializeField]Vector3 movementVector;
    [SerializeField]float speed;
    float movementFactor;

    private void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + movementVector;
    }
    private void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor);
         
    }
}
