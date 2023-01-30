using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyCharacter : CharacterData
{
    public Animator ani;//动画机
    public Rigidbody rig;//刚体只用与击飞效果实现
    public Seeker seeker;//A星寻路
    public Path path;//搜索到的路径

    public List<GameObject> enemyTarget;//敌人通过标签找到的物体
    public List<Vector3> enemyPatrolPos;//敌人的巡逻点位
    List<string> enemyTargetTags = new List<string> { "Player" };//敌人可攻击的物体标签

    public Vector3 currentPos;//当前的目标点
    int posCount = 0;//控制移动的计数点

    Ray ray;
    RaycastHit hit;
    GameObject hitBox;//攻击盒
    GameObject hitTextPrefab;//数值文本预制体

    public StateManager stateManager;//状态管理器

    public float Hp;//实际的血量
    public float Mp;//实际的蓝量
    public float viewAngle;//视野角度
    public float viewDistance;//视野半径
    public float maxIdleTime;//最大待机时间
    public float idleTime;//待机计时器
    public float maxMissingTargetTime;//最大丢失目标时间
    public float missingTargetTime;//丢失目标时间计时器
    public float maxDamageMemory;//伤害存储器最大值
    public float damageMemory;//伤害存储计算器
    public float nextWayPointDistance;//路径上下一个的间隙

    public int currentWayPoint;//当前前进的点位编号

    public bool beControl;//能否进行行为

    public Transform chaseTarget;//追击的目标
    public Canvas canvas;//敌人数值显示画布
    void Awake()
    {
        Init();

        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        seeker = GetComponent<Seeker>();
        hitBox = transform.Find("HitBox").gameObject;
        hitTextPrefab = Resources.Load<GameObject>("UIIcon/HitDamageText");

        MaxHp = 300;
        Hp = MaxHp;
        MaxMp = 200;
        Mp = MaxMp;
        Speed = 7;
        TurnSpeed = 20;
        PhysicalDamge = 3;
        MagicDamge = 1;

        viewAngle = 80f;
        viewDistance = 3f;
        maxIdleTime = 5f;
        idleTime = 0;
        maxMissingTargetTime = 5f;
        missingTargetTime = 0f;
        maxDamageMemory = 20f;
        damageMemory = 0;

        nextWayPointDistance = 0.5f;
        currentWayPoint = 0;

        currentPos = enemyPatrolPos[0];

        State = CharacterState.Idle;
        IdleState idle = new IdleState(0, this);
        MoveState move = new MoveState(1, this);
        AttackState attack = new AttackState(2, this);
        BeHitState behit = new BeHitState(3, this);
        stateManager = new StateManager(idle);
        stateManager.AddState(move);
        stateManager.AddState(attack);
        stateManager.AddState(behit);
    }
    void Update()
    {
        if (idleTime >= maxIdleTime) 
        {
            idleTime = 0;
            stateManager.TranslateState(1);
        }
        if (missingTargetTime >= maxMissingTargetTime) 
        {
            missingTargetTime = 0;
            stateManager.TranslateState(1);
        }
        PatrolPosChange();
        TargetSelection();
        for (int i = 0; i < enemyTarget.Count; i++)
        {
            CheckPlayer(enemyTarget[i].transform);
        }
        HpContrllor();
    }
    void LateUpdate()
    {
        if (beControl) 
        {
            return;
        }
        stateManager.Update();
    }
    void Init()
    {
        foreach (string tag in enemyTargetTags) 
        {
            GameObject[] temp = GameObject.FindGameObjectsWithTag(tag);
            for (int i = 0; i < temp.Length; i++) 
            {
                enemyTarget.Add(temp[i]);
            }
        }
    }
    public void PatrolPosChange() 
    {
        if (Vector3.Distance(transform.position, currentPos) < 0.6f) 
        {
            Debug.Log("到达点位");
            stateManager.TranslateState(0);
            posCount += 1;
            if (posCount >= enemyPatrolPos.Count) 
            {
                posCount = 0;
            }
            currentPos = enemyPatrolPos[posCount];
        }
    }
    public void CheckPlayer(Transform target)
    {
        Vector3 enemyPos = transform.position;
        Quaternion enemyRot = transform.rotation;
        Vector3 norVec = enemyRot * Vector3.forward * viewDistance;
        Vector3 temVec = target.position - enemyPos;
        float distance = Vector3.Distance(enemyPos, target.position);
        float angle = Mathf.Acos(Vector3.Dot(norVec.normalized, temVec.normalized)) * Mathf.Rad2Deg;
        if (distance <= viewDistance && angle <= viewAngle)
        {
            Debug.Log("发现玩家");
            chaseTarget = target;
            missingTargetTime = 0;
            if (stateManager.currentState.ID != 2) 
            {
                //Debug.Log("切换到攻击状态");
                stateManager.TranslateState(2);
            }
        }
    }
    public void TakePlayerDamge(float Damge)  
    {
        Hp -= Damge;
        damageMemory += Damge;
        if (Hp <= 0) 
        {
            ani.SetTrigger("Die");
            Destroy(this);
        }
        else if (damageMemory >= maxDamageMemory) 
        {
            stateManager.TranslateState(3);
        }
    }
    public void TargetSelection() 
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) 
        {
            if (hit.collider.gameObject==this.gameObject)
            {
                //ho.On(Color.red);
            }
            else 
            {
                //ho.Off();
            }
        }
    }
    public void BeHit(float damage, bool isCrit) 
    {
        GameObject go = Instantiate(hitTextPrefab);
        go.transform.SetParent(canvas.transform);
        go.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        if (isCrit)
        {
            float cDamage = damage * 1.5f;
            Hp -= cDamage;
            go.GetComponent<TextShow>().Show(cDamage, true);
        }
        else 
        {
            Hp -= damage;
            go.GetComponent<TextShow>().Show(damage, false);
        }
    }
    public void HpContrllor()
    {
        if (Hp <= 0)
        {
            ani.Play("Die");
        }
        float fill = Hp / MaxHp;
        HealthBar.fillAmount = fill;
        HealthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }
    public void OpenHitBox() 
    {
        hitBox.SetActive(true);
    }
    public void CloseHitBox() 
    {
        hitBox.SetActive(false);
    }
    public void Die() 
    {
        Destroy(this.gameObject);
    }
    
    private void OnTriggerEnter(Collider collider) 
    {
        if (enemyTarget.Contains(collider.gameObject)) 
        {
            Debug.Log("击中玩家");
        }
    }
}