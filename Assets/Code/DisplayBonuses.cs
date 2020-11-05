using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Debug;

public sealed class DisplayBonuses
{
    private Text _text;

    public Display()
    {
        
    }

    public void Display(int value)
    {
        _text.text = $"Вы набрали {value}";
    }
}
