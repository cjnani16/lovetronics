using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HardwarePanelController : MonoBehaviour
{
    public Hardware.Category category;
    public GameObject hardwareRowPrefab;
    public List<Hardware> options;

    private List<HardwareRowController> rows;

    void Start()
    {
        List<Hardware> allHardware = GameManager.manager.getHardware();
        rows = new List<HardwareRowController>();
        options = allHardware.Where(h => h.getCategory() == category).ToList();
        for (int i = 0; i < options.Count; i++)
        {
            GameObject row = Instantiate(hardwareRowPrefab, new Vector3(562, 930 - i * 310, 0), Quaternion.identity);
            row.transform.SetParent(transform);
            HardwareRowController rowController = row.GetComponent<HardwareRowController>();
            rowController.hardware = options[i];
            rowController.controller = this;
            rows.Add(rowController);
        }
    }

    public void updateSelectedStates()
    {
        for (int i = 0; i < rows.Count; i++)
        {
            rows[i].setBackgroundColor();
        }
    }


}
