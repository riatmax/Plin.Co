using UnityEngine;

public class Basket : MonoBehaviour
{
    public int pointsToAdd;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided");
        GameManager.GM.totalPoints += pointsToAdd;
    }
}
