using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    const int MAX_LIFE = 100;
    [SerializeField] Text txtHealth;
    
    int health = MAX_LIFE;
    
    void Start()
    {
        ApplyDamage(0);
    }

    void ApplyDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            txtHealth.text = health.ToString();
        }        
    }
}
