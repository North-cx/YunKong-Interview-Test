using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextPanel : MonoBehaviour
{
    public GameObject popupPanel;
    public TextMeshProUGUI contentText;
    public Button closeButton;
    public Button previousButton;
    public Button nextButton;

    public List<TextAsset> chapters;

    private int currentChapterIndex = 0;

    void Start()
    {
        closeButton.onClick.AddListener(ClosePanel);
        previousButton.onClick.AddListener(ShowPreviousChapter);
        nextButton.onClick.AddListener(ShowNextChapter);

        UpdateContent();
        popupPanel.SetActive(false); // ³õÊ¼Ê±Òþ²ØÃæ°å
    }

    public void ShowPanel()
    {
        popupPanel.SetActive(true);
        UpdateContent();
    }

    void ClosePanel()
    {
        popupPanel.SetActive(false);
    }

    void ShowPreviousChapter()
    {
        if (currentChapterIndex > 0)
        {
            currentChapterIndex--;
            UpdateContent();
        }
    }

    void ShowNextChapter()
    {
        if (currentChapterIndex < chapters.Count - 1)
        {
            currentChapterIndex++;
            UpdateContent();
        }
    }

    void UpdateContent()
    {
        contentText.text = chapters[currentChapterIndex].text;
    }

    public void OnOpenPanelButtonClick()
    {
        ShowPanel();
    }
}
