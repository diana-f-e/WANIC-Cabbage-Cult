using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* vars:
     * 
     * obj in hand: each shop icon needs a held version and placed version
     */
    public GameObject heldObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if smth in hand, track it to the mouse
        if(heldObj != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z += Camera.main.nearClipPlane;
            heldObj.transform.position = mousePosition;

        }
        if(Input.GetMouseButton(0))
        {
            //click
            //holding smth?
            if (heldObj != null)
            {   
                //valid spot?
                    //place it
            }

            //else
                //shop icon?
                    //grab it
                //merge mode?
                    //toggle it

        }
    }

    void placeTower(Vector3 coords, GameObject tower)
    {
        //at the point im given, put a tower
        GameObject placedTower = Instantiate(tower, coords, Quaternion.identity);
    }

    

}
