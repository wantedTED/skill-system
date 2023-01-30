using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyCharacter : CharacterData
{
    public Animator ani;//������
    public Rigidbody rig;//����ֻ�������Ч��ʵ��
    public Seeker seeker;//A��Ѱ·
    public Path path;//��������·��

    public List<GameObject> enemyTarget;//����ͨ����ǩ�ҵ�������
    public List<Vector3> enemyPatrolPos;//���˵�Ѳ�ߵ�λ
    List<string> enemyTargetTags = new List<string> { "Player" };//���˿ɹ����������ǩ

    public Vector3 currentPos;//��ǰ��Ŀ���
    int posCount = 0;//�����ƶ��ļ�����

    Ray ray;
    RaycastHit hit;
    GameObject hitBox;//������
    GameObject hitTextPrefab;//��ֵ�ı�Ԥ����

    public StateManager stateManager;//״̬������

    public float Hp;//ʵ�ʵ�Ѫ��
    public float Mp;//ʵ�ʵ�����
    public float viewAngle;//��Ұ�Ƕ�
    public float viewDistance;//��Ұ�뾶
    public float maxIdleTime;//������ʱ��
    public float idleTime;//������ʱ��
    public float maxMissingTargetTime;//���ʧĿ��ʱ��
    public float missingTargetTime;//��ʧĿ��ʱ���ʱ��
    public float maxDamageMemory;//�˺��洢�����ֵ
    public float damageMemory;//�˺��洢������
    public float nextWayPointDistance;//·������һ���ļ�϶

    public int currentWayPoint;//��ǰǰ���ĵ�λ���

    public bool beControl;//�ܷ������Ϊ

    public Transform chaseTarget;//׷����Ŀ��
    public Canvas canvas;//������ֵ��ʾ����
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
            Debug.Log("�����λ");
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
            Debug.Log("�������");
            chaseTarget = target;
            missingTargetTime = 0;
            if (stateManager.currentState.ID != 2) 
            {
                //Debug.Log("�л�������״̬");
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
            Debug.Log("�������");
        }
    }
}