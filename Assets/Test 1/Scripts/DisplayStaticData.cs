using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStaticData : MonoBehaviour
{
    public TextMeshProUGUI displayText;

    void Start()
    {
        displayText.text = StaticData.textData;
    }
}
