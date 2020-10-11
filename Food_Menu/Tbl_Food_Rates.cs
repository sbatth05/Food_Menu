using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Food_Menu
{
   public class Tbl_Food_Rates
    {
        [PrimaryKey, AutoIncrement] //Column("Id")]
        public int Id { get; set; }
        public int Mfid { get; set; }
        public string Food_Name { get; set; }

        public int Food_Rate { get; set; }
    }
}