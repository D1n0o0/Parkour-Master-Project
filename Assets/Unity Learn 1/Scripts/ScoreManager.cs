using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    Rigidbody rb;

    public int totalScore = 999;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            totalScore = CalculateScore(5);
        }
        if (collision.gameObject.CompareTag("projectile"))
        {
            totalScore = CalculateScore(10);
        }
    }

    public int CalculateScore(int inputScore)
    {
        totalScore -= inputScore;
        return totalScore;
    }
     
}
