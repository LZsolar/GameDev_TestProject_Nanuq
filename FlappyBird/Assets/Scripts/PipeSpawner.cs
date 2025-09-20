using UnityEngine;
using System.Collections;

public class PipeSpawner : MonoBehaviour
{
    public GameObject upPipe;
    public GameObject downPipe;
    public float gapSize = 3.0f;
    public float spawnInterval = 2.0f;
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
            default: return;
        }
    }
    private void startPipeSpawing() { StartCoroutine(SpawnPipe()); }
    private void stopPipeSpawing() { StopAllCoroutines(); }

    private IEnumerator SpawnPipe()
    {
        while (true)
        {
            float randomY = Random.Range(-3f, 3.5f);
            Vector3 downPipePosition = transform.position + new Vector3(0, -gapSize + randomY, 0);
            Instantiate(downPipe, downPipePosition, Quaternion.identity);
            GameObject topPipe = Instantiate(upPipe, transform.position + new Vector3(0, gapSize + randomY, 0), Quaternion.identity);
            topPipe.transform.localScale = new Vector3(1f, -1f, 1f);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
