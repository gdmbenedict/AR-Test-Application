using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject regularUI;
    [SerializeField] private GameObject infoUI;

    public void Quit()
    {
        Application.Quit();
    }

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
