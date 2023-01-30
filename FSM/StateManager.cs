using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager //状态机管理类：由敌人控制，完成状态的存储，切换，和状态的保持
{
    public Dictionary<int, StateBase> stateCache;//存储全部状态的字典
    public StateBase lastState;//上一个状态
    public StateBase currentState;//当前的状态

    public StateManager(StateBase beginState) 
    {
        lastState = null;
        currentState = beginState;
        stateCache = new Dictionary<int, StateBase>();
        AddState(beginState);
        currentState.OnEnter();
    }
    public void AddState(StateBase state) //添加状态
    {
        if (!stateCache.ContainsKey(state.ID)) 
        {
            stateCache.Add(state.ID, state);
            state.manager = this;
        }
    }
    public void TranslateState(int id) //切换状态
    {
        if (!stateCache.ContainsKey(id)) 
        {
            return;
        }
        lastState = currentState;
        currentState = stateCache[id];
        lastState.OnExit();
        currentState.OnEnter();
    }
    public void Update()
    {
        if (currentState != null) 
        {
            currentState.OnStay();
        }
    }
}
