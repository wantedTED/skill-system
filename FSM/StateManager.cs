using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager //״̬�������ࣺ�ɵ��˿��ƣ����״̬�Ĵ洢���л�����״̬�ı���
{
    public Dictionary<int, StateBase> stateCache;//�洢ȫ��״̬���ֵ�
    public StateBase lastState;//��һ��״̬
    public StateBase currentState;//��ǰ��״̬

    public StateManager(StateBase beginState) 
    {
        lastState = null;
        currentState = beginState;
        stateCache = new Dictionary<int, StateBase>();
        AddState(beginState);
        currentState.OnEnter();
    }
    public void AddState(StateBase state) //���״̬
    {
        if (!stateCache.ContainsKey(state.ID)) 
        {
            stateCache.Add(state.ID, state);
            state.manager = this;
        }
    }
    public void TranslateState(int id) //�л�״̬
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
