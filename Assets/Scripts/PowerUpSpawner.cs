using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUps;
    public float spawnInterval = 3f;
    public Vector3 spawnMin;
    public Vector3 spawnMax;
    private Coroutine spawnRoutine;

    void Start()
    {
       spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (powerUps.Length == 0) continue;

            // Loop safety: skip if prefab is null or destroyed
            GameObject prefab = null;
            int safety = 10; // Try up to 10 times to get a valid prefab
            while (prefab == null && safety-- > 0)
            {
                int index = Random.Range(0, powerUps.Length);
                prefab = powerUps[index];
            }

            if (prefab == null) continue; // No valid prefab found

            Vector3 spawnPos = new Vector3(
                Random.Range(spawnMin.x, spawnMax.x),
                0.5f,
                Random.Range(spawnMin.z, spawnMax.z)
            );

            Instantiate(prefab, spawnPos, Quaternion.identity);
        }
    }

    public void RestartSpawning()
    {
        if (spawnRoutine != null)
            StopCoroutine(spawnRoutine);

        spawnRoutine = StartCoroutine(SpawnRoutine());
    }

}
