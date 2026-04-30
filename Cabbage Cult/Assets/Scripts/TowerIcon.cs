/****************************************************************************
* File Name: TowerIcon.cs
* Author: Diana Everman
* DigiPen Email: diana.everman@digipen.edu
* Course: Video Game Programming 1
*
* Description: This file is to be attached to a TowerIcon for its functions.
*
****************************************************************************/
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TowerSO scriptVals;
    public GameManager gameManager;
    public GameObject towerItem;
    public int cost;
    private GameObject infoDisplay;

    private void OnValidate()
    {
        cost = scriptVals.towerCost;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //display name and cost of the tower on its shop icon
        this.GetComponentInChildren<TextMeshProUGUI>().text = "" + scriptVals.towerType + ": " + scriptVals.towerCost;
        infoDisplay = gameManager.towerInfoDisplay;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //when the shop icon is clicked, give the player the relevant tower item
    private void OnMouseDown()
    {
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
        //gameManager.audioSource.PlayOneShot(scriptVals.onBuy);
        gameManager.PlaySound(gameManager.getBuyARC(scriptVals.effect));
    }

    //display the tower's stats 
    public void OnPointerEnter(PointerEventData eventData)
    {
        infoDisplay.GetComponentInChildren<TextMeshProUGUI>().text = gameManager.GetTowerDisplayText(scriptVals);
        infoDisplay.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoDisplay.SetActive(false);
    }
}
