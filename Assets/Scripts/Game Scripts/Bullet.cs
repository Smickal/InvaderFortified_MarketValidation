using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isAOE = false;
    public float aoeRange = 2f;
    public LayerMask enemyMask;
    private Transform target;
    
    Rigidbody2D bulletRigid;
    float speed;
    float bulletDamage;

    Vector2 getStartingPos;

    [SerializeField] GameObject explosionVFX;


    public void Seek(Transform _target)
    {
        target = _target;
    }

    public void SetAtrributes(float _damage, float _speed)
    {
        bulletDamage = _damage;
        speed = _speed;
    }

    private void Start() 
    {
        bulletRigid = GetComponent<Rigidbody2D>();
        getStartingPos = transform.position;
    }

    
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        BulletTravel();
    }

    void BulletTravel()
    {

        transform.position += transform.up * speed * Time.deltaTime;
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            //check health
            Enemy enemy = collision.GetComponent<Enemy>();
            //Reduce health
            if(!isAOE)
            {
                enemy.TakeDamage(bulletDamage);
            }
            else
            {
                Collider2D[] col = Physics2D.OverlapCircleAll(collision.transform.position, aoeRange, enemyMask);
                foreach(Collider2D enem in col)
                {
                    enem.GetComponent<Enemy>().TakeDamage(bulletDamage);
                }
                GameObject obj = Instantiate(explosionVFX, enemy.transform.localPosition, Quaternion.identity);
                Destroy(obj, 2f);
            }
            Destroy(gameObject);
        }

        if (collision.tag == "MenuEnemy")
        {
            GameObject enemy = collision.GetComponent<GameObject>();
            Destroy(enemy.gameObject);
        }
    }
}
