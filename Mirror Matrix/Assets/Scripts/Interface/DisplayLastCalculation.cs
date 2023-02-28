using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// on player death --> displays last calculation so player can see their mistakes
/// </summary>

public class DisplayLastCalculation : MonoBehaviour
{
    [SerializeField] List<GameObject> calcObjects;
    List<bool> calcObjValues = new List<bool>();
    [SerializeField] List<TMPro.TextMeshProUGUI> valueFields;
    List<string> fieldTexts = new List<string>();

    public void SaveCalc()
    {
        calcObjValues.Clear();
        fieldTexts.Clear();

        foreach (GameObject obj in calcObjects)
        {
            calcObjValues.Add(obj.activeInHierarchy);
        }

        foreach (TMPro.TextMeshProUGUI text in valueFields)
        {
            fieldTexts.Add(text.text);
        }
    }

    public void DisplayCalc()
    {
        for (int i = 0; i < calcObjects.Count; i++)
        {
            calcObjects[i].SetActive(calcObjValues[i]);
        }

        for (int i = 0; i < valueFields.Count; i++)
        {
            valueFields[i].text = fieldTexts[i];
        }
    }
}
