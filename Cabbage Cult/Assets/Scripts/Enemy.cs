using UnityEngine;
using static UnityEditor.PlayerSettings;

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
        currentWaypoint = waypoints[0];
        currentWaypointIndex = 0;
        direction = GetNewDirection(myTF.position, currentWaypoint.GetComponent<Transform>().position);
    }

    // Update is called once per frame
    void Update()
    {
        //walk a step toward the next waypoint
        myTF.position += speed * direction * Time.deltaTime;
        //if im at/past the waypoint: reassign waypoint
        //if(ReachedWaypoint(direction, myTF.position, currentWaypoint.GetComponent<Transform>().position))
        if(Vector3.Distance(transform.position, currentWaypoint.GetComponent<Transform>().position) < 1)
        {
            if (currentWaypointIndex == waypoints.Length - 1)
            {
                //Debug.Log("reached last waypoint");
                direction = new Vector3(0, 0, 0);
                Destroy(this.gameObject);

            }
            else
            {
                currentWaypointIndex += 1;
                currentWaypoint = waypoints[currentWaypointIndex];
                //reassign direction
                direction = GetNewDirection(myTF.position, currentWaypoint.GetComponent<Transform>().position);
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

}
