using UnityEngine;

public class UISpawnBallInBounds2D : MonoBehaviour
{
    [Header("References")]
    public SimplePool2D pool;
    public Collider2D spawnBounds;

    public void DropBall()
    {
        if (pool == null || spawnBounds == null) return;
        if (GameManager.GM == null) return;

        if (!GameManager.GM.TryRegisterBallDrop()) return;

        Bounds b = spawnBounds.bounds;

        Vector2 spawnPos = new Vector2(
            Random.Range(b.min.x, b.max.x),
            Random.Range(b.min.y, b.max.y)
        );

        GameObject ballObj = pool.Get(spawnPos);

        PooledBall ball = ballObj.GetComponent<PooledBall>();
        if (ball != null)
        {
            ball.pool = pool;
            GameManager.GM.RegisterBallSpawned(ball);
        }
    }
}
