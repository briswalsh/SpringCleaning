using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPhysics {
    void Hit(int force);

    void Reset();
}