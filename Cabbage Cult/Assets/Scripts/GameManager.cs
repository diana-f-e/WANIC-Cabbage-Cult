using Unity.Burst.CompilerServices;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject heldObj;
    public GameObject clickedObj;
    public Transform shopBorder;
    public float health;
    public float money;
    public TextMeshProUGUI statsText;

    public List<Tower> mergeList;
    public string mergeType;
    public int mergeLevel;
    public string phase = "shop";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //update stats
        statsText.text = "Health: "+ health + "\nMoney: " + money;

        //get the mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z += Camera.main.nearClipPlane;

        //what am i clicking?
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.zero);
        //Debug.Log(hit.collider);


        if (hit.collider != null)
        {
            clickedObj = hit.collider.gameObject;
        }
        else
        {
            clickedObj = null;
        }


        //if smth in hand, track it to the mouse
        if (heldObj != null) { heldObj.transform.position = mousePosition;}
        //click
        if (Input.GetMouseButtonDown(0))
        {
            
            //holding smth?
            if (heldObj != null)
            {
                //valid spot?
                //if(validTowerSpot(mousePosition, heldObj))
                if (heldObj.GetComponent<TowerItem>().validPlacement && mousePosition.x < shopBorder.position.x)
                {
                    //place it
                    PlaceTower(mousePosition, heldObj.GetComponent<TowerItem>().tower, heldObj);
                    heldObj = null;
                }
                    
            }
            else
            {
                
                //shop icon?
                //grab it
            }


        }
        //right click
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Input.GetMouseButtonDown(1");
            Tower clickedTower = clickedObj.GetComponent<Tower>();
            if (clickedTower == null){ clickedTower = clickedObj.GetComponent<TowerAttackCollider>().towerScript; }
            //Is it a tower?
            if (clickedTower != null)
            {
                Debug.Log("(clickedTower != null");
                //Is it in the list? 
                if (mergeList.Contains(clickedTower))
                {
                    RemoveFromMergeList(clickedTower);
                }
                else
                {
                    AddToMergeList(clickedTower);
                }

            }
        }


    }

    private void PlaceTower(Vector3 coords, GameObject tower, GameObject towerItem)
    {
        //at the point im given, put a tower
        GameObject placedTower = Instantiate(tower, coords, Quaternion.identity);
        Destroy(towerItem);
        //placedTower.GetComponent<Tower>().placingCollider.gameObject.SetActive(true);
        //placedTower.GetComponent<Tower>().attackingCollider.gameObject.SetActive(true);
    }

    private bool ValidTowerSpot(Vector3 coords, GameObject tower)
    {
        if (tower.GetComponent<Tower>() == null)
        {
            Debug.Log("heldobj not a tower");
            return false;
        }
        //if not too close to other towers / path (i didnt click a collider of another tower)
        if (clickedObj != null)
        {
            return false;
        }
        return true;
    }

    private bool AddToMergeList(Tower tower)
    {
        //if this is the first thing in the list change mergeType
        if (mergeList.Count == 0)
        {
            //Change mergeType to towerType
            mergeType = tower.towerType;
        }
        //Does its type match mergeType? 
        if (tower.towerType != mergeType)
        {
            //If not, return false
            return false;
        }
        //no open spots: return false
        if (mergeList.Count >= 3)
        {
            return false;
        }
        //if theres an open spot: add the tower, change to selected sprite and return true
        mergeList.Add(tower);
        tower.MergeSelect();


        return true;
    }

    private bool RemoveFromMergeList(Tower tower)
    {
        //Remove it from the list
        mergeList.Remove(tower);
        //Change to unselected sprite
        tower.MergeDeselect();
        //If the list is empty
        if (mergeList.Count <= 0)
        {
            //Change mergeType to null
            mergeType = null;
        }
        return true;
    }
    





}
