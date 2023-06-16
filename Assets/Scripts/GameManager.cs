using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Manager")]
    [System.NonSerialized]
    public int score;

    private bool gameOver;
    
    private GameUI gameUIsc;
    

    [Tooltip("The targets that will spawn.")]
    [SerializeField]
    private List<GameObject> targets;

    void Start()
    {
        gameUIsc = FindAnyObjectByType<GameUI>();
        UpdateScore(0);
    }

    public IEnumerator SpawnTarget(float _spawnRate)
    {
        // Spawn random target
        while (!gameOver)
        {
            yield return new WaitForSeconds(_spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int i)
    {
        // Manage score
        score += i;
        gameUIsc.scoreText.text = "Score: " + score;

        // Check if game is over
        if (score < 0)
        {
            gameUIsc.gameOverText.gameObject.SetActive(true);
            gameOver = true;
        }
    }

    
}
