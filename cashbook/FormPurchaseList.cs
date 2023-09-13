using Cashbook.common;
using Cashbook.dto;
using MySqlConnector;
using System.Data;
using static Cashbook.common.ComControl;
using static Cashbook.dao.MManagerDao;
using static Cashbook.dao.MOfficeDao;
using static Cashbook.FormPurchaseListDao;
using static Cashbook.FormPurchaseListDto.PurchaseListColumns;

namespace Cashbook
{
    /// <summary>�`�[�ꗗ-FormPurchaseList</summary>
    public partial class FormPurchaseList : Form
    {
        private readonly DataTable officeDataTable = new();
        private readonly DataTable managerDataTable = new();

        #region �R���X�g���N�^
        public FormPurchaseList()
        {
            InitializeComponent();
        }
        #endregion �R���X�g���N�^

        #region �C�x���g
        private void FormPurchaseList_Load(object sender, EventArgs e)
        {
            // �R���{�{�b�N�X�̐ݒ�
            ComboBoxParam comboManagerParam = new()
            {
                dt = managerDataTable,
                query = GetSelectManager(DateFrom.Value),
                comboBox = ComboManager,
                displayMember = "name",
                valueMember = "id"
            };
            SetComboBox(comboManagerParam);

            ComboBoxParam comboOfficeParam = new()
            {
                dt = officeDataTable,
                query = GetSelectOffice(),
                comboBox = ComboOffice,
                displayMember = "name",
                valueMember = "id",
            };
            SetComboBox(comboOfficeParam);

            // �����l�ݒ�
            ComboManager.SelectedValue = 0;
            ComboOffice.SelectedValue = 0;

            DateFrom.Value = DateTime.Now;
            DateTo.TextYear = string.Empty;
            DateTo.TextMonth = string.Empty;
            DateTo.TextDay = string.Empty;
        }

        /// <summary>�����{�^��������</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_Click(object sender, EventArgs e)
        {
            // �f�[�^�O���b�h�ɕ\��������
            DateTo.DateTimeFrom = DateFrom.Value;
            PurchaseList.DataSource = GetPurchases();
            ComControl.Columns(PurchaseList, id).HeaderText = "ID";
            ComControl.Columns(PurchaseList, id).Width = 40;
            ComControl.Columns(PurchaseList, payDate).HeaderText = "���t";
            ComControl.Columns(PurchaseList, payDate).Width = 80;
            ComControl.Columns(PurchaseList, destinationId).Visible = false;
            ComControl.Columns(PurchaseList, destination).HeaderText = "����於��";
            ComControl.Columns(PurchaseList, destination).Width = 240;
            ComControl.Columns(PurchaseList, managerId).Visible = false;
            ComControl.Columns(PurchaseList, manager).HeaderText = "�S����";
            ComControl.Columns(PurchaseList, manager).Width = 70;
            ComControl.Columns(PurchaseList, slipNumber).HeaderText = "�`�[�ԍ�";
            ComControl.Columns(PurchaseList, slipNumber).Width = 80;
        }

        private void PurchaseList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                _ = MessageBox.Show("�擪�s�ł�");
            }
            else
            {
                DataGridViewRow row = PurchaseList.Rows[e.RowIndex];

                TPurchaseDto purchaseDto = new()
                {
                    Id = (int)ComControl.Cells(row, id).Value,
                    PayDate = (DateTime)ComControl.Cells(row, payDate).Value,
                    Destination = (int)ComControl.Cells(row, destinationId).Value,
                    Manager = (int)ComControl.Cells(row, managerId).Value,
                    SlipNumber = (string)ComControl.Cells(row, slipNumber).Value,
                    Memo = ComControl.Cells(row, memo).Value is DBNull ? string.Empty : (string)ComControl.Cells(row, memo).Value
                };

                FormPurchaseDetail formPurchaseDetail = new(purchaseDto);
                formPurchaseDetail.Show();
            }
        }

        private void Create_Click(object sender, EventArgs e)
        {
            FormPurchaseDetail formPurchaseDetail = new();
            formPurchaseDetail.Show();
        }

        private void ComboOffice_KeyDown(object sender, KeyEventArgs e)
        {
            Combo_KeyDown(officeDataTable, ComboOffice);
        }

        #endregion �C�x���g
        #region ���\�b�h
        private DataTable GetPurchases()
        {
            // �f�[�^���擾����e�[�u��
            DataTable dt = new();

            using MySqlConnection conn = new(ComConst.connStr);
            using MySqlDataAdapter adapter = new();
            using MySqlCommand command = new();
            try
            {
                // �ڑ����J��
                conn.Open();
                FormPurchaseListDto formPurchaseListDto = new()
                {
                    OfficeId = (int?)ComboOffice.SelectedValue ?? 0,
                    ManagerId = (int?)ComboManager.SelectedValue ?? 0,
                    PayDateFrom = DateFrom.Value,
                    PayDateTo = DateTo.Value
                };
                command.CommandText = GetSelectPurchase(formPurchaseListDto);
                command.Connection = conn;
                adapter.SelectCommand = command;

                _ = adapter.Fill(dt);
            }
            catch (MySqlException mse)
            {
                _ = MessageBox.Show(mse.Message, "�f�[�^�擾�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
        #endregion ���\�b�h

    }
}