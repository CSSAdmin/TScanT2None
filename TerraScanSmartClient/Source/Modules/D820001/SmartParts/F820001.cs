using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using TerraScan.Common;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using TerraScan.SmartParts;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.Utility;
using TerraScan.Infrastructure.Interface.Constants;
using TerraScan.BusinessEntities;
using TerraScan.UI.Controls;
using TerraScan.Helper;

namespace D820001
{
    [SmartPart]
    public partial class F820001 : BaseSmartPart
    {
        private TerraScanCommon.PageModeTypes pageMode;
        private bool isNewRecord = false;
        private List<ComboSQLText> comboTextCollection;
        private List<DataSetCollection> dataSetCollection;
        private bool formMasterPermissionEdit;
        private int masterFormNo;
        private int keyId;
        private PermissionFields slicePermissionField;
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();
        private DataSet formDetailsDataSet = new DataSet();
        private List<Control> lockControlsCollection = new List<Control>();
        private int actualHeight;
        private bool pageLoadStatus;
        private int selectedArchitectID;
        private bool loadedFirst;

        public F820001()
        {
            this.Tag = 820001;
            InitializeComponent();
            this.actualHeight = this.Height;
            comboTextCollection = new List<ComboSQLText>();
            dataSetCollection = new List<DataSetCollection>();
            this.r820001_Vmg_Architect.Image = ExtendedGraphics.GenerateVerticalLabelText(this.r820001_Vmg_Architect.Height, this.r820001_Vmg_Architect.Width, this.r820001_Vmg_Architect.Text, this.r820001_Vmg_Architect.BackColor, this.r820001_Vmg_Architect.Font);
            this.r820001_Vmg_Architect.Text = string.Empty;
            this.AddDataSetValues("r820001_Architect", "StoredProcedure", "r820001_Get");
            //this.AddDataSetValues("Name", "", "SELECT     ArchitectID, Name    FROM         tGD_Architect");
            this.AddDataSetValues("Name", "", "SELECT ArchitectID , [Name] AS [Value]  FROM [dbo].[r820001_udf_GetName] (null) AS A");
            //this.AddDataSetValues("Name", "", "SELECT  MIN(ArchitectID) AS ArchitectID,[Name] FROM tGD_Architect WHERE IsDeleted = 0 GROUP BY [Name]");
        }


        private void F820001_Load(object sender, EventArgs e)
        {
            this.FlagSliceForm = true;
            this.Height = this.actualHeight;
            this.pageLoadStatus = true;
            this.GetLockControls();
            this.ClearFields();
            this.loadedFirst = true;
            this.PopulateFormFields();
            this.pageLoadStatus = false;
        }

        private void BaseSmartPart_Resize(object sender, EventArgs e)
        {
            this.Height = this.actualHeight;
        }

