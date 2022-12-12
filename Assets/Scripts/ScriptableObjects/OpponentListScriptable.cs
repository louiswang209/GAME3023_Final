using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_OpponentList", menuName = "ScriptableObjects/OpponentScriptables/OpponentList", order = 0)]
public class OpponentListScriptable : ScriptableObject
{
    public List<OpponentScriptable> opponentList;

    public OpponentScriptable RandomOpponent()
    {
        return opponentList[Random.Range(0, opponentList.Count - 2)];
    }
}
