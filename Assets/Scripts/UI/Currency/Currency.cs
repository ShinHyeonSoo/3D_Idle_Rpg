using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Currency : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _valueText;
    public int _curValue;

    private void Start()
    {
        _curValue = 0;
        _valueText.text = _curValue.ToString();
    }

    public void Add(int value)
    {
        _curValue += value;
        _valueText.text = _curValue.ToString();
    }

    public void Subtract(int value)
    {
        _curValue -= value;
        _valueText.text = _curValue.ToString();
    }
}
