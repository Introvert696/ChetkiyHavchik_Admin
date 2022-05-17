using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Четкий_Хавчик_Админ
{
    class workWithFile
    {
        public static string getUrl()
        {
            string result = File.ReadAllText("setting.txt");
            return result;

        }
    }
}
