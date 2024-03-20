using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private List<TargetIndicator> _targetIndicators = new List<TargetIndicator>();
    [SerializeField] private Camera MainCam;
    [SerializeField] private GameObject TargetIndicatorPrefab;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (_targetIndicators.Count > 0)
        {
            for (int i = 0; i < _targetIndicators.Count; i++)
            {
                _targetIndicators[i].UpdateTargetIndicator();
            }
        }
    }

    public void AddTargetIndicator(GameObject target)
    {
        TargetIndicator indicator = GameObject.Instantiate(TargetIndicatorPrefab, _canvas.transform)
            .GetComponent<TargetIndicator>();
        indicator.InitialiseTargetIndicator(target, MainCam, _canvas);
        _targetIndicators.Add(indicator);
    }
}
