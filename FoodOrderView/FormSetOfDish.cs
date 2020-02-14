using FoodOrderBusinessLogic.Interfaces;
using FoodOrderBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace FoodOrderView
{
    public partial class FormSetOfDish : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public SetOfDishViewModel ModelView { get; set; }
        private readonly IDishLogic logic;
        public FormSetOfDish(IDishLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormSetOfDish_Load(object sender, EventArgs e)
        {
            try
            {
                List<DishViewModel> list = logic.GetList();
                if (list != null)
                {
                    comboBoxDish.DisplayMember = "DishName";
                    comboBoxDish.ValueMember = "Id";
                    comboBoxDish.DataSource = list;
                    comboBoxDish.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
            if (ModelView != null)
            {
                comboBoxDish.Enabled = false;
                comboBoxDish.SelectedValue = ModelView.DishId;
                textBoxCount.Text = ModelView.Count.ToString();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxDish.SelectedValue == null)
            {
                MessageBox.Show("Выберите блюда", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (ModelView == null)
                {
                    ModelView = new SetOfDishViewModel
                    {
                        DishId = Convert.ToInt32(comboBoxDish.SelectedValue),
                        DishName = comboBoxDish.Text,
                        Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    ModelView.Count = Convert.ToInt32(textBoxCount.Text);
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
