using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{

    private GameManager _manager;
    
    public List<Image> LightSection = new List<Image>();
    public CardSection CardSection;
    
    public Button b_ChosePanel;
    public Button b_OpenBLE;

    public Button b_Osobennosti;
    public Button b_Otdelka;
    public Button b_ChoseOnParametr;
    
    private int _countClick = 0;
    private float _timer = 0;
    
    public void Init(GameManager manager)
    {
        _manager = manager;
        b_ChoseOnParametr.onClick.AddListener(ShowChoseOnParametr);
        b_Osobennosti.onClick.AddListener(ShowOsobennosti);
        b_Otdelka.onClick.AddListener(ShowOtdelka);
        CardSection.Init();
        int k = 0;
        foreach (var image in LightSection)
        {
            k++;
            var k1 = k;
            image.GetComponentInChildren<Button>().onClick.AddListener(()=>OnClickLight(k1));
            image.enabled = false;
        }
    }

    public void Show()
    {
        CardSection.Hide();
        foreach (var image in LightSection)
        {
            image.enabled = false;
        }
    }

    private void Update()
    {
        if (Time.time - _timer > 1.0f)
        {
            _countClick = 0;
        }
    }

    private void OnClickLight(int numberSection)
    {
        foreach (var image in LightSection)
        {
            image.enabled = false;
        }
        
        LightSection[numberSection-1].enabled = true;
        
        foreach (var building in _manager.MyData.Buildings)
        {
            foreach (var mySection in building.MySections)
            {
                if (mySection.Number == numberSection)
                {
                    CardSection.Show(mySection);
                    return;
                }
            }
        }
    }

    private void OnOpenBLE()
    {
        _countClick++;
        _timer = Time.time;
        if (_countClick >= 4)
        {
            _manager.bluetoothManager.MenuPanel.SetActive(true);
        }
    }

    public void Show2()
    {
        _manager.MessageOffAllLight();
        _manager.MessageOnDemo();
    }

    public void ShowChoseOnParametr()
    {
        _manager.choseOnParameterPanel.Show();
    }

    public void ShowOsobennosti()
    {
        _manager.osobennostiProekta.Show();
    }

    public void ShowOtdelka()
    {
        _manager.otdelka.Show();
    }

    private void ChangeFloor(int floor)
    {
        
        
        _manager.MessageOffAllLight();
        _manager.MessageOnFloor(1,1,floor+1);
    }

    private void OnLightKorpus()
    {

        _manager.MessageOffAllLight();
        return;
        
        _manager.MessageOnHouse(1, 1);
    }

}
