using SaveSystem;
using TMPro;
using UnityEngine;

public class TestPanel : MonoBehaviour
{
    private const string IntValue = nameof(IntValue);
    private const string FloatValue = nameof(FloatValue);
    private const string StringValue = nameof(StringValue);
    
    [SerializeField] private TMP_InputField _inputField;

    public void OnSetIntValueButtonClicked()
    {
        int inputFieldText = int.Parse(_inputField.text);
        var intValue = inputFieldText;
        Storage.SetInt(IntValue, intValue);
    }

    public void OnGetIntValueButtonClicked()
    {
        int value = Storage.GetInt(IntValue);
        _inputField.text = value.ToString();
    }
    
    public void OnSetFloatValueButtonClicked()
    {
        float inputFieldText = float.Parse(_inputField.text);
        var floatValue = inputFieldText;
        Storage.SetFloat(FloatValue, floatValue);
    }

    public void OnGetFloatValueButtonClicked()
    {
        float value = Storage.GetFloat(FloatValue);
        _inputField.text = value.ToString();
    }
    
    public void OnSetStringValueButtonClicked()
    {
        string inputFieldText = _inputField.text;
        var stringValue = inputFieldText;
        Storage.SetString(StringValue, stringValue);
    }

    public void OnGetStringValueButtonClicked()
    {
        string value = Storage.GetString(StringValue);
        _inputField.text = value;
    }
}
