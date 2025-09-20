using UnityEngine;
using System.Collections;

public class PipeSpawner : MonoBehaviour
{
    public GameObject Pipe;
    public float gapSize = 3.0f;
    public float spawnInterval;
    public float destroyDistance = 10.0f;

    private void Awake()
    {
        GameManager.OnGameStateChanged += HandleGameState;
    }

    private void HandleGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start: startPipeSpawing(); return;
            case GameState.End: stopPipeSpawing(); return;
            default: DestroyPipe(); return;
        }
    }
    private void startPipeSpawing() {
        spawnInterval = GameManager.Instance.PipeSpawnInterval;
        StartCoroutine(SpawnPipe()); 
    }
    private void stopPipeSpawing() { StopAllCoroutines(); }

    private IEnumerator SpawnPipe()
    {
        while (true)
        {
            Vector3 spawnPos = transform.position + new Vector3(0, Random.Range(-gapSize, gapSize), 0);
            GameObject pipe = Instantiate(Pipe, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void DestroyPipe()
    {
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");

        foreach (GameObject pipe in pipes)
        {
            Destroy(pipe);
        }
    }
}
