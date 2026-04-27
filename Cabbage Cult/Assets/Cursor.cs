using UnityEngine;
using UnityEngine.UI;

public class myCursor : MonoBehaviour
{
    public Sprite idle;
    public Sprite click;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            GetComponent<Image>().sprite = click;
        }
        if(Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            GetComponent<Image>().sprite = idle;
        }
    }
}
