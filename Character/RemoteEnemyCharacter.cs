using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RemoteEnemyCharacter : EnemyCharacter
{
    List<string> enemyTargetTags = new List<string> { "Player" };//敌人可攻击的物体标签

    int posCount = 0;//控制移动的计数点

    Ray ray;
    RaycastHit hit;
    GameObject hitTextPrefab;//数值文本预制体

    public GameObject FireBall;

    void Awake()
    {
        Init();

        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        seeker = GetComponent<Seeker>();
        hitTextPrefab = Resources.Load<GameObject>("UIIcon/HitDamageText");
        FireBall = Resources.Load<GameObject>("SkillPrefab/EnemyFireBall");

        MaxHp = 200;
        Hp = MaxHp;
        MaxMp = 100;
        Mp = MaxMp;
        Speed = 6;
        TurnSpeed = 20;
        PhysicalDamge = 5;
        MagicDamge = 1;

        viewAngle = 150f;
        viewDistance = 4.5f;
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
        RemoteAttackState attack = new RemoteAttackState(2, this);
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
    public void GenerateTheFireBall() 
    {
        GameObject go = Instantiate(FireBall);
        go.transform.position = transform.position;
        go.transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y - 90, transform.rotation.z,transform.rotation.w);
    }
}
