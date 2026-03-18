using UnityEngine;

public class Tower : MonoBehaviour
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
        placingCollider.radius = scriptVals.placeRadius;
    }
}
