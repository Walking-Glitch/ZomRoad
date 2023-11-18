using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivor : MonoBehaviour, IDamageable, ITriggerCheckable
{
    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
    public bool IsInDetectionArea { get; set; }
    public bool IsInAttackArea { get; set; }

    public Animator animator;

    public AIPath aIPathScript;

    public AIDestinationSetter aIDestinationSetterScript;

    public Transform targetObject;

    #region State Machine Variables
    public SurvivorStateMachine StateMachine { get; set; }
    public SurvivorIdleState IdleState { get; set; }
    public SurvivorChaseState ChaseState { get; set; }
    public SurvivorAttackState AttackState { get; set; }
    #endregion

    #region ScriptableObject variables

    [SerializeField] private SurvivorIdleSOBase SurvivorIdleBase;
    [SerializeField] private SurvivorChaseSOBase SurvivorChaseBase;
    [SerializeField] private SurvivorAttackSOBase SurvivorAttackBase;

    public SurvivorIdleSOBase SurvivorIdleBaseInstance { get; set; }
    public SurvivorChaseSOBase SurvivorChaseBaseInstance { get; set; }
    public SurvivorAttackSOBase SurvivorAttackBaseInstance { get; set; }

    #endregion

    private void Awake()
    {
        SurvivorIdleBaseInstance = Instantiate(SurvivorIdleBase);
        SurvivorChaseBaseInstance = Instantiate(SurvivorChaseBase);
        SurvivorAttackBaseInstance = Instantiate(SurvivorAttackBase);

        StateMachine = new SurvivorStateMachine();

        IdleState = new SurvivorIdleState(this, StateMachine);
        ChaseState = new SurvivorChaseState(this, StateMachine);
        AttackState = new SurvivorAttackState(this, StateMachine);
    }
    void Start()
    {
        CurrentHealth = MaxHealth;

        SurvivorIdleBaseInstance.Initialize(gameObject, this);
        SurvivorChaseBaseInstance.Initialize(gameObject, this);
        SurvivorAttackBaseInstance.Initialize(gameObject, this);

        StateMachine.Initialize(IdleState);
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.currentSurvivorState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.currentSurvivorState.PhysicsUpdate();
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
        StateMachine.currentSurvivorState.AnimationTriggerEvent(triggerType);
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
