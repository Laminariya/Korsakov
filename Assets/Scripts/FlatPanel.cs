using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlatPanel : MonoBehaviour
{
    
    public Image Image;
    public TMP_Text Number;
    public TMP_Text Korpus;
    public TMP_Text Floor;
    public TMP_Text Price;
    public TMP_Text Area;
    public TMP_Text Section;
    public TMP_Text Articul;
    public TMP_Text UpKorpus;
    public TMP_Text UpFloor;

    public Button b_Flat;
    public Button b_Floor;
    public Button b_Close;
    
    public Sprite FlatActive;
    public Sprite FlatInactive;
    public Sprite FloorActive;
    public Sprite FloorInactive;
    
    private MyObject _myObject;
    
    public void Init()
    {
        b_Close.onClick.AddListener(Hide);
        b_Flat.onClick.AddListener(OnFlat);
        b_Floor.onClick.AddListener(OnFloor);
        Hide();
    }

    public void Show(MyObject myObject)
    {
        _myObject = myObject;
        gameObject.SetActive(true);
        Number.text = myObject.Number.ToString();
        Korpus.text = myObject.Korpus.ToString();
        Floor.text = myObject.Floor.ToString();
        Price.text = myObject.Price.ToString();
        Area.text = myObject.Area.ToString();
        Section.text = myObject.Section.ToString();
        Articul.text = "ID " + myObject.Id.ToString();
        UpKorpus.text = "Корпус " + _myObject.Korpus.ToString();
        UpFloor.text = "Этаж " + myObject.Floor.ToString();
        OnFlat();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnFlat()
    {
        Image.sprite = _myObject.FlatSprite;
        b_Flat.image.sprite = FlatActive;
        b_Floor.image.sprite = FloorInactive;
    }

    private void OnFloor()
    {
        Image.sprite = _myObject.FloorSprite;
        b_Flat.image.sprite = FlatInactive;
        b_Floor.image.sprite = FloorActive;
    }


}
