/****************************************************************************
* File Name: GameManager.cs
* Author: Diana Everman
* DigiPen Email: diana.everman@digipen.edu
* Course: Video Game Programming 1
*
* Description: This file manages most of the game functions, like merging and 
*   placing towers.
*
****************************************************************************/

using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using NUnit.Framework.Internal;

public class GameManager : MonoBehaviour
{
    public PlaytestingSO scriptVals;

    public GameObject heldObj;
    public GameObject clickedObj;

    public float health;
    public float money;
    public string phase = "shop";

    public TextMeshProUGUI statsTextMoney;
    public Transform shopBorder;

    public List<Tower> mergeList;
    public string mergeType;
    public int mergeLevel;
    public Image[] mergePreviewMinis;
    public Image[] mergePreviewMiniCopies;
    //public Image mergePreview;


    public bool alreadyTithed = false;
    public int[] taxes;
    private int taxIndex;
    public GameObject titheUI;
    public TextMeshProUGUI statsTextTitheTowers;
    public TextMeshProUGUI statsTextTitheMult;
    public TextMeshProUGUI statsTextTitheCurse;
    public TextMeshProUGUI statsTextTithe;

    public GameObject waveUI;
    public TextMeshProUGUI curseText;

    public AudioSource audioSource;

    public GameObject debugMenu;

    public string currentCurseName;
    public CurseSO currentCurse;

    public CurseSO[] curseSOs;
    public float[] curseWeights;
    public float[] curseCutoffs;

    public Image healthBar;
    public GameObject pauseMenu;

    public AudioClip shopMusic;
    public AudioClip waveMusic;

    private void OnValidate()
    {
        money = scriptVals.money;
        health = scriptVals.health;
        taxes = scriptVals.taxes;
        taxIndex = 0;
    }
    public List<Tower> towers;
    public GameObject towerInfoDisplay;

    public GameObject hitEffectPrefab;
    public RuntimeAnimatorController[] hitAnimControllers;
    public AudioResource[] hitARCs;
    public AudioResource summonARC;
    public AudioResource hurtARC;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 2;
        //generate cuttoffs for randomization: get magnitude etc etc
        float chanceMagnitude = 0;
        for (int i = 0; i < curseWeights.Length; i++)
        {
            chanceMagnitude += curseWeights[i];
        }
        curseCutoffs = new float[curseWeights.Length];

        Debug.Log("cutoffs: ");
        float runningTotal = 0;
        for (int i = 0; i < curseWeights.Length; i++)
        {
            curseCutoffs[i] = runningTotal / chanceMagnitude;
            Debug.Log("[" + i + "]: " + curseCutoffs[i]);
            runningTotal += curseWeights[i];
        }

