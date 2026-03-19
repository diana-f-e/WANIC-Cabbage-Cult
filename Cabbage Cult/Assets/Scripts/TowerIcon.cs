using UnityEngine;

public class TowerIcon : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject towerItem;

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
        //if not holding anything
        if (gameManager.heldObj != null)
        {
            //Debug.Log("holding " + gameManager.heldObj.name);
            return;
        }
        //TODO if have enough money
        gameManager.heldObj = Instantiate(towerItem);
    }
}
