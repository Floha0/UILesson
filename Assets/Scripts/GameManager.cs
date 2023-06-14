using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Manager")]
    [System.NonSerialized]
    public int score;

    [SerializeField]
    private string diff;

    private bool gameOver;
    

    // UI
    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI gameOverText;

    [SerializeField]
    private TextMeshProUGUI startText;

    [SerializeField]
    private GameObject difficulty;

    [Tooltip("The targets that will spawn.")]
    [SerializeField]
    private List<GameObject> targets;

    void Start()
    {
        UpdateScore(0);
    }

    IEnumerator SpawnTarget(float _spawnRate)
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
        scoreText.text = "Score: " + score;

        // Check if game is over
        if (score < 0)
        {
            gameOverText.gameObject.SetActive(true);
            gameOver = true;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        startText.gameObject.SetActive(false);
        difficulty.SetActive(true);      
    }

    public void SetDifficulty(string _diff)
    {
        switch (_diff)
        {
            case "Easy":
                StartCoroutine(SpawnTarget(3f)); 
                break;
            case "Medium":
                StartCoroutine(SpawnTarget(1.5f)); 
                break;
            case "Hard":
                StartCoroutine(SpawnTarget(0.5f)); 
                break;
            default:
                StartCoroutine(SpawnTarget(2f)); 
                break;
        }

        difficulty.SetActive(false);
        scoreText.gameObject.SetActive(true);
    }
}
