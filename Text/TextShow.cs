using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextShow : MonoBehaviour
{
    public Text text;
    
    private void Start()
    {
        text = GetComponent<Text>();
    }
    private void Update()
    {
        Destroy(this.gameObject, 1.3f);
    }
    public void Show(float num,bool isCrit) 
    {
        text.text = num.ToString();
        if (isCrit)
        {
            text.fontSize = 120;
            text.color = new Color(255, 211, 0);
        }
        else 
        {
            text.fontSize = 90;
            text.color = Color.red;
        }
        text.rectTransform.DOAnchorPosY(20, 1f);
        text.DOFade(0, 1.2f);
    }
}
