/****************************************************************************
* File Name: TowerAttackCollider.cs
* Author: Diana Everman
* DigiPen Email: diana.everman@digipen.edu
* Course: Video Game Programming 1
*
* Description: This file is to be attached to a tower's attack collider object
*               for its functions, detecting when an enemy enters/exits.
*
****************************************************************************/
using UnityEngine;

public class TowerAttackCollider : MonoBehaviour
{
    public Tower towerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<SpriteRenderer>().size = GetComponent<CircleCollider2D>().bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemyScript = collision.gameObject.GetComponent<Enemy>();
        if (enemyScript == null) { return;}
        towerScript.enemiesInRange.Add(enemyScript);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy enemyScript = collision.gameObject.GetComponent<Enemy>();
        if (enemyScript == null) { return; }
        towerScript.enemiesInRange.Remove(enemyScript);
    }
}
