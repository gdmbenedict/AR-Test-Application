using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject regularUI;
    [SerializeField] private GameObject infoUI;

    [Header("Info Screen")]
    [SerializeField] private Image infoPanel;
    [SerializeField] private Color infoPanelColor;

    public void SwitchToInfo()
    {
        regularUI.SetActive(false);
        infoUI.SetActive(true);
    }

    public void SwitchToReg()
    {
        regularUI.SetActive(true);
        infoUI.SetActive(false);
    }
}
