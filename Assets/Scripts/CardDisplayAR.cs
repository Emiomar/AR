using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CardDisplayAR : MonoBehaviour
{

    public TMP_Text displayText;
    public BDYugiOH cardAccess;

    void Start()
    {
        cardAccess = GameObject.FindGameObjectWithTag("BD").GetComponent<BDYugiOH>();
            
    }

    public void OnTargetFound(Transform imageTargetTransform)
    {
        string cardName = imageTargetTransform.name;
        Debug.Log($"Carta detectada{cardName}");
        displayText = imageTargetTransform.Find("Text").GetComponent<TextMeshPro>();

        if (displayText != null)
        {
            cardAccess.LoadCardData(cardName, displayText);
        }

        else
        {
            Debug.Log("Carta no encontrada");
        }
    }


    public void OnTargetLost (Transform imageTargetTransform)
    {
        displayText = imageTargetTransform.Find("Text").GetComponent<TextMeshPro>();
        if (displayText != null)
        {
            displayText.text = "Buscando carta";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
