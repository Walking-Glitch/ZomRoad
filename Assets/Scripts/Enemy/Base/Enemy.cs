using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, ITriggerCheckable
{
    [SerializeField] public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
    public bool IsInDetectionArea { get; set; }
    public bool IsInAttackArea { get; set; }

    #region State Machine Variables

    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    #endregion

    private void Awake()
    {
        StateMachine = new EnemyStateMachine();

        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
    }
    private void Start()
    {
        CurrentHealth = MaxHealth;

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.currentEnemyState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.currentEnemyState.PhysicsUpdate();
    }
    public void Damage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }


   
    public void SetIsInDetectionArea(bool isInDetectionArea)
    {
        IsInDetectionArea = isInDetectionArea;
    }

    public void SetIsInAttackArea(bool isInAttackArea)
    {
        IsInAttackArea = isInAttackArea;
    }
}
