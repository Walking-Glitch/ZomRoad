using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, ITriggerCheckable
{
    [SerializeField] public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
    public bool IsInDetectionArea { get; set; }
    public bool IsInAttackArea { get; set; }

    public Animator animator;

    public AIPath aIPathScript;

    public AIDestinationSetter aIDestinationSetterScript;

    public Transform targetObject;
     

    #region State Machine Variables
    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    #endregion

    #region ScriptableObject variables

    [SerializeField] private EnemyIdleSOBase EnemyIdleBase;
    [SerializeField] private EnemyChaseSOBase EnemyChaseBase;
    [SerializeField] private EnemyAttackSOBase EnemyAttackBase;

    public EnemyIdleSOBase EnemyIdleBaseInstance { get; set; }
    public EnemyChaseSOBase EnemyChaseBaseInstance { get; set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; set; }

    #endregion

    private void Awake()
    {
        EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);
        EnemyChaseBaseInstance = Instantiate(EnemyChaseBase);
        EnemyAttackBaseInstance = Instantiate(EnemyAttackBase);

        StateMachine = new EnemyStateMachine();

        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
    }
    private void Start()
    {
        CurrentHealth = MaxHealth;

        EnemyIdleBaseInstance.Initialize(gameObject, this);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject,this);

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

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.currentEnemyState.AnimationTriggerEvent(triggerType);
    }

    public enum AnimationTriggerType
    {
         TriggerIdle1, TriggerIdle2,
         TriggerChase1, TriggerChase2,
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
