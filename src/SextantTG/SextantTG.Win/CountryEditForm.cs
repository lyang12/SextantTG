﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SextantTG.ActiveRecord;

namespace SextantTG.Win
{
    public partial class CountryEditForm : Form
    {
        public CountryEditForm()
        {
            InitializeComponent();
        }

        public void SetCountry(Country country)
        {
            this.countryView.SetCountry(country);
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            bool val;
            string msg;
            val = this.countryView.Save(out msg);
            if (val)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("保存失败\r\n" + msg, "提示");
                return;
            }
        }
    }
}
