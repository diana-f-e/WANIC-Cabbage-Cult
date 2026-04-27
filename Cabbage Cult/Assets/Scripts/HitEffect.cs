/****************************************************************************
* File Name: HitEffect.cs
* Author: Diana Everman
* DigiPen Email: diana.everman@digipen.edu
* Course: Video Game Programming 1
*
* Description: This file is to be attached to a hitEffect object that spawns
*               on top of an enemy when it is attacked.
*
****************************************************************************/
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Audio;

public class HitEffect : MonoBehaviour
{
    public Transform myEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        //follow enemy
        if (myEnemy != null)
        {
            transform.position = myEnemy.position;
        }
        
        //if animation done, die
        if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99)
        {
            Destroy(gameObject);
        }
    }
}
