using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerSO scriptVals;
    public CircleCollider2D attackingCollider;

    public float cooldown; // cooldown in seconds
    private float timerCounter;
    public List<Enemy> enemiesInRange = new List<Enemy>();
    public int damage;
    public int level;

    public string effect;

    public string towerType;

    public AudioClip onAttack;
    public GameManager gameManager;

    public bool cursed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerCounter = cooldown;

        assignScriptVals();
    }

    //update in scene view when changed
    private void OnValidate()
    {
        assignScriptVals();


    }

    // Update is called once per frame
    void Update()
    {
        timerCounter -= Time.deltaTime;
        if (timerCounter <= 0)
        {
            //detect enemy in radius
            Attack();
            timerCounter = cooldown;
        }
    }

    public void assignScriptVals()
    {
        //update vals based on scriptable object
        towerType = scriptVals.towerType;
        cooldown = scriptVals.cooldown;
        damage = scriptVals.damage;
        level = scriptVals.towerLevel;
        attackingCollider.radius = scriptVals.attackRadius;
        effect = scriptVals.effect;
        GetComponent<SpriteRenderer>().color = scriptVals.towerColor;
        onAttack = scriptVals.onAttack;
        gameObject.GetComponent<SpriteRenderer>().sprite = scriptVals.skin;
    }


    public void Attack()
    {
        if (enemiesInRange.Count <= 0)
        {
            return;
        }
        if(scriptVals.effectNum == 0 || scriptVals.effectCooldown == 0)
        {
            enemiesInRange[0].Damage(damage, effect);
        }
        else
        {
            enemiesInRange[0].Damage(damage, effect, scriptVals.effectNum, scriptVals.effectCooldown);
        }
        gameManager.audioSource.PlayOneShot(onAttack);
        //Debug.Log("pew pew");

    }

    public void MergeSelect()
    {
        GetComponent<SpriteRenderer>().color += new Color(0.2f, 0.2f, 0.4f);
        //GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.9f);
    }

    public void MergeDeselect()
    {
        GetComponent<SpriteRenderer>().color -= new Color(0.2f, 0.2f, 0.4f);
        //GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.6f, 0.9f);
    }
}
