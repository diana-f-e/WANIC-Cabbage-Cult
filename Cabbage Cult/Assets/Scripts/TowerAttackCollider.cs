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
