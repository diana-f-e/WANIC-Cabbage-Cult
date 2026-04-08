using UnityEngine;
using UnityEngine.SceneManagement;


public class Enemy : MonoBehaviour
{
    public EnemySO scriptVals;
    public GameManager gameManager;

    public Transform[] waypoints;
    private Transform currentWaypoint;
    private int currentWaypointIndex;

    public float speed;
    private Vector3 direction;
    public int damage;
    public int health;

    //effects
    public bool decaying;
    public bool slowed;
    public int helmetHealth;

    public float decayCooldown;
    private float decayCounter;
    private float decayDmg;

    public float slowCooldown;
    private float slowCounter;
    private float slowedAmt;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentWaypoint = waypoints[0];
        currentWaypointIndex = 0;
        direction = GetNewDirection(transform.position, currentWaypoint.position);

        damage = scriptVals.enemyDamage;
        health = scriptVals.enemyHealth;
        speed = scriptVals.enemySpeed;
        helmetHealth = scriptVals.helmetHealth;
        if(scriptVals.skin != null)
        {
            GetComponent<SpriteRenderer>().sprite = scriptVals.skin;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //enemy health display
        ShowHealth();
        
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }

        //effects
        if(decaying)
        {
            decayCounter -= Time.deltaTime;
            if(decayCounter <= 0)
            {
                health -= (int)decayDmg;
                decayCounter = decayCooldown;
            }
        }
        if (slowed)
        {
            slowCounter -= Time.deltaTime;
            if (slowCounter <= 0)
            {
                speed /= slowedAmt;
                slowed = false;
            }
        }


        //walk a step toward the next waypoint
        transform.position += speed * direction * Time.deltaTime;
        //if im at/past the waypoint: reassign waypoint
        if(Vector3.Distance(transform.position, currentWaypoint.position) < (1.01 * speed * Time.deltaTime) ) //TODO fix in build
        {
            if (currentWaypointIndex == waypoints.Length - 1)
            {
                //reached last waypoint
                direction = new Vector3(0, 0, 0);
                gameManager.health -= damage;
                //TODO make sceneswitcher have funcs for die/win
                if(gameManager.health <= 0)
                {
                    SceneManager.LoadScene("LoseScene");
                }
                gameManager.audioSource.PlayOneShot(scriptVals.onDeath);
                Destroy(this.gameObject);

            }
            else
            {
                currentWaypointIndex += 1;
                currentWaypoint = waypoints[currentWaypointIndex];
                //reassign direction
                direction = GetNewDirection(transform.position, currentWaypoint.position);
            }
        }
    }


    private Vector3 GetNewDirection(Vector3 myPos, Vector3 wayPtPos)
    {
        //new direction vector should have magnitude of 1, and point toward next waypoint
        Vector3 newDir = wayPtPos - myPos;
        newDir /= newDir.magnitude;
        return newDir;
    }

    private void ShowHealth()
    {
        //change transparency of sprite to show health
        float healthColor = 1.0f * health / scriptVals.enemyHealth;
        if (healthColor > 1)
            healthColor = 1;
        if (healthColor <= 0)
            healthColor = 0.001f;
        GetComponent<SpriteRenderer>().color = new Color(1.0f, healthColor, healthColor);
    }

    public void Damage(int amount, string effect, float num, float cooldown)
    {
        Damage(amount);
        //apply effect
        if (effect == "slow")
        {
            if (!slowed)
            {
                slowedAmt = num;
                speed *= slowedAmt;

                slowed = true;
                slowCooldown = cooldown;
                slowCounter = slowCooldown;
            }
        }
        else if (effect == "decay")
        {
            if (!decaying)
            {
                decayDmg = num;
                gameObject.GetComponent<SpriteRenderer>().color -= new Color(0f, 0.2f, 0f);

                decaying = true;
                decayCooldown = 2;
                decayCounter = decayCooldown;
            }
        }

    }

    public void Damage(int amount, string effect)
    {
        Damage(amount, effect, 2, 2);
        
    }

    public void Damage(int amount)
    {
        if (helmetHealth > 0)
        {
            helmetHealth -= 1;
            if (helmetHealth == 0)
            {
                //TODO destroy helmet
                gameObject.GetComponent<SpriteRenderer>().color += new Color(0.4f, 0.1f, 0.1f);
            }
        }
        else
        {
            health -= amount;
            gameManager.audioSource.PlayOneShot(scriptVals.onHurt);
        }

    }

}
