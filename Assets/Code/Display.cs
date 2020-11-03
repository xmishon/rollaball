using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Debug;

public sealed class Display
{
    private Text _bonusesCountTextField;
    private Text _gameFinishedTextField;

    public Display()
    {
        _bonusesCountTextField = GameObject.Find("BonusesCountText").GetComponent<Text>();
        _gameFinishedTextField = GameObject.Find("GameFinishedText").GetComponent<Text>();
    }

    public void DisplayBonuses(int value)
    {
        _bonusesCountTextField.text = $"Вы набрали {value}";
        Log("Изменено количество бонусов: " + value);
    }

    public void DisplayGameFinished()
    {
        _bonusesCountTextField.text = "";
        _gameFinishedTextField.text = "Победа";
        Log("Игра закончена");
    }
}
