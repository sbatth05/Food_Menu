using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace Food_Menu
{
    [Activity(Label = "New_Master_Food")]
    public class New_Master_Food : Activity
    {
        List<Tbl_Master_Foods> List_master_food;
        EditText txt_master_food;
        Button btn_save_master_food;
        Button btn_delete_master_food;
        Button btn_update_master_food;
        Button btn_new_master_food;
        Button btn_set_food_rate;

        Spinner spinner;
        TextView txtviewid;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Food_Master_Layout );
            // Create your application here


            btn_save_master_food = FindViewById<Button>(Resource.Id.btn_save_master_food);
            // Create your application here
            btn_save_master_food = FindViewById<Button>(Resource.Id.btn_save_master_food);
            btn_delete_master_food = FindViewById<Button>(Resource.Id.btn_delete_master_food);

            btn_new_master_food = FindViewById<Button>(Resource.Id.btn_new_master_food);
            btn_update_master_food = FindViewById<Button>(Resource.Id.btn_update_master_food);
            btn_set_food_rate= FindViewById<Button>(Resource.Id.btn_set_food_rate);
            txt_master_food = FindViewById<EditText>(Resource.Id.txt_master_food);
            spinner = FindViewById<Spinner>(Resource.Id.spinner_show);
            txtviewid = FindViewById<TextView>(Resource.Id.txt_v_master_food_id);


            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "foods.sqlite");
            var db = new SQLiteConnection(dpPath);

            db.CreateTable<Tbl_Master_Foods>();

            load_spiner_master_food();

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_show_ItemSelected);


            btn_save_master_food.Click += btn_save_master_food_Click; ;
            btn_new_master_food.Click += btn_new_master_food_Click;
            btn_delete_master_food.Click += btn_delete_master_food_Click;
            btn_update_master_food.Click += btn_update_master_food_Click;
            btn_set_food_rate.Click += Btn_set_food_rate_Click;
        }

        private void Btn_set_food_rate_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(New_Food_Rates));
        }

        private void load_spiner_master_food()
        {
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "foods.sqlite");
            var db = new SQLiteConnection(dpPath);
            var data_s = db.Query<Tbl_Master_Foods>("select *  from Tbl_Master_Foods");
            List_master_food = data_s;
            Food_Menu .Resources.Adapter_Master_Foods  da = new Resources.Adapter_Master_Foods(this, List_master_food);
            spinner.Adapter = da;

        }

        private void btn_update_master_food_Click(object sender, EventArgs e)
        {
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "foods.sqlite");
            var db = new SQLiteConnection(dpPath);





            var item = new Tbl_Master_Foods();

            item.Id = Convert.ToInt32(txtviewid.Text);




            item.Master_Food_Name  = txt_master_food.Text;


            db.Update(item);

            Toast.MakeText(this, "Record Updated Successfully...,", ToastLength.Short).Show();

            load_spiner_master_food();
        }

        private void btn_delete_master_food_Click(object sender, EventArgs e)
        {
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "foods.sqlite");
            var db = new SQLiteConnection(dpPath);


            var subitem = new Tbl_Food_Rates();
            subitem.Mfid = Convert.ToInt32(txtviewid.Text);

            var data_s = db.Query<Tbl_Food_Rates>("select *  from Tbl_Food_Rates where Mfid=" + Convert.ToInt32(txtviewid.Text));
            if (data_s.Count > 0)
            {
                Toast.MakeText(this, "Record Will not deleted as Food Exists...,", ToastLength.Short).Show();

            }
            else
            {
                var item = new Tbl_Food_Rates();
                item.Id = Convert.ToInt32(txtviewid.Text);
                var data = db.Delete(item);
                Toast.MakeText(this, "Record Deleted Successfully...,", ToastLength.Short).Show();
                txt_master_food.Text = "";
                load_spiner_master_food();

            }


        }

        private void btn_new_master_food_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(New_Master_Food));
        }

        private void btn_save_master_food_Click(object sender, EventArgs e)
        {
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "foods.sqlite");
            var db = new SQLiteConnection(dpPath);
            db.CreateTable<Tbl_Master_Foods>();
            Tbl_Master_Foods tbl = new Tbl_Master_Foods();
            tbl.Master_Food_Name = txt_master_food.Text;
            db.Insert(tbl);
            Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();
            txt_master_food.Text = "";
            load_spiner_master_food();

        }

        private void spinner_show_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            var id = this.List_master_food.ElementAt(e.Position).Id;
            var masteraccountname = this.List_master_food.ElementAt(e.Position).Master_Food_Name ;
            txtviewid.Text = Convert.ToString(id);
            // txt_category.Text = masteraccountname;
            btn_delete_master_food.Enabled = true;

        }
    }
}