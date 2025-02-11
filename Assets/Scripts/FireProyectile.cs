using System;
using UnityEngine;
using Unity.Netcode;

public class Fireproyectile : NetworkBehaviour
{
    [SerializeField] GameObject proyectile;
    [SerializeField] float delay;
        
    void Update()
    {
        if(!IsOwner) return;
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject clone = Instantiate(proyectile, transform.position, transform.rotation);

            Destroy(clone, delay);
        }
    }
}
