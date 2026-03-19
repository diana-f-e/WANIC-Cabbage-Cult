using UnityEngine;

public class TowerItem : MonoBehaviour
{
    //after a shop icon is clicked on, the corresponding tower item will be put in the players hand. 
    public bool validPlacement;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO change to Tower once placing tower works
        if(collision.gameObject.GetComponent<TowerItem>() == null)
        {
            return;
        }
        validPlacement = false;
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TowerItem>() == null)
        {
            return;
        }
        validPlacement = true;
        GetComponent<SpriteRenderer>().color = Color.green;
    }
}


