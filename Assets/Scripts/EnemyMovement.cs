using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    float enemySpeed;

    public AudioSource enemyActive;
    public AudioSource BackgroundMusic; 

public class Enemy : MonoBehaviour
{
    public float enemySpeed = 3;

    public AudioSource enemyActive;
    public AudioSource BackgroundMusic;

    void enemyMovement()
    {
        enemySpeed += 0.0001f;
        transform.Translate(enemySpeed, 0, 0);
    }

    void Start()
    {
        enemyActive.GetComponent<AudioSource>();
        BackgroundMusic.GetComponent<AudioSource>();
        BackgroundMusic.Stop();
        enemyActive.Play();
        enemySpeed = 0f;
    }

    void Update()
    {
        enemyMovement();
    }
}

void enemyMovement()
    {
        enemySpeed += 0.0001f;
        transform.Translate(enemySpeed, 0, 0);
    }

    void Start()
    {
        enemyActive.GetComponent<AudioSource>();
        BackgroundMusic.GetComponent<AudioSource>();
        BackgroundMusic.Stop();
        enemyActive.Play();
        enemySpeed = 0f;
    }

    void Update()
    {
        enemyMovement();
    }
}
