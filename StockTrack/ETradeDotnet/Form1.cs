using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETradeDotnet
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
            using (ETradeContext context = new ETradeContext())
            {
                LoadProducts();
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            _productDal.Add(new Products
            {
                Name = tbxName.Text,
                UnitPrice = Convert.ToDecimal(tbxUnitprice.Text),
                StockAmount = Convert.ToInt32(tbxStockAmount.Text)
            });
            LoadProducts();
            MessageBox.Show("Added!");
        }
        private void LoadProducts()
        {
            dgwProducts.DataSource = _productDal.GetAll();
        }
        private void SearchProducts(string key)
        {
            var result = _productDal.GetByName(key);
            dgwProducts.DataSource = result;
        }
        private void SearchProductsId(int id)
        {
            List<Products> productList = new List<Products>();
            var result = _productDal.GetById(id);
            if (result != null)
            {
                productList.Add(result);
                dgwProducts.DataSource = productList;
            }
            else
            {
                dgwProducts.DataSource = null;
            }
        }
        private void SearchProductsByUnitPrice(decimal price)
        {
            var result = _productDal.GetByUnitPrice(price);
            dgwProducts.DataSource = result;
        }

        private void SearchProductsByUnitPrice(decimal min, decimal max)
        {
            var result = _productDal.GetByUnitPrice(min, max);
            dgwProducts.DataSource = result;
        }
        private void SearchProductsByStockAmount(int stockAmount)
        {
            var result = _productDal.GetByStockAmount(stockAmount);
            dgwProducts.DataSource = result;
        }
        private void SearchProductsByStockAmount(int minStock, int maxStock)
        {
            var result = _productDal.GetByStockAmount(minStock, maxStock);
            dgwProducts.DataSource = result;
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                _productDal.Update(new Products
                {
                    Id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value),
                    Name = tbxNameUpdate.Text,
                    UnitPrice = Convert.ToDecimal(tbxUnitpriceUpdate.Text),
                    StockAmount = Convert.ToInt32(tbxStockAmountUpdate.Text)
                });
                LoadProducts();
                MessageBox.Show("Updated!");
            }
            catch (Exception)
            {
                MessageBox.Show("Incorrect value entered!");
            }
        }

        private void dgwProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbxNameUpdate.Text = dgwProducts.CurrentRow.Cells[1].Value.ToString();
            tbxUnitpriceUpdate.Text = dgwProducts.CurrentRow.Cells[2].Value.ToString();
            tbxStockAmountUpdate.Text = dgwProducts.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            _productDal.Delete(new Products
            {
                Id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value)
            });
            LoadProducts();
            MessageBox.Show("Deleted!");
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            if (tbxSearch.Text == "")
            {
                LoadProducts();
            }
            else if (btnChoice.Text == "Get By Name")
            {
                SearchProducts(tbxSearch.Text);
            }
            else if (btnChoice.Text == "Get By Id")
            {
                if (int.TryParse(tbxSearch.Text, out int id))
                {
                    SearchProductsId(Convert.ToInt32(tbxSearch.Text));
                }
            }
            else if (btnChoice.Text == "Get By Unit Price")
            {
                if (decimal.TryParse(tbxSearch.Text, out decimal price))
                {
                    SearchProductsByUnitPrice(price);
                }
                else if (tbxSearch.Text.Contains("-"))
                {
                    string[] priceRange = tbxSearch.Text.Split('-');
                    if (priceRange.Length == 2 && decimal.TryParse(priceRange[0], out decimal min) && decimal.TryParse(priceRange[1], out decimal max))
                    {
                        SearchProductsByUnitPrice(min, max);
                    }
                }
            }
            else if (btnChoice.Text == "Get By Stock")
            {
                if (int.TryParse(tbxSearch.Text, out int stockAmount))
                {
                    SearchProductsByStockAmount(stockAmount);
                }
                else if (tbxSearch.Text.Contains("-"))
                {
                    string[] stockRange = tbxSearch.Text.Split('-');
                    if (stockRange.Length == 2 && int.TryParse(stockRange[0], out int minStock) && int.TryParse(stockRange[1], out int maxStock))
                    {
                        SearchProductsByStockAmount(minStock, maxStock);
                    }
                }
            }
        }

        private string[] words = { "Get By Name", "Get By Id", "Get By Unit Price", "Get By Stock" };
        private int clickCount = 0;
        private void btnChoice_Click(object sender, EventArgs e)
        {
            clickCount++;
            if (clickCount >= words.Length)
            {
                clickCount = 0;
            }
            btnChoice.Text = words[clickCount];
        }
    }
}
