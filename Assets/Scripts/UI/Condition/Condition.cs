using ScottGarland;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float _curValue;
    public float _startValue;
    public float _maxValue;
    public float _passiveValue;
    public Image _uiBar;

    // Start is called before the first frame update
    void Start()
    {
        _curValue = _startValue;
    }

    // Update is called once per frame
    void Update()
    {
        _uiBar.fillAmount = GetPercentage();
    }

    private float GetPercentage()
    {
        return _curValue / _maxValue;
    }

    public void Add(float value)
    {
        _curValue = Mathf.Min(_curValue + value, _maxValue);
    }

    public void Subtract(float value)
    {
        _curValue = Mathf.Max(_curValue - value, 0);
    }

    public bool IncreaseMax()
    {
        if(_curValue == _maxValue)
        {
            _maxValue = _curValue * 1.2f;
            _curValue = 0;
            return true;
        }
        
        return false;
    }

    public void OnClickUpgradeButton()
    {
        _maxValue += 50;
        SoundManager.Instance.Play("heal", Sound.Sfx);

        Camera uiCam = CameraManager.Instance._ovetlayCameras[0];

        Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(uiCam, EffectManager.Instance._effectUI.position);
        Vector3 worldPosition = uiCam.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 100f));

        EffectManager.Instance.CreateFx(worldPosition);
        Debug.Log("최대 체력 증가");
    }
}
