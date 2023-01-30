using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class UIManager
{
    public static object _object = new object();
    public static UIManager _instance;
    public static UIManager instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_object)
                {
                    if (_instance == null)
                    {
                        _instance = new UIManager();
                    }
                }
            }
            return _instance;
        }
    }
    private UIManager()
    {
        ParseUIPanelTypeJson();
        canvasTra = GameObject.Find("Canvas").transform;
    }
    public class JsonList
    {
        public List<PanelData> panelDataList { get; set; }
    }
    private Transform canvasTra;//画布位置;
    private JsonList templist = new JsonList();//中转
    private Dictionary<UIType, string> panelPathDic = new Dictionary<UIType, string>();//存储所有面板预制体路径
    private Dictionary<UIType, PanelBase> panelGameObjectDic;//存储所有面板;
    private Stack<PanelBase> panelStack;
    public void PushPanel(UIType type, System.Object o = null) //入栈 显示页面
    {
        if (panelStack == null) 
        {
            panelStack = new Stack<PanelBase>();
        }
        if (panelStack.Count > 0)
        {
            PanelBase topPanel = panelStack.Peek();
            topPanel.OnStay(o);
        }
        PanelBase panel = GetPanel(type);
        if (!panelStack.Contains(panel)) 
        {
            panel.OnEnter(o);
            panelStack.Push(panel);
        }
    }
    public void PopPanle() //出栈 关闭页面
    {
        if (panelStack == null) 
        {
            panelStack = new Stack<PanelBase>();
        }
        if (panelStack.Count <= 0) 
        {
            return;
        }
        PanelBase topPanel = panelStack.Pop();
        topPanel.OnExit();
        if (panelStack.Count <= 0) 
        {
            return;
        }
        PanelBase _topPanel = panelStack.Peek();
        _topPanel.OnResume();

    }
    public PanelBase GetPanel(UIType type)//获取到指定面板
    {
        if (panelGameObjectDic == null)
        {
            panelGameObjectDic = new Dictionary<UIType, PanelBase>();
        }
        PanelBase panel;
        panelGameObjectDic.TryGetValue(type, out panel);
        if (panel == null)
        {
            string path;
            panelPathDic.TryGetValue(type, out path);
            GameObject temp = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            temp.transform.SetParent(canvasTra, false);
            panelGameObjectDic.Add(type, temp.GetComponent<PanelBase>());
            return temp.GetComponent<PanelBase>();
        }
        else 
        {
            return panel;
        }
    }
    private void ParseUIPanelTypeJson() 
    {
        string path = Application.streamingAssetsPath + "/UIType.json";
        if (!File.Exists(path)) 
        {
            Debug.Log("不存在该文夹");
            return;
        }
        using (StreamReader sr = new StreamReader(new FileStream(path, FileMode.Open))) 
        {
            JsonReader json = new JsonReader(sr);
            templist = JsonMapper.ToObject<JsonList>(json);
        }
        foreach (PanelData data in templist.panelDataList)
        {
            data.panelType = (UIType)System.Enum.Parse(typeof(UIType), data.type);
            panelPathDic.Add(data.panelType, data.path);
        }
    }
}
