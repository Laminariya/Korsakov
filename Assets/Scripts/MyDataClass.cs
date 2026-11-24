using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDataClass : MonoBehaviour
{
    
    private GameManager _manager;
    private MyData _myData;

    public void Init()
    {
        Debug.Log("Create My Data");
        _manager = GameManager.instance;
        CreateData();
    }

    private void CreateData()
    {
        Debug.Log("Create My Data 0");
        _myData = new MyData();
        _myData.Buildings = new List<MyBuilding>();
        _myData.MyObjects = new List<MyObject>();
        foreach (var objectClass in _manager.Feed.Objects)
        {

            MyObject myObject = new MyObject(objectClass);
            _myData.MyObjects.Add(myObject);
        }
        Debug.Log("Create My Data 1");
        MyBuilding build1 = new MyBuilding();
        build1.MySections = new List<MySection>();
        _myData.Buildings.Add(build1);
        MyBuilding build2 = new MyBuilding();
        build2.MySections = new List<MySection>();
        _myData.Buildings.Add(build2);

        for (int i = 0; i < 5; i++)
        {
            MySection section1 = new MySection();
            section1.Number = i + 1;
            build1.MySections.Add(section1);
        }

        for (int i = 5; i < 9; i++)
        {
            MySection section1 = new MySection();
            section1.Number = i + 1;
            build2.MySections.Add(section1);
        }
        Debug.Log("Create My Data 2");
        foreach (var myBuilding in _myData.Buildings)
        {
            myBuilding.MyObjects = new List<MyObject>();
            foreach (var mySection in myBuilding.MySections)
            {
                mySection.MyObjects = new List<MyObject>();
                foreach (var myObject in _myData.MyObjects)
                {
                    if (mySection.Number == myObject.Section)
                    {
                        mySection.MyObjects.Add(myObject);
                        myBuilding.MyObjects.Add(myObject);
                    }
                }
            }
        }

        Debug.Log("Create My Data 3");

        int maxPrice = 0;
        int minPrice = int.MaxValue;
        float maxArea = 0;
        float minArea = float.MaxValue;
        int maxFloor = 0;
        int minFloor = int.MaxValue;
        foreach (var myObject in _myData.MyObjects)
        {
            if (myObject.Price > maxPrice) maxPrice = myObject.Price;
            if (myObject.Price < minPrice) minPrice = myObject.Price;

            if (myObject.Area > maxArea) maxArea = myObject.Area;
            if (myObject.Area < minArea) minArea = myObject.Area;

            if (myObject.Floor > maxFloor) maxFloor = myObject.Floor;
            if (myObject.Floor < minFloor) minFloor = myObject.Floor;
        }

        _myData.MaxPrice = maxPrice;
        _myData.MinPrice = minPrice;
        _myData.MaxArea = maxArea;
        _myData.MinArea = minArea;
        _myData.MaxFloor = maxFloor;
        _myData.MinFloor = minFloor;
        
        

        foreach (var myBuilding in _myData.Buildings)
        {
            foreach (var mySection in myBuilding.MySections)
            {
                mySection.Studio = new List<MyObject>();
                mySection.Flat2 = new List<MyObject>();
                mySection.Flat3 = new List<MyObject>();
                
                foreach (var myObject in mySection.MyObjects)
                {
                    if(myObject.CountRooms==0) mySection.Studio.Add(myObject);
                    if(myObject.CountRooms==2) mySection.Flat2.Add(myObject);
                    if(myObject.CountRooms==3) mySection.Flat3.Add(myObject);
                }

                minPrice = int.MaxValue;
                foreach (var myObject in mySection.Studio)
                {
                    if (myObject.Price < minPrice) minPrice = myObject.Price;
                }
                mySection.MinPriceStudio = minPrice;
                
                minPrice = int.MaxValue;
                foreach (var myObject in mySection.Flat2)
                {
                    if (myObject.Price < minPrice) minPrice = myObject.Price;
                }
                mySection.MinPriceFlat2 = minPrice;
                
                minPrice = int.MaxValue;
                foreach (var myObject in mySection.Flat3)
                {
                    if (myObject.Price < minPrice) minPrice = myObject.Price;
                }
                mySection.MinPriceFlat3 = minPrice;
            }
        }


        _manager.MyData = _myData;
        
    }

}

[Serializable]
public class MyData
{
    public List<MyBuilding> Buildings = new List<MyBuilding>();
    public List<MyObject> MyObjects = new List<MyObject>();
    
    public float MinArea;
    public float MaxArea;
    public int MinPrice;
    public int MaxPrice;
    public int MinFloor;
    public int MaxFloor;
}

