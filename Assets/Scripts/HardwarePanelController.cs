using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HardwarePanelController : MonoBehaviour
{
    public Hardware.Category category;
    public GameObject hardwareRowPrefab;
    public List<Hardware> options;

    void Start()
    {
        List<Hardware> allHardware = GameManager.manager.getHardware();
        options = allHardware.Where(h => h.getCategory() == category).ToList();
        for (int i = 0; i < options.Count; i++)
        {
            GameObject row = Instantiate(hardwareRowPrefab, new Vector3(562, 750 - i * 320, 0), Quaternion.identity);
            row.transform.parent = transform;
            row.GetComponent<HardwareRowController>().hardware = options[i];
        }
    }

    private Hardware getSelectedHardware()
    {
        return GameManager.manager.getSelectedHardware();
    }
}
