using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;

    // Adjust velocity
    [Header("Velocity")]
    [SerializeField]
    private float minSpeed = 12f;

    [SerializeField]
    private float maxSpeed = 16f;
    private float speed;

    [SerializeField]
    private float torquePower = 10f;
    private Vector3 torqueVector;

    // Bounds
    private float zRange = 8f;
    private Vector3 spawnPos;

    // Manage Score
    [Header("Type")]
    [SerializeField]
    private string targetType;
    private GameManager gameManagercs;

    // VFX
    [Tooltip("The particle that will seen on clicked.")]
    [SerializeField]
    private ParticleSystem explosionParticle;

    void Start()
    {
        // set Variables

        torqueVector = new Vector3(
            Random.Range(-torquePower, torquePower),
            Random.Range(-torquePower, torquePower),
            Random.Range(-torquePower, torquePower)
        );
        spawnPos = new Vector3(1, -6, Random.Range(-zRange, zRange));
        speed = Random.Range(minSpeed, maxSpeed);
        targetRb = GetComponent<Rigidbody>();
        gameManagercs = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Spawn position and velocity
        targetRb.AddForce(Vector3.up * speed, ForceMode.Impulse);
        targetRb.AddTorque(torqueVector, ForceMode.Impulse);
        transform.position = spawnPos;
    }

    private void FixedUpdate()
    {
        // Bottom Bound Destroy
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
        // On clicked events
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
                    if (gameManagercs.score != 0)
                    {
                        gameManagercs.UpdateScore(-1);
                    }
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
