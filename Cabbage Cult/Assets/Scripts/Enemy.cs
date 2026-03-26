using UnityEngine;
using UnityEngine.SceneManagement;


public class Enemy : MonoBehaviour
{
    public EnemySO scriptVals;

    public Transform[] waypoints;
    private Transform currentWaypoint;
    private int currentWaypointIndex;

    public float speed;
    private Vector3 direction;
    public GameManager gameManager;
    public int damage;
    public int health;

    //effects
    public bool decaying;
    public bool slowed;
    public int helmetHealth;

    public float decayCooldown;
    private float decayCounter;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentWaypoint = waypoints[0];
        currentWaypointIndex = 0;
        direction = GetNewDirection(transform.position, currentWaypoint.position);

        damage = scriptVals.enemyDamage;
        health = scriptVals.enemyHealth;
        speed = scriptVals.enemySpeed;

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
                health -= 1;
                decayCounter = decayCooldown;
            }
        }


        //walk a step toward the next waypoint
        transform.position += speed * direction * Time.deltaTime;
        //if im at/past the waypoint: reassign waypoint
        if(Vector3.Distance(transform.position, currentWaypoint.position) < 0.1) //TODO fix in build
        {
            if (currentWaypointIndex == waypoints.Length - 1)
            {
                //Debug.Log("reached last waypoint");
                direction = new Vector3(0, 0, 0);
                gameManager.health -= damage;
                //TODO make sceneswitcher have funcs for die/win
                if(gameManager.health <= 0)
                {
                    SceneManager.LoadScene("LoseScene");
                }
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
        float healthAlpha = 1.0f * health / scriptVals.enemyHealth;
        if (healthAlpha > 1)
            healthAlpha = 1;
        if (healthAlpha <= 0)
            healthAlpha = 0.01f;
        GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.1f, 0.1f, healthAlpha);
    }

    public void Damage(int amount, string effect)
    {
        Damage(amount);
        //apply effect
        if (effect == "slow")
        {
            if(!slowed)
            {
                speed /= 2;
                slowed = true;
            }
        }
        else if (effect == "decay")
        {
            if(!decaying)
            {
                gameObject.GetComponent<SpriteRenderer>().color -= new Color(0.1f, 0.4f, 0.1f);
                decaying = true;
                decayCooldown = 2;
                decayCounter = decayCooldown;
            }
        }
        
    }

    public void Damage(int amount)
    {
        if (helmetHealth >= 0)
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
        }
    }

}
