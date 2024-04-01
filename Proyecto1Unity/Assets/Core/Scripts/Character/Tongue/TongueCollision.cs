using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueCollision
{
    private TongueCollisionType _type;
    private GameObject _target;

    public TongueCollisionType Type { get { return _type; } }
    public GameObject Target { get { return _target; } }

    public TongueCollision(TongueCollisionType type, GameObject target = null)
    {
        this._type = type;
        this._target = target;
    }
}
