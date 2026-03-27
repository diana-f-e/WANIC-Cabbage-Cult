using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public struct MiniWave
    {
        public string[] enemies;
        public float delay;

        // Optional: You can include a constructor for clean initialization
        public MiniWave(int count, float delayTime)
        {
            delay = delayTime;
            enemies = new string[count];
            for(int i = 0; i < count; i++)
            {
                enemies[i] = "testEnemy";
            }
        }
    }

    public struct Wave
    {
        public MiniWave[] miniWaves;
        public float delay;

        // Optional: You can include a constructor for clean initialization
        public Wave(int count, float delayTime)
        {
            delay = delayTime;
            miniWaves = new MiniWave[count];
            for (int i = 0; i < count; i++)
            {
                miniWaves[i] = new MiniWave(10, 1);
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

    public Wave[] waves;
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
        waveGoal = 5;
        miniWaveGoal = 5;
        enemyGoal = 5;

        timerCounter = cooldown;
        enemyIndex = 0;
        miniWaveIndex = 0;
        waveIndex = 0;
        //TODO make waves
        waves = new Wave[waveGoal];
        for (int i = 0; i < waveGoal; i++)
        {
            waves[i] = new Wave(3, 3);
        }
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
                        gameManager.StartShopPhase();
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
    }
}

