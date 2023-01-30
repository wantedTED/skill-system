using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBase : MonoBehaviour
{
    public virtual void OnEnter(System.Object o = null)
    {
        this.gameObject.SetActive(true);
    }
    public virtual void OnStay(System.Object o = null)
    {

    }
    public virtual void OnResume()
    {

    }
    public virtual void OnExit()
    {
        this.gameObject.SetActive(false);
    }
}