using UnityEngine;

public class Basket : MonoBehaviour
{
    public int pointsToAdd;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PooledBall ball = collision.GetComponent<PooledBall>();
        if (ball == null) return;

        if (GameManager.GM != null)
            GameManager.GM.RegisterBallLanded(pointsToAdd);

        ball.ReturnToPool();
    }
}
