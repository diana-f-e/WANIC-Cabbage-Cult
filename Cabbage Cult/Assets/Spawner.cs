using UnityEngine;

public class Spawner : MonoBehaviour
{
    /* struct mini_wave
     * 
     * 
     * variables:
     * int timer_counter
     * int wave
     * bool spawner_active
     * mini_wave[] mini_waves
     */


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

    public void SpawnNextEnemy()
    {

    }
}

