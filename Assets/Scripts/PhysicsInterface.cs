using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPhysics {
    void Hit(int force, Vector3 dir);

    void GravityControl(bool on);

    void Reset();
}