using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceUI : MonoBehaviour
{

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowIndicator()
    {
        gameObject.SetActive(true);
    }

    public void HideIndicator()
    {
        gameObject.SetActive(false);
    }

}