        public F820001(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.Tag = 820001;
            InitializeComponent();
            this.actualHeight = this.Height;
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
            comboTextCollection = new List<ComboSQLText>();
            dataSetCollection = new List<DataSetCollection>();
            this.r820001_Vmg_Architect.Image = ExtendedGraphics.GenerateVerticalLabelText(this.r820001_Vmg_Architect.Height, this.r820001_Vmg_Architect.Width, this.r820001_Vmg_Architect.Text, this.r820001_Vmg_Architect.BackColor, this.r820001_Vmg_Architect.Font);
            this.r820001_Vmg_Architect.Text = string.Empty;
            this.AddDataSetValues("r820001_Architect", "StoredProcedure", "r820001_Get");
            /*Query Modified as per change request - here query sent by client*/
            ////this.AddDataSetValues("Name", "", "SELECT     ArchitectID, Name    FROM         tGD_Architect");
            this.AddDataSetValues("Name", "", "SELECT ArchitectID , [Name] AS [Value]  FROM [dbo].[r820001_udf_GetName] (null) AS A");
            //this.AddDataSetValues("Name", "", "SELECT  MIN(ArchitectID) AS ArchitectID,[Name] FROM tGD_Architect WHERE IsDeleted = 0 GROUP BY [Name]");
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="F820001"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        /// <param name="featureClassID">The feature class ID.</param>
        public F820001(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassID)
        {
            this.Tag = 820001;
            InitializeComponent();
            this.actualHeight = this.Height;
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
            comboTextCollection = new List<ComboSQLText>();
            dataSetCollection = new List<DataSetCollection>();
            this.r820001_Vmg_Architect.Image = ExtendedGraphics.GenerateVerticalLabelText(this.r820001_Vmg_Architect.Height, this.r820001_Vmg_Architect.Width, this.r820001_Vmg_Architect.Text, this.r820001_Vmg_Architect.BackColor, this.r820001_Vmg_Architect.Font);
            this.r820001_Vmg_Architect.Text = string.Empty;
            this.AddDataSetValues("r820001_Architect", "StoredProcedure", "r820001_Get");
            ////this.AddDataSetValues("Name", "", "SELECT     ArchitectID, Name    FROM         tGD_Architect");
            this.AddDataSetValues("Name", "", "SELECT ArchitectID , [Name] AS [Value]  FROM [dbo].[r820001_udf_GetName] (null) AS A");
            //this.AddDataSetValues("Name", "", "SELECT  MIN(ArchitectID) AS ArchitectID,[Name] FROM tGD_Architect WHERE IsDeleted = 0 GROUP BY [Name]");
        }


        //private OperationSmartPart operationSmartPart;
        private F820001Controller form820001control;
        [CreateNew]
        public F820001Controller Form820001control
        {
            get { return this.form820001control as F820001Controller; }
            set { this.form820001control = value; }
        }
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            try
            {
                if (this != null && this.IsDisposed != true)
                {
                    if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                    {
                        this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                        this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                        this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                        this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                        if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                        {
                            this.LockControls(false);
                        }
                        else
                        {
                            this.LockControls(true);
                        }
                        if (this.formDetailsDataSet.Tables.Count > 0)
                        {
                            if (this.formDetailsDataSet.Tables[0].Rows.Count > 0)
                            {
                                eventArgs.Data.FlagInvalidSliceKey = false;
                            }
                            else
                            {
                                this.LockControls(true);
                                //// Coding Added for the issue 4212 0n 30/5/2009.
                                //// Last Slice does not have a record also it will not return invalid slice
                                if (eventArgs.Data.FlagInvalidSliceKey)
                                {
                                    eventArgs.Data.FlagInvalidSliceKey = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            try
            {
                if (this != null && this.IsDisposed != true)
                {
                    if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                    {
                        this.keyId = eventArgs.Data.SelectedKeyId;
                        if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                        {
                            this.LockControls(false);
                        }
                        else
                        {
                            this.LockControls(true);
                        }
                        this.pageLoadStatus = true;
                        this.ClearFields();
                        this.loadedFirst = true;
                        this.PopulateFormFields();
                        this.pageLoadStatus = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, DataEventArgs<int> eventArgs)
        {
            try
            {
                if (this != null && this.IsDisposed != true)
                {
                    if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                    {
                        if (this.slicePermissionField.newPermission)
                        {
                            this.Cursor = Cursors.WaitCursor;
                            this.pageLoadStatus = true;
                            this.pageMode = TerraScanCommon.PageModeTypes.New;
                            this.ClearFields();
                            isNewRecord = true;
                            this.LockControls(false);
                            this.pageLoadStatus = false;
                            this.Cursor = Cursors.Default;
                        }
                        else
                        {
                            this.LockControls(true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }


        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            try
            {
                if (this != null && this.IsDisposed != true)
                {
                    // In order to load the form using event id 
                    // Make the loadedfirst key to true
                    this.loadedFirst = true;

                    this.Cursor = Cursors.WaitCursor;
                    if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                    {
                        this.LockControls(false);
                    }
                    else
                    {

                        this.LockControls(true);
                    }
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.pageLoadStatus = true;
                    this.ClearFields();
                    this.PopulateFormFields();
                    this.pageLoadStatus = false;
                    this.isNewRecord = false;
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }


        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        {
            try
            {
                if (this != null && this.IsDisposed != true)
                {
                    if (this.slicePermissionField.deletePermission)
                    {
                        //this.DeleteButton_Click(); 
                        RdlToCodeData.ParameterDataDataTable dt = new RdlToCodeData.ParameterDataDataTable();
                        dt.Rows.Add(new object[] { "EventID", this.keyId });
                        string deleteXML = TerraScanCommon.GetXmlString(dt);
                        this.form820001control.WorkItem.RdlToCode_Delete(deleteXML, "r820001");
                    }
                }
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


        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            try
            {
                if (this != null && this.IsDisposed != true)
                {
                    if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
                    {
                        SliceValidationFields sliceValidationFields = new SliceValidationFields();
                        sliceValidationFields.FormNo = eventArgs.Data;
                        //this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data))); 
                        this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
                    }
                    else
                    {
                        SliceValidationFields sliceValidationFields = new SliceValidationFields();
                        sliceValidationFields.FormNo = eventArgs.Data;
                        this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, EventArgs eventArgs)
        {
            try
            {
                if (this != null && this.IsDisposed != true)
                {
                    if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
                    {
                        int currentKeyId = this.SaveForm(isNewRecord);
                        this.isNewRecord = false; if (currentKeyId != -1)
                        {
                            SliceReloadActiveRecord currentSliceInfo;
                            currentSliceInfo.MasterFormNo = this.masterFormNo;
                            currentSliceInfo.SelectedKeyId = currentKeyId;
                            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                            this.pageMode = TerraScanCommon.PageModeTypes.View;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        protected virtual void OnD9030_F9030_ReloadAfterSave(TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this.D9030_F9030_ReloadAfterSave != null)
            {
                this.D9030_F9030_ReloadAfterSave(this, eventArgs);
            }
        }


        protected virtual void OnFormSlice_FormCloseAlert(DataEventArgs<SliceFormCloseAlert> eventArgs)
        {
            if (this.FormSlice_FormCloseAlert != null)
            {
                this.FormSlice_FormCloseAlert(this, eventArgs);
            }
        }


        private void AddDataSetValues(string dsName, string commandType, string commandText)
        {
            DataSetCollection tempDataSetCollection;
            tempDataSetCollection.dataSetName = dsName;
            tempDataSetCollection.commandType = commandType;
            tempDataSetCollection.commandText = commandText;
            this.dataSetCollection.Add(tempDataSetCollection);
        }
        private void AddComboValues(string comboName, string sqlText)
        {
            ComboSQLText comboSQLText;
            comboSQLText.ComboName = comboName;
            comboSQLText.SqlText = sqlText;
            this.comboTextCollection.Add(comboSQLText);
        }
        private void PopulateFormFields()
        {
            //DataSet ds = new DataSet();
            RdlToCodeData.ParameterDataDataTable dt = new RdlToCodeData.ParameterDataDataTable();

            try
            {
                if (this.loadedFirst)
                {
                    dt.Rows.Add(new object[] { "EventID", this.keyId });
                    dt.Rows.Add(new object[] { "IsArchitect", false });
                    this.loadedFirst = false;
                }
                else
                {
                    int selectedID;
                    int.TryParse(this.r820001_Cmb_Name.SelectedValue.ToString(), out selectedID);
                    dt.Rows.Add(new object[] { "EventID", selectedID });
                    dt.Rows.Add(new object[] { "IsArchitect", true });
                }

                dt.AcceptChanges();

                this.formDetailsDataSet = this.form820001control.WorkItem.RdlToCode_Get(TerraScanCommon.GetXmlString(dt), "r820001");

                ////string sampleXml = TerraScanCommon.GetXmlString(dt);
                ////	 ds = TerraScan.Helper.WSHelper.RdlToCode_Get(dt,"r820001");
            }
            catch (Exception ex1)
            {
            }
            if (this.formDetailsDataSet != null)
            {
                if (this.formDetailsDataSet.Tables.Count > 0)
                {
                    if (this.formDetailsDataSet.Tables[0].Rows.Count >= 0)
                    {
                        foreach (Control c in this.Controls)
                        {
                            try
                            {
                                if (c.Name.EndsWith("subreport"))
                                {
                                    string[] formName = c.Name.Split(new char[] { '_' });
                                    DataSet subReportDataset = new DataSet();
                                    ////	 	 subReportDataset = TerraScan.Helper.WSHelper.RdlToCode_Get(dt, formName[2]);
                                    if (c.HasChildren)
                                    {
                                        if (c is Panel)
                                        {
                                            //(c as Panel).BorderStyle = BorderStyle.None; 
                                        }
                                        AddValuesToControl(subReportDataset, c.Controls);
                                    }
                                }
                                else if (c.HasChildren)
                                {
                                    AddValuesToControl(this.formDetailsDataSet, c.Controls);
                                }
                                else if (c.GetType() == typeof(TerraScan.UI.Controls.TerraScanTextBox))
                                {
                                    if (!string.IsNullOrEmpty(c.Tag.ToString()))
                                    {
                                        c.Text = this.formDetailsDataSet.Tables[0].Rows[0][c.Tag.ToString()].ToString();
                                    }
                                }
                                else if (c.GetType() == typeof(TerraScan.UI.Controls.TerraScanCheckBox))
                                {
                                    if (!string.IsNullOrEmpty(c.Tag.ToString()))
                                    {
                                        string result = this.formDetailsDataSet.Tables[0].Rows[0][c.Tag.ToString()].ToString();
                                        if (result == "True")
                                        {
                                            ((CheckBox)c).Checked = true;
                                        }
                                    }
                                }
                                else if (c.GetType() == typeof(RadioButton))
                                {
                                    if (!string.IsNullOrEmpty(c.Tag.ToString()))
                                    {
                                        string result = this.formDetailsDataSet.Tables[0].Rows[0][c.Tag.ToString()].ToString();
                                        if (c.AccessibleName.ToString().EndsWith(result))
                                        {
                                            ((RadioButton)c).Checked = true;
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    else
                    {
                        this.LockControls(true);
                    }
                }
            }
        }
        private void SetBorderStyle(Control currentControl)
        {
            if ((currentControl != null) && (currentControl.Parent != null) && (currentControl.Parent is Panel))
            {
                if ((currentControl.Parent as Panel).BorderStyle != BorderStyle.FixedSingle)
                {
                    //(currentControl.Parent as Panel).BorderStyle = BorderStyle.FixedSingle; 
                }
            }
        }

        private void GetLockControls()
        {
            foreach (Control c in this.Controls)
            {
                if (c.Name.EndsWith("subreport"))
                {
                    string[] formName = c.Name.Split(new char[] { '_' });
                    if (c.HasChildren)
                    {
                        this.AddLockControlsCollection(c.Controls);
                    }
                }
                else if (c.HasChildren)
                {
                    this.AddLockControlsCollection(c.Controls);
                }
                else if (c.GetType().Equals(typeof(TerraScan.UI.Controls.TerraScanTextBox)))
                {
                    (c as TerraScanTextBox).BorderStyle = BorderStyle.None;
                    (c as TerraScanTextBox).ApplyFocusColor = true;
                    (c as TerraScanTextBox).SetFocusColor = Color.FromArgb(255, 255, 121);
                    this.SetBorderStyle(c);
                    this.lockControlsCollection.Add(c);
                }
                else if (c.GetType().Equals(typeof(TerraScan.UI.Controls.TerraScanComboBox)))
                {
                    (c as TerraScanComboBox).FlatStyle = FlatStyle.Popup;
                    this.SetBorderStyle(c);
                    this.lockControlsCollection.Add(c);
                }
                else if (c.GetType().Equals(typeof(TerraScan.UI.Controls.TerraScanCheckBox)))
                {
                    this.SetBorderStyle(c);
                    this.lockControlsCollection.Add(c);
                }
                else if (c.GetType().Equals(typeof(RadioButton)))
                {
                    this.SetBorderStyle(c);
                    this.lockControlsCollection.Add(c);
                }
            }
        }

        private void AddLockControlsCollection(Control.ControlCollection c)
        {
            foreach (Control c1 in c)
            {
                if (c1.HasChildren)
                {
                    this.AddLockControlsCollection(c1.Controls);
                }
                else if (c1.GetType() == typeof(TerraScan.UI.Controls.TerraScanTextBox))
                {
                    (c1 as TerraScanTextBox).BorderStyle = BorderStyle.None;
                    (c1 as TerraScanTextBox).ApplyFocusColor = true;
                    (c1 as TerraScanTextBox).SetFocusColor = Color.FromArgb(255, 255, 121);
                    this.SetBorderStyle(c1);
                    this.lockControlsCollection.Add(c1);
                }
                else if (c1.GetType() == typeof(TerraScan.UI.Controls.TerraScanComboBox))
                {
                    (c1 as TerraScanComboBox).FlatStyle = FlatStyle.Popup;
                    this.SetBorderStyle(c1);
                    this.lockControlsCollection.Add(c1);
                }
                else if (c1.GetType() == typeof(TerraScan.UI.Controls.TerraScanCheckBox))
                {
                    this.SetBorderStyle(c1);
                    this.lockControlsCollection.Add(c1);
                }
                else if (c1.GetType() == typeof(RadioButton))
                {
                    this.SetBorderStyle(c1);
                    this.lockControlsCollection.Add(c1);
                }
            }
        }

        private void LockControls(bool lockValue)
        {
            if (this.lockControlsCollection != null && this.lockControlsCollection.Count > 0)
            {
                for (int iCount = 0; iCount < this.lockControlsCollection.Count; iCount++)
                {
                    Control tempControl = this.lockControlsCollection[iCount];
                    if (tempControl.GetType().Equals(typeof(TerraScanTextBox)))
                    {
                        (tempControl as TerraScanTextBox).LockKeyPress = lockValue;
                    }
                    else if (tempControl.GetType().Equals(typeof(TerraScanComboBox)))
                    {
                        (tempControl as TerraScanComboBox).Enabled = !lockValue;
                    }
                    else if (tempControl.GetType().Equals(typeof(TerraScanCheckBox)))
                    {
                        (tempControl as TerraScanCheckBox).Enabled = !lockValue;
                    }
                    else if (tempControl.GetType().Equals(typeof(RadioButton)))
                    {
                        (tempControl as RadioButton).Enabled = !lockValue;
                    }
                }
            }
        }

        private void AddValuesToControl(DataSet ds, Control.ControlCollection c)
        {
            foreach (Control c1 in c)
            {
                if (c1.HasChildren)
                {
                    if (c1 is Panel)
                    {
                        //(c1 as Panel).BorderStyle = BorderStyle.None; 
                    }
                    AddValuesToControl(ds, c1.Controls);
                }
                else if (c1.GetType() == typeof(TerraScan.UI.Controls.TerraScanTextBox))
                {
                    if (!string.IsNullOrEmpty(c1.Tag.ToString()))
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            c1.Text = ds.Tables[0].Rows[0][c1.Tag.ToString()].ToString();
                        }
                    }
                }
                else if (c1.GetType() == typeof(TerraScan.UI.Controls.TerraScanComboBox))
                {
                    DataSet dsCombo = new DataSet();
                    for (int ctrlCount = 0; ctrlCount < this.dataSetCollection.Count; ctrlCount++)
                    {
                        if (this.dataSetCollection[ctrlCount].dataSetName.ToLower().Equals(c1.Tag.ToString().ToLower()))
                        {
                            dsCombo = this.form820001control.WorkItem.RdlToCode_FillCombo(this.dataSetCollection[ctrlCount].commandText);
                        }
                    }
                    ((TerraScan.UI.Controls.TerraScanComboBox)c1).DataSource = dsCombo.Tables[0];
                    ((TerraScan.UI.Controls.TerraScanComboBox)c1).DisplayMember = dsCombo.Tables[0].Columns[1].ToString();
                    ((TerraScan.UI.Controls.TerraScanComboBox)c1).ValueMember = dsCombo.Tables[0].Columns[0].ToString();
                    //((TerraScan.UI.Controls.TerraScanComboBox)c1).SelectedValue = ds.Tables[0].Rows[0][c1.Tag.ToString()].ToString();
                    int.TryParse(dsCombo.Tables[0].Rows[0]["ArchitectID"].ToString(), out this.selectedArchitectID);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.SetComboSelectedValue(((TerraScan.UI.Controls.TerraScanComboBox)c1), dsCombo, ds);
                    }
                }
                else if (c1.GetType() == typeof(TerraScan.UI.Controls.TerraScanCheckBox))
                {
                    if (!string.IsNullOrEmpty(c1.Tag.ToString()))
                    {
                        string result = string.Empty;
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            result = ds.Tables[0].Rows[0][c1.Tag.ToString()].ToString();
                        }
                        if (result == "True")
                        {
                            ((CheckBox)c1).Checked = true;
                        }
                    }
                }
                else if (c1.GetType() == typeof(RadioButton))
                {
                    if (!string.IsNullOrEmpty(c1.Tag.ToString()))
                    {
                        string result = string.Empty;
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            result = ds.Tables[0].Rows[0][c1.Tag.ToString()].ToString();
                        }
                        if (c1.AccessibleName.ToString().EndsWith(result))
                        {
                            ((RadioButton)c1).Checked = true;
                        }
                    }
                }
            }
        }
        private void EditEnabledEvent(object sender, EventArgs e)
        {
            this.SetEditRecord();
        }

        private void SetEditRecord()
        {
            if (this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                if (!this.pageLoadStatus)
                {
                    if (!string.Equals(this.pageMode, TerraScanCommon.PageModeTypes.Edit))
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                    }
                }
            }
        }
        private int SaveForm(bool newRecord)
        {
            int result = -1;
            DataSet da = new DataSet();
            RdlToCodeData.ParameterDataDataTable dt = new RdlToCodeData.ParameterDataDataTable();
            foreach (Control c in this.Controls)
            {
                try
                {
                    if (c.Name.EndsWith("subreport"))
                    {
                        string[] formName = c.Name.Split(new char[] { '_' });
                        DataSet subReportDataset = new DataSet();
                        if (c.HasChildren)
                        {
                            RdlToCodeData.ParameterDataDataTable subReportDatatable = new RdlToCodeData.ParameterDataDataTable();
                            UpdateValuesToControl(subReportDatatable, c.Controls);
                            result = SaveOperation(subReportDatatable, newRecord, formName[2]);
                        }
                    }
                    else if (c.HasChildren)
                    {
                        UpdateValuesToControl(dt, c.Controls);
                    }
                    else if (c.GetType() == typeof(TerraScan.UI.Controls.TerraScanTextBox))
                    {
                        if (!string.IsNullOrEmpty(c.Tag.ToString()))
                        {
                            dt.Rows.Add(new object[] { c.Tag.ToString(), c.Text });
                        }
                    }
                    else if (c.GetType() == typeof(TerraScan.UI.Controls.TerraScanCheckBox))
                    {
                        if (!string.IsNullOrEmpty(c.Tag.ToString()))
                        {
                            if (((TerraScan.UI.Controls.TerraScanCheckBox)c).Checked == true)
                            {
                                dt.Rows.Add(new object[] { c.Tag.ToString(), true });
                            }
                            else
                            {
                                dt.Rows.Add(new object[] { c.Tag.ToString(), false });
                            }
                        }
                    }
                    else if (c.GetType() == typeof(RadioButton))
                    {
                        if (!string.IsNullOrEmpty(c.Tag.ToString()))
                        {
                            if (((RadioButton)c).Checked == true)
                            {
                                string[] value = c.AccessibleName.Split('_');
                                string actualValue = value[value.Length - 1].ToString();
                                dt.Rows.Add(new object[] { c.Tag.ToString(), actualValue });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            RdlToCodeData.ParameterDataRow[] parameterDataRowcollection = (RdlToCodeData.ParameterDataRow[])dt.Select("KeyId ='Name'");
            if (parameterDataRowcollection.Length > 0)
            {
                parameterDataRowcollection[0].KeyName = this.r820001_Cmb_Name.Text;
            }
            dt.AcceptChanges();

            string saveXML = TerraScanCommon.GetXmlString(dt);

            result = SaveOperation(dt, newRecord, "r820001");
            return result;
        }
        private void UpdateValuesToControl(RdlToCodeData.ParameterDataDataTable dt, Control.ControlCollection c)
        {
            foreach (Control c1 in c)
            {
                string nae = c1.Name;
                if (c1.HasChildren)
                {
                    UpdateValuesToControl(dt, c1.Controls);
                }
                else if (c1.GetType() == typeof(TerraScan.UI.Controls.TerraScanTextBox))
                {
                    if (!string.IsNullOrEmpty(c1.Tag.ToString()))
                    {
                        dt.Rows.Add(new object[] { c1.Tag.ToString(), c1.Text });
                    }
                }
                else if (c1.GetType() == typeof(TerraScan.UI.Controls.TerraScanComboBox))
                {
                    if (!string.IsNullOrEmpty(c1.Tag.ToString()))
                    {
                        dt.Rows.Add(new object[] { c1.Tag.ToString(), ((TerraScan.UI.Controls.TerraScanComboBox)c1).SelectedValue });
                    }
                }
                else if (c1.GetType() == typeof(TerraScan.UI.Controls.TerraScanCheckBox))
                {
                    if (!string.IsNullOrEmpty(c1.Tag.ToString()))
                    {
                        if (((TerraScan.UI.Controls.TerraScanCheckBox)c1).Checked == true)
                        {
                            dt.Rows.Add(new object[] { c1.Tag.ToString(), true });
                        }
                        else
                        {
                            dt.Rows.Add(new object[] { c1.Tag.ToString(), false });
                        }
                    }
                }
                else if (c1.GetType() == typeof(RadioButton))
                {
                    if (!string.IsNullOrEmpty(c1.Tag.ToString()))
                    {
                        if (((RadioButton)c1).Checked == true)
                        {
                            string[] value = c1.AccessibleName.Split('_');
                            string actualValue = value[value.Length - 1].ToString();
                            dt.Rows.Add(new object[] { c1.Tag.ToString(), actualValue });
                        }
                    }
                }
            }
        }
        private void ClearFields()
        {
            foreach (Control c in this.Controls)
            {
                if (c.Name != "subreport1")
                {
                    if (c.HasChildren)
                    {
                        ClearFieldValues(c.Controls);
                    }
                }
            }
        }
        private void ClearFieldValues(Control.ControlCollection c)
        {
            foreach (Control c1 in c)
            {
                if (c1.HasChildren)
                {
                    ClearFieldValues(c1.Controls);
                }
                else if (c1.GetType() == typeof(TerraScan.UI.Controls.TerraScanTextBox))
                {
                    if (!string.IsNullOrEmpty(c1.Tag.ToString()))
                    {
                        c1.Text = "";
                    }
                }
                else if (c1.GetType() == typeof(TerraScan.UI.Controls.TerraScanCheckBox))
                {
                    ((CheckBox)c1).Checked = false;
                }
                else if (c1.GetType() == typeof(TerraScan.UI.Controls.TerraScanComboBox))
                {
                    DataSet dsCombo = new DataSet();
                    for (int ctrlCount = 0; ctrlCount < this.dataSetCollection.Count; ctrlCount++)
                    {
                        if (this.dataSetCollection[ctrlCount].dataSetName.ToLower().Equals(c1.Tag.ToString().ToLower()))
                        {
                            dsCombo = this.form820001control.WorkItem.RdlToCode_FillCombo(this.dataSetCollection[ctrlCount].commandText);
                        }
                        ////int.TryParse(dsCombo.Tables[0].Rows[1]["ArchitectID"].ToString(), out this.selectedArchitectID);
                    }

                    //this.selectedArchitectID = dsCombo.Tables[0].Rows[1].ToString();
                    if (dsCombo != null && dsCombo.Tables.Count > 0)
                    {
                        ((TerraScan.UI.Controls.TerraScanComboBox)c1).DataSource = dsCombo.Tables[0];
                        ((TerraScan.UI.Controls.TerraScanComboBox)c1).DisplayMember = dsCombo.Tables[0].Columns[1].ToString();
                        ((TerraScan.UI.Controls.TerraScanComboBox)c1).ValueMember = dsCombo.Tables[0].Columns[0].ToString();
                        //((TerraScan.UI.Controls.TerraScanComboBox)c1).SelectedValue = ds.Tables[0].Rows[0][c1.Tag.ToString()].ToString();
                        //int.TryParse(dsCombo.Tables[0].Rows[1]["ArchitectID"].ToString(), out this.selectedArchitectID);
                    }
                    ((TerraScan.UI.Controls.TerraScanComboBox)c1).SelectedIndex = -1;
                }
                else if (c1.GetType().Equals(typeof(RadioButton)))
                {
                    ((RadioButton)c1).Checked = false;
                }
            }
        }
        private int SaveOperation(RdlToCodeData.ParameterDataDataTable dt, bool newRecord, string formName)
        {
            int errorId = -1;
            if (!newRecord)
            {
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Add(new object[] { "EventID", this.keyId });
                    string saveXML = TerraScanCommon.GetXmlString(dt);
                    errorId = this.form820001control.WorkItem.RdlToCode_Save(saveXML, formName);
                    this.keyId = errorId;
                    //MessageBox.Show("Record Updated Successfully for " + formName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    string saveXML = TerraScanCommon.GetXmlString(dt);
                    errorId = this.form820001control.WorkItem.RdlToCode_Save(saveXML, formName);
                    this.keyId = errorId;
                    //MessageBox.Show("New Record Created Successfully for " + formName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            return errorId;
        }
        private void SetComboSelectedValue(TerraScanComboBox combobox, DataSet comboDataSet, DataSet getDataSet)
        {
            try
            {
                string columnName = string.Empty;
                string value = string.Empty;
                columnName = this.GetColumnName(combobox.ValueMember, getDataSet);
                if (columnName.Equals(string.Empty))
                {
                    columnName = this.GetColumnName(combobox.DisplayMember, getDataSet);
                    if (columnName.Equals(string.Empty))
                    {
                        this.SetNoneAsDefault(combobox, comboDataSet);
                    }
                    else
                    {
                        value = getDataSet.Tables[0].Rows[0][columnName].ToString();
                        value = this.CheckValue(value, combobox, false, comboDataSet);
                        if (!string.IsNullOrEmpty(value))
                        {
                            combobox.SelectedValue = value;
                        }
                        else
                        {
                            this.SetNoneAsDefault(combobox, comboDataSet);
                        }
                    }
                }
                else // if column name is value member 
                {
                    value = getDataSet.Tables[0].Rows[0][columnName].ToString();
                    if (!string.IsNullOrEmpty(this.CheckValue(value, combobox, true, comboDataSet)))
                    {
                        combobox.SelectedValue = value;
                    }
                    else // if the value from get method not present at combo box(combo DataSet) 
                    {
                        this.SetNoneAsDefault(combobox, comboDataSet);
                        string comboText = getDataSet.Tables[0].Rows[0]["Name"].ToString();
                        combobox.Text = comboText;
                    }
                }
            }
            catch
            {
            }
        }

        private string CheckValue(string value, ComboBox combo, bool flagValueMember, DataSet comboDataSet)
        {
            string columnName = string.Empty;
            if (flagValueMember)
            {
                columnName = combo.ValueMember;
            }
            else
            {
                columnName = combo.DisplayMember;
            }
            DataRow[] getSelctedRows = comboDataSet.Tables[0].Select(columnName + "= '" + value + "'");
            if ((getSelctedRows != null) && (getSelctedRows.Length > 0))
            {
                return getSelctedRows[0][combo.ValueMember].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        private void SetNoneAsDefault(ComboBox combo, DataSet comboDataSet)
        {
            DataRow newRow = comboDataSet.Tables[0].NewRow();
            newRow[combo.DisplayMember] = string.Empty;
            newRow[combo.ValueMember] = 0;
            comboDataSet.Tables[0].Rows.Add(newRow);
            combo.SelectedValue = 0;
        }

        private string GetColumnName(string columnName, DataSet getDataSet)
        {
            foreach (DataColumn column in getDataSet.Tables[0].Columns)
            {
                if (columnName == column.ColumnName)
                {
                    return column.ColumnName;
                }
            }
            return string.Empty;
        }

        private void r820001_Cmb_Name_TextChanged(object sender, EventArgs e)
        {
            if (this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                if (!this.pageLoadStatus)
                {
                    if (!string.Equals(this.pageMode, TerraScanCommon.PageModeTypes.Edit))
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                    }
                }
            }
        }

        private void r820001_Cmb_Name_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                if (!this.pageLoadStatus)
                {
                    if (!string.Equals(this.pageMode, TerraScanCommon.PageModeTypes.Edit))
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                    }

                    // int.TryParse(this.r820001_Cmb_Name.SelectedValue.ToString(), out this.keyId);
                    //this.keyId = this.selectedArchitectID;

                    this.PopulateFormFields();
                }
            }

            //int tempArchitectID;
            //string tempName;

            //tempName = this.r820001_Cmb_Name.Text;
            //tempArchitectID = Convert.ToInt32(this.r820001_Cmb_Name.SelectedValue);                    

        }

    }
}