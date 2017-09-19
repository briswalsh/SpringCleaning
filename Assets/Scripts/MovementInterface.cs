using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement {
    void Hit(int force);

    void Reset();
}