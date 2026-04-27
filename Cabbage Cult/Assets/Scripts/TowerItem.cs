/****************************************************************************
* File Name: TowerItem.cs
* Author: Diana Everman
* DigiPen Email: diana.everman@digipen.edu
* Course: Video Game Programming 1
*
* Description: This file is to be attached to a TowerItem for its functions.
*
****************************************************************************/
using UnityEngine;

public class TowerItem : MonoBehaviour
{
    //after a shop icon is clicked on, the corresponding tower item will be put in the players hand. 
    public bool validPlacement;
    public GameObject tower;
    public TowerSO towerScriptVals;

    public SpriteRenderer AttackPreview;

    private Color invalidSpotColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = towerScriptVals.towerColor;
        invalidSpotColor = new Color(0.4f, 0.4f, 0.4f);
        AttackPreview.size = new Vector2(2*towerScriptVals.attackRadius, 2*towerScriptVals.attackRadius);
        gameObject.GetComponent<SpriteRenderer>().sprite = towerScriptVals.skin;
        setRadius();
    }

    private void OnValidate()
    {
        setRadius();
    }

    //the radius of the placing collider is determined by the height of the sprite and the placeRadius in the 
    //scriptable object, with some leeway
    private void setRadius()
    {
        if (towerScriptVals.placeRadius != 0)
        { 
            gameObject.GetComponent<SpriteRenderer>().sprite = towerScriptVals.skin;
            gameObject.GetComponent<CircleCollider2D>().radius = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2;
            gameObject.GetComponent<CircleCollider2D>().radius *= towerScriptVals.placeRadius;
            gameObject.GetComponent<CircleCollider2D>().radius *= 1.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //if there is a tower or other obstacle within the placing range, the tower cannot be placed 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Tower>() == null && collision.gameObject.tag != "BuildBlocker")
        {
            return;
        }
        validPlacement = false;
        GetComponent<SpriteRenderer>().color = invalidSpotColor;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Tower>() == null && collision.gameObject.tag != "BuildBlocker")
        {
            return;
        }
        validPlacement = false;
        GetComponent<SpriteRenderer>().color = invalidSpotColor;
    }

    //if there is no tower or other obstacle within the placing range, the tower can be placed 
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


