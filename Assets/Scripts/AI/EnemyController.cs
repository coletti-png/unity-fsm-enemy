using UnityEngine;
using TMPro;

public class EnemyController : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform[] patrolPoints;
    public TMP_Text stateText;

    [Header("Movement")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;

    [Header("Perception")]
    public float detectionRange = 6f;
    public LayerMask obstacleMask;

    public StateMachine stateMachine;

    // States
    [HideInInspector] public IdleState idleState;
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public ChaseState chaseState;

    private void Awake()
    {
        stateMachine = new StateMachine();

        idleState = new IdleState(this);
        patrolState = new PatrolState(this);
        chaseState = new ChaseState(this);
    }

    void Start()
    {
        stateMachine.ChangeState(idleState);
    }

    void Update()
    {
        stateMachine.Tick();
    }

    public bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, player.position) <= detectionRange;
    }

    public bool HasLineOfSight()
    {
        Vector3 dir = (player.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, dir, out RaycastHit hit, detectionRange))
        {
            return hit.transform == player;
        }

        return false;
    }

    public void MoveTowards(Vector3 target, float speed)
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    public void UpdateStateText(string stateName)
    {
        if (stateText != null)
            stateText.text = stateName;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
