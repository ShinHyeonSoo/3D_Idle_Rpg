using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition _hp;
    public Condition _mp;
    public Condition _exp;
    public TextMeshProUGUI _levelText;

    void Start()
    {
        CharacterManager.Instance.Player.Condition._uiCondition = this;
    }
}
