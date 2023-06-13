using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;

    [SerializeField]
    private float minSpeed = 12f;

    [SerializeField]
    private float maxSpeed = 16f;
    private float speed;
    private float zRange = 8f;
    private Vector3 spawnPos;

    [SerializeField]
    private float torquePower = 10f;
    private Vector3 torqueVector;

    [SerializeField]
    private string targetType;
    private GameManager gameManagercs;

    [SerializeField]
    private ParticleSystem explosionParticle;

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
        gameManagercs = GameObject.Find("GameManager").GetComponent<GameManager>();

        targetRb.AddForce(Vector3.up * speed, ForceMode.Impulse);
        targetRb.AddTorque(torqueVector, ForceMode.Impulse);
        transform.position = spawnPos;
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -10)
        {
            switch (targetType)
            {
                case "Heal":
                    gameManagercs.UpdateScore(-1);
                    break;
            }

            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        switch (targetType)
        {
            case "Heal":
                gameManagercs.UpdateScore(1);
                break;
            case "Bomb":
                gameManagercs.UpdateScore(-1);
                break;
            case "Random":
                if (Random.Range(0, 2) == 0)
                {
                    gameManagercs.UpdateScore(-1);
                }
                else
                {
                    gameManagercs.UpdateScore(1);
                }
                break;
        }

        Destroy(gameObject);    
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
    }
}
