using FoodOrderBusinessLogic.BindingModels;
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
    public partial class FormSet : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly ISetLogic logic;
        private int? id;
        private List<SetOfDishViewModel> setOfDishes;

        public FormSet(ISetLogic service)
        {
            InitializeComponent();
            this.logic = service;
        }

        private void FormSet_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    SetViewModel view = logic.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.SetName;
                        textBoxPrice.Text = view.Price.ToString();
                        setOfDishes = view.SetOfDishes;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                setOfDishes = new List<SetOfDishViewModel>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (setOfDishes != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = setOfDishes;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode =
                   DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormSetOfDish>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.ModelView != null)
                {
                    if (id.HasValue)
                    {
                        form.ModelView.SetId = id.Value;
                    }
                    setOfDishes.Add(form.ModelView);
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormSetOfDish>();
                form.ModelView =
               setOfDishes[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    setOfDishes[dataGridView.SelectedRows[0].Cells[0].RowIndex] =
                   form.ModelView;
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                        setOfDishes.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (setOfDishes == null || setOfDishes.Count == 0)
            {
                MessageBox.Show("Заполните блюда", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<SetOfDishBindingModel> setOfDishesBM = new
               List<SetOfDishBindingModel>(); for (int i = 0; i < setOfDishes.Count; ++i)
                {
                    setOfDishesBM.Add(new SetOfDishBindingModel
                    {
                        Id = setOfDishes[i].Id,
                        SetId = setOfDishes[i].SetId,
                        DishId = setOfDishes[i].DishId,
                        Count = setOfDishes[i].Count
                    });
                }
                if (id.HasValue)
                {
                    logic.UpdElement(new SetBindingModel
                    {
                        Id = id.Value,
                        SetName = textBoxName.Text,
                        Price = Convert.ToDecimal(textBoxPrice.Text),
                        SetOfDishes = setOfDishesBM
                    });
                }
                else
                {
                    logic.AddElement(new SetBindingModel
                    {
                        SetName = textBoxName.Text,
                        Price = Convert.ToDecimal(textBoxPrice.Text),
                        SetOfDishes = setOfDishesBM
                    });
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
