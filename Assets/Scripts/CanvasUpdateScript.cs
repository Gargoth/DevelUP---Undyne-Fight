using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasUpdateScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthTextUI;
    [SerializeField] private TextMeshProUGUI _scoreTextUI;
    void Start()
    {
        _healthTextUI = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _scoreTextUI = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void SetHealth(int health)
    {
        _healthTextUI.text = "Health : " + health.ToString();
    }

    public void SetScore(int score)
    {
        _scoreTextUI.text = "Score  : " + score.ToString();
    }
}
