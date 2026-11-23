using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Otdelka : MonoBehaviour
{

    public Image image;
    public List<Sprite> spritesFlat1 = new List<Sprite>();
    public List<Sprite> spritesFlat2 = new List<Sprite>();
    public List<Sprite> spritesStudio = new List<Sprite>();
    public Button b_Flat1;
    public Button b_Flat2;
    public Button b_Studio;
    public Button b_Close;
    public Button b_Left;
    public Button b_Right;
    
    private List<Sprite> _sprites = new List<Sprite>();
    private int _spriteIndex = 0;
    
    public void Init()
    {
        b_Flat1.onClick.AddListener(OnFlat1);
        b_Flat2.onClick.AddListener(OnFlat2);
        b_Studio.onClick.AddListener(OnStudio);
        b_Close.onClick.AddListener(OnClose);
        b_Left.onClick.AddListener(OnLeft);
        b_Right.onClick.AddListener(OnRight);
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        OnFlat1();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnClose()
    {
        Hide();
        GameManager.instance.mainPanel.Show();
    }

    private void OnLeft()
    {
        _spriteIndex--;
        if(_spriteIndex < 0) _spriteIndex = _sprites.Count - 1;
        image.sprite = _sprites[_spriteIndex];
    }

    private void OnRight()
    {
        _spriteIndex++;
        if(_spriteIndex >= _sprites.Count) _spriteIndex = 0;
        image.sprite = _sprites[_spriteIndex];
    }

    private void OnFlat1()
    {
        image.sprite = spritesFlat1[0];
        _sprites = spritesFlat1;
        _spriteIndex = 0;
    }

    private void OnFlat2()
    {
        image.sprite = spritesFlat2[0];
        _sprites = spritesFlat2;
        _spriteIndex = 0;
    }

    private void OnStudio()
    {
        image.sprite = spritesStudio[0];
        _sprites = spritesStudio;
        _spriteIndex = 0;
    }
}
