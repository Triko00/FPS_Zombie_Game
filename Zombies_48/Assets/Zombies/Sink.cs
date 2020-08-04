using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{
    float destroyHeight;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartSink()
    {
        destroyHeight = Terrain.activeTerrain.SampleHeight(this.transform.position) - 5;
        Collider[] colList = this.GetComponentsInChildren<Collider>();
        foreach (Collider c in colList)
        {
            Destroy(c);
        }

        InvokeRepeating("SinkIntoGround", 5, 0.1f); 

    }

    void SinkIntoGround()
    {
        this.transform.Translate(0, -0.001f, 0);
        if (this.transform.position.y < destroyHeight)
        {
            Destroy(this.gameObject);
        }
    }
}
