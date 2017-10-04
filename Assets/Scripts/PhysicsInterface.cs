using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPhysics {

    void Hit(float force, Vector3 dir);

    void GravityControl(bool on, float grav);

    void Move();
}