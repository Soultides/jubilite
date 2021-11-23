using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public float enemySpawnTimer;
    public GameObject[] enemies;
    Transform[] spawnPoints;

    private GameManager gm;
    public float score;

    public int enemiesOnScreen;
    public float enemyCap = 1f;
    public float mod = 150f;
    public float untilDiffRise = 1000f;
    public float timerCalc;
    public float eTimer = .2f;

    public Image[] levels;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        spawnPoints = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        score = gm.score;

        if (score <= 1500f)
        {
            timerCalc = Time.deltaTime * Mathf.Round(score);
        }

        if (score >= 1500f)
        {
            timerCalc = Time.deltaTime * Mathf.Round(score / (1500 / enemyCap));
        }

        eTimer -= timerCalc;

        if (score > untilDiffRise && enemyCap <= 14)
        {
            enemyCap += 1;
            Ramp();

            for (int i = 0; i < enemyCap; i++)
            {
                levels[i].enabled = true;
            }

        }


        if (eTimer <= Time.deltaTime && enemiesOnScreen <= enemyCap - 1)
        {

            SpawnEnemy();
            //Debug.Log("Spawn");
            eTimer = enemySpawnTimer;
            enemiesOnScreen += 1;

        }

    }

    void SpawnEnemy()
    {
        int randomE = Random.Range(0, enemies.Length);
        int randomP = Random.Range(0, spawnPoints.Length);

        Vector3 spawnPosition = spawnPoints[randomP].position;
        Quaternion spawnRotation = spawnPoints[randomP].rotation;
        Instantiate(enemies[randomE], spawnPosition, spawnRotation);

    }

    void Ramp()
    {
        untilDiffRise += (mod * enemyCap) / 2f;
    }
}
