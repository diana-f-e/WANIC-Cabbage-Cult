using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerSO scriptVals;
    public CircleCollider2D attackingCollider;

    public float cooldown; // cooldown in seconds
    private float timerCounter;
    public List<Enemy> enemiesInRange = new List<Enemy>();
    public int damage;

    public string towerType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerCounter = cooldown;
        towerType = "default";
    }

    // Update is called once per frame
    void Update()
    {
        timerCounter -= Time.deltaTime;
        if (timerCounter <= 0)
        {
            //detect enemy in radius
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
        if (enemiesInRange.Count <= 0)
        {
            return;
        }
        enemiesInRange[0].health -= damage;
        //Debug.Log("pew pew");

    }

    public void MergeSelect()
    {
        GetComponent<SpriteRenderer>().color += new Color(0.2f, 0.2f, 0.4f);
        //GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.9f);
    }

    public void MergeDeselect()
    {
        GetComponent<SpriteRenderer>().color -= new Color(0.2f, 0.2f, 0.4f);
        //GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.6f, 0.9f);
    }
}
