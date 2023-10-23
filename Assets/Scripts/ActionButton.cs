using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ActionButton : MonoBehaviour
{
    [SerializeField] private TMP_Text buttonText;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void SetupButton(string text, UnityAction action)
    {
        buttonText.text = text;
        _button.onClick.AddListener(action);
    }

    public void DestroyItself()
    {
        ActionsPanel actionsPanel = transform.parent.GetComponent<ActionsPanel>();
        actionsPanel.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}