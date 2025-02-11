using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class PlayerDamage : NetworkBehaviour
{
    const int MAX_LIFE = 100;
    [SerializeField] Text txtHealth;
    
    public NetworkVariable<int> health = new NetworkVariable<int>(MAX_LIFE);
    
    public override void OnNetworkSpawn()
    {
        ApplyDamage(0);
    }

    void ApplyDamage(int damage)
    {
        if(!IsOwner) return;

        if (health.Value > 0)
        {
            health.Value -= damage;
            txtHealth.text = health.ToString();
        }        
    }
}
