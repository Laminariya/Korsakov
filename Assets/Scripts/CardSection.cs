using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardSection : MonoBehaviour
{
    
    public TMP_Text Korpus;
    public TMP_Text CountSold;
    public TMP_Text CountStudio;
    public TMP_Text Count2;
    public TMP_Text Count3;
    public TMP_Text PriceStudio;
    public TMP_Text Price2;
    public TMP_Text Price3;
    public Button b_ChoseFlat;
    
    private MySection _section;
    
    public void Init()
    {
        b_ChoseFlat.onClick.AddListener(OnChosenFlat);
        Hide();
    }

    public void Show(MySection section)
    {
        _section = section;
        Korpus.text = "Подъезд " + _section.Number.ToString();
        CountSold.text = "Объектов в продаже: " + _section.MyObjects.Count;
        CountStudio.text = _section.Studio.Count.ToString();
        Count2.text = _section.Flat2.Count.ToString();
        Count3.text = _section.Flat3.Count.ToString();
        PriceStudio.text = "От " + GameManager.instance.GetShortPrice(_section.MinPriceStudio) + " " +
                           GameManager.instance.SymvolRuble;
        Price2.text = "От " + GameManager.instance.GetShortPrice(_section.MinPriceFlat2) + " " +
                           GameManager.instance.SymvolRuble;
        Price3.text = "От " + GameManager.instance.GetShortPrice(_section.MinPriceFlat3) + " " +
                           GameManager.instance.SymvolRuble;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnChosenFlat()
    {
        if (_section.Number <= 5)
            GameManager.instance.chosenPanel.Show(2);
        else
        {
            GameManager.instance.chosenPanel.Show(3);
        }
    }
}
