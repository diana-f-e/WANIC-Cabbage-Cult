using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class fontChanger : MonoBehaviour
{
    public TMP_FontAsset myFont;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
        foreach(TextMeshProUGUI t in FindObjectsByType<TextMeshProUGUI>(FindObjectsSortMode.None))
        {
            t.font = myFont;
        }
    }
}
