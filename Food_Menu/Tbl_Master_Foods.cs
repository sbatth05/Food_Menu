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
   public class Tbl_Master_Foods
    {
        [PrimaryKey, AutoIncrement] //Column("Id")]
        public int Id { get; set; }

        [MaxLength(25)]
        public string Master_Food_Name { get; set; }
    }
}