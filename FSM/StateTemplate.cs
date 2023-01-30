using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTemplate<T> : StateBase //状态拥有者类（泛型类）
{
    public T owner;

    public StateTemplate(int id, T o) : base(id) 
    {
        owner = o;
    }
}
