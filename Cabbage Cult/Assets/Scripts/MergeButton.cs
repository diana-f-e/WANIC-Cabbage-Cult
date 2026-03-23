using UnityEngine;

public class MergeButton : MonoBehaviour
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
            gameManager.heldObj = Instantiate(towerItem);
            foreach (Tower t in gameManager.mergeList)
            {
                Destroy(t.gameObject);
            }
            gameManager.mergeList.Clear();
        }
    }

    private GameObject GetUpgradedTower()
    {
        //TODO return the correct tower based on gamemanager type and level
        return towerItem;
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
