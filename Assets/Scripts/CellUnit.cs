using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellUnit : MonoBehaviour
{   
    
    HexCell location;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public HexCell Location
    {
        get
        {
            return location;
        }
        set
        {
            location = value;
            transform.localPosition = value.transform.localPosition;
        }
    }

 
}
