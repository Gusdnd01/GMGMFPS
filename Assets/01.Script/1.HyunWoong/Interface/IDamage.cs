using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    /// <summary>
    /// ��ҵ��� �޴� ����
    /// </summary>
    /// <param name="damage">���� ����</param>
    public abstract void OnDamaged(int damage);
}
