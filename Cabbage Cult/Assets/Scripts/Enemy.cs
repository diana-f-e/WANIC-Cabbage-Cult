using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Enemy : MonoBehaviour
{
    public Transform[] waypoints;
    private Transform currentWaypoint;
    private int currentWaypointIndex;

    public float speed;
    private Vector3 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentWaypoint = waypoints[0];
        currentWaypointIndex = 0;
        direction = GetNewDirection(transform.position, currentWaypoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        //walk a step toward the next waypoint
        transform.position += speed * direction * Time.deltaTime;
        //if im at/past the waypoint: reassign waypoint
        if(Vector3.Distance(transform.position, currentWaypoint.position) < 1)
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

}
