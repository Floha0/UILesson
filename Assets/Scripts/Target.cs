using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    public float minSpeed = 12f;
    public float maxSpeed = 16f;
    private float speed;
    private float zRange = 8f;
    private Vector3 spawnPos;
    public float torquePower = 10f;
    private Vector3 torqueVector;
    private GameObject gameManager;
    private string targetTag;

    void Start()
    {
        torqueVector = new Vector3(
            Random.Range(-torquePower, torquePower),
            Random.Range(-torquePower, torquePower),
            Random.Range(-torquePower, torquePower)
        );
        spawnPos = new Vector3(1, -6, Random.Range(-zRange, zRange));
        speed = Random.Range(minSpeed, maxSpeed);
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager");

        targetRb.AddForce(Vector3.up * speed, ForceMode.Impulse);
        targetRb.AddTorque(torqueVector, ForceMode.Impulse);
        transform.position = spawnPos;
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -10)
        {
            switch (tag)
            {
                case "Heal":
                    gameManager.GetComponent<GameManager>().score--;
                    break;
                case "Bomb":
                    gameManager.GetComponent<GameManager>().score++;
                    break;
            }
            Destroy(gameObject);
            Debug.Log(gameManager.GetComponent<GameManager>().score);
        }
    }

    private void OnMouseDown()
    {
        switch (tag)
        {
            case "Heal":
                gameManager.GetComponent<GameManager>().score++;
                break;
            case "Bomb":
                gameManager.GetComponent<GameManager>().score--;
                break;
            case "Random":
                if (Random.Range(0, 2) == 0)
                {
                    gameManager.GetComponent<GameManager>().score--;
                }
                else
                {
                    gameManager.GetComponent<GameManager>().score++;
                }
                break;
        }

        Destroy(gameObject);
        Debug.Log(gameManager.GetComponent<GameManager>().score);
    }
}
