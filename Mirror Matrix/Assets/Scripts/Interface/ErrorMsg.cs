using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErrorMsg : MonoBehaviour
{
    //0
    [SerializeField] [TextArea] string story1;
    [SerializeField] string tstory1;
    //1
    [SerializeField] [TextArea] string story2;
    [SerializeField] string tstory2;
    //2
    [SerializeField] [TextArea] string story3;
    [SerializeField] string tstory3;
    //3
    [SerializeField] [TextArea] string death;
    [SerializeField] string tdeath;
    //4
    [SerializeField] [TextArea] string win;
    [SerializeField] string twin;
    //5
    [SerializeField] [TextArea] string energy;
    [SerializeField] string tenergy;
    //6
    [SerializeField] [TextArea] string calculation;
    [SerializeField] string tcalculation;
    //7
    [SerializeField] [TextArea] string values;
    [SerializeField] string tvalues;

    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;

    [SerializeField] GameObject messageObject;
    [SerializeField] GameObject deathButton;
    [SerializeField] GameObject certificateButton;
    [SerializeField] GameObject closeButton;

    [SerializeField] private float waitForDestroy = 1.5f;

    List<string> errors = new List<string>();
    List<string> terrors = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        errors.Add(story1);
        errors.Add(story2);
        errors.Add(story3);
        errors.Add(death);
        errors.Add(win);
        errors.Add(energy);
        errors.Add(calculation);
        errors.Add(values);

        terrors.Add(tstory1);
        terrors.Add(tstory2);
        terrors.Add(tstory3);
        terrors.Add(tdeath);
        terrors.Add(twin);
        terrors.Add(tenergy);
        terrors.Add(tcalculation);
        terrors.Add(tvalues);

        closeButton.SetActive(true);
        deathButton.SetActive(false);
        certificateButton.SetActive(false);

        EventManager.Instance.AddEventListener("ERROR", ErrorListener);

        title.text = terrors[0];
        description.text = errors[0];
        messageObject.SetActive(true);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveEventListener("ERROR", ErrorListener);
    }

    void ErrorListener(string eventName, object param)
    {
        OpenError((int)param);
    }

    public void OpenError(int errorNumber)
    {
        if (errorNumber == 3)
        {
            StartCoroutine(WaitForDestroy());
        }
        else
        {
            title.text = terrors[errorNumber];
            description.text = errors[errorNumber];
            messageObject.SetActive(true);

            if (errorNumber == 4)
            {
                certificateButton.SetActive(true);
                closeButton.SetActive(false);
            }
        }
    }

    public void ShowDestroy()
    {
        title.text = terrors[3];
        description.text = errors[3];
        messageObject.SetActive(true);
        deathButton.SetActive(true);
        closeButton.SetActive(false);
    }

    public void CloseError()
    {
        messageObject.SetActive(false);
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(waitForDestroy);
        ShowDestroy(); 
    }

    public void DisableAllErrorButtons()
    {
        CloseError();
        deathButton.SetActive(false);
        certificateButton.SetActive(false);
    }
}
