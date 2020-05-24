using FoodOrderBusinessLogic.BindingModels;
using FoodOrderBusinessLogic.BusinessLogics;
using FoodOrderBusinessLogic.Interfaces;
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
    public partial class FormFillStorage : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IDishLogic logicD;
        private readonly MainLogic logicM;
        private readonly IStorageLogic logicS;
        public FormFillStorage(IDishLogic logicD, MainLogic logicM, IStorageLogic logicS)
        {
            InitializeComponent();
            this.logicD = logicD;
            this.logicM = logicM;
            this.logicS = logicS;
        }
        private void FormFillStorage_Load(object sender, EventArgs e)
        {
            try
            {
                var storageList = logicS.GetList();
                comboBoxStorage.DataSource = storageList;
                comboBoxStorage.DisplayMember = "StorageName";
                comboBoxStorage.ValueMember = "Id";

                var dishList = logicD.Read(null);
                comboBoxDish.DataSource = dishList;
                comboBoxDish.DisplayMember = "DishName";
                comboBoxDish.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните количество", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (comboBoxStorage.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK,
MessageBoxIcon.Error);
                return;
            }
            if (comboBoxDish.SelectedValue == null)
            {
                MessageBox.Show("Выберите цветок", "Ошибка", MessageBoxButtons.OK,
MessageBoxIcon.Error);
                return;
            }

            try
            {
                int storageId = Convert.ToInt32(comboBoxStorage.SelectedValue);
                int dishId = Convert.ToInt32(comboBoxDish.SelectedValue);
                int count = Convert.ToInt32(textBoxCount.Text);

                logicM.FillStorage(new StorageDishBindingModel
                {
                    StorageId = storageId,
                    DishId = dishId,
                    Count = count
                });
                MessageBox.Show("Склад успешно пополнен", "Сообщение",
                  MessageBoxButtons.OK, MessageBoxIcon.Information);
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
