using UnityEngine;
using Unity.Netcode;

public class BulletMove : NetworkBehaviour
{
    public float speed = 10.0f;
    public  float lifeTime = 5f;

    public int PLAYER_DAMAGE = 10;
    
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

    void OnCollisionEnter(Collision collision)
    {
        if(IsServer){
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.SendMessage("ApplyDamage", PLAYER_DAMAGE);
                DestroyBullet();
            }
        }
    }
}
