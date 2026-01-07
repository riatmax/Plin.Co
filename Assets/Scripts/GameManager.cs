using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    [Header("Scoring")]
    public int totalPoints;
    public int roundPoints;

    [Header("Round Settings")]
    public int ballsPerRound = 3;

    [Header("Resolve Timing")]
    public float roundResolveDelay = 1.0f;

    public int ballsLeftToDrop { get; private set; }
    public int ballsInPlay { get; private set; }
    public bool isResolving { get; private set; }

    // Track balls currently spawned this round
    private readonly List<PooledBall> activeBalls = new List<PooledBall>();

    private void Awake()
    {
        if (GM == null)
        {
            GM = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        totalPoints = 0;
        BeginRound();
    }

    public void BeginRound()
    {
        roundPoints = 0;
        ballsLeftToDrop = ballsPerRound;
        ballsInPlay = 0;
        isResolving = false;
        activeBalls.Clear();
    }

    public bool TryRegisterBallDrop()
    {
        if (isResolving) return false;
        if (ballsLeftToDrop <= 0) return false;

        ballsLeftToDrop--;
        ballsInPlay++;
        return true;
    }

    // Call this right after you spawn a ball from the pool
    public void RegisterBallSpawned(PooledBall ball)
    {
        if (ball == null) return;
        if (!activeBalls.Contains(ball)) activeBalls.Add(ball);
    }

    // Called by the ball when it returns to pool
    public void RegisterBallReturned(PooledBall ball)
    {
        if (ball == null) return;
        activeBalls.Remove(ball);
    }

    // Called when a ball lands in a bucket
    public void RegisterBallLanded(int bucketPoints)
    {
        roundPoints += bucketPoints;              // updates after each ball
        ballsInPlay = Mathf.Max(0, ballsInPlay - 1);

        if (ballsLeftToDrop == 0 && ballsInPlay == 0 && !isResolving)
        {
            StartCoroutine(ResolveRound());
        }
    }

    private IEnumerator ResolveRound()
    {
        isResolving = true;

        // Force-remove any balls that might still be active for any reason
        for (int i = activeBalls.Count - 1; i >= 0; i--)
        {
            if (activeBalls[i] != null)
                activeBalls[i].ReturnToPool();
        }

        // Let the player see the final roundPoints for a moment
        if (roundResolveDelay > 0f)
            yield return new WaitForSeconds(roundResolveDelay);

        totalPoints += roundPoints;

        BeginRound();
    }
}
