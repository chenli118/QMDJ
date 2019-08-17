using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace QMDJ
{
    internal class SubGridView: System.Windows.Forms.DataGridView
    {
        public int focusRowIndex { get; set; }
        public int focusColmunIndex { get; set; }
        public SubGridView()
        {
            RowsAdded += SubGridView_RowsAdded;
            DataBindingComplete += SubGridView_DataBindingComplete;
            ColumnHeadersVisible = false;
            this.AllowUserToAddRows = false;              
            RowHeadersVisible = false;
            this.ReadOnly = true;
            for (int i = 0; i < this.Columns.Count; i++)
            {
                this.Columns[i].Width = 16;
            }
        }

        private void SubGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CurrentCell = this.Rows[focusRowIndex].Cells[focusColmunIndex];
        }

        private void SubGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            ClearSelection(); 

        }

        //protected override  CreateRow(int rowIndex, int dataSourceIndex, DataControlRowType rowType, DataControlRowState rowState)
        //{
        //    if (rowState == DataControlRowState.Normal || rowState == DataControlRowState.Alternate)
        //        return base.CreateRow(rowIndex, dataSourceIndex, rowType, rowState | DataControlRowState.Normal);
        //    else
        //        return base.CreateRow(rowIndex, dataSourceIndex, rowType, rowState | DataControlRowState.Edit);

        //}
        public void SetFocusCell(int row,int col)
        {
            CurrentCell = Rows[row].Cells[col];
            //this.Rows[row].Cells[col].Selected =true;
            
        }
    }
}
