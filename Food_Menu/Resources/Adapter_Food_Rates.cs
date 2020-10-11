using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Food_Menu.Resources
{
   public class Adapter_Food_Rates : BaseAdapter<Tbl_Food_Rates>
    {
        private readonly Activity context;
        private readonly List<Tbl_Food_Rates> mItems;

        public Adapter_Food_Rates(Activity context, List<Tbl_Food_Rates> items)
        {
            this.mItems = items;
            this.context = context;
        }



        public override int Count
        {
            get { return mItems.Count; }

        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Tbl_Food_Rates this[int position]
        {
            get { return mItems[position]; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            var row = convertView;


            if (row == null)
            {
                row = LayoutInflater.From(context).Inflate(Resource.Layout.List_Food_Rates, null, false);
            }

           
           




            TextView txtRowName_food = row.FindViewById<TextView>(Resource.Id.txtRowName_food);
            txtRowName_food.Text = mItems[position].Food_Name ;

            TextView txt_food_rate = row.FindViewById<TextView>(Resource.Id.txt_food_rate);

            txt_food_rate.Text = ">>>>    Rates : $ " + Convert.ToString(mItems[position].Food_Rate );





            return row;


        }
    }
}