using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class SerializeXML : MonoBehaviour
{

    private HttpClient Client = new HttpClient();
    private string _json;
    [HideInInspector] public FeedClass _feedClass;
    //public string _feedURL;
    
    //ссылка на json
    private string _url = "https://bx.sskuban.ru/local/integrat/feed/yandex/4275607";
    //private string _url = "https://feed.storage-s3.ru/feed_flat_regions_new.xml";
    private GameManager _manager;
    
    
    public async Task Init(GameManager manager)
    {
        _manager = manager;
        await LoadJSON(_url);
    }

    private async Task LoadJSON(string url)
    {
        var uri = new Uri(url);
        _manager.InfoStartPanel.text += "Load Json4..."+"\r\n";
        using var result = await Client.GetAsync(_url);
        _manager.InfoStartPanel.text += "Load Json5..."+"\r\n";
        string str = await result.Content.ReadAsStringAsync();
        _manager.InfoStartPanel.text += "Load Json6..."+"\r\n";

        // using (UnityWebRequest request = UnityWebRequest.PostWwwForm(_url,""))
        // {
        //     request.SetRequestHeader("Content-Type", "application/json");
        //     request.timeout = 5;
        //     
        //     
        // }
        
        
        XmlSerializer serializer = new XmlSerializer(typeof(FeedClass));
        
        using (StringReader reader = new StringReader(str))
        {
            Debug.Log("CC");
            try
            {
                _manager.InfoStartPanel.text += "Load Json1..."+"\r\n";
                Debug.Log("Try");
                _feedClass = (FeedClass)serializer.Deserialize(reader);
            }
            catch (Exception e)
            {
                _manager.InfoStartPanel.text += "Load Json2..."+"\r\n";
                Debug.Log(e);
                throw;
            }

            GameManager.instance.Feed = _feedClass;
            Debug.Log("XX "+_feedClass.Objects.Count);
             foreach (var obj in _feedClass.Objects)
             {
                 try
                 {
                     Debug.Log(obj.Image.Count);
                 }
                 catch (Exception e)
                 {
                     Debug.Log(e);
                 }
                
             }
            reader.Close();
        }

        _manager.InfoStartPanel.text += "\r\nLoad Feed Complete"+"\r\n";
        result.Dispose();
        serializer = null;

        // foreach (var objectClass in _feedClass.Objects)
        // {
        //     if (objectClass.BuildingSection == "Корпус 1" && objectClass.ApartmentNumber == 167)
        //     {
        //         Debug.Log(objectClass.Name);
        //     }
        // }
        
    }

    private void SaveText(string url, string text)
    {
        File.WriteAllText(url, text);
    }

}


    [XmlRoot("realty-feed", Namespace = "http://webmaster.yandex.ru/schemas/feed/realty/2010-06"), Serializable]
    public class FeedClass
    {
        [XmlElement("generation-date")] 
        public string GenerationDate;
        
        [XmlElement("offer")] 
        public List<ObjectClass> Objects = new List<ObjectClass>();
    }

    [Serializable]
    public class ObjectClass
    {
        [XmlAttribute("internal-id")] public int Id;
        
        [XmlElement("type")] public string TypeSale;

        [XmlElement("category")] public string Category;
        
        [XmlElement("property-type")] public string PropertyType;

        [XmlElement("creation-date")] public string CreationDate;
        
        [XmlElement("last-update-date")] public string LastUpdateDate;
        
        [XmlElement("location")] public Location Location;
        
        //[XmlElement("sales-agent")] public JKSchema JKSchema;
        
        //[XmlElement("deal-status")] public JKSchema JKSchema;
        
        [XmlElement("price")] public Price Price;
        
        [XmlElement("image")] public List<ImageXML> Image = new List<ImageXML>();
        // [XmlElement("image")] public string Image2;
        // [XmlElement("image")] public string Image3;
        // [XmlElement("image")] public string Image4;
        // [XmlElement("image")] public string Image5;
        
        [XmlElement("area")] public Area Area;
        
        [XmlElement("kitchen-space")] public KitchenSpace KitchenSpace;
        
        [XmlElement("renovation")] public string Renovation;
        
        [XmlElement("description")] public string Description;
        
        [XmlElement("new-flat")] public string NewFlat;
        
        [XmlElement("floor")] public int Floor;
        
        [XmlElement("rooms", IsNullable = true)] public int? Rooms;
        
        //[XmlElement("studio")] public string Studio;
        
        [XmlElement("apartments")] public string Apartments;
        
        [XmlElement("floors-total")] public int FloorsTotal;
        
        [XmlElement("building-name")] public string BuildingName;
        
        //[XmlElement("yandex-building-id")] public JKSchema JKSchema;
        
        //[XmlElement("yandex-house-id")] public JKSchema JKSchema;
        
        //[XmlElement("building-state")] public JKSchema JKSchema;
        
        [XmlElement("building-section")] public string Section;
        
        //[XmlElement("built-year")] public JKSchema JKSchema;
        
        //[XmlElement("ready-quarter")] public JKSchema JKSchema;
        
        //[XmlElement("building-type")] public JKSchema JKSchema;
        
        //[XmlElement("lift")] public JKSchema JKSchema;
        
        
        
    }
    
    [Serializable]
    public class Location
    {
        [XmlElement("address")] public string Address;
        [XmlElement("country")] public string Country;
        [XmlElement("apartment")] public int Apartment;
        [XmlElement("region")] public string Region;
        [XmlElement("locality-name")] public string LocalityName;
    }
    
    [Serializable]
    public class Price
    {
        [XmlElement("value")] public int Value;
        [XmlElement("currency")] public string Currency;
    }

    [Serializable]
    public class ImageXML
    {
        [XmlAttribute("tag")] public string Tag;
        [XmlText] public string Url;
    }

    [Serializable]
    public class Area
    {
        [XmlElement("value")] public float Value;
        [XmlElement("unit")] public string Unit;
    }

    [Serializable]
    public class KitchenSpace
    {
        [XmlElement("value")] public float Value;
        [XmlElement("unit")] public string Unit;
    }