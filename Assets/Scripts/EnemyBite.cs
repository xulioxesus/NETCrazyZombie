using UnityEngine;

public class EnemyBite : MonoBehaviour
{
    
    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("ApplyDamage", 1);
        }
    }
    
}
