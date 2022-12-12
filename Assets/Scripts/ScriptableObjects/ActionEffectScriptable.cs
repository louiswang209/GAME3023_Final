using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Target
{
    User,
    Opponent
}

/// <summary>
/// Base script for ability effect scriptable objects
/// </summary>
public class ActionEffectScriptable : ScriptableObject
{
    public Target target;

    public virtual void ApplyEffect(PartyDetails user, PartyDetails opponent)
    {
        
    }
}
