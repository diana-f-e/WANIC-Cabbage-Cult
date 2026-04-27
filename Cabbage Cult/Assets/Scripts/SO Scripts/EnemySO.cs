/****************************************************************************
* File Name: EnemySO.cs
* Author: Diana Everman
* DigiPen Email: diana.everman@digipen.edu
* Course: Video Game Programming 1
*
* Description: A scriptable object made using this file provides the values
*              for an enemy type.
*
****************************************************************************/
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO")]
public class EnemySO : ScriptableObject
{
    public int enemyDamage;
    public int enemyHealth;
    public float enemySpeed;
    public int helmetHealth;
    public AudioResource onHurt;
    public Sprite skin;
    public RuntimeAnimatorController runtimeAnimator;
}
