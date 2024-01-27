using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Step
{
    public virtual void OnEnter()
    {

    }
    public virtual void OnUpdate()
    {

    }

    public abstract bool IsFinsh();

}
