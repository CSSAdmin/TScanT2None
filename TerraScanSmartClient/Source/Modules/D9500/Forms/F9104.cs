//--------------------------------------------------------------------------------------------
// <copyright file="F9104.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9104.cs.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09/11/2006       M.Vijaya Kumar     Created// 
//*********************************************************************************/

namespace D9500
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Web.Services.Protocols;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.Utilities;    

    /// <summary>
    /// Class file for F9104
    /// </summary>
    public partial class F9104 : Form
    {
        #region Variable

        /// <summary>
        /// controller F9104Controller
        /// </summary>
        private F9104Controller form9104Control;

        /// <summary>
        /// Instance for F9104FundSelectionData
        /// </summary>
        private F9104FundSelectionData fundSelectionData = new F9104FundSelectionData();

        /// <summary>
        /// Used to store the no of rows in the grid
        /// </summary>
        private int fundSelectionGridRowCount;

        /// <summary>
        /// Used to store the fundId
        /// </summary>
        private int fundId;

        /// <summary>
        /// Used to store the Fund Value selected from the Grid
        /// </summary>
        private string fund;

        #endregion Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9104"/> class.
        /// </summary>
        public F9104()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// For F9104Control
        /// </summary>
        [CreateNew]
        public F9104Controller Form9104Control
        {
            get { return this.form9104Control as F9104Controller; }
            set { this.form9104Control = value; }
        }

        /// <summary>
        /// Gets or sets the FundId.
        /// </summary>
        /// <value>The FundId.</value>
        public int FundId
        {
            get { return this.fundId; }
            set { this.fundId = value; }
        }

        /// <summary>
        /// Gets or sets the Fund.
        /// </summary>
        /// <value>The Fund.</value>
        public string FundItem
        {
            get { return this.fund; }
            set { this.fund = value; }
        }

        #endregion Property

        #region Methods

        /// <summary>
        /// Selects the FunDID from the Fund Selection grid.
        /// </summary>
        private void SelectFunDId()
        {
            int rowId = 0;

            try
            {
                ////To get the Row index for Fund Selection DataGridView
                rowId = this.GetRowIndex();

                if (this.fundSelectionGridRowCount > 0 && rowId >= 0)
                {
                    if (!string.IsNullOrEmpty(this.FundSelectionDataGridView.Rows[rowId].Cells[this.fundSelectionData.GetFundSelection.FundIDColumn.ColumnName].Value.ToString()))
                    {
                        int.TryParse(this.FundSelectionDataGridView.Rows[rowId].Cells[this.fundSelectionData.GetFundSelection.FundIDColumn.ColumnName].Value.ToString(), out this.fundId);
                    }

                    if (!string.IsNullOrEmpty(this.FundSelectionDataGridView.Rows[rowId].Cells[this.fundSelectionData.GetFundSelection.FundColumn.ColumnName].Value.ToString()))
                    {
                        this.fund = this.FundSelectionDataGridView.Rows[rowId].Cells[this.fundSelectionData.GetFundSelection.FundColumn.ColumnName].Value.ToString();
                    }

                    this.DialogResult = DialogResult.OK;

                    this.Close();
                }
                else
                {
                    this.DialogResult = DialogResult.None;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }       

        /// <summary>
        /// Gets the index of the row.
        /// </summary>
        /// <returns>integer value of grid row index</returns>
        private int GetRowIndex()
        {
            try
            {
                if (this.fundSelectionGridRowCount > 0)
                {
                    if (this.FundSelectionDataGridView.CurrentCell != null)
                    {
                        return this.FundSelectionDataGridView.CurrentCell.RowIndex;
                    }

                    return -1;
                }
                else
                {                    
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// Custimizes the fund selection grid.
        /// </summary>
        private void CustimizeFundSelectionGrid()
        {
            this.FundSelectionDataGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection columns = this.FundSelectionDataGridView.Columns;

            columns[this.fundSelectionData.GetFundSelection.FundIDColumn.ColumnName].DataPropertyName = this.fundSelectionData.GetFundSelection.FundIDColumn.ColumnName;
            columns[this.fundSelectionData.GetFundSelection.FundColumn.ColumnName].DataPropertyName = this.fundSelectionData.GetFundSelection.FundColumn.ColumnName;
            columns[this.fundSelectionData.GetFundSelection.DescriptionColumn.ColumnName].DataPropertyName = this.fundSelectionData.GetFundSelection.DescriptionColumn.ColumnName;

            columns[this.fundSelectionData.GetFundSelection.FundIDColumn.ColumnName].DisplayIndex = 0;
            columns[this.fundSelectionData.GetFundSelection.FundColumn.ColumnName].DisplayIndex = 1;
            columns[this.fundSelectionData.GetFundSelection.DescriptionColumn.ColumnName].DisplayIndex = 2; 
        }

        /// <summary>
        /// Loads the FundSelectionDataGridView
        /// </summary>
        /// <param name="fundValue">The fund value form FundTextBox</param>
        /// <param name="descriptionValue">The description form DescTextBox </param>
        private void LoadFundSelectionGrid(int fundValue, string descriptionValue)
        {
            try 
            {
                this.Cursor = Cursors.WaitCursor;
                ////this.fundSelectionData = this.form9104Control.WorkItem.F9104_GetFundSelection(fundValue, descriptionValue);
                this.fundSelectionGridRowCount = this.fundSelectionData.GetFundSelection.Rows.Count;
                if (this.fundSelectionGridRowCount > 0)
                {   
                    this.FundSelectionDataGridView.Enabled = true;                    

                    this.FundSelectionDataGridView.DataSource = this.fundSelectionData.GetFundSelection;                    
                    this.FundSelectionDataGridView.Rows[0].Selected = true;

                    this.FundAcceptButton.Enabled = true;
                }
                else
                {
                    this.ClearFundSelectionGrid();
                }

                ////to enable or disable the vertical scroll bar
                if (this.fundSelectionGridRowCount > this.FundSelectionDataGridView.NumRowsVisible)
                {
                    this.FundSlectionVerticalScroll.Visible = false;
                }
                else
                {
                    this.FundSlectionVerticalScroll.Visible = true;
                }

                ////To display the no of display rows in the grid
                this.RecordCountLabel.Text = this.fundSelectionGridRowCount + SharedFunctions.GetResourceString("9101MasterNameSearch");                
            }
            catch (SoapException e)
            {
                ExceptionManager.ManageException(e, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Clears the FundSelectionDataGridView
        /// </summary>
        private void ClearFundSelectionGrid()
        {
            this.FundSelectionDataGridView.Enabled = false;
            this.fundSelectionData.GetFundSelection.Clear();
            this.FundSelectionDataGridView.DataSource = this.fundSelectionData.GetFundSelection; 
            this.FundSelectionDataGridView.Rows[0].Selected = false;
            this.FundSlectionVerticalScroll.Visible = true;
            this.FundAcceptButton.Enabled = false;
        }

        /// <summary>
        /// Clears the FundTextBox,DescTextBox and RecordCountLabel
        /// </summary>
        private void ClearFundSelection()
        {
            this.FundTextBox.Text = string.Empty;
            this.DescTextBox.Text = string.Empty;
            this.RecordCountLabel.Text = string.Empty;
        }

        /// <summary>
        /// To disable the accept,search, and clear buttons
        /// </summary>        
        private void DisableButtons()
        {
            this.FundSearchButton.Enabled = false;
            this.FundAcceptButton.Enabled = false;
            this.FundClearButton.Enabled = false;
        }

        /// <summary>
        /// Enables the search button.
        /// </summary>
        private void EnableSearchButton()
        {
            if (!string.IsNullOrEmpty(this.FundTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.DescTextBox.Text.Trim()))
            {
                this.FundSearchButton.Enabled = true;
                this.FundClearButton.Enabled = true;
            }
            else
            {
                this.FundSearchButton.Enabled = false;

                if (this.fundSelectionGridRowCount <= 0)
                {
                    this.FundAcceptButton.Enabled = false;
                    this.FundClearButton.Enabled = false;
                }
                else
                {
                    this.FundAcceptButton.Enabled = true;
                    this.FundClearButton.Enabled = true;
                }
            }
        }

        #endregion Methods

        #region Events

        #endregion Events

        /// <summary>
        /// Handles the Load event of the F9104 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9104_Load(object sender, EventArgs e)
        {
            try
            {
                this.ClearFundSelection();
                this.ClearFundSelectionGrid();
                this.CustimizeFundSelectionGrid();
                this.DisableButtons();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the FundSearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FundSearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                int fundSelectedValue;
                if (!string.IsNullOrEmpty(this.FundTextBox.Text.Trim()))
                {
                    int.TryParse(this.FundTextBox.Text.Trim(), out fundSelectedValue);

                    if (fundSelectedValue > Int16.MaxValue)
                    {
                        ////when the value is greather than the max value message box is raised
                        MessageBox.Show(SharedFunctions.GetResourceString("9104FundTextBoxValidate"), ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    fundSelectedValue = -999;
                }

                this.LoadFundSelectionGrid(fundSelectedValue, this.DescTextBox.Text.Trim());
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the FundClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FundClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ClearFundSelection();
                this.ClearFundSelectionGrid();
                this.DisableButtons();
                this.FundTextBox.Focus();
                this.DialogResult = DialogResult.None;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the FundCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FundCancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the FundManagementLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void FundManagementLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                //////Form fundManagementForm = new Form();
                //////fundManagementForm = this.form1030control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(11003, null, this.form9104Control.WorkItem);
                //////if (fundManagementForm != null)
                //////{
                //////    fundManagementForm.ShowDialog();
                //////}

                //////this.FundSelectionDataGridView.Focus();

                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(11003);
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Editexts the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AllFundTextBoxs_TextChanged(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(this.FundTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.DescTextBox.Text.Trim())))
            {
                this.EnableSearchButton();
            }
            else
            {
                if (this.fundSelectionGridRowCount > 0)
                {
                    this.FundSearchButton.Enabled = false;
                }
                else
                {
                    this.DisableButtons();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the FundAcceptButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FundAcceptButton_Click(object sender, EventArgs e)
        {
            if (this.fundSelectionGridRowCount > 0)
            {
                this.SelectFunDId();
                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the FundSelectionDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void FundSelectionDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && this.FundAcceptButton.Enabled)
                {
                    this.SelectFunDId();
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowHeaderMouseDoubleClick event of the FundSelectionDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void FundSelectionDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.FundAcceptButton.Enabled)
                {
                    this.SelectFunDId();
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }       
    }
}