using UnityEngine;
using Unity.Netcode;

public class EnemyDamage : NetworkBehaviour
{
    const int HITS_TO_DIE = 3;
    int hitCount;

    void OnCollisionEnter(Collision collision)
    {
        if(IsServer){
            if (collision.gameObject.CompareTag("Bullet"))
            {
                hitCount++;

                if (hitCount == HITS_TO_DIE)
                {
                    GetComponent<NetworkObject>().Despawn();
                    Destroy(gameObject);
                }
            }
        }
    }
}
