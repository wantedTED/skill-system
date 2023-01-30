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
    private Transform canvasTra;//����λ��;
    private JsonList templist = new JsonList();//��ת
    private Dictionary<UIType, string> panelPathDic = new Dictionary<UIType, string>();//�洢�������Ԥ����·��
    private Dictionary<UIType, PanelBase> panelGameObjectDic;//�洢�������;
    private Stack<PanelBase> panelStack;
    public void PushPanel(UIType type, System.Object o = null) //��ջ ��ʾҳ��
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
    public void PopPanle() //��ջ �ر�ҳ��
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
    public PanelBase GetPanel(UIType type)//��ȡ��ָ�����
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
            Debug.Log("�����ڸ��ļ�");
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
