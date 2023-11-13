using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerCheckable  
{
     bool IsInDetectionArea { get; set; }
     bool IsInAttackArea { get; set; }

     void SetIsInDetectionArea(bool isInDetectionArea);
     void SetIsInAttackArea(bool isInAttackArea);
}
