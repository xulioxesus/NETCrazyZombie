using UnityEngine;
using Unity.Netcode;

public class ZombieBite : NetworkBehaviour
{
    
    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(IsServer){
                other.gameObject.SendMessage("ApplyDamage", 1);
            }
        }
    }
    
}
