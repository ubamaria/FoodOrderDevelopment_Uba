using FoodOrderBusinessLogic.BindingModels;
using FoodOrderBusinessLogic.BusinessLogics;
using FoodOrderBusinessLogic.Interfaces;
using System;
using System.Windows.Forms;
using Unity;

namespace FoodOrderView
{
    public partial class FormMain : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly MainLogic logic;
        private readonly IOrderLogic orderLogic;
        private readonly ReportLogic report;
        private readonly WorkModeling work;
        private readonly BackUpAbstractLogic backUpAbstractLogic;
        public FormMain(MainLogic logic, IOrderLogic orderLogic, ReportLogic report, WorkModeling work, BackUpAbstractLogic backUpAbstractLogic)
        {
            InitializeComponent();
            this.logic = logic;
            this.orderLogic = orderLogic;
            this.report = report;
            this.work = work;
            this.backUpAbstractLogic = backUpAbstractLogic;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                Program.ConfigGrid(orderLogic.Read(null), dataGridView);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void блюдаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormDishes>();
            form.ShowDialog();
        }

        private void наборыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormSets>();
            form.ShowDialog();
        }

        private void buttonCreateOrder_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCreateOrder>();
            form.ShowDialog();
            LoadData();
        }

        private void buttonPayOrder_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    logic.PayOrder(new ChangeStatusBindingModel { OrderId = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void блюдаПоНаборамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReportSetOfDish>();
            form.ShowDialog();
        }

        private void списокЗаказовToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReportOrders>();
            form.ShowDialog();
        }

        private void списокНаборовToolStripMenuItem_Click(object sender, EventArgs e)
        {

            using (var dialog = new SaveFileDialog { Filter = "docx|*.docx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    report.SaveSetsToWordFile(new ReportBindingModel
                    {
                        FileName = dialog.FileName
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                }
            }
        }
        private void запускРаботToolStripMenuItem_Click(object sender, EventArgs e)
        {
            work.DoWork();
            LoadData();
        }

        private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormClients>();
            form.ShowDialog();
        }

        private void исполнителиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormImplementers>();
            form.ShowDialog();
        }

        private void buttonMessages_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormMessages>();
            form.ShowDialog();
        }

        private void создатьБекапToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { 
                if (backUpAbstractLogic != null) { 
                    var fbd = new FolderBrowserDialog(); 
                    if (fbd.ShowDialog() == DialogResult.OK) { 
                        backUpAbstractLogic.CreateArchive(fbd.SelectedPath); 
                        MessageBox.Show("Бекап создан", "Сообщение", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information); } } } 
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message, "Ошибка", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error); }
        }
    }
}
