using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLoader : MonoBehaviour
{
    public Text text;
    public TextAsset textAsset;
    
    // Start is called before the first frame update
    void Start()
    {
        if (textAsset != null)
        {
            text.text = textAsset.text;
        }
        else
        {
            text.text = "文件未指定";
        }
    }
}
