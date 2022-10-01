using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public static class StaticData
    {
        private static string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
        private static string path = Path.GetDirectoryName(location)+@"\SystemPath";

        public static string PathKeyFale = path + @"\GeneratorParameter.key";
        public static string PathInputText = path + @"\InputText.txt";
        public static string PathEncodeText = path + @"\EncodeText.txt";
        public static string PathDecodeText = path + @"\DecodeText.txt";
        public static string PathHistogrammData = path + @"\HistogrammData.txt";
        public static string PathHistogrammCapture = path + @"\HistogrammCapture.txt";
    }
}
