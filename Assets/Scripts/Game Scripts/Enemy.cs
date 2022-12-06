using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [Header("Enemy Attributes")]
    public float enemySpeed = 10f;
    public float enemyHealth = 0;
    public float enemyRange = 1f;
    public float detectRange = 5f;
    public float attackDelay = 2f;
    private float currentEnemySpeed;
    public float enemyDamage = 5;
    private bool isAttacking = false;

    [Header("Setups")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Animator slimeAnimator;
    [SerializeField] private Transform attPoint;
    [SerializeField] LayerMask mask;
    WaveSpawner waveSpawner;

    ExtraMainTowerAttributes extraMainTowerAttributes;
    Vector3 target;
    int maxHealth;
    bool isProvoke = false;
    GameObject attacker;
    SpriteRenderer sprite;

    Vector3 attPointLeft, attPointRight;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Target").GetComponent<Transform>().position;
        sprite = GetComponentInChildren<SpriteRenderer>();

        Vector3 temp = attPoint.localPosition;

        attPointRight = temp;
        attPointLeft = new Vector3(-temp.x, temp.y, temp.z);
    }
    private void Start()
    {
        extraMainTowerAttributes = FindObjectOfType<ExtraMainTowerAttributes>();
        waveSpawner = FindObjectOfType<WaveSpawner>();
        maxHealth = (int)enemyHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        currentEnemySpeed = enemySpeed;
    }

    void UpdateEnemyTarget(GameObject attacker)
    {
        isProvoke = true;
        this.attacker = attacker;
        
    }

    void Update()
    {
        HealthBarDisplay();

        CheckPositionToTarget();

        FaceTheTarget();
        if (isProvoke)
        {
            if (attacker == null)
            {
                isProvoke = false;
                return;
            }
            
            //Debug.Log( "DistanceTOtarget: "+ Vector3.Distance(attacker.transform.position, transform.position) + "Ydistance: "+ Mathf.Abs(attacker.transform.position.y - transform.position.y));
            if (Vector3.Distance(attacker.transform.position, transform.position) >= 1f && Mathf.Abs(attacker.transform.position.y - transform.position.y) >= 0.2f)
            {
                GetComponent<EnemyMovement>().SetToNewTarget(attacker.transform.position);
            }
            else
            {
                Attack();
            }
        }
        else
        {
            GetComponent<EnemyMovement>().ChaseTarget();
            slimeAnimator.SetBool("IsAttacking", false);

        }
    }

    private void CheckPositionToTarget()
    {
        if (Vector3.Distance(transform.position, target) <= 0.5f)
        {
            waveSpawner.getCurrentEnemiesInScreen--;
            extraMainTowerAttributes.ReduceMainTowerHp(enemyDamage);
            Destroy(gameObject);
        }
    }

    void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            StartCoroutine(DelayAttacking());
        }
    }

    IEnumerator DelayAttacking()
    {
        yield return new WaitForSeconds(attackDelay);
        if (attacker == null) yield break;
        slimeAnimator.SetBool("IsAttacking", true);
        Collider2D col = Physics2D.OverlapCircle(attPoint.transform.position, enemyRange, mask);
        if (col != null)
        {
            col.GetComponent<Turrets>().TakeDamage(enemyDamage);
            Debug.Log("attacked");
        }
        isAttacking = false;
    }

    void HealthBarDisplay()
    {
        healthSlider.value = enemyHealth;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attPoint.transform.position, enemyRange);
        Gizmos.color = Color.red;
    }

    public Vector3 GetTargetPos()
    {
        return target;
    }

    public void PROVOKED()
    {
        isProvoke = true;
    }

    public void TakeDamage(float damage, GameObject attacker)
    {
        enemyHealth -= damage;
        UpdateEnemyTarget(attacker);
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);

            waveSpawner.getCurrentEnemiesInScreen--;
            return;
        }
    }

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            waveSpawner.ReduceCurrentEnemy();
            Destroy(gameObject);
            return;
        }
    }

    public void FaceTheTarget()
    {
        Vector3 temp;
        if(attacker != null)
        {
            temp = attacker.transform.position;
        }
        else
        {
            temp = target;
        }
        Vector3 distance = temp - transform.position;

        if (distance.x < 0f)
        {
            sprite.flipX = true;
            attPoint.transform.localPosition = attPointLeft;
        }
        else
        {
            sprite.flipX = false;
            attPoint.transform.localPosition = attPointRight;
        }

    }


}
