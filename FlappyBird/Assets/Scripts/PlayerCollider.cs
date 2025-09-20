using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Pipe") && GameManager.Instance.currentGameState == GameState.Start)
        {
            GameManager.Instance.ToggleGameState(GameState.End);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Score")
        {
            ScoreManager.Instance.addScore();
        }
    }
}
