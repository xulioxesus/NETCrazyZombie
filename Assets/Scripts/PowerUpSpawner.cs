using System.Collections;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject prefab;
    [SerializeField] Transform[] spawnPoints;

    [Header("Settings")]
    [SerializeField] float delay;

    GameObject powerUp;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            if (powerUp == null)
            {
                yield return new WaitForSeconds(delay);
                
                Vector3 position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                
                powerUp = Instantiate(prefab, position, Quaternion.identity);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
