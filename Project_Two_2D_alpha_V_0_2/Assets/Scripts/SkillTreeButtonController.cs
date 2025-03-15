using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillTreeButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string description;
    [SerializeField] private List<Button> buttons;
    [SerializeField] private TMP_Text descriptionText;

    public void OnButtonClicked()
    {
        if (buttons.Count != 0)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].interactable = true;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (descriptionText != null)
        {
            descriptionText.text = description;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (descriptionText != null)
        {
            descriptionText.text = "";
        }
    }
}