using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    private GameManager gameManagersc;

    // UI
    [Header("UI")]
    public TextMeshProUGUI scoreText;

    
    public TextMeshProUGUI gameOverText;

    [SerializeField]
    private TextMeshProUGUI startText;

    [SerializeField]
    private GameObject difficulty;
    
    void Start()
    {
        gameManagersc = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
                StartCoroutine(gameManagersc.SpawnTarget(3f)); 
                break;
            case "Medium":
                StartCoroutine(gameManagersc.SpawnTarget(1.5f)); 
                break;
            case "Hard":
                StartCoroutine(gameManagersc.SpawnTarget(0.5f)); 
                break;
            default:
                StartCoroutine(gameManagersc.SpawnTarget(2f)); 
                break;
        }

        difficulty.SetActive(false);
        scoreText.gameObject.SetActive(true);
    }
}
