using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public struct MiniWave
    {
        public Enemy[] enemies;

        // Optional: You can include a constructor for clean initialization
        public MiniWave(int count)
        {
            enemies = new Enemy[count];
            for(int i = 0; i < count; i++)
            {
                enemies[i] = new Enemy();
            }
        }
    }

    public struct Wave
    {
        public MiniWave[] miniWaves;

        // Optional: You can include a constructor for clean initialization
        public Wave(int count)
        {
            miniWaves = new MiniWave[count];
            for (int i = 0; i < count; i++)
            {
                miniWaves[i] = new MiniWave(3);
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
    private int miniWaveGoal;
    private int miniWaveIndex;
    private int enemyTotal;
    public int enemyGoal;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerCounter = cooldown;
        enemyTotal = 0;
        miniWaveIndex = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyTotal >= enemyGoal)
        {
            if(FindFirstObjectByType<Enemy>() == null)
            {
                SceneManager.LoadScene("WinScene");
            }
            else
            {
                return;
            }
        }

        //increment timer, spawn enemy when timer done
        timerCounter -= Time.deltaTime;
        if(timerCounter <= 0)
        {
            SpawnNextEnemy();
            timerCounter = cooldown;


            //plan:
            //if spawner active
            //increment timer
            //if readyupped (timer 0):
            //if theres another enemy to spawn (if mini wave not done)
           
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
        enemyTotal += 1;
    }
}

