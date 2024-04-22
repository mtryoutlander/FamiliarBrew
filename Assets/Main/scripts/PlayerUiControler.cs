using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUiControler : MonoBehaviour
{
    [SerializeField] private PlayerControler player;
    [SerializeField] private Image progressBar;

    private void Start()
    {
        player.OnHealthChange += UpdateHealthBar;
        progressBar.fillAmount = 0f;
        Hide();
    }

    private void UpdateHealthBar(object sender, PlayerControler.OnHealthChangeEvent e)
    {
        Show();
        progressBar.fillAmount = e.progressNormalized;
        if (progressBar.fillAmount >= 1)
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
