using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Opponent", menuName = "ScriptableObjects/OpponentScriptables/Opponent", order = 1)]
public class OpponentScriptable : ScriptableObject
{
    public string oppName;
    [Tooltip("Enemy health is randomized between minHealth and 40")]
    public int minHealth;
    public Sprite oppSprite;
}
