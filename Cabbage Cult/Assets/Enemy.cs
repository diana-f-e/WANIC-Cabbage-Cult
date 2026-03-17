using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform myTF;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myTF = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        myTF.position += new Vector3(0.005f, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }

   
}
