using UnityEngine;

public class TowerItem : MonoBehaviour
{
    //after a shop icon is clicked on, the corresponding tower item will be put in the players hand. 
    public bool validPlacement;
    public GameObject tower;
    public TowerSO towerScriptVals;

    private Color invalidSpotColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = towerScriptVals.towerColor;
        invalidSpotColor = new Color(0.4f, 0.4f, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO change to Tower once placing tower works
        if(collision.gameObject.GetComponent<Tower>() == null && collision.gameObject.tag != "BuildBlocker")
        {
            return;
        }
        validPlacement = false;
        GetComponent<SpriteRenderer>().color = invalidSpotColor;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //TODO change to Tower once placing tower works
        if (collision.gameObject.GetComponent<Tower>() == null && collision.gameObject.tag != "BuildBlocker")
        {
            return;
        }
        validPlacement = false;
        GetComponent<SpriteRenderer>().color = invalidSpotColor;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Tower>() == null && collision.gameObject.tag != "BuildBlocker")
        {
            return;
        }
        validPlacement = true;
        GetComponent<SpriteRenderer>().color = towerScriptVals.towerColor;
    }
}


