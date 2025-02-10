using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    const float TIME = 1;
    float timer;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= TIME)
        {
            Destroy(gameObject);
        }
    }
}
