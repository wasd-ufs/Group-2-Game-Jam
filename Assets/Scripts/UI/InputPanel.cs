using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InputPanel : MonoBehaviour
{
    public readonly UnityEvent onShowPanel  = new();
    public readonly UnityEvent onHidePanel = new();
    public readonly UnityEvent<string> onNameInputed = new();

    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button confirmButton;
    
    public void Start()
    {
        confirmButton.onClick.AddListener(OnConfirmButtonClicked);
    }

    public void Show()
    {
        if (gameObject.activeSelf)
            return;
        
        gameObject.SetActive(true);
        inputField.text = "";
        inputField.ActivateInputField();
        onShowPanel.Invoke();
    }

    public void Hide()
    {
        if (!gameObject.activeSelf)
            return;
        
        gameObject.SetActive(false);
        onHidePanel.Invoke();
    }

    private void OnConfirmButtonClicked()
    {
        if (inputField.text.Length == 0)
            return;
        
        onNameInputed.Invoke(inputField.text.Trim());
    }
    
    public bool IsHiding() => !gameObject.activeSelf;
    public bool IsShowing() => gameObject.activeSelf;
}
