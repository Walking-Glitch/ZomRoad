using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorStateMachine 
{
   public SurvivorState currentSurvivorState { get; set; }

   public void Initialize(SurvivorState startingState)
   {
       currentSurvivorState = startingState;
       currentSurvivorState.EnterState();
   }

   public void ChangeState(SurvivorState newState)
   {
        currentSurvivorState.ExitState();
        currentSurvivorState = newState;
        currentSurvivorState.EnterState();
   }

}
