using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{

    private GameManager _manager;
    
    public List<Image> LightSection = new List<Image>();
    public CardSection CardSection;
    
    public Button b_OpenBLE;

    public Button b_Osobennosti;
    public Button b_Otdelka;
    public Button b_ChoseOnParametr;

    public Button b_Korpus1;
    public Sprite Korpus1Off;
    public Sprite Korpus1On;
    
    private int _countClick = 0;
    private float _timer = 0;
    
    public void Init(GameManager manager)
    {
        _manager = manager;
        b_ChoseOnParametr.onClick.AddListener(ShowChoseOnParametr);
        b_Osobennosti.onClick.AddListener(ShowOsobennosti);
        b_Otdelka.onClick.AddListener(ShowOtdelka);
        b_Korpus1.onClick.AddListener(OnLightKorpus1);
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
        
        _manager.MessageOffAllLight();
        _manager.MessageOnDemo();
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
                    
                    _manager.MessageOffAllLight();
                    for (int i = 1; i < 9; i++)
                    {
                        _manager.MessageOnFloor(2, mySection.Number, i);
                    }
                    return;
                }
            }
        }
    }

    private void OnLightKorpus1()
    {
        if (b_Korpus1.image.sprite == Korpus1Off)
        {
            b_Korpus1.image.sprite = Korpus1On;
            _manager.MessageOffAllLight();
            _manager.MessageOnHouse(1,1);
        }
        else
        {
            b_Korpus1.image.sprite = Korpus1Off;
            _manager.MessageOffAllLight();
            _manager.MessageOnDemo();
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

    public void ShowChoseOnParametr()
    {
        _manager.choseOnParameterPanel.Show();
    }

    public void ShowOsobennosti()
    {
        _manager.osobennostiProekta.Show();
        _manager.MessageOnDemo();
    }

    public void ShowOtdelka()
    {
        _manager.otdelka.Show();
        _manager.MessageOnDemo();
    }
    

}
