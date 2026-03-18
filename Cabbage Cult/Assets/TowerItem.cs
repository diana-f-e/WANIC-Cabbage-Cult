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
        Debug.Log("OnTriggerEnter2D");
        validPlacement = false;
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("OnTriggerExit2D");
        validPlacement = true;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}


