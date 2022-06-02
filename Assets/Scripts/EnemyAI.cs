using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;
    [SerializeField] LayerMask whatIsGround, whatIsPlayer, whatIsObstacle;

    Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField] float walkPointRange;

    [SerializeField] float timeBetweenAttacks;
    bool alreadyAttack;

    [SerializeField] float sightRange, attackRange;
    [SerializeField] bool playerIsInSightRange, playerIsInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerIsInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerIsInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerIsInSightRange && !playerIsInAttackRange) Patroling();
        if (playerIsInSightRange && !playerIsInAttackRange) ChasePlayer();
        if (playerIsInAttackRange) AttackPlayer();
    }

    void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet) agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
        if (Physics.CheckSphere(transform.position, 1.1f, whatIsObstacle))
        {
            walkPointSet = false;
            Debug.Log("Reseting walking point because a collision happened");
        }
            
    }

    void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z);
        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
        if (Physics.CheckSphere(transform.position, 1.1f, whatIsObstacle))
        {
            agent.SetDestination(transform.position);
            walkPointSet = false;
            Patroling();
            Debug.Log("Reseting chasing because a collision happened");
        }
    }

    void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        if(!alreadyAttack)
        {
            player.GetComponent<PlayerController>().TakeDamage();
            alreadyAttack = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttack = false;
    }
}
