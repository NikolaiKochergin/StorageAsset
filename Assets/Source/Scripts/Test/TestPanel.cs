using SaveSystem;
using TMPro;
using UnityEngine;

public class TestPanel : MonoBehaviour
{
    private const string IntValue = nameof(IntValue);
    private const string FloatValue = nameof(FloatValue);
    private const string StringValue = nameof(StringValue);

    [SerializeField] private GameSceneManager _gameSceneManager;
    [SerializeField] private TMP_InputField _inputField;

    public void OnSetIntValueButtonClicked()
    {
        int inputFieldText = int.Parse(_inputField.text);
        var intValue = inputFieldText;
        _gameSceneManager.Storage.SetInt(IntValue, intValue);
    }

    public void OnGetIntValueButtonClicked()
    {
        int value = _gameSceneManager.Storage.GetInt(IntValue);
        _inputField.text = value.ToString();
    }
    
    public void OnSetFloatValueButtonClicked()
    {
        float inputFieldText = float.Parse(_inputField.text);
        var floatValue = inputFieldText;
        _gameSceneManager.Storage.SetFloat(FloatValue, floatValue);
    }

    public void OnGetFloatValueButtonClicked()
    {
        float value = _gameSceneManager.Storage.GetFloat(FloatValue);
        _inputField.text = value.ToString();
    }
    
    public void OnSetStringValueButtonClicked()
    {
        string inputFieldText = _inputField.text;
        var stringValue = inputFieldText;
        _gameSceneManager.Storage.SetString(StringValue, stringValue);
    }

    public void OnGetStringValueButtonClicked()
    {
        string value = _gameSceneManager.Storage.GetString(StringValue);
        _inputField.text = value;
    }
}
