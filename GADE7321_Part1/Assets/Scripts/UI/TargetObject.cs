using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{

    private void Awake()
    { 
        UIController ui = GetComponentInParent<UIController>();
        if(ui == null)
        {
            ui = GameObject.Find("UIController").GetComponent<UIController>();
        }

        if (ui == null) Debug.LogError("No UIController component found");

        ui.AddTargetIndicator(gameObject);
    }
}
