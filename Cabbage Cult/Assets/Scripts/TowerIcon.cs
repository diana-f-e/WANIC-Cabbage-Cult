using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TowerSO scriptVals;
    public GameManager gameManager;
    public GameObject towerItem;
    public int cost;
    public GameObject infoDisplay;

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
        this.GetComponentInChildren<TextMeshProUGUI>().text = "" + scriptVals.towerType + ": " + scriptVals.towerCost;

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
        if (gameManager.heldObj != null || gameManager.money < cost || gameManager.titheUI.activeInHierarchy)
        {
            return;
        }
        GameObject newToweritem = Instantiate(towerItem);
        newToweritem.GetComponent<TowerItem>().towerScriptVals = scriptVals;
        gameManager.heldObj = newToweritem;
        gameManager.money -= cost;
        gameManager.audioSource.PlayOneShot(scriptVals.onBuy);
    }

    //display the tower's stats 
    public void OnPointerEnter(PointerEventData eventData)
    {
        string displayText = "";
        displayText += scriptVals.towerType + " Tower \n";
        displayText += " - damage per attack: " + scriptVals.damage + "\n";
        displayText += " - attack cooldown: " + scriptVals.cooldown + " seconds\n";
        if(scriptVals.effect != "" && scriptVals.effect != null)
        {
            displayText += " - attack effect: " + scriptVals.effect + "\n";
        }
        infoDisplay.GetComponentInChildren<TextMeshProUGUI>().text = displayText;
        infoDisplay.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoDisplay.SetActive(false);
    }
}
