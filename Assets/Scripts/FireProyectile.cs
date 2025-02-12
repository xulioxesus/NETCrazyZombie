using System;
using UnityEngine;
using Unity.Netcode;

public class Fireproyectile : NetworkBehaviour
{
    [SerializeField] GameObject proyectile;
        
    void Update()
    {
        if(!IsOwner) return;
        if (Input.GetButtonDown("Fire1"))
        {
            FireRpc();
        }
    }

    [Rpc(SendTo.Server)]
    void FireRpc(){
        GameObject bala = Instantiate(proyectile, transform.position, transform.rotation);
        bala.GetComponent<NetworkObject>().Spawn(true);
    }
}
