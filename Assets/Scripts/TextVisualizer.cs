using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TextVisualizer : MonoBehaviour
{
    private TMP_Text _textField;
    private int _currentChaptersAmount;
    private string _targetText;

    private void Awake()
    {
        _textField = GetComponent<TMP_Text>();
    }

    private IEnumerator WriteText()
    {
        while (_currentChaptersAmount < _targetText.Length)
        {
            _textField.text = _targetText.Substring(0, _currentChaptersAmount + 1);
            _currentChaptersAmount++;

            if (_currentChaptersAmount == _targetText.Length)
            {
                _currentChaptersAmount = 0;
                yield break;
            }

            yield return new WaitForSeconds(0.01f);
        }
    }

    public void Write(string text)
    {
        _targetText = text;
        StartCoroutine(WriteText());
    }
}