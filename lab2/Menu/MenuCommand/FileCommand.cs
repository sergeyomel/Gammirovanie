using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Menu.MenuCommand
{
    public class FileCommand : MenuStorage
    {
        public List<string> GetMenus()
        {
            return new List<string>
            {
                "Кодирование файла",
                "Декодирование файла",
                "Выход"
            };
        }
    }
}
