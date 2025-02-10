using Unity.VisualScripting;
using UnityEngine;

public class PowerUpApply : MonoBehaviour
{
    const int POWER = 50;

    [SerializeField] AudioClip clip;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SendMessage("ApplyDamage", -POWER);

            AudioSource.PlayClipAtPoint(clip, transform.position);
            
            Destroy(gameObject);
        }
    }
}
