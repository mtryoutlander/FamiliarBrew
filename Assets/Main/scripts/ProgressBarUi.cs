using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUi : MonoBehaviour {

    [SerializeField] private ShopTool tool;
    [SerializeField] private Image progressBar;

    private void Start()
    {
        tool.OnProgressChanged += UpdateProgressBar;
        progressBar.fillAmount = 0f;
        Hide();
    }

    private void UpdateProgressBar(object sender, ShopTool.OnProgressChangeEventArgs e)
    {
        Show();
        progressBar.fillAmount = e.progressNormalized;
        if(progressBar.fillAmount >= 1)
        {
            Hide();
        }
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
}
