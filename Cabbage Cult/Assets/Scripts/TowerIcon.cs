using UnityEngine;

public class TowerIcon : MonoBehaviour
{
    public TowerSO scriptVals;
    public GameManager gameManager;
    public GameObject towerItem;
    public int cost;

    private void OnValidate()
    {
        cost = scriptVals.towerCost;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //when the shop icon is clicked, give the player the relevant tower item
    private void OnMouseDown()
    {
        //Debug.Log("OnMouseDown");
        //if not holding anything or dont have enough money
        if (gameManager.heldObj != null || gameManager.money < cost)
        {
            return;
        }
            gameManager.heldObj = Instantiate(towerItem);
            gameManager.money -= cost;
    }
}
