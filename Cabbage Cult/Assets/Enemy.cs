using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform myTF;
    public GameObject[] waypoints;
    public float speed;

    private Vector3 direction;
    private GameObject currentWaypoint;
    private int currentWaypointIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myTF = this.GetComponent<Transform>();
        direction = new Vector3(0.005f, 0.005f, 0);
        currentWaypoint = waypoints[0];
        currentWaypointIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //walk a step toward the next waypoint
        myTF.position += speed * direction;
        //if im at/past the waypoint: reassign waypoint
        if(ReachedWaypoint(direction, myTF.position, currentWaypoint.GetComponent<Transform>().position))
        {
            if (currentWaypointIndex == waypoints.Length - 1)
            {
                Debug.Log("reached last waypoint");
                direction = new Vector3(0, 0, 0);

            }
            else
            {
                currentWaypointIndex += 1;
                currentWaypoint = waypoints[currentWaypointIndex];
                //reassign direction
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }

    //detect whether the enemy has reached the current waypoint
    private bool ReachedWaypoint(Vector3 myDirection, Vector3 myPos, Vector3 wayPtPos)
    {
        //assume i am past the waypoint, return false if not
        //check x
        if(!HelpReachedWaypoint(myDirection.x, myPos.x, wayPtPos.x))
        {
            return false;
        }
        //check y
        if (!HelpReachedWaypoint(myDirection.y, myPos.y, wayPtPos.y))
        {
            return false;
        }
        //check z
        if (!HelpReachedWaypoint(myDirection.z, myPos.z, wayPtPos.z))
        {
            return false;
        }
        return true;
    }

    //help check if the enemy has reached a waypoint by checking a given direction
    private bool HelpReachedWaypoint(float myDir, float myNum, float wayPtNum)
    {
        //my position should be further right than the waypoint, so mypos-waypos matches the sign of my direction
        if(myDir == 0)
        {
            return true;
        }
        float diffPos = myNum - wayPtNum;
        if (myDir < 0)
        {
            if (diffPos > 0)
            {
                return false;
            }
        }
        else if (myDir > 0)
        {
            if (diffPos < 0)
            {
                return false;
            }
        }
        return true;
    }


}
