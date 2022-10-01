namespace lab2.Menu.MenuCommand
{
    public class PRNGCommand : MenuStorage
    {
        public List<string> GetMenus()
        {
            return new List<string>
            {
                "Генерация новых значений",
                "Получение числа",
                "Получение последовательности чисел",
                "Сохранить параметры",
                "Выход"
            };
        }
    }
}
