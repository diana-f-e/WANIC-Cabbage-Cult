using System.Collections.Generic;
using UnityEngine;

public class MergeButton : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject towerItem;
    public TowerSO defaultTowerScriptVals;
    public List<TowerSO> towerSOs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //when the merge icon is clicked, if mergelist is valid, give the player the relevant upgraded tower item
    private void OnMouseDown()
    {
        //Debug.Log("OnMouseDown");
        //if not holding anything or dont have enough money
        //Merge();
    }

    public void Merge()
    {
        if (ValidateMergeList())
        {
            GameObject newTower = Instantiate(towerItem);
            newTower.GetComponent<TowerItem>().towerScriptVals = GetUpgradedTowerSO();
            gameManager.heldObj = newTower;

            //clear the merge list
            foreach (Tower t in gameManager.mergeList)
            {
                Destroy(t.gameObject);
            }
            gameManager.mergeList.Clear();
        }
    }

    private TowerSO GetUpgradedTowerSO()
    {
        //TODO return the correct tower based on gamemanager type and level
        string towerType = gameManager.mergeType;
        int level = gameManager.mergeLevel + 1;
        foreach(TowerSO t in towerSOs)
        {
            if(t.towerLevel == level && t.towerType == towerType)
            {
                return t;
            }
        }
        return defaultTowerScriptVals;
    }

    private bool ValidateMergeList()
    {
        if(gameManager.mergeList.Count < 3)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
