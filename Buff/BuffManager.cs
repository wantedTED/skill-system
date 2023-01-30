using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager 
{
    public static object _object = new object();
    public static BuffManager _instance;
    public static BuffManager instance 
    {
        get 
        {
            if (_instance == null) 
            {
                lock (_object) 
                {
                    if (_instance == null) 
                    {
                        _instance = new BuffManager();
                    }
                }
            }
        return _instance;
        }
    }
    public List<BuffBase> AllBuffs = new List<BuffBase>();

    public BuffManager() 
    {
        AllBuffs.Add(new FloatedBuff(BuffType.addFloated, BuffOverlap.none, BuffCloseType.All, BuffCalculateType.Once, 1, 0.4f, 0));
    }
}
