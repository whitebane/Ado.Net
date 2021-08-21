﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoNet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ProductDal _productDal = new ProductDal();
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void LoadProduct()
        {
            dgwProducts.DataSource = _productDal.GetAll();
        }

     

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _productDal.Add(new Product
            {
                Name=txtName.Text,
                UnitPrice=Convert.ToDecimal(txtUnit.Text),
                StockAmount=Convert.ToInt32(txtStock.Text)

            });

            MessageBox.Show("Added");
            LoadProduct();
        }

        private void dgwProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUpdateName.Text = dgwProducts.CurrentRow.Cells[1].Value.ToString();
            txtUnitName.Text = dgwProducts.CurrentRow.Cells[2].Value.ToString();
            txtUpdateStock.Text = dgwProducts.CurrentRow.Cells[3].Value.ToString();
           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Product product = new Product
            {
                Id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value),
                Name = txtUpdateName.Text,
                UnitPrice = Convert.ToDecimal(txtUnitName.Text),
                StockAmount = Convert.ToInt32(txtUpdateStock.Text)
            };
            _productDal.Update(product);
            MessageBox.Show("Updated");
            LoadProduct();

        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value);
            _productDal.Delete(id);
            MessageBox.Show("Deleted");
            LoadProduct();
        }
    }
}
