using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDamage 
{
    public bool TakeDamage(Pokemon attacker, AbilityObject ability, out bool isCritical, out float effectiveness);
    
}
