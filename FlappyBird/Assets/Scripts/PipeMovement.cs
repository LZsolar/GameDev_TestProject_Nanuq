using UnityEngine;
using System.Collections;

public class PipeMovement : MonoBehaviour
{
    float moveSpeed;
    private void OnEnable()
    {
        moveSpeed = GameManager.Instance.PipeSpeed;
        GameManager.OnGameStateChanged += destroyOnGameReset;
    }
    void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DestroyPoint") { Destroy(this); }
    }
    private void destroyOnGameReset(GameState state)
    {
        if (state == GameState.End) { moveSpeed = 0; }
        if (state == GameState.Waiting) { Destroy(this); }
    }
}
