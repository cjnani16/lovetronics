using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedPartInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        gameObject.GetComponent<HardwareRowController>().hardware = GameManager.manager.getDroppedPart();
        Debug.Log("successfully set dropped part: "+(GameManager.manager.getDroppedPart() != null));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
