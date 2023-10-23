using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(HumanoidAnimator))]
[RequireComponent(typeof(NavMeshAgent))]
public class Character : Entity, IMoveable, IDamageable, ISelectable
{
    [SerializeField] private int hitPoints = 25;
    [SerializeField] private float interactionRadius = .3f;

    private NavMeshAgent _navMesh;
    private HumanoidAnimator _entityAnimator;

    private void Start()
    {
        _entityAnimator = GetComponent<HumanoidAnimator>();
        _navMesh = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        int currentState = _entityAnimator.GetCurrentState();
        if (currentState == _entityAnimator.Run && DestinationReached())
        {
            _entityAnimator.PlayAnimation(_entityAnimator.Idle);
        }
    }

    public void Move(Vector3 destination)
    {
        if (_navMesh == null) return;
        
        _entityAnimator.PlayAnimation(_entityAnimator.Run);

        if (DestinationReachable(destination) == false)
        {
            Vector3 newDestination = GetClosestValidPosition(destination);
            _navMesh.SetDestination(newDestination);
            return;
        }

        _navMesh.SetDestination(destination);
    }

    public void TakeDamage(int damageAmount)
    {
        if (hitPoints <= 0) return;

        hitPoints -= damageAmount;

        if (hitPoints <= 0)
        {
            Die();
        }
    }

    public Entity GetCharacter()
    {
        return this;
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " is dead!");
    }

    private bool DestinationReached()
    {
        return _navMesh.remainingDistance <= _navMesh.stoppingDistance;
    }

    private bool DestinationReachable(Vector3 destination)
    {
        NavMeshPath path = new NavMeshPath();
        if (_navMesh.CalculatePath(destination, path))
        {
            return path.status == NavMeshPathStatus.PathComplete;
        }

        return false;
    }

    private Vector3 GetClosestValidPosition(Vector3 destination)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(destination, out hit, 5f, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
