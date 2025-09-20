using UnityEngine;
using System.Collections;

public class PipeSpawner : MonoBehaviour
{
    public GameObject Pipe;
    public float spawnRange;
    public float spawnInterval;
    public float pipemovespeed;

    private void Awake()
    {
        GameManager.OnGameStateChanged += HandleGameState;
        GameManager.OnGameDifficultyChanged += HandleDifficultyChange;
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

    private void HandleDifficultyChange(GameDifficulty data)
    {
        spawnRange = data.PipeSpawnRange;
        spawnInterval = data.PipeSpawnInterval;
        pipemovespeed = data.PipeSpeed;
    }


    private void startPipeSpawing() {
        StartCoroutine(SpawnPipe()); 
    }
    private void stopPipeSpawing() { StopAllCoroutines(); }

    private IEnumerator SpawnPipe()
    {
        while (true)
        {
            Vector3 spawnPos = transform.position + new Vector3(0, Random.Range(-spawnRange, spawnRange), 0);
            GameObject pipe = Instantiate(Pipe, spawnPos, Quaternion.identity);
            pipe.GetComponent<PipeMovement>().setMoveSpeed(pipemovespeed);
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
