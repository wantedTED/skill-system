using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase //×´Ì¬»ùÀà
{
    public int ID
    {
        get;
        set;
    }
    public StateManager manager;
    public StateBase(int id) 
    {
        this.ID = id;
    }
    public virtual void OnEnter(params object[] args) 
    {

    }
    public virtual void OnStay(params object[] args) 
    {

    }
    public virtual void OnExit(params object[] args)
    {
        
    }
}
