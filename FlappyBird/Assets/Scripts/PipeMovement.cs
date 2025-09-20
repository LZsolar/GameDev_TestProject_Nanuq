using UnityEngine;
using System.Collections;

public class PipeMovement : MonoBehaviour
{
    float moveSpeed;
    private void OnEnable()
    {
        GameManager.OnGameStateChanged += stopOnGameEnd;
    }
    void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    public void setMoveSpeed(float newMoveSpeed) { moveSpeed = newMoveSpeed; }
    private void stopOnGameEnd(GameState state)
    {
        if (state == GameState.End) { moveSpeed = 0; }
    }
}
