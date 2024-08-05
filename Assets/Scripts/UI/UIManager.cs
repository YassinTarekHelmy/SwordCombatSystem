using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private GameObject _deathScreen;

    private void Awake() {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        _deathScreen.SetActive(false);
    }
    
    public void ShowDeathScreen()
    {
        _deathScreen.SetActive(true);
    }

    public void HideDeathScreen()
    {
        _deathScreen.SetActive(false);
    }
}
