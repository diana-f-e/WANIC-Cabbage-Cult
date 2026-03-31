using Unity.Burst.CompilerServices;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UIElements;

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

    public PlaytestingSO scriptVals;

    public bool alreadyTithed = false;
    public int tax;

    public GameObject titheButton;

    public AudioSource audioSource;
    private void OnValidate()
    {
        money = scriptVals.money;
        health = scriptVals.health;
        tax = scriptVals.tax;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //update stats
        statsText.text = "Health: "+ health + "\nMoney: " + money + "\nPhase: " + phase;

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
            Tower clickedTower = clickedObj.GetComponent<Tower>();
            //loop through everything clicked to find the tower
            if (clickedTower == null)
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector3.zero);
                foreach(RaycastHit2D h in hits)
                {
                    if (h.collider.gameObject.GetComponent<Tower>() != null)
                    {
                        clickedTower = h.collider.gameObject.GetComponent<Tower>();
                        break;
                    }
                }
                //clickedTower = clickedObj.GetComponent<TowerAttackCollider>().towerScript; 
            }
            //Is it a tower?
            if (clickedTower != null)
            {
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
        placedTower.GetComponent<Tower>().scriptVals = towerItem.GetComponent<TowerItem>().towerScriptVals;
        placedTower.GetComponent<Tower>().gameManager = this;
        Destroy(towerItem);
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
            mergeLevel = tower.level;
        }
        //Does its type and level match merge type and level? 
        if (tower.towerType != mergeType || tower.level != mergeLevel)
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

    public void StartShopPhase()
    {
        phase = "shop";
        money += scriptVals.moneyPerRound;
        alreadyTithed = false;
        //undo curse
        Tower[] towers = FindObjectsByType<Tower>(FindObjectsSortMode.None);
        foreach (Tower t in towers)
        {
            if(t.cursed)
            {
                t.cursed = false;
                t.damage *= 2;
                t.gameObject.GetComponent<SpriteRenderer>().color = t.scriptVals.towerColor;
            }
        }
        // show tithe button
        titheButton.SetActive(true);
    }

    public void Curse()
    {
        Tower[] towers = FindObjectsByType<Tower>(FindObjectsSortMode.None);
        foreach(Tower t in towers)
        {
            t.cursed = true;
            t.damage /= 2;
            t.gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
        }
        Debug.Log("cursedy curse");
    }

    public void Tithe()
    {
        if (alreadyTithed) { return; }
        if (phase != "shop") { return; }

        //hide tithe button
        titheButton.SetActive(false);
        Debug.Log("tithing...");
        money -= tax;
        if(money < 0)
        {
            money = 0;
        }

        alreadyTithed = true;
    }

    public void DontTithe()
    {
        //hide tithe button
        titheButton.SetActive(false);
    }

    public void ChangeTimeScale(float num)
    {
        if(Time.timeScale == num)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = num;
        }
    }




}
