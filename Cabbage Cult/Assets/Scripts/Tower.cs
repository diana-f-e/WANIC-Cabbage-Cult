using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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
    public LineRenderer attackLine;

    public float effectNum;
    public float effectCooldown;

    public GameObject infoDisplay;

    private Color storedColor;
    private bool showingInfo = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerCounter = cooldown;
        infoDisplay = gameManager.towerInfoDisplay;
        assignScriptVals();
        attackingCollider.gameObject.GetComponent<SpriteRenderer>().enabled = false;
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
        //show stats when moused over
        if(gameManager.clickedObj == gameObject)
        {
            if(!showingInfo)
            {
                showInfo(); 
            }
        }
        else
        {
            if (showingInfo)
            {
                hideInfo();
            }
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
        attackingCollider.gameObject.GetComponent<SpriteRenderer>().size = attackingCollider.gameObject.GetComponent<CircleCollider2D>().bounds.size;
        GetComponent<Animator>().runtimeAnimatorController = scriptVals.runtimeAnimator;
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
        //Debug.Log("trying to play animation: " + scriptVals.towerType + "Atk");

        GetComponent<Animator>().Play(scriptVals.towerType + "Atk", 0);
        AnimateAttack(target);
        //Debug.Log("pew pew");

    }

    public void AnimateAttack(Enemy target)
    {
        // draw line between tower and target
        if (towerType == "laser")
        {
            attackLine.gameObject.SetActive(true);
            //Debug.Log("laser pew pew");
            attackLine.positionCount = 2;

            //attack from eye
            Vector3 laserOrigin = transform.position;
            float xdiff = 0;
            float ydiff = 0;
            if (level == 1)
            {
                xdiff = 0.2f;//0.54f;
                ydiff = 0.524f;//0.35f;
            }
            //if enemy is on tower's right
            if (target.gameObject.transform.position.x > transform.position.x)
            {
                laserOrigin.x += xdiff;
            }
            else
            {
                laserOrigin.x -= xdiff;
            }
            laserOrigin.y += ydiff;

            attackLine.SetPosition(0, laserOrigin);
            attackLine.SetPosition(1, target.gameObject.transform.position);
        }
        //flip sprite to face enemy
        if (target.gameObject.transform.position.x > transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void MergeSelect()
    {
        storedColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(2, 2, 2);
        //GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.9f);
    }

    public void MergeDeselect()
    {
        if(storedColor != null)
        {
            GetComponent<SpriteRenderer>().color = storedColor;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        //new Color(0.2f, 0.2f, 0.4f);
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

    /*old
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("gumba OnPointerEnter");
        showInfo();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("gumba OnPointerExit");
        hideInfo();
    }*/

    public void showInfo()
    {
        infoDisplay.GetComponentInChildren<TextMeshProUGUI>().text = gameManager.GetTowerDisplayText(scriptVals);
        infoDisplay.SetActive(true);
        attackingCollider.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        showingInfo = true;
    }

    public void hideInfo()
    {
        infoDisplay.SetActive(false);
        attackingCollider.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        showingInfo = false;
    }

}
