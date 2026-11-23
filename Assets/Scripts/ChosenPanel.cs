using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChosenPanel : MonoBehaviour
{
    
    public List<GameObject> Sections = new List<GameObject>();
    
    public Button b_Back;
    public Button b_Korpus2;
    public Button b_Korpus3;
    
    public Sprite Korpus2Sprite;
    public Sprite Korpus3Sprite;
    public Sprite Korpus2_2Sprite;
    public Sprite Korpus3_2Sprite;

    public void Init()
    {
        b_Back.onClick.AddListener(Hide);
        b_Korpus2.onClick.AddListener(OnKorpus2Clicked);
        b_Korpus3.onClick.AddListener(OnKorpus3Clicked);
        for (int i = 0; i < Sections.Count; i++)
        {
            Button[] buttons = Sections[i].GetComponentsInChildren<Button>(true);
            foreach (var button in buttons)
            {
                button.gameObject.AddComponent<ButtonChoseFlat>();
            }

            foreach (var myBuilding in GameManager.instance.MyData.Buildings)
            {
                foreach (var mySection in myBuilding.MySections)
                {
                    if (mySection.Number == i + 1)
                    {
                        for (int j = 0; j < buttons.Length; j++)
                        {
                            bool isTrue = false;
                            foreach (var myObject in mySection.MyObjects)
                            {
                                if (j + 1 == myObject.NumberInSection)
                                {
                                    buttons[j].GetComponent<ButtonChoseFlat>().Init(myObject);
                                    isTrue = true;
                                    //Debug.Log((i+1).ToString() +" "+ myObject.NumberInSection);
                                    break;
                                }
                            }

                            if (isTrue == false)
                            {
                                buttons[j].GetComponent<ButtonChoseFlat>().Init(null);
                                //Debug.Log("Null" + (i + 1).ToString() + " " + (j + 1));
                            }
                        }
                    }
                }
            }
        }

        Hide();
    }

    public void Show(int numberKorpus)
    {
        gameObject.SetActive(true);
        if (numberKorpus == 2)
            OnKorpus2Clicked();
        if (numberKorpus == 3)
            OnKorpus3Clicked();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        GameManager.instance.mainPanel.Show();
    }

    public void OnKorpus2Clicked()
    {
        for (int i = 0; i < 5; i++)
        {
            Sections[i].gameObject.SetActive(true);
        }
        for (int i = 5; i < 9; i++)
        {
            Sections[i].gameObject.SetActive(false);
        }
        
        b_Korpus3.image.sprite = Korpus3Sprite;
        b_Korpus2.image.sprite = Korpus2_2Sprite;
    }

    public void OnKorpus3Clicked()
    {
        for (int i = 0; i < 5; i++)
        {
            Sections[i].gameObject.SetActive(false);
        }
        for (int i = 5; i < 9; i++)
        {
            Sections[i].gameObject.SetActive(true);
        }
        
        b_Korpus3.image.sprite = Korpus3_2Sprite;
        b_Korpus2.image.sprite = Korpus2Sprite;
    }

}
