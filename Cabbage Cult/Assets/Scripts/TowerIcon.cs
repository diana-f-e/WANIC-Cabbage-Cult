using TMPro;
using UnityEngine;

public class TowerIcon : MonoBehaviour
{
    public TowerSO scriptVals;
    public GameManager gameManager;
    public GameObject towerItem;
    public int cost;

    //public TextMeshProUGUI textBox;

    private void OnValidate()
    {
        cost = scriptVals.towerCost;
        //gameObject.GetComponent<SpriteRenderer>().sprite = scriptVals.skin;
        //if(!textBox.text.Contains(cost.ToString()))
        //{
        //    textBox.text += ": " + cost;
        //}
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
        onClicked();


    }

    public void onClicked()
    {
        //if not holding anything or dont have enough money
        if (gameManager.heldObj != null || gameManager.money < cost || gameManager.titheButton.activeInHierarchy)
        {
            return;
        }
        GameObject newToweritem = Instantiate(towerItem);
        newToweritem.GetComponent<TowerItem>().towerScriptVals = scriptVals;
        gameManager.heldObj = newToweritem;
        gameManager.money -= cost;
    }
}
