using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bonus damage type ability effect scriptable object
/// </summary>
[CreateAssetMenu(fileName = "BonusDamageEffect", menuName = "ScriptableObjects/ActionScriptables/BonusDamageEffect", order = 4)]
public class BonusDamageEffectScriptable : ActionEffectScriptable
{
    public int damage;
    public int chance;

    public override void ApplyEffect(PartyDetails user, PartyDetails opponent)
    {
        switch (target)
        {
            case Target.User:
                if(Random.Range(0, 100) <= chance)
                    user.ModifyHealth(damage);
                break;
            case Target.Opponent:
                if (Random.Range(0, 100) <= chance)
                    opponent.ModifyHealth(damage);
                break;
        }
    }
}
