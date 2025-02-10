using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    const int HITS_TO_DIE = 3;
    int hitCount;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hitCount++;

            if (hitCount == HITS_TO_DIE)
            {
                Destroy(gameObject);
            }
        }
    }
}
