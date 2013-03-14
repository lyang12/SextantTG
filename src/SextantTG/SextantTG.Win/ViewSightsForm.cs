﻿using SextantTG.ActiveRecord;
using SextantTG.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SextantTG.Win
{
    public partial class ViewSightsForm : Form
    {
        public ViewSightsForm()
        {
            InitializeComponent();
        }

        private void SightsForm_Load(object sender, EventArgs e)
        {
            if (Config.AppConfig.User == null)
            {
                this.checkBox_Visited.Enabled = false;
                this.tabControl1.TabPages.Remove(tabPage4);
            }
            else
            {
                this.checkBox_Visited.Enabled = true;
            }

            this.comboBox_Country.DisplayMember = "CountryName";
            this.comboBox_Country.ValueMember = "CountryId";

            this.comboBox_Province.DisplayMember = "ProvinceName";
            this.comboBox_Province.ValueMember = "ProvinceId";

            this.comboBox_City.DisplayMember = "CityName";
            this.comboBox_City.ValueMember = "CityId";

            this.listBox_Sights.DisplayMember = "SightsName";
            this.listBox_Sights.ValueMember = "SightsId";

            List<Country> countries = UIUtil.GetCountries();
            Country country = new Country();
            country.CountryId = "*";
            country.CountryName = "<全部>";
            countries.Insert(0, country);
            this.comboBox_Country.DataSource = countries;
        }

        private void comboBox_Country_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_Country.SelectedValue != null)
            {
                if (comboBox_Country.SelectedValue.ToString() != "*")
                {
                    List<Province> provinces = UIUtil.GetProvincesByCountryId(comboBox_Country.SelectedValue.ToString());
                    Province province = new Province();
                    province.ProvinceId = "*";
                    province.ProvinceName = "<全部>";
                    provinces.Insert(0, province);
                    this.comboBox_Province.DataSource = provinces;
                }
                else
                {
                    List<Province> provinces = UIUtil.GetProvinces();
                    Province province = new Province();
                    province.ProvinceId = "*";
                    province.ProvinceName = "<全部>";
                    provinces.Insert(0, province);
                    this.comboBox_Province.DataSource = provinces;
                }
            }
            else
            {
                this.comboBox_Province.DataSource = null;
                this.comboBox_City.DataSource = null;
            }
        }

        private void comboBox_Province_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_Province.SelectedValue != null)
            {
                if (comboBox_Province.SelectedValue.ToString() != "*")
                {
                    List<City> cities = UIUtil.GetCitiesByProvinceId(comboBox_Province.SelectedValue.ToString());
                    City city = new City();
                    city.CityId = "*";
                    city.CityName = "<全部>";
                    cities.Insert(0, city);
                    this.comboBox_City.DataSource = cities;
                }
                else
                {
                    if (comboBox_Country.SelectedValue.ToString() != "*")
                    {
                        List<City> cities = UIUtil.GetCitiesByCountryId(comboBox_Country.SelectedValue.ToString());
                        City city = new City();
                        city.CityId = "*";
                        city.CityName = "<全部>";
                        cities.Insert(0, city);
                        this.comboBox_City.DataSource = cities;
                    }
                    else
                    {
                        List<City> cities = UIUtil.GetCities();
                        City city = new City();
                        city.CityId = "*";
                        city.CityName = "<全部>";
                        cities.Insert(0, city);
                        this.comboBox_City.DataSource = cities;
                    }
                }
            }
            else
            {
                this.comboBox_City.DataSource = null;
            }
        }

        private void comboBox_City_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSightsList();
        }

        private void BindSightsList()
        {
            string userId = Config.AppConfig.User != null && this.checkBox_Visited.Checked ? Config.AppConfig.User.UserId : "";
            List<Sights> ds = null;
            if (comboBox_City.SelectedValue != null)
            {
                if (comboBox_City.SelectedValue.ToString() != "*")
                {
                    ds = UIUtil.GetVisitedSightsByCityId(comboBox_City.SelectedValue.ToString(), userId);
                }
                else
                {
                    if (comboBox_Province.SelectedValue.ToString() != "*")
                    {
                        ds = UIUtil.GetVisitedSightsByProvinceId(comboBox_Province.SelectedValue.ToString(), userId);
                    }
                    else
                    {
                        if (comboBox_Country.SelectedValue.ToString() != "*")
                        {
                            ds = UIUtil.GetVisitedSightsByCountryId(comboBox_Country.SelectedValue.ToString(), userId);
                        }
                        else
                        {
                            ds = UIUtil.GetVisitedSights(userId);
                        }
                    }
                }

                string nameFilter = this.textBox_FilterSightsName.Text.Trim();
                if (string.IsNullOrEmpty(nameFilter))
                {
                    this.listBox_Sights.DataSource = ds;
                }
                else
                {
                    this.listBox_Sights.DataSource = ds.FindAll(delegate(Sights sights) { return sights.SightsName.Contains(nameFilter); });
                }
            }
            else
            {
                ds = null;
            }
        }

        private void listBox_Sights_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_Sights.SelectedItem != null)
            {
                BindItem(this.listBox_Sights.SelectedItem as Sights);
            }
        }

        private void BindItem(Sights sights)
        {
            float? stars = UIUtil.GetAverageStarsBySightsId(sights.SightsId);
            this.textBox_SightsName.Text = sights.SightsName;
            this.textBox_Stars.Text = CustomTypeConverter.ToString(stars, "n2");
            this.textBox_Country.Text = UIUtil.GetCountryByProvinceId(UIUtil.GetProvinceByCityId(sights.CityId).ProvinceId).CountryName;
            this.textBox_Province.Text = UIUtil.GetProvinceByCityId(sights.CityId).ProvinceName;
            this.textBox_City.Text = UIUtil.GetCityByCityId(sights.CityId).CityName;
            this.textBox_SightsLevel.Text = sights.SightsLevel;
            this.textBox_Price.Text = CustomTypeConverter.ToString(sights.Price, "n2");
            this.textBox_Description.Text = sights.Description;

            //
            this.stgReadonlyPictures.SetPicturesForSights(sights.SightsId, "0000");

            //
            this.dataGridView_Comment.AutoGenerateColumns = true;
            this.bindingSource_Comment.DataSource = UIUtil.GetSightsCommentsBySightsId(sights.SightsId);

            //
            this.dataGridView_Blog.AutoGenerateColumns = true;
            this.bindingSource_Blog.DataSource = UIUtil.GetBlogsBySightsId(sights.SightsId);

            if (Config.AppConfig.User != null)
            {
                Favorite fav = UIUtil.GetFavoriteByUserIdAndSightsId(Config.AppConfig.User.UserId, sights.SightsId);
                if (fav != null)
                {
                    this.textBox_MyVisited.Text = fav.Visited == 1 ? "是" : "否";
                    this.textBox_MyStars.Text = CustomTypeConverter.ToString(fav.Stars, "n0");
                }
            }
        }

        private void textBox_FilterSightsName_TextChanged(object sender, EventArgs e)
        {
            BindSightsList();
        }

        private void checkBox_Visited_CheckedChanged(object sender, EventArgs e)
        {
            BindSightsList();
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Comment_Click(object sender, EventArgs e)
        {
            if (listBox_Sights.SelectedItem != null)
            {
                using (CommentEditForm form = new CommentEditForm(this.listBox_Sights.SelectedItem as Sights))
                {
                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        BindSightsList();
                    }
                }
            }
        }

        private void button_Favorite_Click(object sender, EventArgs e)
        {
            if (listBox_Sights.SelectedItem != null)
            {
                using (FavoriteEditForm form = new FavoriteEditForm((this.listBox_Sights.SelectedItem as Sights).SightsId))
                {
                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        BindSightsList();
                    }
                }
            }
        }
    }
}
