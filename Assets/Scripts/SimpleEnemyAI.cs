using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;
    [SerializeField] LayerMask whatIsGround, whatIsPlayer, whatIsObstacle;

    Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField] float walkPointRange;

    [SerializeField] float timeBetweenAttacks;
    bool alreadyAttack;

    [SerializeField] float attackRange;
    [SerializeField] bool playerIsInAttackRange;
    bool walkDirection = false;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerIsInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerIsInAttackRange) Patroling();
        if (playerIsInAttackRange) AttackPlayer();
    }

    void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet) agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    void SearchWalkPoint()
    {
        float positionX = walkDirection ? walkPointRange : -walkPointRange;
        walkPoint = new Vector3(transform.position.x + positionX, transform.position.y, transform.position.z);
        if(Physics.CheckSphere(walkPoint, walkPointRange, whatIsObstacle))
        {
            walkDirection = !walkDirection;
            return;
        }
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        if (!alreadyAttack)
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