        statsTextTitheCurse.text = "Curse Chances:\n";
        int currentIndex = 0;
        foreach (CurseSO c in curseSOs)
        {
            statsTextTitheCurse.text += "" + c.curseName + ": " + (int)(curseWeights[currentIndex] / chanceMagnitude * 100) + "%\n";
            currentIndex++;
        }


    }

    // Update is called once per frame
    void Update()
    {
        //check if debug menu being opened
        if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.B))
        {
            ShowDebug();
        }

        //update stats
        //statsText.text = "Health: "+ health + "\nMoney: " + money + "\nPhase: " + phase + "\nCurse: " + currentCurseName;
        statsTextMoney.text = "" + money;
        healthBar.fillAmount = health / scriptVals.health;
        if(titheUI.activeInHierarchy)
        {
            statsTextTitheTowers.text = "Towers:\n" + towers.Count;
            statsTextTitheMult.text = "Multiplier:\n"+ (int)taxes[taxIndex] +"x";
            statsTextTithe.text = "Pay the " + towers.Count * (int)taxes[taxIndex] + " Soul Dust Tithe?";
        }

        //get the mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z += Camera.main.nearClipPlane;

        //what am i clicking?
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.zero);
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
        audioSource.PlayOneShot(placedTower.GetComponent<Tower>().scriptVals.onBuy);
        //apply curse
        if(currentCurse != null)
        {
            Debug.Log("currentCurse != null");
            placedTower.GetComponent<Tower>().ApplyCurse(currentCurse);
        }

        Destroy(towerItem);
        //UpdateTowerList
        UpdateTowerList();
    }

    public void UpdateTowerList()
    {
        towers.Clear();
        foreach(Tower t in FindObjectsByType<Tower>(FindObjectsSortMode.None))
        {
            towers.Add(t);
        }
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
        displayMergeList();
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
        displayMergeList();
        return true;
    }

    public void displayMergeList()
    {
        //display merge list
        int index = 0;
        foreach (Image i in mergePreviewMinis)
        {
            if(mergeList.Count < index + 1)
            {
                i.gameObject.SetActive(false);
            }
            else
            {
                Sprite newSprite = mergeList[index].GetComponent<SpriteRenderer>().sprite;
                i.gameObject.gameObject.SetActive(true);
                i.sprite = newSprite;
            }
            mergePreviewMiniCopies[index].sprite = i.sprite;
            mergePreviewMiniCopies[index].gameObject.SetActive(i.gameObject.activeSelf);
            index++;
        }
        /*
        if (mergeList.Count == 3)
        {
            mergePreview.gameObject.SetActive(true);
            mergePreview.sprite = FindFirstObjectByType<MergeButton>().GetUpgradedTowerSO().skin;
        }
        else
        {
            mergePreview.gameObject.SetActive(false);
        }*/
    }

    public void StartShopPhase()
    {
        phase = "shop";
        money += scriptVals.moneyPerRound;
        taxIndex += 1;
        alreadyTithed = false;
        //undo curse
        currentCurseName = "none";
        currentCurse = null;
        Tower[] towers = FindObjectsByType<Tower>(FindObjectsSortMode.None);
        foreach (Tower t in towers)
        {
            if(t.cursed)
            {
                t.cursed = false;
                t.assignScriptVals();
            }
        }
        // show tithe button
        titheUI.SetActive(true);
        curseText.text = "You are not cursed. Cva'pfel-roth has been appeased.";
        waveUI.SetActive(false);
        audioSource.clip = shopMusic;
        audioSource.Play();
    }

    public void Curse()
    {
        float myRand = 0;
        CurseSO chosenCurseSO = null;
        //to randomly generate: for each enemy, random val, and cutoff decides type
        myRand = Random.value;
        for (int j = 0; j < curseCutoffs.Length; j++)
        {
            if (myRand >= curseCutoffs[j])
            {
                chosenCurseSO = curseSOs[j];
            }
            else
            {
                break;
            }
            //Debug.Log("MiniWave builder: i = "+i+", j = "+j+", enemies[i] = " + enemies[i].name);
        }
        if (chosenCurseSO == null)
        {
            Debug.Log("myRand failed:  " + myRand);
            chosenCurseSO = curseSOs[0];

        }

        currentCurseName = chosenCurseSO.curseName;
        currentCurse = chosenCurseSO;//curseSOs[0];
        Tower[] towers = FindObjectsByType<Tower>(FindObjectsSortMode.None);
        foreach(Tower t in towers)
        {
            t.ApplyCurse(chosenCurseSO);
        }
        //show curse
        curseText.text = "You are cursed! Cva'pfel-roth has inflicted "+ chosenCurseSO.curseName + ".";
    }

    public void OldCurse()
    {
        //example curse: reduce
        currentCurseName = "ex. SO reduce";
        Tower[] towers = FindObjectsByType<Tower>(FindObjectsSortMode.None);
        foreach (Tower t in towers)
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
        if(taxes[taxIndex] * towers.Count > money)
        {
            return;
        }

        //hide tithe button
        titheUI.SetActive(false);
        Debug.Log("tithing...");
        money -= taxes[taxIndex] * towers.Count;
        alreadyTithed = true;
    }

    public void DontTithe()
    {
        //hide tithe button
        titheUI.SetActive(false);
    }

    public void ChangeTimeScale(float num)
    {
        if(Time.timeScale == num)
        {
            Time.timeScale = 2;
        }
        else
        {
            Time.timeScale = num;
        }
        if (num == 0)
        {
            pauseMenu.SetActive(Time.timeScale == num);
        }
    }

    public void ShowDebug()
    {
        debugMenu.SetActive(!debugMenu.activeInHierarchy);
    }

    public void MyDebugHealth(float amount)
    {
        health += amount;
    }

    public void MyDebugMoney(float amount)
    {
        money += amount;
    }

    public void ChangeSpeedButtonColor(Button myButton)
    {
        if (Time.timeScale == 1)
        {
            myButton.GetComponentInChildren<Image>().color = Color.white;
        }
        else
        {
            myButton.GetComponentInChildren<Image>().color = Color.gray;
        }
    }

    public string GetTowerDisplayText(TowerSO scriptVals)
    {
        string displayText = "";
        displayText += scriptVals.towerType + " Tower \n";
        //damage display
        displayText += "DMG: ";
        for(int i = 0; i < 5; i++)
        {
            if(scriptVals.damage > 20 * i)
            {
                displayText += "\u2605";
            }
            else
            {
                displayText += "\u2606";
            }
        }
        displayText += "\n";
        displayText += "Speed: ";// + scriptVals.cooldown;
        //ATK cooldown
        if(scriptVals.cooldown > 3)
        {
            displayText += " fast\n";
        }
        else if(scriptVals.cooldown > 1)
        {
            displayText += " medium\n";
        }
        else
        {
            displayText += " slow\n";
        }
        //display effect
        if (scriptVals.effect != "" && scriptVals.effect != null)
        {
            
            if (scriptVals.effect == "decay")
            {
                displayText += "Effect DMG: ";
                
            }
            else if (scriptVals.effect == "slow")
            {
                displayText += "Effect Slow: ";
            }

            for (int i = 0; i < 5; i++)
            {
                if (scriptVals.effectNum > 20 * i)
                {
                    displayText += "\u2605";
                }
                else
                {
                    displayText += "\u2606";
                }
            }
            displayText += "\n";
        }
        
        displayText += scriptVals.description;
        return displayText;

        /*
         * Decay Tower
DMG: ?????
Attack CD: Average
Effect DMG: ?????
This tower applies the Decay effect to hit enemies, dealing 10>40>80 damage every second it’s applied at level 1>2>3.
         */
    }

    public RuntimeAnimatorController getHitAnimController(string hitType)
    {
        if(hitType == "decay")
        {
            return hitAnimControllers[0];
        }
        else if (hitType == "slow")
        {
            return hitAnimControllers[1];
        }
        else
        {
            return hitAnimControllers[2];
        }
    }

    public AudioResource getHitARC(string hitType)
    {
        if (hitType == "decay")
        {
            return hitARCs[0];
        }
        else if (hitType == "slow")
        {
            return hitARCs[1];
        }
        else
        {
            return hitARCs[2];
        }
    }



}
