using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejednevnichek__
{
    public class Notes
    {
        public string name;
        public string description;
        public DateTime date1;


        public Notes(string s1, string s2, DateTime d1)
        {
            name = s1;
            description = s2;
            date1 = d1;

        }
        public static List<Notes> MyNotesDs = new List<Notes>();
        //Заметка должна состоять из названия, описания, и даты, когда заметка должна быть выполнена
    }
}

