using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] Animator anim;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        
        if (other.gameObject.tag == "Player")
        {
            anim.SetBool("Open", true);
        }
    }
}
