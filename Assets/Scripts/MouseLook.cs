using System;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    const float CLAMP_MIN = -45.0f;
    const float CLAMP_MAX = 45.0f;

    [SerializeField] float lookSensitivity;
    GameObject player;
    Vector2 rotation = Vector2.zero;
    Vector2 smoothRot = Vector2.zero;
    Vector2 velRot = Vector2.zero;

    void Start()
    {
        player = transform.parent.gameObject;    
    }

    void Update()
    {
        // giro alrededor del eje Y
        player.transform.RotateAround(transform.position, Vector3.up, 
            Input.GetAxis("Mouse X") * lookSensitivity);

        // giro up/down de la cámara/caberza
        rotation.y += Input.GetAxis("Mouse Y");
        rotation.y = Mathf.Clamp(rotation.y, CLAMP_MIN, CLAMP_MAX);
        smoothRot.y = Mathf.SmoothDamp(smoothRot.y, rotation.y, ref velRot.y, 0.1f);
        transform.localEulerAngles = new Vector3(-smoothRot.y, 0, 0);
    }
}
