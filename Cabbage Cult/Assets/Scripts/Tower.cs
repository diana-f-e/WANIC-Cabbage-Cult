using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerSO scriptVals;
    public CircleCollider2D attackingCollider;

    public float cooldown; // cooldown in seconds
    private float timerCounter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerCounter = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        timerCounter -= Time.deltaTime;
        if (timerCounter <= 0)
        {
            Attack();
            timerCounter = cooldown;
        }
    }

    //update in scene view when changed
    private void OnValidate()
    {
        //update vals based on scriptable object
        attackingCollider.radius = scriptVals.attackRadius;
    }

    public void Attack()
    {
        Debug.Log("pew pew");
    }
}
