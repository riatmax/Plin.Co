using UnityEngine;

public class Basket : MonoBehaviour
{
    public int pointsToAdd;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Only react to pooled balls
        PooledBall ball = collision.GetComponent<PooledBall>();
        if (ball == null) return;

        Debug.Log("Ball landed in basket: " + pointsToAdd);

        if (GameManager.GM != null)
        {
            GameManager.GM.RegisterBallLanded(pointsToAdd);
        }

        ball.ReturnToPool();
    }
}
