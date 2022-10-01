namespace lab2.Menu.MenuCommand
{
    public class ProgrammCommand : MenuStorage
    {
        public List<string> GetMenus()
        {
            return new List<string>
            {
                "ГПСЧ",
                "Работа с файлами",
                "Выход"
            };
        }
    }
}
