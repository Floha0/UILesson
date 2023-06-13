using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private List<GameObject> targets;
    void Start()
    {
        StartCoroutine(SpawnTarget(1f));
        UpdateScore(0);
    }

    IEnumerator SpawnTarget(float spawnRate)
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int i)
    {
        score += i;
        scoreText.text = "Score: " + score;
    }
}
