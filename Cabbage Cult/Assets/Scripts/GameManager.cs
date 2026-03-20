using Unity.Burst.CompilerServices;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject heldObj;
    private GameObject clickedObj;
    public Transform shopBorder;
    public float health;
    public float money;
    public TextMeshProUGUI statsText;

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
                        placeTower(mousePosition, heldObj.GetComponent<TowerItem>().tower);
                        heldObj = null;
                }
                    
            }
            else
            {
                
                //shop icon?
                //grab it
                //merge mode?
                //toggle it
            }


        }
    }

    private void placeTower(Vector3 coords, GameObject tower)
    {
        //at the point im given, put a tower
        GameObject placedTower = Instantiate(tower, coords, Quaternion.identity);
        //placedTower.GetComponent<Tower>().placingCollider.gameObject.SetActive(true);
        //placedTower.GetComponent<Tower>().attackingCollider.gameObject.SetActive(true);
    }

    private bool validTowerSpot(Vector3 coords, GameObject tower)
    {
        if (tower.GetComponent<Tower>() == null)
        {
            Debug.Log("heldobj not a tower");
            return false;
        }
        //if not on path
        //if not too close to other towers (i didnt click a collider of another tower)
        if(clickedObj != null)
        {
            return false;
        }
        return true;
    }





}
