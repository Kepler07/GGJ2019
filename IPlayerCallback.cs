using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Analytics;

public interface IPlayerCallback
{
    void onMonsterAdded(List<GameObject> newMonsterList);
}