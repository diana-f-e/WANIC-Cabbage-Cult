using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
    public LineRenderer attackLine;

    public float effectNum;
    public float effectCooldown;

    public GameObject infoDisplay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerCounter = cooldown;
        infoDisplay = gameManager.towerInfoDisplay;
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
        //animation lasts .1 of the cooldown
        if(timerCounter <= cooldown * 0.9)
        {
            attackLine.gameObject.SetActive(false);
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
        effectNum = scriptVals.effectNum;
        effectCooldown = scriptVals.effectCooldown;
        if (scriptVals.skin != null)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = scriptVals.skin;
        }
    }


    public void Attack()
    {
        if (enemiesInRange.Count <= 0)
        {
            return;
        }
        Enemy target = enemiesInRange[0];
        if (effect == "decay")
        {
            Debug.Log("effect == \"decay\"");
            foreach(Enemy e in enemiesInRange)
            {
                if(!e.decaying)
                {
                    target = e;
                    break;
                }
            }
        }

        if(effectNum == 0 || effectCooldown == 0)
        {
            target.Damage(damage, effect);
        }
        else
        {
            target.Damage(damage, effect, effectNum, effectCooldown);
        }
        GetComponent<AudioSource>().PlayOneShot(onAttack);
        AnimateAttack(target);
        //Debug.Log("pew pew");

    }

    public void AnimateAttack(Enemy target)
    {
        // draw line between tower and target
        if (towerType == "laser")
        {
            attackLine.gameObject.SetActive(true);
            Debug.Log("laser pew pew");
            attackLine.positionCount = 2;

            attackLine.SetPosition(0, transform.position);
            attackLine.SetPosition(1, target.gameObject.transform.position);
        }
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

    public void ApplyCurse(CurseSO curse)
    {
        GetComponent<SpriteRenderer>().color = curse.curseColor;
        if (curse.cooldown != 0)
        {
            cooldown *= curse.cooldown;
        }
        if (curse.damage != 0)
        {
            damage = (int)(curse.damage * damage);
        }
        if (curse.attackRadius != 0)
        {
            attackingCollider.radius *= curse.attackRadius;
            attackingCollider.gameObject.GetComponent<SpriteRenderer>().size = attackingCollider.gameObject.GetComponent<CircleCollider2D>().bounds.size;
        }
        if (curse.effectNum != 0)
        {
            effectNum *= curse.effectNum;
        }
        if (curse.effectCooldown != 0)
        {
            effectCooldown *= curse.effectCooldown;
        }
        cursed = true;
        /*
        towerLevel;
    cooldown;
    damage;
    attackRadius;
    effectNum;
    effectCooldown;*/

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("gumba OnPointerEnter");
        infoDisplay.GetComponentInChildren<TextMeshProUGUI>().text = gameManager.GetTowerDisplayText(scriptVals);
        infoDisplay.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("gumba OnPointerExit");
        infoDisplay.SetActive(false);
    }

}
