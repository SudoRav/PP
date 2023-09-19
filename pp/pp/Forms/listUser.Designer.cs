
namespace pp.Forms
{
    partial class listUser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGrid_listUser = new System.Windows.Forms.DataGridView();
            this.btn_end = new System.Windows.Forms.Button();
            this.btn_addStaff = new System.Windows.Forms.Button();
            this.findbox = new System.Windows.Forms.TextBox();
            this.sorts = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_listUser)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGrid_listUser
            // 
            this.dataGrid_listUser.AllowUserToAddRows = false;
            this.dataGrid_listUser.AllowUserToDeleteRows = false;
            this.dataGrid_listUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_listUser.Location = new System.Drawing.Point(16, 58);
            this.dataGrid_listUser.MultiSelect = false;
            this.dataGrid_listUser.Name = "dataGrid_listUser";
            this.dataGrid_listUser.ReadOnly = true;
            this.dataGrid_listUser.RowHeadersVisible = false;
            this.dataGrid_listUser.RowHeadersWidth = 51;
            this.dataGrid_listUser.RowTemplate.Height = 24;
            this.dataGrid_listUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid_listUser.Size = new System.Drawing.Size(1167, 617);
            this.dataGrid_listUser.TabIndex = 42;
            this.dataGrid_listUser.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_listUser_CellDoubleClick);
            // 
            // btn_end
            // 
            this.btn_end.Location = new System.Drawing.Point(1071, 683);
            this.btn_end.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_end.Name = "btn_end";
            this.btn_end.Size = new System.Drawing.Size(112, 35);
            this.btn_end.TabIndex = 43;
            this.btn_end.Text = "Закрыть";
            this.btn_end.UseVisualStyleBackColor = true;
            this.btn_end.Click += new System.EventHandler(this.btn_end_Click);
            // 
            // btn_addStaff
            // 
            this.btn_addStaff.Location = new System.Drawing.Point(18, 683);
            this.btn_addStaff.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_addStaff.Name = "btn_addStaff";
            this.btn_addStaff.Size = new System.Drawing.Size(112, 35);
            this.btn_addStaff.TabIndex = 44;
            this.btn_addStaff.Text = "Выбрать";
            this.btn_addStaff.UseVisualStyleBackColor = true;
            this.btn_addStaff.Click += new System.EventHandler(this.btn_addStaff_Click);
            // 
            // findbox
            // 
            this.findbox.Location = new System.Drawing.Point(16, 24);
            this.findbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.findbox.Name = "findbox";
            this.findbox.Size = new System.Drawing.Size(392, 26);
            this.findbox.TabIndex = 46;
            this.findbox.TextChanged += new System.EventHandler(this.uchot_sotrudniki_findbox_TextChanged);
            // 
            // sorts
            // 
            this.sorts.FormattingEnabled = true;
            this.sorts.Items.AddRange(new object[] {
            "ФИО",
            "Должность"});
            this.sorts.Location = new System.Drawing.Point(419, 22);
            this.sorts.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sorts.Name = "sorts";
            this.sorts.Size = new System.Drawing.Size(392, 28);
            this.sorts.TabIndex = 45;
            // 
            // listUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 731);
            this.Controls.Add(this.findbox);
            this.Controls.Add(this.sorts);
            this.Controls.Add(this.btn_addStaff);
            this.Controls.Add(this.btn_end);
            this.Controls.Add(this.dataGrid_listUser);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "listUser";
            this.Text = "listUser";
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_listUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid_listUser;
        private System.Windows.Forms.Button btn_end;
        private System.Windows.Forms.Button btn_addStaff;
        private System.Windows.Forms.TextBox findbox;
        private System.Windows.Forms.ComboBox sorts;
    }
}