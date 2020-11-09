using System.Collections.Generic;
using UnityEngine;

namespace mzmeevskiy
{
    public static class CustomColors
    {
        public static Dictionary<string, Color> ColorsDictionary
        {
            get
            {
                return new Dictionary<string, Color>() { 
                    { "Red",  Color.red },
                    { "Green", Color.green },
                    { "Blue", Color.blue },
                    { "Yellow", Color.yellow },
                    { "Magenta", Color.magenta },
                    { "Black", Color.black },
                    { "Gray", Color.gray }};
            }
            private set
            {
                ColorsDictionary = value;
            }
            
        }

        public enum CustomColor : int
        {
            Red,
            Green,
            Blue,
            Yellow,
            Magenta,
            Black,
            Gray
        }
        
        public static Color GetColor(CustomColor colorEnum)
        {
            return ColorsDictionary[colorEnum.ToString()];
        }

        public static CustomColor GetRandomColor()
        {
            int enumLength = System.Enum.GetNames(typeof(CustomColor)).Length;
            System.Random random = new System.Random();
            int rNum = random.Next(enumLength);
            return (CustomColor) rNum;
        }
    }
}