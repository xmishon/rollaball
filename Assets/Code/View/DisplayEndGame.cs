using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Debug;

public sealed class DisplayEndGame
{
    private Text _finishGameLabel;

    public DisplayEndGame(Text finishGameLabel)
    {
        _finishGameLabel = finishGameLabel;
    }

    public void GameOver(string name, string color)
    {
        _finishGameLabel.text = $"Вы проиграли. Вас убил {name} {color} цвета";
    }
}
