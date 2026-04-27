using UnityEngine;

public class oldTower : MonoBehaviour
{
    public TowerSO scriptVals;
    public CircleCollider2D placingCollider;
    public CircleCollider2D attackingCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //update in scene view when changed
    private void OnValidate()
    {
        //update vals based on scriptable object
        //placingCollider.radius = scriptVals.placeRadius;
        attackingCollider.radius = scriptVals.attackRadius;
    }

    //if the placing collider is colliding with something, like another tower or the path, it can't be placed
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
