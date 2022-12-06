using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class Turrets : MonoBehaviour
{
    Transform target;
    [Header("Turret Attributes")]
    public bool isWalkable = false;
    public bool isRotate = false;
    public bool isRocketLauncher = false;
    public bool isBrawler = false;
    public string turretName;
    public float turretHP = 10f;
    public float turretRange = 15f;
    public float meleeRange = 2f;
    public float turretTurnSpeed = 10f;
    public float fireRate = 1f;
    private float fireCountDown = 0f;
    [SerializeField] float minRotation = -180f;
    [SerializeField] float maxRotation = 0f;


    [Header("Minions")]
    public float turretDamage = 10f;
    public float bulletSpeed = 5f;

    [Header("Setups")]
    public string enemyTag = ("Enemy");
    public GameObject bulletPrefab;
    public Transform gunBarrel;
    [SerializeField] BoxCollider2D col;
    [SerializeField] LayerMask enemyLayer;

    SpriteRenderer[] sprite;

    NavMeshAgent agent;
    Animator anim;
    Vector3 gunBarrelRightPos, gunBarrelLeftPos;
    private void Awake()
    {
        if (isWalkable)
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
       
        }
        else
        {
            if(!isRotate)
                transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            else
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }

        sprite = GetComponentsInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();

        Vector3 temp = gunBarrel.localPosition;

        gunBarrelRightPos = temp;
        if (!isBrawler)
        {
            gunBarrelLeftPos = new Vector3(temp.x, -temp.y, temp.z);
        }
        else
        {
            gunBarrelLeftPos = new Vector3(-temp.x, temp.y, temp.z);
        }
    }

    void Start()
    {
        
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        SetAttributes();
    }

    void UpdateTarget()
    {
        GameObject[] enemies;
        if (SceneManager.GetActiveScene().buildIndex == 1)
            enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        else
            enemies = GameObject.FindGameObjectsWithTag("MenuEnemy");
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= turretRange)
        {
            target = nearestEnemy.transform;

            Vector2 dir = target.transform.position - transform.position;
            float rotation = MathF.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if ( (rotation >= maxRotation || rotation < minRotation) && isRotate)
            {
                target = null;
            }
        }
        else
        {
            target = null;
        }

    }
    void Update()
    {
        UpdateTarget();
        if(!isWalkable)
        {
            ProcessNormalTurret();
        }
        else
        {
            ProcessBrawlTurret();
        }
    }

    private void ProcessBrawlTurret()
    {
        //no target? Go home
        if (target == null)
        {
            Vector3 homeTarget = GetComponentInParent<SpawnMinions>().transform.position;
            if (Vector3.Distance(transform.position, homeTarget) >= 1f)
                agent.SetDestination(homeTarget);
        }
        else
        {
            FaceTheTarget();
            if (Vector3.Distance(transform.position, target.position) >= 1.2f && Mathf.Abs(target.position.y - transform.position.y) >= 0.2f)
            {
                agent.SetDestination(target.position);
                //Debug.Log(Vector3.Distance(transform.position, target.position));              
            }
            else
            {
                if (fireCountDown <= 0)
                {
                    Attack();
                    fireCountDown = 1f / fireRate;
                }
                fireCountDown -= Time.deltaTime;
            }
        }
    }

    void ProcessNormalTurret()
    {
        if (target == null)
        {
            if(!isRocketLauncher || !isBrawler)
                anim.SetBool("IsAttacking", false);
            return;
        }
        else
        {
            if (!isRocketLauncher || !isBrawler)
                anim.SetBool("IsAttacking", true);
        }
        //RotateTowardsTarget();
        
        if(!isRotate)
        {
            FaceTheTarget();
            RotateGunTowardsTarget();
        }
        else
        {
            RotateTowardsTarget();
        }
        UpdateTarget();
        if (fireCountDown <= 0)
        {
            Shoot();
            fireCountDown = 1f / fireRate;
        }
        fireCountDown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation) as GameObject;
        Bullet bullet = newBullet.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
            bullet.SetAtrributes(turretDamage, bulletSpeed);
        }
    }

    void Attack()
    {
        Collider2D[] enemyCol = Physics2D.OverlapCircleAll(gunBarrel.position, meleeRange, enemyLayer);

        foreach(Collider2D enem in enemyCol)
        {
            Enemy enemy = enem.GetComponent<Enemy>();
            enemy.TakeDamage(turretDamage, this.gameObject);

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, turretRange);
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(gunBarrel.position, meleeRange);

    }

    void RotateTowardsTarget()
    {
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Clamp(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, minRotation, maxRotation);      
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * turretTurnSpeed);
    }

    void RotateGunTowardsTarget()
    {
        Vector3 direction = target.position - gunBarrel.transform.position;
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
        gunBarrel.transform.rotation = Quaternion.Lerp(gunBarrel.transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * turretTurnSpeed); 
    }

    void FaceTheTarget()
    {
        Vector3 distance = target.position - transform.position;
        
        if (distance.x < 0f)
        {
            if(!isBrawler)
                this.transform.localScale = new Vector3(transform.localScale.x, -1, transform.localScale.z);
            else
                this.transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            if(!isBrawler)
                this.transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
            else
                this.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        
    }

    void SetAttributes()
    {
        if (GetComponentInParent<SpawnMinions>() == null) return;
        SpawnMinions spawnMinions = GetComponentInParent<SpawnMinions>();
        this.turretHP = spawnMinions.turretHP;
        this.turretDamage = spawnMinions.turretDamage;
        this.turretRange = spawnMinions.turretRange;
    }

    public void TakeDamage(float dmg)
    {
        turretHP -= dmg;
        if(turretHP <= 0f)
        {
            GetComponentInParent<SpawnMinions>().ReduceCurrentTower();
            Destroy(gameObject);
        }
    }

    public void SetClamp(float min, float max)
    {
        minRotation = min;
        maxRotation = max;
    }
}
