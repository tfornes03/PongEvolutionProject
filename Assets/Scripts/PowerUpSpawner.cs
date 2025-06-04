using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUps;
    public float spawnInterval = 3f;
    public Vector3 spawnMin;
    public Vector3 spawnMax;
    private Coroutine spawnRoutine;
    private bool gameOver = false;

    void Start()
    {
        spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (powerUps.Length == 0) continue;

            GameObject prefab = null;
            int safety = 10;

            while (prefab == null && safety-- > 0)
            {
                int index = Random.Range(0, powerUps.Length);
                prefab = powerUps[index];
            }

            if (prefab == null) continue;

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

        gameOver = false;
        spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        gameOver = true;

        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }
    }
}
