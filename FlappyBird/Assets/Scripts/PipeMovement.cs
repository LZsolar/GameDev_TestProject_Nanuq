using UnityEngine;
using System.Collections;

public class PipeMovement : MonoBehaviour
{
    float moveSpeed;
    private void OnEnable()
    {
        moveSpeed = GameManager.Instance.PipeSpeed;
        GameManager.OnGameStateChanged += stopOnGameEnd;
    }
    void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }
    private void stopOnGameEnd(GameState state)
    {
        if (state == GameState.End) { moveSpeed = 0; }
    }
}
