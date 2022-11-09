using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject spriteObj;
    [SerializeField] float turnMultiplier = 1.5f;
    [SerializeField] Transform attPoint;

    Vector3 target;
    NavMeshAgent agent;
    Enemy enemy;
    bool isProvoked = false;
    

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
        

    }

    void Start()
    {
        target = enemy.GetTargetPos();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isProvoked)
        {
            agent.SetDestination(target);
        }
    }

    void RotateTowardsTarget()
    {
        Vector3 direction = target - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spriteObj.transform.rotation = Quaternion.Lerp(spriteObj.transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * turnMultiplier);
    }

    
   


    public void ChaseTarget()
    {
        isProvoked = false;
    }

    public void SetToNewTarget(Vector3 temp)
    {
        isProvoked=true;
        agent.SetDestination(temp);
        
        
    }

}
