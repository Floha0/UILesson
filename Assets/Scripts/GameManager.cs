using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    public List<GameObject> targets;
    void Start()
    {
        StartCoroutine(SpawnTarget(1f));
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
