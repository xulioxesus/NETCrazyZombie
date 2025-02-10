using UnityEngine;

public class ProyectileMovement : MonoBehaviour
{
    const float SPEED = 10.0f;
    
    void Update()
    {
        transform.Translate(Vector3.forward * SPEED * Time.deltaTime);        
    }
}
