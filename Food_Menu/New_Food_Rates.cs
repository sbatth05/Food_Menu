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
    [Activity(Label = "New_Food_Rates")]
    public class New_Food_Rates : Activity
    {
        List<Tbl_Food_Rates> List_All_Foods;
        List<Tbl_Master_Foods> List_All_Master_Foods;
        EditText txtfoodname;
        Button btnsavefood;
        Button btndeletefood;
        Button btnupdatefood;
        Button btnnewfood;
        ListView ListView1;
        Spinner spinnershowmasterFood;
        Spinner spinnershowfood;
        TextView txt_rate;
        TextView txtfoodid;
        TextView txtmasterfoodid;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Set_Food_Rates_Layout );
            btnnewfood = FindViewById<Button>(Resource.Id.btn_new_food);

            btnsavefood = FindViewById<Button>(Resource.Id.btn_save_food);

            btndeletefood = FindViewById<Button>(Resource.Id.btn_delete_food);
            btnupdatefood = FindViewById<Button>(Resource.Id.btn_update_food);
            txtfoodname = FindViewById<EditText>(Resource.Id.txt_food_name);
            spinnershowmasterFood = FindViewById<Spinner>(Resource.Id.spinner_show_master_food);
            spinnershowfood = FindViewById<Spinner>(Resource.Id.spinner_show_food);
            txt_rate = FindViewById<TextView>(Resource.Id.txt_Rate);

            txtfoodid = FindViewById<TextView>(Resource.Id.txt_v_food_id);
            txtmasterfoodid = FindViewById<TextView>(Resource.Id.txt_master_food_id);
            ListView1 = FindViewById<ListView>(Resource.Id.listView1);

            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "foods.sqlite");
            var db = new SQLiteConnection(dpPath);

            db.CreateTable<Tbl_Food_Rates>();






            load_spiner_Master_Foods();
            load_spiner_food();


            spinnershowmasterFood.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_show_Master_Food_ItemSelected);
            spinnershowfood.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_show_Food_ItemSelected);
            btnsavefood.Click += Btnsavefood_Click;
            btndeletefood.Click += Btndeletefood_Click;
            btnupdatefood.Click += Btnupdatefood_Click;
           

        }

        private void Btnupdatefood_Click(object sender, EventArgs e)
        {
            var item_food = new Tbl_Food_Rates();
            item_food.Id = Convert.ToInt32(txtfoodid.Text);
            item_food.Food_Name  = txtfoodname.Text;
            item_food.Food_Rate  = Convert.ToInt32(txt_rate.Text);
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "foods.sqlite");
            var db = new SQLiteConnection(dpPath);

            //db.Update(item_book);

            Toast.MakeText(this, "Record Updated Successfully...,", ToastLength.Short).Show();

            load_spiner_Master_Foods();
            load_spiner_food();

        }

        private void Btndeletefood_Click(object sender, EventArgs e)
        {
            var item = new Tbl_Food_Rates();
            item.Id = Convert.ToInt32(txtfoodid.Text);

            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "foods.sqlite");
            var db = new SQLiteConnection(dpPath);
            var data = db.Delete(item);
            Toast.MakeText(this, "Record Deleted Successfully...,", ToastLength.Short).Show();
            txtfoodname.Text = "";
            load_spiner_Master_Foods();
            load_spiner_food();

        }

        private void Btnsavefood_Click(object sender, EventArgs e)
        {
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "foods.sqlite");
            var db = new SQLiteConnection(dpPath);
            db.CreateTable<Tbl_Food_Rates>();
            Tbl_Food_Rates tbl = new Tbl_Food_Rates();
            tbl.Food_Name  = Convert.ToString(txtfoodname.Text);
            tbl.Mfid  = Convert.ToInt32(txtmasterfoodid.Text);
            tbl.Food_Rate  = Convert.ToInt32(txt_rate.Text);
            db.Insert(tbl);
            Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();
            txtfoodname.Text = "";
            load_spiner_Master_Foods();
            load_spiner_food();

        }

        private void spinner_show_Food_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            var id = this.List_All_Foods.ElementAt(e.Position).Id;
            var foodname = this.List_All_Foods.ElementAt(e.Position).Food_Name ;
            var rate = this.List_All_Foods.ElementAt(e.Position).Food_Rate ;
            txtfoodid.Text = Convert.ToString(id);

            txtfoodname.Text = foodname;
            txt_rate.Text = Convert.ToString(rate);

        }

        private void spinner_show_Master_Food_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            var id = this.List_All_Master_Foods.ElementAt(e.Position).Id;

            txtmasterfoodid.Text = Convert.ToString(id);

            load_spiner_food();

        }

        private void load_spiner_food()
        {
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "foods.sqlite");
            var db = new SQLiteConnection(dpPath);
            var data_s = db.Query<Tbl_Food_Rates>("select *  from Tbl_Food_Rates where Mfid=" + txtmasterfoodid.Text);
            List_All_Foods = data_s;
            Food_Menu .Resources.Adapter_Food_Rates  da = new Resources.Adapter_Food_Rates(this, List_All_Foods);
            spinnershowfood.Adapter = da;
            ListView1.Adapter = da;

        }

        private void load_spiner_Master_Foods()
        {
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "foods.sqlite");
            var db = new SQLiteConnection(dpPath);
            var data_s = db.Query<Tbl_Master_Foods>("select *  from Tbl_Master_Foods");
            List_All_Master_Foods = data_s;
            Food_Menu .Resources.Adapter_Master_Foods  da = new Resources.Adapter_Master_Foods(this, List_All_Master_Foods);
            spinnershowmasterFood.Adapter = da;
        }
    }
}