[Serializable]
public class MyBuilding
{
    public List<MyObject> MyObjects = new List<MyObject>();
    public List<MySection> MySections = new List<MySection>();
    public int Number;
    public string BuildingName;
}

public class MySection
{
    public List<MyObject> MyObjects = new List<MyObject>();
    public int Korpus;
    public int Number;
    public int MinPriceStudio;
    public int MinPriceFlat2;
    public int MinPriceFlat3;
    public List<MyObject> Studio = new List<MyObject>();
    public List<MyObject> Flat2 = new List<MyObject>();
    public List<MyObject> Flat3 = new List<MyObject>();
}

// [Serializable]
// public class MyFloor
// {
//     public List<MyObject> MyObjects = new List<MyObject>();
//     public int MinPrice;
//     public float MaxArea;
//     public float MinArea;
//     public int NumberFloor;
// }

[Serializable]
public class MyObject
{
    public ObjectClass ObjectClass;
    public float Area;
    public float AreaKitchen;
    public int Korpus;
    public int Section;
    public int Floor;
    public int CountFloor;
    public float CeilingHeight; //Находиться в Билдинге
    public int Number;
    public int NumberInSection;
    public int NumberLight;
    public int Price;
    public int? CountRooms;
    public int Id;
    
   
    public bool IsFree;
    public Sprite FlatSprite;
    public Sprite FloorSprite;
    public int Status;
    public string BuildingName;
    public string UrlFlat;
    public string UrlFloor;

    public MyObject(ObjectClass objectClass)
    {
        ObjectClass = objectClass;
        Area = ObjectClass.Area.Value;
        Korpus = 1;
        Floor = ObjectClass.Floor;
        CountFloor = ObjectClass.FloorsTotal;
        Price = ObjectClass.Price.Value;
        if (ObjectClass.KitchenSpace == null) AreaKitchen = 0;
        else AreaKitchen = ObjectClass.KitchenSpace.Value;
        Number = ObjectClass.Location.Apartment;
        string[] strings = ObjectClass.Section.Split(' ');
        Korpus = int.Parse(strings[1]);
        BuildingName = ObjectClass.Section;
        if (ObjectClass.Rooms == null) CountRooms = 0;
        else CountRooms = ObjectClass.Rooms;
        UrlFlat = ObjectClass.Image[0].Url;
        UrlFloor = ObjectClass.Image[ObjectClass.Image.Count - 1].Url;
        Id = ObjectClass.Id;

        if (Korpus == 2)
        {
            if (Number <= 102)
            {
                Section = 1;
                NumberInSection = Number;
                NumberLight = Number - 11;
            }
            else if (Number <= 196)
            {
                Section = 2;
                NumberInSection = Number - 102;
                NumberLight = NumberInSection-10+100;
            }
            else if (Number <= 298)
            {
                Section = 3;
                NumberInSection = Number - 196;
                NumberLight = NumberInSection - 11 + 200;
            }
            else if (Number <= 392)
            {
                Section = 4;
                NumberInSection = Number - 298;
                NumberLight = NumberInSection - 10 + 300;
            }
            else
            {
                Section = 5;
                NumberInSection = Number - 392;
                NumberLight = NumberInSection - 10 + 400;
            }
        }

        if (Korpus == 3)
        {
            if (Number <= 94)
            {
                Section = 6;
                NumberInSection = Number;
                NumberLight = Number - 10 + 500;
            }
            else if (Number <= 188)
            {
                Section = 7;
                NumberInSection = Number - 94;
                NumberLight = NumberInSection - 10 + 600;
            }
            else if (Number <= 272)
            {
                Section = 8;
                NumberInSection = Number - 188 + 2;
                NumberLight = NumberInSection + 700;
            }
            else
            {
                Section = 9;
                NumberInSection = Number - 274 + 5;
                NumberLight = NumberInSection + 800;
            }
        }
    }

    public IEnumerator LoadSprites()
    {
        FlatSprite = Resources.Load<Sprite>("Flat/" + Korpus + "_" + Section + "_" + NumberInSection + "_" + Number);
        FloorSprite = Resources.Load<Sprite>("Floor/" + Korpus + "_" + Section + "_" + NumberInSection + "_" + Number);

        if (FlatSprite == null)
        {
            //Грузим с сайта
            yield return GameManager.instance.createImagePNG.LoadSpriteFromUrl(this);
        }
        yield return null;
    }

}

// [Serializable]
// public class SendNumberFlat
// {
//     public int Korpus;
//     public int House;
//     public int Porch;
//     public int StartFlat;
//     public int FinishFlat;
// }


