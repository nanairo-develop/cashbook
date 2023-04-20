using cashbook.common;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cashbook
{
    public partial class FormOfficeList : Form
    {
        public FormOfficeList()
        {
            InitializeComponent();
        }

        private void Search_Click(object sender, EventArgs e)
        {

            MySqlConnection conn = new(ComConst.connStr);
            try
            {
                // 接続を開く
                conn.Open();

                // データを取得するテーブル
                DataTable tbl = new();

                string query = GetQueryOffice();

                // SQLを実行する
                MySqlDataAdapter dataAdp = new(query, conn);
                _ = dataAdp.Fill(tbl);

                // データグリッドに表示させる
                OfficeList.DataSource = tbl;
                OfficeList.Columns["id"].Visible = false;
                OfficeList.Columns["name"].Width = 240;

                conn.Close();

            }
            catch (MySqlException mse)
            {
                _ = MessageBox.Show(mse.Message, "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return;
        }

        private string GetQueryOffice()
        {
            string nameCondition = "";
            if (OfficeName.Text != string.Empty)
            {
                nameCondition = $"""
                    AND
                        name LIKE '%{OfficeName.Text}%'
                    """;
            }
            string query = $"""
                SELECT
                    id,
                    name,
                    displayOrder
                FROM
                    m_office
                WHERE
                    1 = 1
                {nameCondition}
                ORDER BY
                    displayOrder DESC
                ;
                """;

            return query;
        }

        private void OfficeList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                MessageBox.Show("先頭行です");
            }
            else
            {
                FormOfficeDetail formOfficeDetail = new(
                    id: (int)OfficeList.Rows[e.RowIndex].Cells[0].Value,
                    officeName: (string)OfficeList.Rows[e.RowIndex].Cells[1].Value,
                    displayOrder: OfficeList.Rows[e.RowIndex].Cells[2].Value ?? ""
                    );
                formOfficeDetail.Show();
            }

        }
    }
}
