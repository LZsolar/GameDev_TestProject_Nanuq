using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" && GameManager.Instance.currentGameState == GameState.Start)
        {
            GameManager.Instance.ToggleGameState(GameState.End);
        }
    }
}
