using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    /// <summary>
    /// 요소들이 받는 피해
    /// </summary>
    /// <param name="damage">받을 피해</param>
    public abstract void OnDamaged(int damage);
}
