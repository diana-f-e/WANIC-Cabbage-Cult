using UnityEngine;
using UnityEngine.EventSystems;

public class TowerInfoDisplayHider : MonoBehaviour, IPointerExitHandler
{
    public GameObject infoDisplay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("AA");
        //infoDisplay.SetActive(false);
    }
}
