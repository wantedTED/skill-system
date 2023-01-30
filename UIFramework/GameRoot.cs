using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRoot : MonoBehaviour
{
    public string[] HpShowEnemyTags;
    public List<Transform> enemyList;

    GameObject HpBarPrefab;
    private void Awake()
    {
        UIManager.instance.PushPanel(UIType.MainMenu);

        HpBarPrefab = Resources.Load<GameObject>("UIIcon/HpBar");

        FindAllEnemy();
        CreateTheEnemyHp();
    }
    public void FindAllEnemy() 
    {
        foreach (string tag in HpShowEnemyTags)
        {
            GameObject[] temp = GameObject.FindGameObjectsWithTag(tag);
            foreach (var t in temp)
            {
                enemyList.Add(t.transform);
            }
        }
    }
    public void CreateTheEnemyHp() 
    {
        foreach (var e in enemyList)
        {
            Debug.Log("Éú³ÉÑªÌõ" );
            GameObject hpbar = Instantiate(HpBarPrefab);
            hpbar.transform.SetParent(this.transform);
            e.GetComponent<CharacterData>().HealthBar = hpbar.transform.GetComponent<Image>();
        }
    }
}
