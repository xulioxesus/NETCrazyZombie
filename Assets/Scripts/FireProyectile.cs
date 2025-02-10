using System;
using UnityEngine;

public class Fireproyectile : MonoBehaviour
{
    [SerializeField] GameObject proyectile;
    [SerializeField] float delay;
        
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject clone = Instantiate(proyectile, transform.position, transform.rotation);

            Destroy(clone, delay);
        }
    }
}
