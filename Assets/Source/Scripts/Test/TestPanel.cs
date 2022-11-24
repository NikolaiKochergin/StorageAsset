using Source.Scripts.SceneManagement;
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
        if (int.TryParse(_inputField.text, out int inputFieldText))
            _gameSceneManager.Storage.SetInt(IntValue, inputFieldText);
        else
            _inputField.text = "Enter an integer value";
    }

    public void OnGetIntValueButtonClicked()
    {
        if (_gameSceneManager.Storage.HasKeyInt(IntValue))
        {
            int value = _gameSceneManager.Storage.GetInt(IntValue);
            _inputField.text = value.ToString();
        }
        else
        {
            _inputField.text = "Value is not setup";
        }
    }
    
    public void OnSetFloatValueButtonClicked()
    {
        if (float.TryParse(_inputField.text, out float inputFieldText))
            _gameSceneManager.Storage.SetFloat(FloatValue, inputFieldText);
        else
            _inputField.text = "Enter a float value";
    }

    public void OnGetFloatValueButtonClicked()
    {
        if (_gameSceneManager.Storage.HasKeyFloat(FloatValue))
        {
            float value = _gameSceneManager.Storage.GetFloat(FloatValue);
            _inputField.text = value.ToString();
        }
        else
        {
            _inputField.text = "Value is not setup";
        }
    }
    
    public void OnSetStringValueButtonClicked()
    {
        string inputFieldText = _inputField.text;
        _gameSceneManager.Storage.SetString(StringValue, inputFieldText);
    }

    public void OnGetStringValueButtonClicked()
    {
        if (_gameSceneManager.Storage.HasKeyString(StringValue))
        {
            string value = _gameSceneManager.Storage.GetString(StringValue);
            _inputField.text = value;
        }
        else
        {
            _inputField.text = "Value is not setup";
        }
    }
}
