using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ScottGarland;

public class Currency : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _valueText;
    public int _curValue;
    public BigInteger _bigValue;

    public int _getCount;
    public BigInteger _coinValue;

    private void Start()
    {
        _bigValue = new BigInteger(0);
        _coinValue = 500;
        _curValue = 0;
        UpdateText();
    }

    public void Add(int value)
    {
        _curValue += value;
        UpdateText();
    }

    public void Subtract(int value)
    {
        _curValue -= value;
        UpdateText();
    }

    public void UpdateText()
    {
        _valueText.text = _curValue.ToString();
    }

    public void UpdateBigText()
    {
        string result = BigIntegerFormatter.FormatBigInteger(_bigValue);
        _valueText.text = result;
    }

    public void OnClickGetCoinButton()
    {
        ++_getCount;

        if(_getCount == 2)
        {
            BigInteger big = 1000;
            _coinValue = BigInteger.Multiply(big, _coinValue);
            _getCount = 0;
        }

        _bigValue = BigInteger.Add(_bigValue, _coinValue);
        UpdateBigText();
        Debug.Log("Silver Coins : " + _bigValue.ToString());
    }
}
