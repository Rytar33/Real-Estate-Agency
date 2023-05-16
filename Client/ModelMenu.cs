using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ModelMenu
    {
        public List<string> lines { get; set; }
        public bool IsChoise { get; set; } = false;
        public string? ChoiseText { get; set; } = "Введите индекс";
        public string GetMenu()
        {
            string menu = "";
            for (int i = 0; i < lines.Count; i++)
            {
                menu += $"{i + 1}) " + lines[i] + "\n";
            }
            if (IsChoise) menu += $"{ChoiseText}: ";
            return menu;
        }
    }
}
