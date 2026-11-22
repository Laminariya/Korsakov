using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OsobennostiProekta : MonoBehaviour
{
    
    public Image image;
    public List<Sprite> Sprites = new List<Sprite>();
    
    public Button b_Close;
    public Button b_MainMenu;
    
    public Button b_1;
    public Button b_2;
    public Button b_3;
    public Button b_4;
    public Button b_5;
    public Button b_6;

    public Button b_1_1;
    public Button b_2_1;
    public Button b_3_1;
    public Button b_4_1;
    public Button b_5_1;
    public Button b_6_1;
    
    public void Init()
    {
        b_1.onClick.AddListener(()=>OnClickOsobennosti(0));
        b_2.onClick.AddListener(()=>OnClickOsobennosti(1));
        b_3.onClick.AddListener(()=>OnClickOsobennosti(2));
        b_4.onClick.AddListener(()=>OnClickOsobennosti(3));
        b_5.onClick.AddListener(()=>OnClickOsobennosti(4));
        b_6.onClick.AddListener(()=>OnClickOsobennosti(5));
        b_1_1.onClick.AddListener(()=>OnClickOsobennosti(0));
        b_2_1.onClick.AddListener(()=>OnClickOsobennosti(1));
        b_3_1.onClick.AddListener(()=>OnClickOsobennosti(2));
        b_4_1.onClick.AddListener(()=>OnClickOsobennosti(3));
        b_5_1.onClick.AddListener(()=>OnClickOsobennosti(4));
        b_6_1.onClick.AddListener(()=>OnClickOsobennosti(5));
        
        b_Close.onClick.AddListener(OnClose);
        b_MainMenu.onClick.AddListener(OnMainMenu);
        
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        OnClose();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnClickOsobennosti(int number)
    {
        image.gameObject.SetActive(true);
        image.sprite = Sprites[number];
    }

    private void OnClose()
    {
        image.gameObject.SetActive(false);
    }

    private void OnMainMenu()
    {
        Hide();
    }
}
