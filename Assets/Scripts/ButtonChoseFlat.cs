using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChoseFlat : MonoBehaviour
{
    
    private MyObject _myObject;
    
    public void Init(MyObject myObject)
    {
        if (myObject == null)
        {
            gameObject.SetActive(false);
            return;
        }
        
        gameObject.SetActive(true);
        _myObject = myObject;
        GetComponent<Button>().onClick.AddListener(OnClick);
        if (_myObject.CountRooms == 0)
        {
            GetComponentInChildren<TMP_Text>().text = "C";
        }
        else
        {
            GetComponentInChildren<TMP_Text>().text = myObject.CountRooms.ToString();
        }
    }

    private void OnClick()
    {
        GameManager.instance.flatPanel.Show(_myObject);
    }
}
