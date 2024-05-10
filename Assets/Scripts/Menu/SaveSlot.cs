using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profileId = "";

    [Header("Content")] 
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;
   
    [SerializeField] private TextMeshProUGUI fileNameText;
    [SerializeField] private TextMeshProUGUI timeText;

    [Header("Clear Data Button")] [SerializeField]
    private Button clearButton;
   
    private Button saveSlotButton;

    private void Awake()
    {
        saveSlotButton = this.GetComponent<Button>();
    }

    public void SetData(GameData data)
    {
        // theres no data for this profile id
        if (data == null)
        {
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
            clearButton.gameObject.SetActive(false);
        }
        else
        {
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);
            clearButton.gameObject.SetActive(true);
         
            timeText.text = data.GetTime().ToString();
            fileNameText.text = data.GetFileName();
        }
    }

    public string GetProfileID()
    {
        return this.profileId;
    }
   
    public void Setinteractable(bool interactable)
    {
        saveSlotButton.interactable = interactable;
        clearButton.interactable = interactable;
    }
}