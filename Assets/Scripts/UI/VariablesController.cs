using System;
using TMPro;
using UnityEngine;

public class VariablesController : MonoBehaviour
{
    public TMP_Text player1EnergyText;
    public TMP_Text player2EnergyText;
    public TMP_Text variable1Text;
    public TMP_Text variable2Text;
    public TMP_Text variable3Text;
    public TMP_Text variable4Text;

    public void Start()
    {
        GameManager.onProgrammingPhaseEntered.AddListener(() =>
        {
            player1EnergyText.text = $"Player 1 Energy: {PlayerVariables.Player1Energy}";
            player2EnergyText.text = $"Player 2 Energy: {PlayerVariables.Player2Energy}";
            variable1Text.text = $"Variable1: {GlobalVariables.variable1}";
            variable2Text.text = $"Variable2: {GlobalVariables.variable2}";
            variable3Text.text = $"variable3: {GlobalVariables.variable3}";
            variable4Text.text = $"variable4: {GlobalVariables.variable4}";
        });
    }
}
