using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Spawner;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    
    public struct MiniWave
    {
        public EnemySO[] enemies;
        public float delay;

        // Optional: You can include a constructor for clean initialization
        /*
        public MiniWave(int count, float delayTime)
        {
            delay = delayTime;
            enemies = new EnemySO[count];
            for(int i = 0; i < count; i++)
            {
                enemies[i] = "testEnemy";
            }
        }*/

        public MiniWave(MiniWaveSO so)
        {
            delay = so.delay;
            if (so.construction == "ratio")
            {
                //for ratio, each miniWave type should only be listed once, with the corresponding index in")]
                //the chances array denoting the chance of the type spawning.")]
                //generate cuttoffs for randomization: get magnitude etc etc
                float chanceMagnitude = 0;
                for (int i = 0; i < so.chances.Length; i++)
                {
                    chanceMagnitude += so.chances[i];
                }
                float[] cutoffs = new float[so.chances.Length];
                float runningTotal = 0;
                for (int i = 0; i < so.chances.Length; i++)
                {
                    cutoffs[i] = runningTotal / chanceMagnitude;
                    runningTotal += so.chances[i];
                }
                float myRand = 0;

                enemies = new EnemySO[so.count];
                for (int i = 0; i < so.count; i++)
                {
                    //to randomly generate: for each enemy, random val, and cutoff decides type
                    myRand = Random.value;
                    for (int j = 0; j < cutoffs.Length; j++)
                    {
                        if (myRand >= cutoffs[j])
                        {
                            enemies[i] = so.types[j];
                            break;
                        }
                        else
                        {
                            enemies[i] = so.types[0];
                        }
                    }
                }

            }
            else if (so.construction == "complex")
            {
                //[Header("- for complex, the miniWaves array will spawn a number of enemies of that type")]
                //[Header("equal to the corresponding index in the chances array. count will be disregarded.")]
                List<EnemySO> enemySOs = new List<EnemySO>();
                for (int i = 0; i < so.types.Length; i++)
                {
                    for (int j = 0; j < so.chances[i]; j++)
                    {
                        enemySOs.Add(so.types[i]);
                    }
                }

                enemies = new EnemySO[enemySOs.Count];
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i] = enemySOs[i];
                }
            }
            else
            {
                Debug.Log("invalid construction type");
                enemies = null;
                delay = 1;
            }
        }
    }

    public struct Wave
    {
        public MiniWave[] miniWaves;
        public float delay;

        // Optional: You can include a constructor for clean initialization
        public Wave(WaveSO so)
        {
            delay = so.delay;
            miniWaves = new MiniWave[so.miniWaves.Length];
            for (int i = 0; i < miniWaves.Length; i++)
            {
                miniWaves[i] = new MiniWave(so.miniWaves[i]);
            }
        }
    }
    /* struct mini_wave
     * 
     * 
     * variables:
     * int timer_counter
     * int wave
     * bool spawner_active
     */
    
    public GameObject enemyPrefab;
    public float cooldown; // cooldown in seconds
    public float timerCounter;
    //to be given to newly spawned enemies
    public Transform[] waypoints;
    public GameManager gameManager;

    public PlaytestingSO scriptVals;
    public EnemySO enemyScriptVals;

    public WaveSO[] waveSOs;
    private Wave[] waves;
    public int waveGoal;
    public int waveIndex;
    public int miniWaveGoal;
    public int miniWaveIndex;
    public int enemyGoal;
    public int enemyIndex;

    private void OnValidate()
    {
        cooldown = scriptVals.enemySpawnCooldown;
        //enemyGoal = scriptVals.enemiesToSpawn;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //waves = new Wave[waveSOs.Length];
        //for(int i = 0; i < waves.Length; i++)
        //{
        //    waves[i] = new Wave(waveSOs[i]);
        //}

        waveGoal = 5;//waves.Length;
        miniWaveGoal = 5;
        enemyGoal = 5;

        timerCounter = cooldown;
        enemyIndex = 0;
        miniWaveIndex = 0;
        waveIndex = 0;
        //TODO make waves
        /*
        waves = new Wave[waveGoal];
        for (int i = 0; i < waveGoal; i++)
        {
            waves[i] = new Wave(3, 3);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
        /*
        if (enemyIndex >= enemyGoal)
        {
            if(FindFirstObjectByType<Enemy>() == null)
            {
                SceneManager.LoadScene("WinScene");
            }
            else
            {
                return;
            }
        }*/

        //increment timer, spawn enemy when timer done
        if(gameManager.phase == "wave")
        {
            timerCounter -= Time.deltaTime;
        }
        else
        {
            return;
        }
        if(timerCounter <= 0)
        {
            timerCounter = cooldown;


            //plan:
            //if spawner active
            //increment timer
            //if readyupped (timer 0):
            //if theres another enemy to spawn (if mini wave not done)
            if(enemyIndex < enemyGoal)
            {
                SpawnNextEnemy();
            }
            
            else
            {
                if(miniWaveIndex < miniWaveGoal)
                {
                    StartMiniWave();
                }
                else
                {
                    //(the wave is done)
                        //spawner not active
                        //if that was the last wave
                            //win!
                        //else
                            //go to shop phase
                    if(waveIndex < waveGoal)
                    {
                        if (FindFirstObjectByType<Enemy>() == null)
                        {
                            gameManager.StartShopPhase();
                        }
                        return;
                    }
                    else
                    {
                        if (FindFirstObjectByType<Enemy>() == null)
                        {
                            SceneManager.LoadScene("WinScene");
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
            //spawn the enemy
            //else (mini wave is done)
            //if another mini wave (if wave not done)
            //start that mini wave
            //else (the wave is done)
            //spawner not active
            //if that was the last wave
            //win!
            //else
            //go to shop phase


        }
        //temp win condition

    }

    //spawn the next enemy
    public void SpawnNextEnemy()
    {
        //enemyScriptVals = waves[waveIndex].miniWaves[miniWaveIndex].enemies[enemyIndex];
        GameObject newEnemy = Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity);
        newEnemy.GetComponent<Enemy>().waypoints = waypoints;
        newEnemy.GetComponent<Enemy>().gameManager = gameManager;
        newEnemy.GetComponent<Enemy>().scriptVals = enemyScriptVals;
        enemyIndex += 1;
    }

    public void StartMiniWave()
    {
        enemyIndex = 0;
        miniWaveIndex += 1;
        //enemyGoal = waves[waveIndex].miniWaves[miniWaveIndex].enemies.Length;
    }

    public void StartWave()
    {
        if(gameManager.phase == "wave")
        {
            return;
        }
        if(!gameManager.alreadyTithed)
        {
            gameManager.Curse();
        }
        gameManager.phase = "wave";
        miniWaveIndex = 0;
        waveIndex += 1;
        //miniWaveGoal = waves[waveIndex].miniWaves.Length;
    }

}

