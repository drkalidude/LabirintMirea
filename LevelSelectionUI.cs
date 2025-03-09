using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelSelectionUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject levelSelectionPanel; 
    [SerializeField] private Button toggleButton; 
    [SerializeField] private List<Button> levelButtons; 
    [SerializeField] private Text levelText;
    [SerializeField] private ARLevelController levelController; 

    private bool isPanelOpen = false;

    void Start()
    {
        toggleButton.onClick.AddListener(ToggleLevelPanel);

        for (int i = 0; i < levelButtons.Count; i++)
        {
            int index = i; 
            levelButtons[i].onClick.AddListener(() => SelectLevel(index));
        }

        HideLevelButtons();
        UpdateLevelText(); 
    }

    private void ToggleLevelPanel()
    {
        isPanelOpen = !isPanelOpen;

        if (isPanelOpen)
        {
            ShowLevelButtons();
        }
        else
        {
            HideLevelButtons();
        }
    }

    private void ShowLevelButtons()
    {
        levelSelectionPanel.SetActive(true);
        foreach (var button in levelButtons)
        {
            button.gameObject.SetActive(true);
        }
    }

    private void HideLevelButtons()
    {
        levelSelectionPanel.SetActive(false);
        foreach (var button in levelButtons)
        {
            button.gameObject.SetActive(false);
        }
        isPanelOpen = false;
    }

    private void SelectLevel(int index)
    {
        Debug.Log("Выбран уровень: " + (index + 1));
        levelController.LoadLevelByIndex(index); 
        UpdateLevelText(); 
        HideLevelButtons(); 
    }

    public void UpdateLevelText()
    {
        if (levelController != null && levelText != null)
        {
            levelText.text = (levelController.CurrentLevelIndex + 1).ToString();
        }
    }
}
