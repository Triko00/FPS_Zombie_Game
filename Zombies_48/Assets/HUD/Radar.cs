using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarObject
{
    public Image icon { get; set; }
    public GameObject owner { get; set; }
}

public class Radar : MonoBehaviour
{
    public Transform playerPos;
    public float mapScale = 2.0f;

    public static List<RadarObject> radObjects = new List<RadarObject>();

    public static void RegisterRadarObject(GameObject o, Image i)
    {
        Image image = Instantiate(i);
        radObjects.Add(new RadarObject() { owner = o, icon = image });
    }

    public static void RemoveRadarObject(GameObject o)
    {
        List<RadarObject> newList = new List<RadarObject>();
        for(int i = 0; i <radObjects.Count; i++)
        {
            if (radObjects[i].owner == o)
            {
                Destroy(radObjects[i].icon);
                continue;
            }
            else
                newList.Add(radObjects[i]);
        }

        radObjects.RemoveRange(0, radObjects.Count);
        radObjects.AddRange(newList);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
