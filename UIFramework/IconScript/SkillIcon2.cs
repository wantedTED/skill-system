using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using MOBASkill;

public class SkillIcon2 : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, ICanvasRaycastFilter
{
    public Image CdImage;
    public Text cdText;
    public SkillData data;

    bool isRayThrough = true;
    Transform lastPos;
    float timer;
    private void Update()
    {
        if (data.cdRemain != 0)
        {
            timer += Time.deltaTime;
            cdText.gameObject.SetActive(true);
            cdText.text = data.cdRemain.ToString();
            CdImage.fillAmount = 1 - (timer / data.skillCd);
        }
        else 
        {
            timer = 0;
            cdText.gameObject.SetActive(false);
        }

    }
    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        return isRayThrough;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastPos = transform.parent;
        isRayThrough = false;
        transform.parent = transform.parent.parent;
        Debug.Log("下方的技能图标开始拖拽");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject go = eventData.pointerCurrentRaycast.gameObject;
        if (go != null)
        {
            Debug.Log("鼠标打到的物体是" + go.name);
            if (go.CompareTag("SkillGrid"))
            {
                SetParentAndPos(transform, go.transform);

            }
            else if (go.CompareTag("SkillIcon"))
            {
                SetParentAndPos(transform, go.transform.parent);
                int index = go.transform.parent.GetComponent<SkillButton>().ButtonID;
                GameManager.instance.ButtonGetSkill(index, data.skillId);

                SetParentAndPos(go.transform, lastPos);
                int _index = lastPos.GetComponent<SkillButton>().ButtonID;
                GameManager.instance.ButtonGetSkill(_index, go.GetComponent<SkillIcon2>().data.skillId);
            }
        }
        else 
        {
            SetParentAndPos(transform, lastPos);
        }
        isRayThrough = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
    public void SetParentAndPos(Transform kid, Transform father) 
    {
        kid.SetParent(father);
        kid.SetAsFirstSibling();
        kid.position = father.position;
        Debug.Log("=============");
        int index = father.GetComponent<SkillButton>().ButtonID;
        Debug.Log("拖到得格子是" + index + "号");
        GameManager.instance.ButtonGetSkill(index, data.skillId);
    }
}
