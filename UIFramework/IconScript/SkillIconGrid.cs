using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MOBASkill;

public class SkillIconGrid : MonoBehaviour,IPointerClickHandler,IBeginDragHandler,IDragHandler,IEndDragHandler,ICanvasRaycastFilter
{
    public SkillData skill;
    public CharacterSkillManager ckm;
    public PlayerCharacter player;

    Image image;//解锁技能显示
    Text text;//技能等级显示
    GameObject skillIconPre;//图标预制体
    Transform startPos;
    bool isRayThrough;
    bool isUnlock;
    private void Awake()
    {
        ckm = GameObject.Find("Player").GetComponent<CharacterSkillManager>();
        player = GameObject.Find("Player").GetComponent<PlayerCharacter>();
        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
        skillIconPre = Resources.Load("UIIcon/SkillIcon2") as GameObject;
        isRayThrough = true;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isUnlock) 
        {
            startPos = transform.parent;
            isRayThrough = false;
            Debug.Log("开始拖拽");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isUnlock) 
        {
            return;
        }
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isUnlock) 
        {
            return;
        }
        GameObject grid = eventData.pointerCurrentRaycast.gameObject;
        if (grid != null)
        {
            Debug.Log("从技能面板拖过来" + grid.name);
            if (grid.CompareTag("SkillGrid"))
            {
                GameObject icon = GenerateSameIcon(grid.transform);
            }
        }
        transform.SetParent(startPos);
        transform.position = startPos.transform.position;
        isRayThrough = true;
        Debug.Log("结束拖拽");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 1)
        {
            UIManager.instance.PushPanel(UIType.SkillMessage, skill);
        }
        else if (eventData.clickCount == 2 && player.SkillPoint != 0)   
        {
            isUnlock = true;
            image.color = new Color(1, 1, 1);
            ckm.SkillLevelUp(skill.skillId);
            text.text = skill.level.ToString();
            player.SkillPoint -= 1;
        }
    }

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        return isRayThrough;
    }
    public GameObject GenerateSameIcon(Transform transform) 
    {
        GameObject icon = Instantiate(skillIconPre, transform);
        icon.transform.SetAsFirstSibling();
        icon.GetComponent<SkillIcon2>().data = skill;
        icon.GetComponent<Image>().sprite = skill.skillIcon;
        int index = transform.GetComponent<SkillButton>().ButtonID;
        GameManager.instance.ButtonGetSkill(index, skill.skillId);
        return icon;
    }
}
