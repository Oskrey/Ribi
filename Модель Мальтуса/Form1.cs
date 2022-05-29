using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Модель_Мальтуса
{
    public partial class FormModel : Form
    {
        double k, q; //Коэффициенты рождаемости и смертности
        double count, years; //Численность популяции и временной промежуток

        public FormModel()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.chart1.Series[0].Points.Clear();
            textBoxK.Clear();
            textBoxQ.Clear();
            dataGridView1.Rows.Clear();
        }

        private void textBoxK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;        //Рзрешить ввод цифр
            if (e.KeyChar == 8) return;                 //Разрешить <-
            if (e.KeyChar == 27)                        //Разрешить esc
            {
                (sender as TextBox).Text = "";
                return;
            }
            if (e.KeyChar == '.') e.KeyChar = ',';      //Замена "." на ","
            if (e.KeyChar == ',')  //Если ","
            {
                if ((sender as TextBox).Text.Contains(","))   //Поиск
                    e.Handled = true;     //Запретить
                if ((sender as TextBox).Text == "")     //Строка пустая
                    e.Handled = true;     //Запретить
                return;    //Остальное можно
            }
            e.Handled = true;    //Все остальное - запрет
        }


        private void FormModel_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            this.chart1.Series[0].Points.Clear();

            k = Convert.ToDouble(textBoxK.Text);
            q = Convert.ToDouble(textBoxQ.Text);
            count = Convert.ToDouble(numericUpDownCount.Value);
            years = Convert.ToDouble(numericUpDownYears.Value);

            dataGridView1.ColumnCount = 2;

            dataGridView1.Columns[0].Name = "Год";
            dataGridView1.Columns[1].Name = "Численность";

            this.chart1.Series[0].Points.AddXY(0, count);
            dataGridView1.Rows.Add(0, Math.Round(count, 3));

            for (int i = 1; i <= years; i++)
            {
                count = count + k * count - q * Math.Pow(count,2);
                this.chart1.Series[0].Points.AddXY(i , count);
                dataGridView1.Rows.Add(i , Math.Round(count, 3));
            }

        }
    }
}
