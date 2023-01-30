using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Pathfinding;


public class PlayerCharacter : CharacterData
{
    //public NavMeshAgent agent;
    public GameObject currentTarget;

    public Image healthBar;
    public Image magicBar;
    public Image exBar;
    private static readonly int FillLevel = Shader.PropertyToID("_FillLevel");

    Ray ray; 
    RaycastHit hit;
    Animator ani;
    Vector3 stopPos;//目标点
    Seeker seeker;//路径工具
    Path path;//存储路径
    bool stopMove;

    public float Hp;
    public float Mp;
    public float MaxEX;
    public float NextWaypointDistance;//判断玩家与点的距离

    public int currentWayPoint;//当前点位的编号

    public int Stat;//属性点（每次升级加五点）
    public int SkillPoint;//技能点（每次升级加一点）
    public int AttackCount;//距离上一次暴击的次数

    public ItemUI[] EQUIP = new ItemUI[7];//玩家装备栏

    void Awake()
    {
        ani = GetComponent<Animator>();
        seeker = GetComponent<Seeker>();

        State = CharacterState.Idle;

        Level = 1;
        MaxHp = 100;
        Hp = MaxHp;
        MaxMp = 100;
        Mp = MaxMp;
        PhysicalDamge = 10;
        MagicDamge = 1;
        Speed = 10;
        TurnSpeed = 20;
        NextWaypointDistance = 0.5f;
        Defense = 0.1f;
        Crit = 0.05f;
        DOD = 0f;
        MaxEX = 120f;
        Ex = 0;

        Strength = 0;
        Intellect = 0;
        Agility = 0;
        Stamina = 0;

        Stat = 5;
        SkillPoint = 1;
        AttackCount = 0;

        stopPos = transform.position;
    }
    void Update()
    {
        HpController();
        MpController();
        ExController();
        LevelUp();
        switch (State)
        {
            case CharacterState.Idle:
                ani.SetBool("isMove", false);
                if (Vector3.Distance(transform.position, stopPos) > 0.5f) 
                {
                    State = CharacterState.Move;
                }
                break;
            case CharacterState.Move:
                MoveStateByAStar();
                break;
            case CharacterState.Attack:
                ani.SetBool("isMove", false);
                ani.SetBool("isAttack",true);
                break;
            case CharacterState.UseSkill:

                break;
            case CharacterState.InBuff:

                break;
            case CharacterState.Behit:

                break;
            case CharacterState.Death:

                break;
        }
    }
    private void MoveStateByAStar() 
    {
        if (path == null || stopMove)  
        {
            return;
        }

        if (currentWayPoint >= path.vectorPath.Count)//当前点位大于等于路径存储的总点数时结束 
        {
            Debug.Log("路径搜索结束");
            stopMove = true;
            currentWayPoint = 0;
            path = null;
            if (currentTarget == null) 
            {
                State = CharacterState.Idle;
                stopPos = transform.position;
            }
            else 
            {
                if (Vector3.Distance(transform.position, currentTarget.transform.position) < 1f)
                {
                    State = CharacterState.Attack;
                }
            }
            return;
        }

        ani.SetBool("isAttack", false);
        ani.SetBool("isMove", true);

        Vector3 dir = (path.vectorPath[currentWayPoint] - transform.position).normalized;
        dir *= Speed * Time.fixedDeltaTime;

        transform.Translate(Vector3.forward * Time.fixedDeltaTime * Speed);
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * TurnSpeed);
        if (Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]) < NextWaypointDistance) 
        {
            currentWayPoint++;
            return;
        }
    }
    private void OnPathComplete(Path p) 
    {
        Debug.Log("发现这个路线" + p.error);
        if (!p.error) 
        {
            path = p;
            currentWayPoint = 0;
            stopMove = false;
        }
    }
    public void GetTargetByAStar() 
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) 
        {
            if (hit.collider.CompareTag("Map"))
            {
                State = CharacterState.Move;
                stopPos = hit.point;
                seeker.StartPath(transform.position, stopPos, OnPathComplete);//开始寻路
            }
            else if (hit.collider.gameObject.layer == 6) 
            {
                State = CharacterState.Move;
                currentTarget = hit.collider.gameObject;
                stopPos = hit.point;
                seeker.StartPath(transform.position, stopPos, OnPathComplete);//开始寻路
            }
        }
    }
    public bool CritController() 
    {
        AttackCount += 1;
        int P = (int)(Crit * 100);
        Debug.Log(P);
        float C = NumericalManager.instance.critDic[P];
        float PC = AttackCount * C;
        int r = Random.Range(0, 100);
        if (r <= PC * 100)
        {
            AttackCount = 0;
            return true;
        }
        else 
        {
            return false;
        }

    }
    public void HpController() 
    {
        if (healthBar == null)
        {
            return;
        }
        if (Hp > MaxHp)
        {
            Hp = MaxHp;
        }
        float value = Hp / MaxHp;
        healthBar.material.SetFloat(FillLevel, value);
    }
    public void MpController() 
    {
        if (magicBar == null) 
        {
            return;
        }
        if (Mp > MaxMp)
        {
            Mp = MaxMp;
        }
        float value = Mp / MaxMp;
        magicBar.material.SetFloat(FillLevel, value);
    }
    public void ExController() 
    {
        if (exBar == null) 
        {
            return;
        }
        float value = Ex / MaxEX;
        exBar.fillAmount = value;
    }
    public void LevelUp() 
    {
        if (Ex >= MaxEX) 
        {
            Level+=1;
            Stat += 5;
            SkillPoint += 1;
            Ex = 0;
            MaxEX = NumericalManager.instance.NextMaxEx(MaxEX);
        }
    }
    public void InstallEquipment(ItemUI e) 
    {
        EquipmentType et = (e.item as Equipment).EquipType;
        switch (et)
        {
            case EquipmentType.Head:
                AddEquipment(2, e);
                break;
            case EquipmentType.Neck:
                AddEquipment(6, e);
                break;
            case EquipmentType.Ring:
                AddEquipment(5, e);
                break;
            case EquipmentType.Armor:
                AddEquipment(1, e);
                break;
            case EquipmentType.Bracer:
                AddEquipment(3, e);
                break;
            case EquipmentType.Boots:
                AddEquipment(4, e);
                break;
            case EquipmentType.PhysicalWeapon:
                AddEquipment(0, e);
                break;
            case EquipmentType.MagicWeapon:
                break;
        }
    }
    private void AddEquipment(int index,ItemUI itemUI) 
    {
        if (EQUIP[index] != null)
        {
            EQUIP[index].EquipmentUsed();
        }
        else
        {
            EQUIP[index] = itemUI;
        }
    }
}