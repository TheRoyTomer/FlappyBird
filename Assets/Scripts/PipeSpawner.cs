using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pipePairPrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float minY = -1f;
    [SerializeField] private float maxY = 1f;

    private float timer;

    private void Update()
    {
        if (GameManagerScript.Instance.IsGameOver)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnPipe();
            timer = 0f;
        }
    }

    private void SpawnPipe()
    {
        float randomY = Random.Range(minY, maxY);

        Instantiate(
            pipePairPrefab,
            new Vector3(transform.position.x, randomY, 0f),
            Quaternion.identity
        );
    }
}