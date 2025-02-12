using UnityEngine;
using Unity.Netcode;

public class ProyectileMovement : NetworkBehaviour
{
    public float speed = 10.0f;
    public  float lifeTime = 5f;
    
    private void Start()
    {
        if (IsServer) // Solo el servidor controla la destrucción
            Invoke(nameof(DestroyBullet), lifeTime);
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void DestroyBullet()
    {
        GetComponent<NetworkObject>().Despawn(); // Despawner en la red
        Destroy(gameObject);
    }
}
