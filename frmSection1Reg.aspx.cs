using AjaxControlToolkit;
using KcBal.Common;
using KcBal.YP;
using KcDal.YP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LsWb.UI.Common
{
    public partial class frmSection1Reg : System.Web.UI.Page, ITemplate
    {
        DataTable dt = new DataTable(); DataTable dtChild = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dts = new DataTable();

        BAL_Statics obals = new BAL_Statics();
        BAL_Dynamic obal = new BAL_Dynamic();
        DAL_DynamicTest odal = new DAL_DynamicTest();
        string _formCode = "101";
        string _subformcode = "1";
        string StoredProcedureSuffixName = "Sec1RespondentReg";
        string _tblName = "";
        string _userid = "";
        string stateId = "";
        string districtId = "";
        string cityId = "";



        string QuestionMasterSessionName = "QnDatatable_frmSec1";
        string QuestionMasterChildSessionName = "QnDatatable_frmSec1_Child";
        string SectionButtonSessionName = "BtnDatatable_frmSec1";
        string GridDetailsViewStateName = "GridDetails";
        string QuestionMasterTblName = "[Master].[tblDynamicQnMaster_101]";
        string DistinctValSessionName = "distinctVal_frmSec1";
        string PreviousPage = "../Common/frmPreTraining.aspx";
        string NextRedirectPageUrl = "frmSection3FamilyEnvironment.aspx";
        string CurrentPageName = "frmSection1Reg.aspx";
        bool IsLastSection = false;

        public void ActiveTab(int TabToActive)
        {
            Session["ActiveTab"] = Convert.ToString(TabToActive);
        }
        public void EncryptAndRedirect(string url, string sysId)
        {
            DESCryptoServiceProvider md5Obj = new DESCryptoServiceProvider();

            byte[] key = new byte[] { 149, 48, 49, 167, 66, 187, 187, 125 };

            md5Obj.Key = key;
            md5Obj.IV = key;

            ICryptoTransform desencrypt = md5Obj.CreateEncryptor();

            byte[] lifeSkill = System.Text.Encoding.ASCII.GetBytes(sysId);

            byte[] nimLs = desencrypt.TransformFinalBlock(lifeSkill, 0, lifeSkill.Length);


            string nim = BitConverter.ToString(nimLs);
            Response.Redirect(url + "?LifeSkill=" + nim.ToString());


        }
        public string Encrypt(string url, string sysId)
        {
            DESCryptoServiceProvider md5Obj = new DESCryptoServiceProvider();

            byte[] key = new byte[] { 149, 48, 49, 167, 66, 187, 187, 125 };

            md5Obj.Key = key;
            md5Obj.IV = key;

            ICryptoTransform desencrypt = md5Obj.CreateEncryptor();

            byte[] lifeSkill = System.Text.Encoding.ASCII.GetBytes(sysId);

            byte[] nimLs = desencrypt.TransformFinalBlock(lifeSkill, 0, lifeSkill.Length);


            string nim = BitConverter.ToString(nimLs);

            return url + "?LifeSkill=" + nim.ToString();



        }
        public string Decrypt()
        {

            try
            {


                if (Request.QueryString["LifeSkill"] != null)
                {
                    string data = Request.QueryString["LifeSkill"].ToString();


                    string[] arrayData = data.Split("-".ToCharArray());


                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                    byte[] inputByteArray = new byte[arrayData.Length];


                    for (int i = 0; i < arrayData.Length; i++)
                    {
                        inputByteArray[i] = byte.Parse(arrayData[i], System.Globalization.NumberStyles.HexNumber);
                    }



                    //des.Key = new byte[] { 146, 43, 41, 160, 64, 185, 185, 121 };
                    des.Key = new byte[] { 149, 48, 49, 167, 66, 187, 187, 125 };
                    des.IV = new byte[] { 149, 48, 49, 167, 66, 187, 187, 125 };

                    ICryptoTransform desencrypt = des.CreateDecryptor();

                    byte[] result = desencrypt.TransformFinalBlock(inputByteArray, 0, inputByteArray.Length);




                    string sysId = Encoding.UTF8.GetString(result);
                    return sysId;
                }
                return "";
            }
            catch (CryptographicException ex)
            {
                Session["EncryptionError"] = "Error";
                Response.Redirect("~/Error.aspx");
                return "";
            }

        }
        public bool IsCheckSaveOrModify()
        {
            string Id = Decrypt();

            ViewState["QueryStringVal"] = Id;

            if (Id != "" && Id != null)
            {
                //modify
                return false;
            }
            else if (Id == "" || Id == null)
            {
                //save
                return true;
            }
            return true;
        }



        static string _taluka;
        [WebMethod(EnableSession = true)]
        public static string[] GetuserId(string prefixText)
        {


            DAL_DynamicTest odal = new DAL_DynamicTest();
            // Academic = Convert.ToString(Session["AcademicYearID"]);
            DataTable dt = new DataTable();
            dt = odal.ReturnDt("select fldVillageName from [bind].[tblVillage] where fldVillageName like '" + prefixText + "%' and fldTalukaId='" + _taluka + "' order by fldVillageName");
            string[] items = new string[dt.Rows.Count];
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                items.SetValue(dr["fldVillageName"].ToString(), i);
                i++;
            } return items;
        }

        private void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            ViewState["StartTime"] = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss");

            bool CheckSaveOrModify = IsCheckSaveOrModify();
            if (CheckSaveOrModify)
            {
                //save
            }
            else if (!CheckSaveOrModify)
            {
                //modify
            }
            Page.MaintainScrollPositionOnPostBack = true;


            btnsave.Attributes.Add("onclick", " this.disabled=true;" + ClientScript.GetPostBackEventReference(btnsave, null) + ";");

            if (!Page.IsPostBack)
            {
                ActiveTab(1);
                dt = obal.getUIField(_formCode, _subformcode, QuestionMasterTblName);

                Session[QuestionMasterSessionName] = dt;

                dtChild = obal.getUIFieldChild(_formCode);

                Session[QuestionMasterChildSessionName] = dtChild;


                dt1 = obal.getSecDetails(_formCode);

                Session[SectionButtonSessionName] = dt1;

            }

            dt = (DataTable)Session[QuestionMasterSessionName];

            dtChild = (DataTable)Session[QuestionMasterChildSessionName];

            dt1 = (DataTable)Session[SectionButtonSessionName];

           


            stateId = Convert.ToString(Session["StateId"]);
            districtId = Convert.ToString(Session["DistrictId"]);
            cityId = Convert.ToString(Session["CityId"]);


            DataView view = new DataView(dt);
            DataTable distinctVal = view.ToTable(true, "fldDivID");

            Session[DistinctValSessionName] = distinctVal;

            DataTable dtform = obal.getFormTitle(_formCode, _subformcode);
            string FormTitle = string.Empty; string FormName = string.Empty;
            string FormCtrlDetails = string.Empty;
            if (dtform.Rows.Count > 0)
            {
                FormTitle = Convert.ToString(dtform.Rows[0]["fldFormTitle"]);
                FormName = Convert.ToString(dtform.Rows[0]["fldFormName"]);
                FormCtrlDetails = Convert.ToString(dtform.Rows[0]["fldFormControlDetails"]);
            }

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();

            var Result = (from c in dt.AsEnumerable()
                          select new
                          {
                              fldQnEngText = c.Field<string>("fldQnEngText"),
                              fldQnWidgetId = c.Field<string>("fldQnWidgetId"),
                              fldQnWidgetName = c.Field<string>("fldQnWidgetName"),
                              fldQnMandatory = c.Field<bool>("fldQnMandatory"),
                              fldIsDependent = c.Field<bool>("fldIsDependent"),
                              fldDependentColNames = c.Field<string>("fldDependentColNames"),
                              fldDependentColValues = c.Field<string>("fldDependentColValues"),
                              fldDependentColType = c.Field<string>("fldDependentColType"),
                              fldDivID = c.Field<string>("fldDivID"),
                              fldIsGridData = c.Field<bool>("fldIsGridData"),
                              fldIsAutoBindOrInputQn = c.Field<Int32>("fldIsAutoBindOrInputQn")

                          }).ToList();

            hdnControl.Value = oSerializer.Serialize(Result);

            JavaScriptSerializer oSerializerChild = new JavaScriptSerializer();

            var ResultChild = (from c in dtChild.AsEnumerable()
                               select new
                               {
                                   fldQnId = c.Field<string>("fldQnId"),
                                   fldFormId = c.Field<string>("fldFormId"),
                                   fldIsCrossValidation = c.Field<bool>("fldIsCrossValidation"),
                                   fldOperation = c.Field<string>("fldOperation"),
                                   fldValue = c.Field<string>("fldValue"),
                                   fldColType = c.Field<string>("fldColType"),
                                   fldColToEnableOrDisable = c.Field<string>("fldColToEnableOrDisable")

                               }).ToList();

            hdnControlChild.Value = oSerializerChild.Serialize(ResultChild);

            JavaScriptSerializer oSerializerpostback = new JavaScriptSerializer();

            var Resultpostback = (from c in dt.AsEnumerable()
                                  select new
                                  {
                                      fldQnEngText = c.Field<string>("fldQnEngText"),
                                      fldQnWidgetId = c.Field<string>("fldQnWidgetId"),
                                      fldQnWidgetName = c.Field<string>("fldQnWidgetName"),
                                      fldQnMandatory = c.Field<bool>("fldQnMandatory"),
                                      fldIsDependent = c.Field<bool>("fldIsDependent"),
                                      fldDependentColNames = c.Field<string>("fldDependentColNames"),
                                      fldDependentColValues = c.Field<string>("fldDependentColValues"),
                                      fldDependentColType = c.Field<string>("fldDependentColType"),
                                      fldIsCrossValidation = c.Field<bool>("fldIsCrossValidation"),
                                      fldOperation = c.Field<string>("fldOperation"),
                                      fldValue = c.Field<string>("fldValue"),
                                      fldColType = c.Field<string>("fldColType"),
                                      fldColToEnableOrDisable = c.Field<string>("fldColToEnableOrDisable"),
                                      fldQnId = c.Field<string>("fldQnId")

                                  }).ToList();

            hdnpostback.Value = oSerializerpostback.Serialize(Resultpostback);

            bindHeader(dt1, FormTitle, FormName);

            hdnFrmCtrlDetails.Value = FormCtrlDetails;

            Dictionary<string, int> dic = new Dictionary<string, int>(); int IsGridDataExists = 1;
            if (ViewState[GridDetailsViewStateName] == null)
            {
                IsGridDataExists = 0;
            }
            else if (ViewState[GridDetailsViewStateName] != null)
            {
                dic = (Dictionary<string, int>)ViewState[GridDetailsViewStateName];
                if (dic.Count > 0)
                {
                    hfGridDetails.Value = "";
                }

                foreach (KeyValuePair<string, int> entry in dic)
                {

                    hfGridDetails.Value = hfGridDetails.Value + entry.Key + "&" + entry.Value + "_";
                }
            }

            for (int i = 0; i < distinctVal.Rows.Count; i++)
            {
                DataTable dtdiv = new DataTable();
                dtdiv = dt.Clone();
                DataRow[] dr = dt.Select("fldDivID=" + "'" + Convert.ToString(distinctVal.Rows[i]["fldDivID"]) + "'");
                string _subformtitle = string.Empty;
                _subformtitle = obal.getSubFormTitle(_formCode, _subformcode, Convert.ToString(distinctVal.Rows[i]["fldDivID"]));
                foreach (DataRow d in dr)
                {

                    dtdiv.ImportRow(d);
                }

                bindcontrols(dtdiv, Convert.ToString(distinctVal.Rows[i]["fldDivID"]), _subformtitle, IsGridDataExists, dic);

            }

            if (ViewState[GridDetailsViewStateName] != null)
            {
                dic = (Dictionary<string, int>)ViewState[GridDetailsViewStateName];
                if (dic.Count > 0)
                {
                    hfGridDetails.Value = "";
                }

                foreach (KeyValuePair<string, int> entry in dic)
                {

                    hfGridDetails.Value = hfGridDetails.Value + entry.Key + "&" + entry.Value + "_";
                }
            }


            GetUserRole();

            hffocus.Value = Convert.ToString(dt.Rows[0]["fldQnWidgetName"]);

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Alert", "PostBackControlCheck();", true);

        }
        private void Txt_ChangeEvent(object sender, EventArgs e)
        {
            TextBox txta = (TextBox)sender;
            string TxtId = txta.ID;
            string _Value = txta.Text;


            if (TxtId == "txtVillage")
            {
                TextBox txtVillage = (TextBox)(divmain.FindControl("txtVillage"));


                dt = odal.ReturnDt("select fldDistrictName,fldTalukaName,fldVillageName from [bind].[tblVillage] where fldVillageName='" + _Value + "'and fldIsActive=1 ");


                if (dt.Rows.Count == 0)
                {
                    //string msg = "alert('This village does not exist');";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", msg, true);
                    //txtVillage.Text = "";
                    //txtVillage.Focus();
                    txtVillage.Text = "";
                    string msg = "This village does not exist ";
                    ShowPopUp(msg, TxtId);
                }
            }
            hfkeypress.Value = "0";
            hffocus.Value = TxtId;
            //  ddl.Focus();

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Popup", "stopLoading();", true);
        }

        private void cl_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckBoxList clb = (CheckBoxList)sender;
            string ddlId = clb.ID;
            string _Value = "";// clb.SelectedItem.Value;


            hfkeypress.Value = "0";
            hffocus.Value = ddlId;
            // clb.Focus();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Popup", "stopLoading();", true);
        }

        private void ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // System.Threading.Thread.Sleep(10000);

            DropDownList ddl = (DropDownList)sender;
            string ddlId = ddl.ID;
            string _Value = ddl.SelectedValue;

            if (ddlId == "ddlTaluka")
            {
                _taluka = _Value;
            }


            DataTable tblFiltered = dt.AsEnumerable()
                        .Where(row => row.Field<String>("fldQnWidgetName") == ddlId
                                )
                        .CopyToDataTable();

            //Array arrChildType = new Array[0];

            string _Parent = tblFiltered.Rows[0]["fldIsQnParent"].ToString();

            if (_Parent == "True")
            {

                string _ChildType = tblFiltered.Rows[0]["fldQnChildWidgetId"].ToString();
                string[] arrChildType = _ChildType.Split('#');

                string _Child = tblFiltered.Rows[0]["fldQnWidgetChildName"].ToString();
                string[] arrChild = _Child.Split('#');

                string _bind = tblFiltered.Rows[0]["fldChildBindQuery"].ToString();
                string[] arrbind = _bind.Split('#');


                string DependentCtrlToClrId = tblFiltered.Rows[0]["fldDependentCtrlToClrId"].ToString();
                string[] arrDependentCtrlToClrId = DependentCtrlToClrId.Split('#');

                string DependentCtrlToClr = tblFiltered.Rows[0]["fldDependentCtrlToClr"].ToString();
                string[] arrDependentCtrlToClr = DependentCtrlToClr.Split('#');

                if (DependentCtrlToClrId != "" && DependentCtrlToClrId != null)
                {
                    // to clear child controls
                    for (int arr = 0; arr < arrDependentCtrlToClr.Length; arr++)
                    {
                        DependentCtrlToClr = arrDependentCtrlToClr[arr];
                        DependentCtrlToClrId = arrDependentCtrlToClrId[arr];

                        if (DependentCtrlToClrId == "3")
                        {

                            DropDownList ddlclr = (DropDownList)(divmain.FindControl(DependentCtrlToClr.ToString()));

                            ddlclr.Items.Clear();
                            ddlclr.DataSource = null;
                            ddlclr.SelectedIndex = -1;
                        }

                        else if (DependentCtrlToClrId == "4")
                        {

                            CheckBoxList clbclr = (CheckBoxList)(divmain.FindControl(DependentCtrlToClr.ToString()));

                            foreach (ListItem item in clbclr.Items)
                            {
                                if (item.Selected)
                                    item.Selected = false;
                            }

                        }

                        else if (DependentCtrlToClrId == "1")
                        {

                            TextBox txtclr = (TextBox)(divmain.FindControl(DependentCtrlToClr.ToString()));

                            txtclr.Text = "";

                        }

                    }
                }

                // to bind child controls
                for (int arr = 0; arr < arrChildType.Length; arr++)
                {
                    _ChildType = arrChildType[arr];
                    _Child = arrChild[arr];
                    _bind = arrbind[arr];

                    if (_ChildType == "3")
                    {
                        DropDownList ddla = (DropDownList)(divmain.FindControl(_Child.ToString()));
                        obal.bindddlChild(ddla, _bind, _Value);
                    }

                    if (_ChildType == "10")
                    {
                        GridView gv = (GridView)(divmain.FindControl(_Child.ToString()));

                        obal.getGridBind(_bind, Convert.ToString(ViewState["QueryStringVal"]));
                    }

                    else if (_ChildType == "4")
                    {
                        CheckBoxList cla = (CheckBoxList)(divmain.FindControl(_Child.ToString()));
                        obal.bindddlChildcl(cla, _bind, _Value);
                    }

                    else if (_ChildType == "1")
                    {
                        TextBox txta = (TextBox)(divmain.FindControl(_Child.ToString()));
                        DataTable dtTxt = obal.returnTxtValues(_bind, _Value);
                        if (dtTxt.Rows.Count > 0)
                        {
                            txta.Text = dtTxt.Rows[0][0].ToString();
                        }
                    }

                }

                //DropDownList ddla = (DropDownList)(divmain.FindControl(_Child.ToString()));
                // obal.bindddlChild(ddla, _bind, _Value);

            }


            if (ddlId == "ddlPartTrained")
            {

                DataTable tblFilteredch = dt.AsEnumerable()
                       .Where(row => row.Field<String>("fldQnWidgetName") == "gvFamilyDetails"
                               )
                       .CopyToDataTable();

                string _GridViewStateName = tblFilteredch.Rows[0]["fldGridViewStateName"].ToString();
                GridView gv = (GridView)(divmain.FindControl("gvFamilyDetails"));
                TextBox ddlAlreadyTrained = (TextBox)(divmain.FindControl("ddlAlreadyTrained"));
                if (_Value == "2")
                {

                    binddtforTrained();

                    // CreateGrid(gv,dts);
                    gv.Visible = true;
                    gv.DataSource = dts;
                    ViewState[_GridViewStateName] = dts;

                    gv.DataBind();


                }
                else
                {
                    gv.Visible = false;
                    gv.DataSource = null;
                    gv.DataBind();
                    ViewState[_GridViewStateName] = null;
                    ddlAlreadyTrained.Text = "";
                }
            }
            hfkeypress.Value = "0";
            hffocus.Value = ddlId;
            //  ddl.Focus();

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Popup", "stopLoading();", true);

        }





        public void CreateGrid(GridView gv, DataTable dts)
        {
            string controlId = "lbtnSelect_DivId";
            TemplateField tf = new TemplateField();
            tf.HeaderText = "Select";
            Random r = new Random();
            //tf.ItemTemplate = GridViewTemplate(DataControlRowType.DataRow, "", controlId, "LinkButton", r.Next(1, 1000));



            templateType = DataControlRowType.DataRow;
            columnName = "";
            columnNameBinding = controlId;
            controlType = "LinkButton";
            count = r.Next(1, 1000);

            tf.ItemTemplate = this;

            gv.Columns.Add(tf);

            // DataTable dtsource = new DataTable();
            //for (int i = 0; i < dtgrid.Rows.Count; i++)
            //{
            //    BoundField bndfield = new BoundField();
            //    if (Convert.ToString(dtgrid.Rows[i]["fldQnWidgetId"]) == "3" || Convert.ToString(dtgrid.Rows[i]["fldQnWidgetId"]) == "4")
            //    {
            //        bndfield.DataField = Convert.ToString(dtgrid.Rows[i]["fldQnWidgetName"]) + "txt";
            //        dtsource.Columns.Add(Convert.ToString(dtgrid.Rows[i]["fldQnWidgetName"]), typeof(string));
            //    }
            //    else
            //    {
            //        bndfield.DataField = Convert.ToString(dtgrid.Rows[i]["fldQnWidgetName"]);
            //    }
            //    dtsource.Columns.Add(bndfield.DataField, typeof(string));

            //    bndfield.HeaderText = Convert.ToString(dtgrid.Rows[i]["fldQnEngText"]);
            //    gv.Columns.Add(bndfield);

            //}

            //if (ViewState[ViewStateName] == null)
            //{
            //    ViewState[ViewStateName] = dtsource;
            //}

            //Session["GridData"] = null;
            //if (ViewState[ViewStateName] != null)
            //{
            //    dtsource = (DataTable)ViewState[ViewStateName];
            //    Session["GridData"] = dtsource;
            //}


            gv.DataSource = dts; gv.DataBind();



        }
        private void ModifyGridData(int rowindex, string _lctrlName, string _ctrlvalue, string ViewStateName)
        {
            DataTable dtvalue = new DataTable();
            if (ViewState[ViewStateName] != null)
            {
                dtvalue = (DataTable)ViewState[ViewStateName];
            }

            dtvalue.Rows[rowindex][_lctrlName] = _ctrlvalue;
            ViewState[ViewStateName] = dtvalue;
        }
        public bool ChkDataRowExists(DataTable dT, DataRow dr, string ColNameToChkDuplicate)
        {
            //foreach (DataRow item in dT.Rows)
            //{
            //    if (Enumerable.SequenceEqual(item.ItemArray, dr.ItemArray))
            //        return true;
            //}
            //bool exists = dT.Select().ToList().Exists(row => row["Quarter"].ToString().ToUpper() == "Q9");
            //return false;

            //int index = ColNameToChkDuplicate.LastIndexOf(" and ");
            int index = ColNameToChkDuplicate.LastIndexOf(" and ");
            ColNameToChkDuplicate = ColNameToChkDuplicate.Substring(0, index);


            DataView view = new DataView(dT);
            view.RowFilter = ColNameToChkDuplicate;
            DataTable dtResult = view.ToTable();

            if (dtResult.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string BtnId = btn.ID;
            string _Value = btn.Text;
            string _ViewStateName = "";
            DataTable dtgrid = new DataTable();
            if (Session[QuestionMasterSessionName] != null)
            {
                dtgrid = (DataTable)Session[QuestionMasterSessionName];
            }

            DataTable dtdata = dtgrid.AsEnumerable()
             .Where(row => row.Field<String>("fldQnWidgetName") == BtnId
                     )
             .CopyToDataTable();

            string divid = string.Empty;
            string gridviewstatename = string.Empty;
            if (dtdata.Rows.Count > 0)
            {
                divid = Convert.ToString(dtdata.Rows[0]["fldDivID"]);
                gridviewstatename = Convert.ToString(dtdata.Rows[0]["fldGridViewStateName"]);

                // ViewState["GridViewName"] = gridviewstatename+btn.ID;
            }



            DataTable dtdiv = dtgrid.AsEnumerable()
            .Where(row => row.Field<String>("fldDivID") == divid
                    )
            .CopyToDataTable();

            string _lName = string.Empty;
            string _lctrlName = string.Empty;
            string _lctrlType = string.Empty;
            string _lfldIsMandatory = string.Empty; string lvalue = string.Empty;
            string _IsGridDupChk = string.Empty;

            string _GridId = string.Empty; string _GridVSName = string.Empty; string _GridManditory = string.Empty;
            int i = 0;

            DataTable dtvalue = new DataTable();



            if (ViewState[gridviewstatename] != null)
            {
                dtvalue = (DataTable)ViewState[gridviewstatename];
            }

            DataRow drvalue = dtvalue.NewRow();
            //ArrayList arr = new ArrayList();
            // DataColumn[] keys = new DataColumn[3]; int cnt = 0;

            //Dictionary<string, string> data = new Dictionary<string, string>();
            string ColNameToChkDuplicate = string.Empty;
            for (int row = 0; row < dtdiv.Rows.Count; row++)
            {

                _lName = Convert.ToString(dtdiv.Rows[row]["fldQnEngText"]);
                _lctrlName = Convert.ToString(dtdiv.Rows[row]["fldQnWidgetName"]);
                _lctrlType = Convert.ToString(dtdiv.Rows[row]["fldQnWidgetId"]);
                _lfldIsMandatory = Convert.ToString(dtdiv.Rows[row]["fldQnMandatory"]);
                _IsGridDupChk = Convert.ToString(dtdiv.Rows[row]["fldIsGridDupChk"]);
                _ViewStateName = Convert.ToString(dtdata.Rows[0]["fldGridViewStateName"]);

                if (_lctrlType == "8")
                {
                    _GridId = Convert.ToString(dtdiv.Rows[row]["fldQnWidgetName"]);
                    _GridVSName = Convert.ToString(dtdiv.Rows[row]["fldGridViewStateName"]);
                    _GridManditory = Convert.ToString(dtdiv.Rows[row]["fldQnMandatory"]);
                }
                else if (_lctrlType != "7")
                {


                    lvalue = returnValues(_lName, _lctrlName, _lctrlType, _lfldIsMandatory, _ViewStateName);



                    if (lvalue == "" && (_lfldIsMandatory == "1" || _lfldIsMandatory == "True"))
                    {

                        string msg = " Please enter the " + _lName;
                        ShowPopUp(msg, _lName);

                        return;
                    }


                    if (_Value == "Modify")
                    {
                        ModifyGridData(Convert.ToInt32(ViewState["SelGridViewRow"]), _lctrlName, lvalue, gridviewstatename);
                    }
                    else if (_Value == "AddToList")
                    {


                        if (ViewState[gridviewstatename] == null)
                        {
                            dtvalue.Columns.Add(_lctrlName, typeof(string));
                            // DataColumn column = new DataColumn();
                            // column.DataType = System.Type.GetType("System.String");
                            // column.ColumnName = _lctrlName;


                            // dtvalue.Columns.Add(column);
                            // keys[cnt] = column;

                        }

                        //data.Add(_lctrlName, lvalue);
                        if (_IsGridDupChk == "1" || _IsGridDupChk == "True")
                        {
                            //ColNameToChkDuplicate = ColNameToChkDuplicate + _lctrlName + " = " + "'" + lvalue + "' and ";
                            ColNameToChkDuplicate = ColNameToChkDuplicate + _lctrlName + " = " + "'" + lvalue + "' or ";
                        }

                        //arr.Add(lvalue);
                        drvalue[row + i] = lvalue;
                    }

                    if (_lctrlType == "3" || _lctrlType == "4")
                    {
                        string lvalue1 = returnValues(_lName, _lctrlName, _lctrlType + "txt", _lfldIsMandatory, _ViewStateName);
                        i = i + 1;
                        if (_Value == "Modify")
                        {
                            ModifyGridData(Convert.ToInt32(ViewState["SelGridViewRow"]), _lctrlName + "txt", lvalue1, gridviewstatename);
                        }
                        else if (_Value == "AddToList")
                        {
                            if (ViewState[gridviewstatename] == null)
                            {
                                dtvalue.Columns.Add(_lctrlName + "txt", typeof(string));
                            }

                            drvalue[row + i] = lvalue1;
                        }



                    }

                    //cnt++;
                }


            }

            //if (ViewState["DtPrimaryKey"] != null)
            //{
            //    keys = (DataColumn[])ViewState["DtPrimaryKey"];
            //}
            //else if (ViewState["DtPrimaryKey"] == null)
            //{
            //    dtvalue.PrimaryKey = keys;

            //    ViewState["DtPrimaryKey"] = keys;
            //}


            //object[] a = arr.ToArray();





            if (_Value == "AddToList")
            {

                bool IsValid = ChkDataRowExists(dtvalue, drvalue, ColNameToChkDuplicate);
                if (!IsValid)
                {
                    dtvalue.Rows.InsertAt(drvalue, dtvalue.Rows.Count);
                }
                else
                {
                    ClearControls(dtdiv);
                    ShowPopUp("Record Already Exists", btn.ID); return;
                }

            }
            //find grid
            ViewState[gridviewstatename] = dtvalue;


            Dictionary<string, int> d = new Dictionary<string, int>();

            if (_GridManditory == "True" || _GridManditory == "true" || _GridManditory == "1" || _GridManditory.ToUpper() == "TRUE")
            {


                if (ViewState[GridDetailsViewStateName] != null)
                {
                    d = (Dictionary<string, int>)ViewState[GridDetailsViewStateName];

                }


                if (!d.ContainsKey(gridviewstatename))
                {
                    d.Add(gridviewstatename, dtvalue.Rows.Count);

                }
                else if (d.ContainsKey(gridviewstatename))
                {
                    d[gridviewstatename] = dtvalue.Rows.Count;
                }


                ViewState[GridDetailsViewStateName] = d;

                if (d.Count > 0)
                {
                    hfGridDetails.Value = "";
                }


                foreach (KeyValuePair<string, int> entry in d)
                {

                    hfGridDetails.Value = hfGridDetails.Value + entry.Key + "&" + entry.Value + "_";
                }

            }

            //DataTable dtg = dtgrid.AsEnumerable()
            //.Where(row => row.Field<String>("fldQnWidgetId") == "8"
            //        )
            //.CopyToDataTable();


            //if (dtg.Rows.Count > 0)
            //{
            //    _GridId = Convert.ToString(dtg.Rows[0]["fldQnWidgetName"]);
            //}

            if (_GridId != "" || _GridId != null)
            {


                GridView gv = divmain.FindControl(_GridId) as GridView;

                gv.DataSource = dtvalue;
                gv.DataBind();
            }

            ClearControls(dtdiv);

            hfkeypress.Value = "0";
            hffocus.Value = BtnId;
            //  ddl.Focus();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Popup", "stopLoading();", true);

            ////string _Parent = Convert.ToString(tblFiltered.Rows[0]["fldIsQnParent"]);
            ////string _Child = Convert.ToString(tblFiltered.Rows[0]["fldQnWidgetChildName"]);
            ////string _bind = Convert.ToString(tblFiltered.Rows[0]["fldChildBindQuery"]);
            //string _RedirectUrl = Convert.ToString(tblFiltered.Rows[0]["fldRedirectUrl"]);

            //Response.Redirect(_RedirectUrl);

            if (_Value == "Modify")
            {
                btn.Text = "AddToList";
            }



            hfkeypress.Value = "0";
            hffocus.Value = BtnId;
            //  ddl.Focus();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Popup", "stopLoading();", true);


        }

        private void btn_SecClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string BtnId = btn.ID;
            string _Value = btn.Text;

            string nextpageurl = string.Empty;

            nextpageurl = Encrypt(btn.PostBackUrl, Convert.ToString(ViewState["QueryStringVal"]));

            Response.Redirect(nextpageurl);


        }

        private bool ShowPopUpMesage(string msg, string _lctrlName)
        {

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Popup", "ShowPopupContentServer();", true);
            return false;

        }
        private bool ShowPopUp(string msg, string _lctrlName)
        {

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Popup", "ShowPopup('" + msg + "', '" + _lctrlName + "');", true);
            return false;

        }
        private bool ShowPopupContent(string msg, string _lctrlName)
        {

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Popup", "ShowPopupContent('" + msg + "', '" + _lctrlName + "');", true);
            return false;

        }
        private bool ShowPopupSuccess(string msg, string _lctrlName)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Popup", "ShowPopupSuccess('" + msg + "', '" + _lctrlName + "');", true);
            return false;
        }
        private bool ShowPopupSuccessAndRedirect(string msg, string _lctrlName, string _FormName)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Popup", "ShowPopupSuccessAndRedirect('" + msg + "', '" + _lctrlName + "', '" + _FormName + "');", true);
            return false;
        }

        public void bindcontrols(DataTable dt, string _key, string _subformtitle, int IsGridDataExists, Dictionary<string, int> dic)
        {

            HtmlGenericControl divPanel = new HtmlGenericControl("div");
            divPanel.Attributes.Add("class", "panel-body PanelBg");

            HtmlGenericControl divSecTitle = new HtmlGenericControl("div");
            divSecTitle.Attributes.Add("class", "col-md-12 col-sm-12");

            HtmlGenericControl divSecTitle1 = new HtmlGenericControl("div");
            divSecTitle1.Attributes.Add("class", "col-md-12 col-sm-12 ");

            HtmlGenericControl divSecTitle2 = new HtmlGenericControl("div");
            divSecTitle2.Attributes.Add("class", "clearfix");
            divSecTitle2.InnerHtml = "";

            HtmlGenericControl divSecTitle3 = new HtmlGenericControl("div");
            divSecTitle3.Attributes.Add("class", "clearfix");
            divSecTitle3.InnerHtml = "&nbsp;";

            HtmlGenericControl h2 = new HtmlGenericControl("h2");
            h2.Attributes.Add("class", "h2head");
            h2.InnerText = _subformtitle;// "Patient Engagement";

            divSecTitle1.Controls.Add(divSecTitle2);
            divSecTitle1.Controls.Add(h2);
            divSecTitle1.Controls.Add(divSecTitle3);
            divSecTitle.Controls.Add(divSecTitle1);

            HtmlGenericControl divmain1 = new HtmlGenericControl("div");
            //  divmain1.Attributes.Add("class", "col-md-1 col-sm-1");


            HtmlGenericControl divrow = new HtmlGenericControl("div");
            divrow.Attributes.Add("class", "col-md-12 col-sm-12");



            int lc = (Convert.ToInt32(dt.Rows[0]["fldColumnCount"]));
            int result = lc;
            int rowcount = 0;
        next:
            DataRow[] dr = dt.Select("fldSno>" + (lc - result) + "and fldSno<=" + lc);
            DataTable dtnew = dt.Clone();
            foreach (DataRow d in dr)
            {
                dtnew.ImportRow(d);
                lc++;
            }

            rowcount = rowcount + dtnew.Rows.Count;

            HtmlGenericControl subdiv = new HtmlGenericControl("div");
            subdiv.Attributes.Add("class", "row");

            HtmlGenericControl innerdiv = new HtmlGenericControl("div");
            innerdiv.Attributes.Add("class", "row");




            for (int row = 0; row < dtnew.Rows.Count; row++)
            {




                HtmlGenericControl divcol = new HtmlGenericControl("div");

                // initialize lable
                Label lbla = new Label();
                lbla.Attributes.Add("class", "control-label lbl");


                Label lblstar = new Label();
                lblstar.Text = "*";
                lblstar.Attributes.Add("class", "star");

                // initialize textbox

                TextBox txta = new TextBox();
                txta.Attributes.Add("class", "form-control");
                txta.Attributes.Add("autocomplete", "off");


                UpdatePanel upt = new UpdatePanel();

                Button btn = new Button();
                btn.Attributes.Add("class", "form-control");

                // initialize ddl
                DropDownList ddla = new DropDownList();
                ddla.Attributes.Add("class", "form-control");

                RadioButtonList rbl = new RadioButtonList();
                rbl.Attributes.Add("class", "radio-inline");

                ScriptManager sm = new ScriptManager();

                //RadDatePicker dp = new RadDatePicker();

                CheckBoxList cl = new CheckBoxList();
                cl.Attributes.Add("class", "checkbox");

                CascadingDropDown cddl = new CascadingDropDown();

                GridView gv = new GridView();
                gv.Attributes.Add("class", "form-control col-md-11 col-sm-11 col-md-offset-1 col-sm-offset-1");

                HtmlGenericControl div = new HtmlGenericControl("div");
                // HtmlGenericControl divsub = new HtmlGenericControl("div");
                div.Attributes.Add("class", "form-group");
                //div.ID = _key + Convert.ToString(row);

                string _chk = string.Empty;
                string _id = string.Empty;
                string _bind = string.Empty;
                string _maxLength = string.Empty; string _minLength = string.Empty;
                // string _isDepend = "";
                bool _isDepend = true;
                string _retType = string.Empty;
                bool _enabled = true;
                bool _mandatry = true;
                bool _autoPostback = false;
                string _Name = string.Empty;
                string _class = string.Empty;
                string _AllowedContentLength = string.Empty;
                string _IsParent = string.Empty;
                string _Category = string.Empty;
                string _ParentControlID = string.Empty;
                string _ServiceMethod = string.Empty;

                string _IsCrossValidation = string.Empty;
                string _Operation = string.Empty;
                string _Value = string.Empty;
                string _ColToEnableOrDisable = string.Empty;
                string _ColType = string.Empty;
                string _IsDuplicateChk = string.Empty;
                string _QryToChkDuplicate = string.Empty;
                string _ColToChkForDuplicate = string.Empty;
                string _DbColToChkForDuplicate = string.Empty;
                string _Qid = string.Empty;
                string _WebQuery = string.Empty;
                string _DivId = string.Empty;
                string _MaxValue = string.Empty;
                string _MinValue = string.Empty;
                string _IsGridDupChk = string.Empty;
                string _GridViewStateName = string.Empty;
                string _IsAutoBindOrInputQn = string.Empty;
                bool _IsReplaceWebBindQuery = false;

                _id = Convert.ToString(dtnew.Rows[row]["fldQnWidgetName"]);
                _chk = Convert.ToString(dtnew.Rows[row]["fldQnWidgetId"]);
                _bind = Convert.ToString(dtnew.Rows[row]["fldWebBindQuery"]);
                _maxLength = Convert.ToString(dtnew.Rows[row]["fldMaxLength"]);
                _minLength = Convert.ToString(dtnew.Rows[row]["fldMinLength"]);
                // _isDepend = dtnew.Rows[row]["fldIsQnChild"].ToString();
                _retType = Convert.ToString(dtnew.Rows[row]["fldQnReturnType"]);

                _Name = Convert.ToString(dtnew.Rows[row]["fldQnEngText"]);
                _class = Convert.ToString(dtnew.Rows[row]["fldCssClass"]);

                _AllowedContentLength = Convert.ToString(dtnew.Rows[row]["fldAllowedContentLength"]);

                _IsParent = Convert.ToString(dtnew.Rows[row]["fldIsParent"]);
                _Category = Convert.ToString(dtnew.Rows[row]["fldCategory"]);
                _ParentControlID = Convert.ToString(dtnew.Rows[row]["fldParentControlID"]);
                _ServiceMethod = Convert.ToString(dtnew.Rows[row]["fldServiceMethod"]);


                _IsCrossValidation = Convert.ToString(dtnew.Rows[row]["fldIsCrossValidation"]);
                _Operation = Convert.ToString(dtnew.Rows[row]["fldOperation"]);
                _Value = Convert.ToString(dtnew.Rows[row]["fldValue"]);
                _ColToEnableOrDisable = Convert.ToString(dtnew.Rows[row]["fldColToEnableOrDisable"]);
                _ColType = Convert.ToString(dtnew.Rows[row]["fldColType"]);
                _IsDuplicateChk = Convert.ToString(dtnew.Rows[row]["fldIsDuplicateChk"]);
                _QryToChkDuplicate = Convert.ToString(dtnew.Rows[row]["fldQryToChkDuplicate"]);
                _ColToChkForDuplicate = Convert.ToString(dtnew.Rows[row]["fldColToChkForDuplicate"]);
                _DbColToChkForDuplicate = Convert.ToString(dtnew.Rows[row]["fldDbColToChkForDuplicate"]);
                _Qid = Convert.ToString(dtnew.Rows[row]["fldQnId"]);
                _WebQuery = Convert.ToString(dtnew.Rows[row]["fldWebBindQuery"]);
                _DivId = Convert.ToString(dtnew.Rows[row]["fldDivID"]);
                _MaxValue = Convert.ToString(dtnew.Rows[row]["fldMaxValue"]);
                _MinValue = Convert.ToString(dtnew.Rows[row]["fldMinValue"]);
                _IsGridDupChk = Convert.ToString(dtnew.Rows[row]["fldIsGridDupChk"]);
                _GridViewStateName = Convert.ToString(dtnew.Rows[row]["fldGridViewStateName"]);
                _IsAutoBindOrInputQn = Convert.ToString(dtnew.Rows[row]["fldIsAutoBindOrInputQn"]);
                _IsReplaceWebBindQuery = Convert.ToBoolean(dtnew.Rows[row]["fldIsReplaceWebBindQuery"]);

                if (Convert.ToString(dtnew.Rows[row]["fldQnEnableDisableOnLoad"]) != "")
                {
                    _enabled = Convert.ToBoolean(Convert.ToString(dtnew.Rows[row]["fldQnEnableDisableOnLoad"]));
                }
                if (Convert.ToString(dtnew.Rows[row]["fldQnMandatory"]) != "")
                {
                    _mandatry = Convert.ToBoolean(Convert.ToString(dtnew.Rows[row]["fldQnMandatory"]));
                }

                if (Convert.ToString(dtnew.Rows[row]["fldIsQnChild"]) != "")
                {
                    _isDepend = Convert.ToBoolean(Convert.ToString(dtnew.Rows[row]["fldIsQnChild"]));
                }

                if (Convert.ToString(dtnew.Rows[row]["fldAutopostback"]) != "")
                {
                    _autoPostback = Convert.ToBoolean(Convert.ToString(dtnew.Rows[row]["fldAutopostback"]));
                }

                //int subdivcount = 2;
                //int res = (row + 1) % (subdivcount);


                switch (_chk)
                {

                    //-------------------for textbox-----------------------//

                    case "1":
                        divcol.Attributes.Add("class", _class);

                        lbla.Text = _Name;
                        txta.Enabled = _enabled;
                        txta.ID = _id;
                        txta.Attributes.Add("placeholder", _AllowedContentLength);

                        if (_maxLength != "" && _maxLength != null)
                        {
                            txta.MaxLength = Convert.ToInt16(_maxLength);
                        }


                        string Evt = "return OnKeyPress(this,event," + "'" + _retType + "')";

                        txta.Attributes.Add("onkeypress", Evt);

                        //Evt = "OnChange(this,event," + "'" + _retType + "','" + _minLength + "','" + _MinValue + "','" + _Name + "','" + _IsDuplicateChk + "','" + _QryToChkDuplicate + "','" + _ColToChkForDuplicate + "','" + _DbColToChkForDuplicate + "','" + _chk + "'" + ")";


                        Evt = "OnChange(this,event," + "'" + _retType + "','" + _minLength + "','" + _MinValue + "','" + _MaxValue + "','" + _Name + "','" + _IsDuplicateChk + "','" + _QryToChkDuplicate + "','" + _ColToChkForDuplicate + "','" + _DbColToChkForDuplicate + "','" + _chk + "','" + "','" + _IsCrossValidation + "','" + _ColToEnableOrDisable + "','" + _Value + "','" + _ColType + "','" + _Operation + "','" + _autoPostback + "','" + _Qid + "'" + ")";




                        txta.Attributes.Add("onchange", Evt);


                        //if (_retType == "Number")
                        //{
                        //    txta.Attributes.Add("onkeypress", "return isNumberKey(this,event)");
                        //}
                        //if (_retType == "Email")
                        //{
                        //    txta.Attributes.Add("onchange", "return validateEmailId(this,event)");
                        //}
                        //if (_retType == "Mobile")
                        //{
                        //    txta.Attributes.Add("onkeypress", "return isNumberKey(this,event)");
                        //    txta.Attributes.Add("onchange", "return validateMobileNo(this,event)");
                        //}
                        //if (_retType == "Phone")
                        //{
                        //    txta.Attributes.Add("onkeypress", "return isNumberKey(this,event)");
                        //    txta.Attributes.Add("onchange", "return validatePhoneno(this,event)");
                        //}
                        //if (_retType == "Age")
                        //{
                        //    txta.Attributes.Add("onkeypress", "return isNumberKey(this,event)");
                        //    txta.Attributes.Add("onchange", "return validateAge(this,event)");
                        //}

                        //if (_retType == "Alphanumeric")
                        //{
                        //    txta.Attributes.Add("onkeypress", "return txtAlphanumeric(this,event)");

                        //}

                        //if (_retType == "Pincode")
                        //{
                        //    txta.Attributes.Add("onkeypress", "return isNumberKey(this,event)");
                        //    txta.Attributes.Add("onchange", "return validatePincode(this,event)");
                        //}




                        div.Attributes.Add("Class", "form-group");

                        if (_mandatry == true)
                        {
                            div.Controls.Add(lblstar);
                        }

                        if (_autoPostback == true)
                        {
                            txta.TextChanged += new EventHandler(Txt_ChangeEvent);
                            txta.AutoPostBack = true;
                        }

                        if (_id == "ddlAlreadyTrained")
                        {

                            txta.ReadOnly = true;

                        }

                        //if (_IsDuplicateChk == "True")
                        //{
                        //    string qry = "DuplicateChk(this,event," + "'" + _QryToChkDuplicate + "','" + _ColToChkForDuplicate + "','" + _DbColToChkForDuplicate + "'" + ")";
                        //    txta.Attributes.Add("onchange", qry);
                        //}

                        // string Evt1 = "return ChkLength(this,event," + "'" + _Name + "'," + _minLength + ")";

                        // txta.Attributes.Add("onchange", Evt1);


                        div.Controls.Add(lbla);
                        div.Controls.Add(txta);
                        divcol.Controls.Add(div);


                        break;


                    //-------------------for auto control textbox-----------------------//

                    case "2":
                        lbla.Text = _Name;
                        divcol.Attributes.Add("class", _class);
                        // divcol.Attributes.Add("style", "margin-left: -24.99%");
                        txta.ID = _id;
                        if (_autoPostback == true)
                        {
                            txta.TextChanged += new EventHandler(Txt_ChangeEvent);
                            txta.AutoPostBack = true;
                        }
                        txta.Enabled = _enabled;
                        //if (_retType == "Text")
                        //{
                        //    txta.Attributes.Add("onkeypress", "return isCharKey(this,event)");
                        //}


                        // string EvtAuto = "return OnKeyPress(this,event," + "'" + _retType + "')";

                        //  txta.Attributes.Add("onkeypress", EvtAuto);

                        // EvtAuto = "return OnChange(this,event," + "'" + _retType + "','" + _minLength + "','" + _Name + "','" + _IsDuplicateChk + "','" + _QryToChkDuplicate + "','" + _ColToChkForDuplicate + "','" + _DbColToChkForDuplicate + "','" + _chk + "'" + ")";

                        // txta.Attributes.Add("onchange", EvtAuto);


                        txta.Attributes.Add("autocomplete", "off");
                        txta.Attributes.Add("placeholder", _AllowedContentLength);

                        AjaxControlToolkit.AutoCompleteExtender autoCompleteExtender = new AjaxControlToolkit.AutoCompleteExtender();
                        autoCompleteExtender.ID = "ACE" + _id;
                        autoCompleteExtender.TargetControlID = txta.ID;
                        autoCompleteExtender.ServiceMethod = "GetuserId";

                        autoCompleteExtender.MinimumPrefixLength = 1;
                        autoCompleteExtender.ShowOnlyCurrentWordInCompletionListItem = true;

                        autoCompleteExtender.EnableCaching = true;
                        if (_mandatry == true)
                        {
                            div.Controls.Add(lblstar);
                        }

                        //if (_IsDuplicateChk == "True")
                        //{
                        //    string qry = "DuplicateChk(this,event," + "'" + _QryToChkDuplicate + "','" + _ColToChkForDuplicate + "','" + _DbColToChkForDuplicate + "'" + ")";
                        //    txta.Attributes.Add("onchange", qry);
                        //}

                        div.Controls.Add(lbla);
                        div.Controls.Add(txta);
                        div.Controls.Add(autoCompleteExtender);

                        divcol.Controls.Add(div);
                        break;

                    //-------------------for dropdown-----------------------//

                    case "3":
                        divcol.Attributes.Add("class", _class);

                        lbla.Text = _Name;
                        ddla.ID = _id;
                        ddla.Attributes.Add("runat", "server");
                        ddla.Enabled = _enabled;
                        if (_mandatry == true)
                        {
                            div.Controls.Add(lblstar);
                        }
                        div.Controls.Add(lbla);
                        ddla.AutoPostBack = _autoPostback;

                        div.Controls.Add(ddla);
                        divcol.Controls.Add(div);

                        //string s = "validateDropdown(this,event," + "'" + _IsCrossValidation + "','" + _ColToEnableOrDisable + "','" + _Value + "','" + _ColType + "','" + _Operation + "','" + _autoPostback + "','" + _Qid + "','"+ _chk+"'" + ")";
                        //ddla.Attributes.Add("onchange", s);

                        string EvtDdl = "DdlOnChange(this,event," + "'" + _IsCrossValidation + "','" + _ColToEnableOrDisable + "','" + _Value + "','" + _ColType + "','" + _Operation + "','" + _autoPostback + "','" + _Qid + "','" + _chk + "','" + _IsDuplicateChk + "','" + _QryToChkDuplicate + "','" + _ColToChkForDuplicate + "','" + _DbColToChkForDuplicate + "'" + ")";

                        ddla.Attributes.Add("onchange", EvtDdl);

                        if (_isDepend == true)
                        {

                            if (_id == "ddlTuName")
                            {
                                obal.bindddlDependent(_id, ddla, _bind, cityId);
                            }



                        }
                        else
                        {

                            obal.bindddl(_id, ddla, _bind);
                        }

                        if (_autoPostback == true)
                        {
                            ddla.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);
                            ddla.AutoPostBack = true;
                        }

                        //if (_id == "ddlPartTrained")
                        //{

                        //    DropDownList ddlPartTrained = (DropDownList)(divmain).FindControl("ddlPartTrained");
                        //    _Value = "";
                        //    if (_Value=="2" || _Value=="")
                        //    {
                        //        TextBox ddlAlreadyTrained = (TextBox)(divmain).FindControl("ddlAlreadyTrained");
                        //        ddlAlreadyTrained.ReadOnly = true;
                        //    }
                        //}

                        //if (_IsDuplicateChk == "True")
                        //{
                        //    string qry = "DuplicateChk(this,event," + "'" + _QryToChkDuplicate + "','" + _ColToChkForDuplicate + "','" + _DbColToChkForDuplicate + "'" + ")";
                        //    ddla.Attributes.Add("onchange", qry);
                        //}



                        break;

                    //-------------------for checklistbox-----------------------//

                    case "4":

                        HtmlGenericControl divcl = new HtmlGenericControl("div");
                        divcl.Attributes.Add("class", "form-control");

                        divcol.Attributes.Add("class", "col-md-5 col-sm-5 col-md-offset-1 col-sm-offset-1");

                        HtmlGenericControl divchkListBdy = new HtmlGenericControl("div");
                        divchkListBdy.Attributes.Add("class", "panel-chk");


                        lbla.Text = _Name;

                        cl.Attributes.Add("runat", "server");

                        string EvtClb = "ClbOnChange(this,event," + "'" + _IsCrossValidation + "','" + _ColToEnableOrDisable + "','" + _Value + "','" + _ColType + "','" + _Operation + "','" + _autoPostback + "','" + _Qid + "','" + _chk + "','" + _IsDuplicateChk + "','" + _QryToChkDuplicate + "','" + _ColToChkForDuplicate + "','" + _DbColToChkForDuplicate + "'" + ")";

                        cl.Attributes.Add("onchange", EvtClb);

                        cl.ID = _id;
                        cl.Enabled = _enabled;
                        cl.Attributes.Add("class", "checkbox-inline big-checkbox");

                        if (_mandatry == true)
                        {
                            div.Controls.Add(lblstar);
                        }
                        div.Controls.Add(lbla);


                        divchkListBdy.Controls.Add(cl);
                        div.Controls.Add(divchkListBdy);

                        divcol.Controls.Add(div);

                        obal.bindcl(_id, cl, _bind);

                        if (_autoPostback == true)
                        {
                            cl.SelectedIndexChanged += new EventHandler(cl_SelectedIndexChanged);
                            cl.AutoPostBack = true;
                        }


                        break;


                    //-------------------for date-----------------------//

                    case "5":
                        divcol.Attributes.Add("class", _class);

                        lbla.Text = _Name;
                        txta.ID = _id;

                        //string EvtDate = "DateOnChange(this,event"+",'"+_chk+"'"+")";
                        //txta.Attributes.Add("onchange", EvtDate);

                        string EvtDate = "DateOnChange(this,event," + "'" + _IsCrossValidation + "','" + _ColToEnableOrDisable + "','" + _Value + "','" + _ColType + "','" + _Operation + "','" + _autoPostback + "','" + _Qid + "','" + _chk + "','" + _IsDuplicateChk + "','" + _QryToChkDuplicate + "','" + _ColToChkForDuplicate + "','" + _DbColToChkForDuplicate + "'" + ")";

                        txta.Attributes.Add("onchange", EvtDate);

                        //txta.Attributes.Add("onchange", "return isDate(this,event)");

                        if (_mandatry == true)
                        {
                            div.Controls.Add(lblstar);
                        }

                        AjaxControlToolkit.CalendarExtender calExender = new AjaxControlToolkit.CalendarExtender();
                        calExender.ID = "calenderDate_" + txta.ID;
                        calExender.TargetControlID = _id;
                        txta.Attributes.Add("ReadOnly", "true");
                        txta.Attributes.Add("runat", "server");
                        txta.Attributes.Add("placeholder", _AllowedContentLength);
                        calExender.Format = _AllowedContentLength;

                        _MinValue = obal.getdateMin(_bind);

                        // if (_MaxValue != null && _MaxValue != "" && _MaxValue.Trim() != "")
                        {
                            calExender.EndDate = DateTime.Now;
                        }
                        if (_MinValue != null && _MinValue != "" && _MinValue.Trim() != "")
                        {
                            // calExender.StartDate = Convert.ToDateTime(_MinValue);
                            calExender.StartDate = DateTime.Parse(_MinValue, CultureInfo.CurrentCulture);
                        }

                        if (_autoPostback == true)
                        {
                            txta.AutoPostBack = true;
                            txta.TextChanged += new EventHandler(Cal_SelectedIndexChanged);
                        }

                        txta.Enabled = _enabled;
                        div.Controls.Add(lbla);
                        txta.BackColor = System.Drawing.Color.White;
                        div.Controls.Add(txta);
                        calExender.CssClass = "cal_Theme1";
                        div.Controls.Add(calExender);
                        divcol.Controls.Add(div);


                        //if (_IsDuplicateChk == "True")
                        //{
                        //    string qry = "DuplicateChk(this,event," + "'" + _QryToChkDuplicate + "','" + _ColToChkForDuplicate + "','" + _DbColToChkForDuplicate + "'" + ")";
                        //    txta.Attributes.Add("onchange", qry);
                        //}

                        break;

                    // for cascading dropdown 

                    case "6":

                        divcol.Attributes.Add("class", _class);

                        lbla.Text = _Name;
                        ddla.ID = _id;
                        cddl.ID = "cddl" + _id;
                        cddl.TargetControlID = _id;
                        cddl.ServiceMethod = _ServiceMethod;
                        //cddl.ServicePath = "~/frmWebService.asmx";
                        cddl.EmptyText = "--Select--";
                        if (_IsParent == "False")
                        {
                            cddl.ParentControlID = _ParentControlID;
                        }
                        cddl.Category = _Category;
                        cddl.EmptyText = "No Data";
                        ddla.Attributes.Add("runat", "server");
                        ddla.Enabled = _enabled;
                        if (_mandatry == true)
                        {
                            div.Controls.Add(lblstar);
                        }
                        div.Controls.Add(lbla);
                        ddla.AutoPostBack = _autoPostback;

                        div.Controls.Add(ddla);
                        divcol.Controls.Add(div);

                        string EvtCDdl = "DdlOnChange(this,event," + "'" + _IsCrossValidation + "','" + _ColToEnableOrDisable + "','" + _Value + "','" + _ColType + "','" + _Operation + "','" + _autoPostback + "','" + _Qid + "','" + _chk + "','" + _IsDuplicateChk + "','" + _QryToChkDuplicate + "','" + _ColToChkForDuplicate + "','" + _DbColToChkForDuplicate + "'" + ")";

                        ddla.Attributes.Add("onchange", EvtCDdl);

                        //ddla.Attributes.Add("onchange", "validateDropdown(this,event)");

                        if (_isDepend == true)
                        {

                            if (_id == "ddlTBUnit")
                            {
                                // obal.bindddlDependent(_id, ddla, _bind, _userid);
                            }



                        }
                        else
                        {
                            if (_IsParent != "False")
                            {
                                //obal.bindddl(_id, ddla, _bind);
                            }
                        }

                        if (_autoPostback == true)
                        {
                            ddla.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);
                            ddla.AutoPostBack = true;
                        }

                        //if (_IsDuplicateChk == "True")
                        //{
                        //    string qry = "DuplicateChk(this,event," + "'" + _QryToChkDuplicate + "','" + _ColToChkForDuplicate + "','" + _DbColToChkForDuplicate + "'" + ")";
                        //    ddla.Attributes.Add("onchange", qry);
                        //}

                        break;


                    case "7":
                        //-------------------for Button-----------------------//
                        HtmlGenericControl divbtn = new HtmlGenericControl("div");

                        divcol.Attributes.Add("class", _class);
                        divbtn.Attributes.Add("class", "col-md-5 col-sm-5 col-md-offset-4 col-sm-offset-4");

                        lbla.Text = _Name;
                        btn.Text = _Name;
                        btn.ID = _id;
                        btn.Attributes.Add("runat", "server");
                        btn.Attributes.Add("class", "btn btnCLass btn-large btn-warning");
                        btn.Enabled = _enabled;
                        btn.Click += new EventHandler(btn_Click);

                        string method = string.Empty;
                        method = "return Validation(" + _DivId + ");";

                        btn.Attributes.Add("onclick", method);

                        if (_mandatry == true)
                        {
                            div.Controls.Add(lblstar);
                        }

                        //div.Controls.Add(lbla);

                        div.Controls.Add(btn);
                        divbtn.Controls.Add(div);
                        divcol.Controls.Add(divbtn);
                        // btn.Attributes.Add("onchange", "validateDropdown(this,event)");

                        break;


                    case "8":

                        //for gridview
                        Panel pnl = new Panel();
                        pnl.ID = "pnl" + _id;
                        pnl.Attributes.Add("runat", "server");
                        pnl.ScrollBars = ScrollBars.Both;
                        //pnl.Height = 450;


                        divcol.Attributes.Add("class", _class);


                        lbla.Text = _Name;

                        gv.ID = _id;
                        gv.Attributes.Add("runat", "server");

                        gv.Enabled = _enabled;

                        gv.AutoGenerateColumns = false;
                        gv.CssClass = "table table-bordered";

                        gv.ShowHeaderWhenEmpty = true;
                        // string[] str = { "fldSlNo" };

                        // gv.DataKeyNames = str;

                        gv.PagerSettings.FirstPageText = "First";
                        gv.PagerSettings.LastPageText = "Last";
                        gv.PageIndexChanging += new GridViewPageEventHandler(this.gv_PageIndexChanging);
                        //gv.PageIndexChanged += new EventHandler(this.gv_PageIndexChanged);
                        gv.RowDataBound += new GridViewRowEventHandler(this.gv_RowDataBound);

                        DataTable dtgrid = new DataTable();
                        //  if (_WebQuery != "" && _WebQuery != null)
                        {
                            // dtgrid = obal.DtGrid(_WebQuery);
                            dtgrid = obal.DtGrid(_subformcode, _DivId, QuestionMasterTblName);

                        }

                        //gv.PageSize = 5;
                        //gv.AllowPaging = true;
                        //gv.PagerSettings.Visible = true;
                        //gv.PagerSettings.Position = PagerPosition.Bottom;
                        //gv.PagerSettings.Mode = PagerButtons.Numeric;
                        //gv.PagerSettings.PageButtonCount = 10;
                        //gv.PagerSettings.NextPageText = "&gt;";
                        //gv.PagerSettings.PreviousPageText = "&lt;";
                        //gv.PageIndex = 0;

                        CreateGrid(gv, dtgrid, _DivId, _GridViewStateName);



                        if (_mandatry == true)
                        {
                            div.Controls.Add(lblstar);
                        }

                        //div.Controls.Add(lbla);

                        pnl.Controls.Add(gv);
                        div.Controls.Add(pnl);

                        divcol.Controls.Add(div);

                        if (IsGridDataExists == 0 && _mandatry == true)
                        {
                            dic.Add(_GridViewStateName, 0);
                        }

                        break;

                    case "9":

                        divcol.Attributes.Add("class", _class);

                        HtmlGenericControl divrbltBdy = new HtmlGenericControl("div");
                        divrbltBdy.Attributes.Add("class", "panel-chk");

                        lbla.Text = _Name;
                        rbl.ID = _id;
                        rbl.Attributes.Add("runat", "server");
                        rbl.Enabled = _enabled;
                        if (_mandatry == true)
                        {
                            div.Controls.Add(lblstar);
                        }
                        div.Controls.Add(lbla);
                        ddla.AutoPostBack = _autoPostback;

                        divrbltBdy.Controls.Add(rbl);
                        div.Controls.Add(divrbltBdy);
                        divcol.Controls.Add(div);

                        //string s = "validateDropdown(this,event," + "'" + _IsCrossValidation + "','" + _ColToEnableOrDisable + "','" + _Value + "','" + _ColType + "','" + _Operation + "','" + _autoPostback + "','" + _Qid + "','"+ _chk+"'" + ")";
                        //ddla.Attributes.Add("onchange", s);

                        string EvtRbt = "DdlOnChange(this,event," + "'" + _IsCrossValidation + "','" + _ColToEnableOrDisable + "','" + _Value + "','" + _ColType + "','" + _Operation + "','" + _autoPostback + "','" + _Qid + "','" + _chk + "','" + _IsDuplicateChk + "','" + _QryToChkDuplicate + "','" + _ColToChkForDuplicate + "','" + _DbColToChkForDuplicate + "'" + ")";

                        rbl.Attributes.Add("onchange", EvtRbt);

                        if (_isDepend == true)
                        {

                            if (_id == "ddlTuName")
                            {
                                obal.bindddlDependent(_id, ddla, _bind, cityId);
                            }



                        }
                        else
                        {

                            obal.bindrbl(_id, rbl, _bind);
                        }

                        if (_autoPostback == true)
                        {
                            rbl.SelectedIndexChanged += new EventHandler(rbl_SelectedIndexChanged);
                            rbl.AutoPostBack = true;
                        }


                        break;

                    case "10":

                        //for gridview
                        Panel pnla = new Panel();
                        pnla.ID = "pnl" + _id;
                        pnla.Attributes.Add("runat", "server");
                        pnla.ScrollBars = ScrollBars.Both;
                        //pnl.Height = 450;


                        divcol.Attributes.Add("class", _class);


                        lbla.Text = _Name;

                        gv.ID = _id;
                        gv.Attributes.Add("runat", "server");

                        gv.Enabled = _enabled;

                        gv.AutoGenerateColumns = true;
                        gv.CssClass = "table table-bordered";

                        gv.ShowHeaderWhenEmpty = false;
                        // string[] str = { "fldSlNo" };

                        // gv.DataKeyNames = str;

                        gv.PagerSettings.FirstPageText = "First";
                        gv.PagerSettings.LastPageText = "Last";
                        gv.PageIndexChanging += new GridViewPageEventHandler(this.gv_PageIndexChanging);
                        //gv.PageIndexChanged += new EventHandler(this.gv_PageIndexChanged);
                        gv.RowDataBound += new GridViewRowEventHandler(this.gv_RowDataBound);

                        //  DataTable dtgrid = new DataTable();

                        dtgrid = obal.getGridBind(_bind, Convert.ToString(ViewState["QueryStringVal"]));

                        if (ViewState[_GridViewStateName] == null)
                        {
                            // ViewState[_GridViewStateName] = dtsource;
                            CreateGrid(gv, dtgrid);
                        }

                        Session["GridData"] = null;
                        if (ViewState[_GridViewStateName] != null)
                        {
                            dtgrid = (DataTable)ViewState[_GridViewStateName];
                            Session["GridData"] = dtgrid;
                            gv.DataSource = dtgrid;

                            CreateGrid(gv, dtgrid);
                        }



                        // gv.DataSource = dtgrid;
                        //  gv.DataBind();

                        //  if (_WebQuery != "" && _WebQuery != null)
                        //{
                        //    // dtgrid = obal.DtGrid(_WebQuery);
                        //    dtgrid = obal.DtGrid(_subformcode, _DivId, QuestionMasterTblName);

                        //}

                        //gv.PageSize = 5;
                        //gv.AllowPaging = true;
                        //gv.PagerSettings.Visible = true;
                        //gv.PagerSettings.Position = PagerPosition.Bottom;
                        //gv.PagerSettings.Mode = PagerButtons.Numeric;
                        //gv.PagerSettings.PageButtonCount = 10;
                        //gv.PagerSettings.NextPageText = "&gt;";
                        //gv.PagerSettings.PreviousPageText = "&lt;";
                        //gv.PageIndex = 0;

                        //CreateGrid(gv, dtgrid, _DivId, _GridViewStateName);



                        if (_mandatry == true)
                        {
                            div.Controls.Add(lblstar);
                        }

                        //div.Controls.Add(lbla);

                        pnla.Controls.Add(gv);
                        div.Controls.Add(pnla);

                        divcol.Controls.Add(div);

                        if (IsGridDataExists == 0 && _mandatry == true)
                        {
                            dic.Add(_GridViewStateName, 0);
                        }

                        break;


                }
                //if (res == 1)
                //{
                //    HtmlGenericControl subdiv = new HtmlGenericControl("div");
                //    subdiv.ID = "subdiv" + Convert.ToString(res) + Convert.ToString(subdivcount);
                //    subdiv.Attributes.Add("runat", "server");
                //    Session["DivId"] = subdiv.ID;
                //    subdiv.Controls.Add(divcol);
                //    divrow.Controls.Add(subdiv);
                //}
                //else
                //{
                //    string id = Convert.ToString(Session["DivId"]);
                //    HtmlGenericControl subdiv = (HtmlGenericControl)(divmain1.FindControl(id));

                //    subdiv.Controls.Add(divcol);
                //    divrow.Controls.Add(subdiv);

                //}

                innerdiv.Controls.Add(divcol);



            }

            //subdiv.Controls.Add(innerdiv);
            divrow.Controls.Add(innerdiv);
            divPanel.Controls.Add(divSecTitle);
            divPanel.Controls.Add(divmain1);
            divPanel.Controls.Add(divrow);



            if (rowcount >= dt.Rows.Count) goto step;
            goto next;
        //to check

            step:
            if (_key == "1")
            {
                divmain.Controls.Add(divPanel);
            }
            else
            {
                HtmlGenericControl Extradiv = new HtmlGenericControl("div");
                Extradiv.Attributes.Add("class", "clearfix");

                Extradiv.InnerHtml = "</br>";

                HtmlGenericControl div = new HtmlGenericControl("div");
                div.Attributes.Add("class", "panel panel-success");

                HtmlGenericControl divheading = new HtmlGenericControl("div");
                divheading.Attributes.Add("class", "panel-heading");
                divheading.ID = "divheading" + _key;

                div.ID = "div" + _key;
                divheading.Controls.Add(div);
                div.Controls.Add(divPanel);
                fscontent.Controls.Add(Extradiv);
                fscontent.Controls.Add(div);

            }

            if (IsGridDataExists == 0)
            {
                ViewState[GridDetailsViewStateName] = dic;

                if (dic.Count > 0)
                {
                    hfGridDetails.Value = "";
                }


                foreach (KeyValuePair<string, int> entry in dic)
                {
                    hfGridDetails.Value = hfGridDetails.Value + entry.Key + "&" + entry.Value + "_";
                }
            }

        }

        private void rbl_SelectedIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void Cal_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox cal = (TextBox)sender;
            string calId = cal.ID;
            string _Value = cal.Text;


            DataTable tblFiltered = dt.AsEnumerable()
                        .Where(row => row.Field<String>("fldQnWidgetName") == calId)
                        .CopyToDataTable();

            //Array arrChildType = new Array[0];

            string _Parent = tblFiltered.Rows[0]["fldIsQnParent"].ToString();

            if (_Parent == "True")
            {

                string _Child = tblFiltered.Rows[0]["fldQnWidgetChildName"].ToString();

                TextBox txtChild = (TextBox)(divmain.FindControl(_Child.ToString()));
                txtChild.Text = "";
                txtChild.Enabled = true;

                AjaxControlToolkit.CalendarExtender calExender = (AjaxControlToolkit.CalendarExtender)(divmain.FindControl("calenderDate_" + _Child.ToString()));

                if (Request.Form[cal.UniqueID] != null && Request.Form[cal.UniqueID] != "")
                {
                    DateTime strtDate = DateTime.ParseExact(Request.Form[cal.UniqueID], "MM-yyyy", CultureInfo.InvariantCulture);
                    // calExender.StartDate = strtDate;

                    DateTime endDate = DateTime.ParseExact(Request.Form[cal.UniqueID], "MM-yyyy", CultureInfo.InvariantCulture).AddMonths(1).AddDays(-1);
                    // calExender.EndDate = endDate;

                    if (endDate > DateTime.Now)
                    {
                        //  calExender.EndDate = DateTime.Now;
                    }

                }
            }
        }
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void gv_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

        }
        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {


        }
        public void ClearControls(DataTable dt)
        {
            string _lctrlType = "";
            string _lctrlName = "";
            string _IsAutobind = "";


            for (int row = 0; row < dt.Rows.Count; row++)
            {
                _lctrlName = dt.Rows[row]["fldQnWidgetName"].ToString();
                _lctrlType = dt.Rows[row]["fldQnWidgetId"].ToString();
                _IsAutobind = dt.Rows[row]["fldIsAutoBindOrInputQn"].ToString();

                if (_IsAutobind != "1")
                {
                    switch (_lctrlType)
                    {
                        case "1":
                            TextBox txt = (TextBox)(divmain.FindControl(_lctrlName.ToString()));
                            txt.Text = "";
                            //txt.Focus();
                            break;
                        case "3":
                            DropDownList ddl = (DropDownList)(divmain.FindControl(_lctrlName.ToString()));
                            ddl.SelectedIndex = 0;
                            break;

                        case "4":

                            CheckBoxList cl = (CheckBoxList)(divmain.FindControl(_lctrlName.ToString()));
                            foreach (ListItem item in cl.Items)
                            {
                                if (item.Selected)
                                    item.Selected = false;
                            }
                            break;
                        case "2":
                            TextBox txta = (TextBox)(divmain.FindControl(_lctrlName.ToString()));
                            txta.Text = "";
                            break;

                        case "5":
                            TextBox rad = (TextBox)divmain.FindControl(_lctrlName.ToString());
                            rad.Text = "";
                            break;
                    }
                }

            }


        }
        private string returnValues(string _Name, string _ctrlName, string _ctrlType, string fldIsMandatory, string _ViewStateName)
        {
            string _value = "";


            switch (_ctrlType)
            {
                case "1":
                    TextBox txt = (TextBox)(divmain.FindControl(_ctrlName.ToString()));
                    _value = txt.Text;
                    break;

                case "2":
                    TextBox txta = (TextBox)(divmain.FindControl(_ctrlName.ToString()));
                    _value = txta.Text;

                    txta.Focus();

                    break;

                case "3":
                    DropDownList ddl = (DropDownList)(divmain.FindControl(_ctrlName.ToString()));

                    _value = ddl.SelectedValue;

                    if (_value == "--Select--")
                    {
                        _value = "";
                    }

                    break;

                case "3txt":
                    DropDownList ddltxt = (DropDownList)(divmain.FindControl(_ctrlName.ToString()));

                    if (ddltxt.SelectedItem != null)
                    {


                        _value = ddltxt.SelectedItem.ToString();

                        if (_value == "--Select--")
                        {
                            _value = "";
                        }
                    }
                    break;

                case "4":

                    CheckBoxList cl = (CheckBoxList)(divmain.FindControl(_ctrlName.ToString()));

                    string Message = "";
                    for (int i = 0; i < cl.Items.Count; i++)
                    {
                        if (cl.Items[i].Selected)
                        {
                            if (Message == "")
                            {

                                Message += cl.Items[i].Value;

                            }
                            else
                            {
                                Message = Message + "," + cl.Items[i].Value;
                            }
                        }
                    }

                    _value = Message;

                    break;

                case "4txt":

                    CheckBoxList cltxt = (CheckBoxList)(divmain.FindControl(_ctrlName.ToString()));

                    string Messagetxt = "";
                    for (int i = 0; i < cltxt.Items.Count; i++)
                    {
                        if (cltxt.Items[i].Selected)
                        {
                            if (Messagetxt == "")
                            {

                                Messagetxt += cltxt.Items[i].Text;

                            }
                            else
                            {
                                Messagetxt = Messagetxt + "," + cltxt.Items[i].Text;
                            }
                        }
                    }

                    _value = Messagetxt;

                    break;


                case "5":
                    TextBox rad = (TextBox)divmain.FindControl(_ctrlName.ToString());

                    if (Request.Form[rad.UniqueID] != null && Request.Form[rad.UniqueID] != "")
                    {
                        _value = DateTime.ParseExact(Request.Form[rad.UniqueID], "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                        //rad.SelectedDate.Value.ToString("yyyy/MM/dd");                       
                    }
                    if (_value == "" && fldIsMandatory == "True")
                    {
                        _value = "";
                    }

                    break;

                case "6":

                    DropDownList ddlnew = (DropDownList)(divmain.FindControl(_ctrlName.ToString()));

                    _value = ddlnew.SelectedValue;

                    if (_value == "--Select--")
                    {
                        _value = "";
                    }

                    break;
                case "8":

                    GridView gv = (GridView)(divmain.FindControl(_ctrlName.ToString()));

                    if (ViewState[_ViewStateName] != null)
                    {
                        DataTable dtgrid = (DataTable)ViewState[_ViewStateName];
                        if (dtgrid.Rows.Count == 0)
                        {
                            _value = "";
                        }
                        else if (dtgrid.Rows.Count > 0)
                        {
                            _value = "1";
                        }
                    }

                    break;

                case "9":


                    RadioButtonList rbtnl = (RadioButtonList)(divmain.FindControl(_ctrlName.ToString()));

                    _value = rbtnl.SelectedValue;


                    break;

                case "9txt":


                    RadioButtonList rbtnltxt = (RadioButtonList)(divmain.FindControl(_ctrlName.ToString()));

                    if (rbtnltxt.SelectedValue != "" && rbtnltxt.SelectedValue != null)
                    {
                        _value = rbtnltxt.SelectedItem.ToString();
                    }



                    break;
            }

            return _value;
        }

        public bool ChkDtExistsInDs(DataSet ds, DataTable dtdata)
        {
            foreach (DataTable d in ds.Tables)
            {
                if (d.Columns.Count == dtdata.Columns.Count)
                {
                    if (d.Columns[1].ColumnName == dtdata.Columns[1].ColumnName)
                    {

                        return false;
                    }

                    else
                    {

                    }
                }
            }
            return true;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {

            //string IsGridPresent = hdnFrmCtrlDetails.Value;

            //if (IsGridPresent == "1") //NoGrid
            //{

            //}
            //else if (IsGridPresent == "2") //GridAndOthers
            //{

            //}
            //else if (IsGridPresent == "3") //OnlyGrid
            //{

            //}

            string _lName = "";
            string _lctrlName = "";
            string _lctrlType = "";
            string _lfldIsMandatory = "";
            string _ViewStateName = "";
            string _IsGridData = "";

            DataTable dtvalue = new DataTable();
            DataRow drvalue = dtvalue.NewRow();
            int i = 0;

            int EnteredFieldCount = 0; int IsSecFilledCompletely = 1;



            Dictionary<string, int> d = new Dictionary<string, int>(); int IsGridDataExists = 1;
            if (ViewState[GridDetailsViewStateName] == null)
            {
                IsGridDataExists = 0;
            }

            for (int row = 0; row < dt.Rows.Count; row++)
            {

                _lName = Convert.ToString(dt.Rows[row]["fldQnEngText"]);
                _lctrlName = Convert.ToString(dt.Rows[row]["fldQnWidgetName"]);
                _lctrlType = Convert.ToString(dt.Rows[row]["fldQnWidgetId"]);
                _lfldIsMandatory = Convert.ToString(dt.Rows[row]["fldQnMandatory"]);
                _ViewStateName = Convert.ToString(dt.Rows[row]["fldGridViewStateName"]);
                _IsGridData = Convert.ToString(dt.Rows[row]["fldIsGridData"]);

                string lvalue = returnValues(_lName, _lctrlName, _lctrlType, _lfldIsMandatory, _ViewStateName);



                //if (lvalue == "" && (_lfldIsMandatory == "1" || _lfldIsMandatory == "True"))
                //{

                //    string msg = " Please enter the " + _lName  ;
                //    ShowPopUp(msg, _lName);

                //    return;
                //}

                if (_lctrlType == "8" && (_lfldIsMandatory == "1" || _lfldIsMandatory == "True"))
                {
                    if (IsGridDataExists == 0)
                    {
                        d.Add(_ViewStateName, 0);
                    }

                }

                if (lvalue != "")
                {
                    EnteredFieldCount = EnteredFieldCount + 1;
                }

                if (lvalue == "" && (_lfldIsMandatory == "1" || _lfldIsMandatory == "True") && (_IsGridData != "1" && _IsGridData != "True"))
                {
                    IsSecFilledCompletely = 0;
                }

                dtvalue.Columns.Add(_lctrlName, typeof(string));

                drvalue[row + i] = lvalue;

                if (_lctrlType == "3" || _lctrlType == "4" || _lctrlType == "9")
                {
                    string lvalue1 = returnValues(_lName, _lctrlName, _lctrlType + "txt", _lfldIsMandatory, _ViewStateName);

                    dtvalue.Columns.Add(_lctrlName + "txt", typeof(string));
                    i = i + 1;
                    drvalue[row + i] = lvalue1;


                }

            }


            if (IsGridDataExists == 0)
            {
                ViewState[GridDetailsViewStateName] = d;
                if (d.Count > 0)
                {
                    hfGridDetails.Value = "";
                }

                foreach (KeyValuePair<string, int> entry in d)
                {
                    hfGridDetails.Value = hfGridDetails.Value + entry.Key + "&" + entry.Value + "_";
                }
            }

            //check for grid exist or not

            int _GridDataCount = 0; int _GridActualLen = 0;

            if (hfGridDetails.Value != "")
            {
                if (hfGridDetails.Value.Contains("_"))
                {
                    string[] _GridArr = hfGridDetails.Value.Split('_');
                    for (int _GridCount = 0; _GridCount < _GridArr.Length; _GridCount++)
                    {
                        if (_GridArr[_GridCount] != "")
                        {
                            _GridActualLen++;
                            string _GridVal = _GridArr[_GridCount];
                            if (_GridVal.Contains('&'))
                            {
                                string[] _GArr = _GridVal.Split('&');

                                for (int _GCount = 0; _GCount < _GArr.Length; _GCount++)
                                {
                                    if (_GCount > 0)
                                    {
                                        if (Convert.ToInt32(_GArr[_GCount]) > 0)
                                        {

                                            _GridDataCount++;
                                        }
                                    }
                                }

                            }

                        }
                    }
                }
            }


            int IsGridDataEntered = 0; int GridCount = 0;

            if (_GridDataCount == _GridActualLen && _GridDataCount != 0 && _GridActualLen != 0)
            {
                IsSecFilledCompletely = 1;
            }

            if (EnteredFieldCount == 0)
            {
                if (_GridDataCount != _GridActualLen && _GridDataCount != 0 && _GridActualLen != 0)
                {

                    ShowPopUp(" Atleast make one entry in grid to save partially ", _lName); return;
                }
                else if (_GridDataCount == 0 && _GridActualLen == 0)
                {

                    ShowPopUp(" Atleast make one entry to save partially ", _lName); return;
                }

            }

            dtvalue.Rows.Add(drvalue);




            DataSet ds = new DataSet();




            dtvalue.TableName = "MainTable";


            //DataTable dtG = dt.AsEnumerable()
            //.Where(row => row.Field<String>("fldQnWidgetId") == "8"
            //        )
            //.CopyToDataTable();
            DataTable dtG = new DataTable();
            dtG = dt.Clone();
            DataRow[] dr = dt.Select("fldQnWidgetId=" + "'8'");

            foreach (DataRow data in dr)
            {
                dtG.ImportRow(data);
            }

            DataTable dtdiv = new DataTable();
            if (Session[DistinctValSessionName] != null)
            {
                dtdiv = (DataTable)Session[DistinctValSessionName];
            }

            //check whether grid exists or not
            bool IsGridOnlyPresent = false;
            if (dtG.Rows.Count > 0)
            {
                DataView view = new DataView(dtG);
                DataTable distinctVal = view.ToTable(true, "fldDivID");

                if (distinctVal.Rows.Count > 0)
                {
                    string[] array = distinctVal.Rows.OfType<DataRow>().Select(k => k["fldDivID"].ToString()).ToArray();

                    string ss = string.Empty;
                    for (int loop = 0; loop < array.Length; loop++)
                    {
                        ss = ss + "fldDivID <> " + "'" + array[loop] + "' and ";
                    }


                    int index = ss.LastIndexOf(" and ");
                    ss = ss.Substring(0, index);


                    DataView dview = new DataView(dt);
                    dview.RowFilter = ss;
                    DataTable dtResult = dview.ToTable();

                    if (dtResult.Rows.Count > 0)
                    {
                        //grid and other data available
                        IsGridOnlyPresent = false;
                    }
                    else
                    {
                        //only grid available
                        IsGridOnlyPresent = true;
                    }
                }

            }
            else
            {

            }

            if (!IsGridOnlyPresent)
            {
                //add parent data table to dataset ( 1st table should be parent table )
                ds.Tables.Add(dtvalue);
            }


            string ViewStateName = "";

            //if (dtdiv.Rows.Count > 1)
            {

                for (int k = 0; k < dtG.Rows.Count; k++)
                {
                    ViewStateName = Convert.ToString(dtG.Rows[k]["fldGridViewStateName"]);



                    if (ViewState[ViewStateName] != null)
                    {
                        //adding grid data table to dataset one by one

                        DataTable dtchild = (DataTable)ViewState[ViewStateName];
                        dtchild.TableName = "Child" + k;
                        bool res = ChkDtExistsInDs(ds, dtchild);
                        if (res)
                        {
                            ds.Tables.Add(dtchild);
                        }


                    }
                }

            }



            //if (EnteredFieldCount == 0)
            //{

            //    ShowPopUp(" Atleast make one entry to save partially ", _lName); return;
            //}





            //add first grid data table if present




            //save the record 


            string SecStartTime = string.Empty;

            _userid = Convert.ToString(Session["UserID"]);

            SecStartTime = Convert.ToString(ViewState["StartTime"]);

            if (btnsave.Text == "Next")
            {
                bool IsSave = true;



                //method 1 start
                //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
                //return value as datatable
                DataTable dtresult = obal.insertRecordWithRetValAsDT(ds, StoredProcedureSuffixName, _userid, IsSecFilledCompletely, SecStartTime, IsSave, Convert.ToString(ViewState["QueryStringVal"]));

                if (dtresult.Rows.Count > 0)
                {
                    DataTable dtlbl = new DataTable();
                    string RetVal = Convert.ToString(dtresult.Rows[0]["RetVal"]);
                    dtlbl = obals.bindlblDetails(RetVal);
                    Session["StudtNo"] = dtlbl.Rows[0]["fldStudyNo"].ToString();
                    Session["PatName"] = dtlbl.Rows[0]["fldName"].ToString();
                    Session["MobileNo"] = dtlbl.Rows[0]["fldPhoneNo"].ToString();
                    Session["PatDistrict"] = dtlbl.Rows[0]["fldDistrictName"].ToString();
                    string NextSecUrl = Convert.ToString(dtresult.Rows[0]["NextSecUrl"]);

                    string Msg = Convert.ToString(dtresult.Rows[0]["Msg"]);
                    string nextpageurl = string.Empty;
                    nextpageurl = Encrypt(NextSecUrl, RetVal);

                    ShowPopupSuccessAndRedirect(Msg, "ddlsr", nextpageurl); return;

                }
                else if (dtresult.Rows.Count == 0)
                {
                    ShowPopupSuccessAndRedirect("Record Already Exists", "ddlsr", CurrentPageName);
                    return;

                }


                //VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV
                //method 1 end


                //method 2 start
                //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
                //return value as string

                string ResVal = obal.insertRecordWithRetValAsString(ds, StoredProcedureSuffixName, _userid, IsSecFilledCompletely, SecStartTime, IsSave, Convert.ToString(ViewState["QueryStringVal"]));

                if (ResVal != "")
                {
                    string RetVal = ResVal;

                    if (IsSecFilledCompletely == 1)
                    {
                        string nextpageurl = string.Empty;


                        if (IsLastSection)
                        {
                            nextpageurl = Encrypt(NextRedirectPageUrl, ""); //give your next page url to redirect
                        }
                        else if (!IsLastSection)
                        {
                            nextpageurl = Encrypt(NextRedirectPageUrl, RetVal); //give your next page url to redirect
                        }

                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Popup", "Redirect('" + nextpageurl + "');", true); return;
                    }
                    else if (IsSecFilledCompletely == 0)
                    {
                        string nextpageurl = string.Empty;

                        nextpageurl = Encrypt(CurrentPageName, "");

                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Popup", "Redirect('" + nextpageurl + "');", true); return;
                    }

                }
                else if (ResVal == "")
                {
                    ShowPopupSuccess("Record Already Exists", "ddlsr");
                    return;

                }


                //VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV
                //method 2 end

                //method 3 start
                //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
                //return value as row count

                int Val = obal.insertRecordWithRetValAsRowCount(ds, StoredProcedureSuffixName, _userid, IsSecFilledCompletely, SecStartTime, IsSave, Convert.ToString(ViewState["QueryStringVal"]));

                if (Val > 0)
                {
                    ShowPopupSuccess("Section Saved Successfully <br/> and will redirect to next section", "ddlsr"); return;
                }
                else if (Val == 0)
                {
                    ShowPopupSuccess("Record Already Exists", "ddlsr");
                    return;

                }

                //VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV
                //method 3 end

            }
            else if (btnsave.Text == "Modify")
            {
                bool IsSave = false;
                //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
                //return value as datatable
                DataTable dtresult = obal.insertRecordWithRetValAsDT(ds, StoredProcedureSuffixName, _userid, IsSecFilledCompletely, SecStartTime, IsSave, Convert.ToString(ViewState["QueryStringVal"]));

                if (dtresult.Rows.Count > 0)
                {
                    string RetVal = Convert.ToString(dtresult.Rows[0]["RetVal"]);

                    Int32 RowsAffected = Convert.ToInt32(dtresult.Rows[0]["Rows"]);

                    if (IsSecFilledCompletely == 1 && RowsAffected > 0)
                    {
                        string nextpageurl = string.Empty;

                        if (IsLastSection)
                        {
                            nextpageurl = Encrypt(NextRedirectPageUrl, ""); //give your next page url to redirect
                        }
                        else if (!IsLastSection)
                        {
                            nextpageurl = Encrypt(NextRedirectPageUrl, RetVal); //give your next page url to redirect
                        }

                        ShowPopupSuccessAndRedirect("Record Updated Successfully", "ddlsr", nextpageurl); return;
                    }
                    else if (IsSecFilledCompletely == 0 && RowsAffected > 0)
                    {
                        string nextpageurl = string.Empty;

                        nextpageurl = Encrypt(CurrentPageName, "");

                        ShowPopupSuccessAndRedirect("Record Updated Successfully", "ddlsr", nextpageurl); return;


                    }

                }

                //VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV

                //method 2 start
                //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
                //return value as string

                string ResVal = obal.insertRecordWithRetValAsString(ds, StoredProcedureSuffixName, _userid, IsSecFilledCompletely, SecStartTime, IsSave, Convert.ToString(ViewState["QueryStringVal"]));

                if (ResVal != "")
                {
                    string RetVal = ResVal;

                    if (IsSecFilledCompletely == 1)
                    {
                        string nextpageurl = string.Empty;


                        if (IsLastSection)
                        {
                            nextpageurl = Encrypt(NextRedirectPageUrl, ""); //give your next page url to redirect
                        }
                        else if (!IsLastSection)
                        {
                            nextpageurl = Encrypt(NextRedirectPageUrl, RetVal); //give your next page url to redirect
                        }

                        ShowPopupSuccessAndRedirect("Record Updated Successfully", "ddlsr", nextpageurl); return;
                    }
                    else if (IsSecFilledCompletely == 0)
                    {
                        string nextpageurl = string.Empty;

                        nextpageurl = Encrypt(CurrentPageName, "");

                        ShowPopupSuccessAndRedirect("Record Updated Successfully", "ddlsr", nextpageurl); return;


                    }

                }

                //VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV
                //method 2 end

                //method 3 start
                //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
                //return value as row count

                int Val = obal.insertRecordWithRetValAsRowCount(ds, StoredProcedureSuffixName, _userid, IsSecFilledCompletely, SecStartTime, IsSave, Convert.ToString(ViewState["QueryStringVal"]));

                if (Val > 0)
                {
                    ShowPopupSuccess("Record Updated Successfully", "ddlsr"); return;
                }

                //VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV
                //method 3 end

            }


        }

        public void bindHeader(DataTable dt, string FormTitle, string FormName)
        {
            HtmlGenericControl leg = new HtmlGenericControl("legend");
            leg.Attributes.Add("class", "scheduler-border");
            leg.InnerText = FormTitle;

            formNameH.InnerText = FormName;

            // fsheader.Controls.Add(leg);

            HtmlGenericControl div1 = new HtmlGenericControl("div");
            div1.Attributes.Add("class", "");

            HtmlGenericControl div2 = new HtmlGenericControl("div");
            div2.Attributes.Add("class", "");

            HtmlGenericControl h4 = new HtmlGenericControl("h4");
            h4.Attributes.Add("class", "text-center");

            div2.Controls.Add(h4);

            div1.Controls.Add(div2);

            HtmlGenericControl div3 = new HtmlGenericControl("div");
            div3.Attributes.Add("class", "");

            for (int row = 0; row < dt.Rows.Count; row++)
            {

                Button btn = new Button();

                HtmlGenericControl divcol = new HtmlGenericControl("div");

                HtmlGenericControl div = new HtmlGenericControl("div");
                // HtmlGenericControl divsub = new HtmlGenericControl("div");
                div.Attributes.Add("class", "form-group");
                div.ID = "Sec" + row + Convert.ToString(row);

                string _chk = string.Empty;
                string _id = string.Empty; string _Type = string.Empty;
                string _bind = string.Empty;
                string _maxLength = string.Empty;
                // string _isDepend = "";

                string _retType = string.Empty;
                bool _enabled = true;

                string _Name = string.Empty;
                string _class = string.Empty;
                string _AllowedContentLength = string.Empty;
                string _IsParent = string.Empty;
                string _Category = string.Empty;
                string _ParentControlID = string.Empty;
                string _ServiceMethod = string.Empty;

                string _IsCrossValidation = string.Empty;
                string _Operation = string.Empty;
                string _Value = string.Empty;
                string _ColToEnableOrDisable = string.Empty;
                string _ColType = string.Empty;
                string _Message = string.Empty;
                string _RedirectUrl = string.Empty;

                _id = Convert.ToString(dt.Rows[row]["fldQnWidgetName"]);
                _Type = Convert.ToString(dt.Rows[row]["fldQnWidgetType"]);

                _chk = Convert.ToString(dt.Rows[row]["fldQnWidgetId"]);
                _bind = Convert.ToString(dt.Rows[row]["fldWebBindQuery"]);
                _Message = Convert.ToString(dt.Rows[row]["fldMessage"]);


                _Name = Convert.ToString(dt.Rows[row]["fldQnEngText"]);
                _class = Convert.ToString(dt.Rows[row]["fldCssClass"]);
                _RedirectUrl = Convert.ToString(dt.Rows[row]["fldRedirectUrl"]);




                switch (_chk)
                {

                    //-------------------for button-----------------------//

                    case "1":
                        divcol.Attributes.Add("class", _class);


                        btn.Enabled = _enabled;
                        btn.ID = _id;
                        btn.Text = _Name;
                        btn.Attributes.Add("Class", "SectionHover");


                        string QryStrVal = string.Empty;

                        if (ViewState["QueryStringVal"] != null)
                        {
                            QryStrVal = Convert.ToString(ViewState["QueryStringVal"]);
                        }

                        string RetVal = string.Empty;

                        string Id = string.Empty; string PreBtnId = string.Empty;

                        Id = _id.ToUpper().Replace("BTNSEC", "");

                        int Idd = Convert.ToInt32(Id) - 1;

                        PreBtnId = _id.Replace(Id, "") + Convert.ToString(Idd);

                        //if (QryStrVal != "")
                        //{
                        RetVal = obal.GetFilledStatus(_bind + "'" + QryStrVal + "'");
                        //}


                        if (RetVal == "1")
                        {
                            btn.BackColor = System.Drawing.Color.Green;
                        }
                        else if (RetVal == "0")
                        {
                            btn.BackColor = System.Drawing.Color.Orange;
                        }

                        string s = "return validateButton(this,event," + "'" + RetVal + "','" + _Message + "','" + _Type + "','" + _id + "'," + "'" + PreBtnId + "'," + "'" + Idd + "'" + ")";
                        btn.Attributes.Add("onclick", s);

                        if (_RedirectUrl != "" && _RedirectUrl != null && _RedirectUrl.Trim() != "")
                        {
                            btn.PostBackUrl = _RedirectUrl;
                        }


                        div.Attributes.Add("Class", "form-group");

                        div.Controls.Add(btn);
                        divcol.Controls.Add(div);

                        //if (_autoPostback == true)
                        {
                            btn.Click += new EventHandler(btn_SecClick);

                        }

                        break;

                }

                div3.Controls.Add(divcol);

            }

            div1.Controls.Add(div3);
            // fsheader.Controls.Add(div1);
        }

        [WebMethod]
        public static string ChkDuplicate(string Data, string Qry)
        {
            Qry = Qry.Replace("&quot;", "'");
            BAL_Dynamic obal = new BAL_Dynamic();
            Int32 RetVal = obal.ChkDuplicate(Qry);

            return Convert.ToString(RetVal);
        }




        public void CreateGrid(DataTable dt)
        {
            GridView gv = new GridView();
            gv.AutoGenerateColumns = false;

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                BoundField bndfield = new BoundField();
                bndfield.DataField = dt.Columns[i].ColumnName;
                bndfield.HeaderText = dt.Columns[i].ColumnName;
                gv.Columns.Add(bndfield);
                TemplateField tf = new TemplateField();
                tf.HeaderText = "TestHeader";
                Random r = new Random();
                // tf.ItemTemplate = frmHomePage(DataControlRowType.DataRow, "", dt.Columns[i].ColumnName, "LinkButton", r.Next(1, 1000));



                // gv.Columns.Add(tf);
            }





            gv.DataSource = dt; gv.DataBind();



            divmain.Controls.Add(gv);


        }

        public void CreateGrid(GridView gv, DataTable dtgrid, string _DivId, string ViewStateName)
        {
            string controlId = "lbtnEdit_DivId" + _DivId;
            TemplateField tf = new TemplateField();
            tf.HeaderText = "Edit";
            Random r = new Random();
            //  tf.ItemTemplate = GridViewTemplate(DataControlRowType.DataRow, "", controlId, "LinkButton", r.Next(1, 1000));



            templateType = DataControlRowType.DataRow;
            columnName = "";
            columnNameBinding = controlId;
            controlType = "ImageButton";
            count = r.Next(1, 1000);

            tf.ItemTemplate = this;

            gv.Columns.Add(tf);

            DataTable dtsource = new DataTable();
            for (int i = 0; i < dtgrid.Rows.Count; i++)
            {
                BoundField bndfield = new BoundField();
                if (Convert.ToString(dtgrid.Rows[i]["fldQnWidgetId"]) == "3" || Convert.ToString(dtgrid.Rows[i]["fldQnWidgetId"]) == "4")
                {
                    bndfield.DataField = Convert.ToString(dtgrid.Rows[i]["fldQnWidgetName"]) + "txt";
                    dtsource.Columns.Add(Convert.ToString(dtgrid.Rows[i]["fldQnWidgetName"]), typeof(string));
                }
                else
                {
                    bndfield.DataField = Convert.ToString(dtgrid.Rows[i]["fldQnWidgetName"]);
                }
                dtsource.Columns.Add(bndfield.DataField, typeof(string));

                bndfield.HeaderText = Convert.ToString(dtgrid.Rows[i]["fldQnEngText"]);
                gv.Columns.Add(bndfield);

            }

            if (ViewState[ViewStateName] == null)
            {
                ViewState[ViewStateName] = dtsource;
            }


            if (ViewState[ViewStateName] != null)
            {
                dtsource = (DataTable)ViewState[ViewStateName];
            }


            gv.DataSource = dtsource; gv.DataBind();



        }

        protected void btnaddtolist_Click(object sender, EventArgs e)
        {

        }
        public void BindGridForSelectedRow(string SelButtonId, int rowindex)
        {


            string[] Id = SelButtonId.Split('_'); string id = string.Empty;
            if (Id.Length > 1)
            {
                id = Id[1]; id = id.Replace("DivId", "");
            }

            DataTable dtgrid = new DataTable(); DataTable dtdata = new DataTable();
            if (Session[QuestionMasterSessionName] != null)
            {
                dtgrid = (DataTable)Session[QuestionMasterSessionName];
            }
            dtdata = dtgrid.Clone();
            DataRow[] dr = dtgrid.Select("fldDivID=" + id + " and fldQnWidgetId <> 8");

            if (dr.Length > 0)
            {
                foreach (DataRow d in dr)
                {
                    dtdata.ImportRow(d);
                }

            }

            string _lctrlName = string.Empty;
            string _lctrlType = string.Empty;
            string _value = string.Empty; string _GridViewStateName = string.Empty; bool _enabled = true;
            for (int i = 0; i < dtdata.Rows.Count; i++)
            {
                _lctrlName = Convert.ToString(dtdata.Rows[i]["fldQnWidgetName"]);
                _lctrlType = Convert.ToString(dtdata.Rows[i]["fldQnWidgetId"]);
                _GridViewStateName = Convert.ToString(dtdata.Rows[i]["fldGridViewStateName"]);


                if (Convert.ToString(dtdata.Rows[i]["fldQnEnableDisableOnLoad"]) != "")
                {
                    _enabled = Convert.ToBoolean(Convert.ToString(dtdata.Rows[i]["fldQnEnableDisableOnLoad"]));
                }

                DataTable dtvalue = new DataTable();
                if (ViewState[_GridViewStateName] != null)
                {
                    dtvalue = (DataTable)ViewState[_GridViewStateName];
                }

                if (_lctrlType != "7")
                {
                    _value = Convert.ToString(dtvalue.Rows[rowindex][_lctrlName]);

                }
                bindvalues(_lctrlName, _lctrlType, _value, _enabled);

            }

        }

        public void bindvalues(string _ctrlName, string _ctrlType, string _value, bool _enabled)
        {
            switch (_ctrlType)
            {
                case "1":
                    TextBox txt = (TextBox)(divmain.FindControl(_ctrlName.ToString()));
                    txt.Text = _value;
                    txt.Enabled = _enabled;
                    break;
                case "2":
                    TextBox txta = (TextBox)(divmain.FindControl(_ctrlName.ToString()));
                    txta.Text = _value;
                    txta.Enabled = _enabled;
                    break;
                case "3":
                    DropDownList ddl = (DropDownList)(divmain.FindControl(_ctrlName.ToString()));

                    if (_value != "" && _value != null && _value != "Null")
                    {
                        ddl.SelectedValue = _value;
                    }
                    ddl.Enabled = _enabled;



                    break;
                case "4":
                    CheckBoxList cl = (CheckBoxList)(divmain.FindControl(_ctrlName.ToString()));
                    _value = cl.SelectedValue.ToString();
                    cl.Focus();
                    break;
                case "5":
                    TextBox txtdate = (TextBox)(divmain.FindControl(_ctrlName.ToString()));
                    txtdate.Text = (Convert.ToDateTime(_value).ToString("dd-MM-yyyy"));
                    txtdate.Enabled = _enabled;
                    break;
                case "7":
                    Button btn = (Button)(divmain.FindControl(_ctrlName.ToString()));
                    btn.Text = "Modify";
                    break;
            }

        }
        private void BindforUpdate(string QueryString)
        {
            DataTable dtMod = obal.getDetails(QueryString, _tblName);

            string _lName = "";
            string _lctrlName = "";
            string _lctrlType = "";
            string _lfldIsMandatory = "";
            string _dbfiled = "";
            bool _enabled = false;

            for (int row = 0; row < dt.Rows.Count; row++)
            {

                _lName = dt.Rows[row]["fldQnEngText"].ToString();
                _lctrlName = dt.Rows[row]["fldQnWidgetName"].ToString();
                _lctrlType = dt.Rows[row]["fldQnWidgetId"].ToString();
                _lfldIsMandatory = dt.Rows[row]["fldQnMandatory"].ToString();
                _dbfiled = dt.Rows[row]["fldQnDbColName"].ToString();

                if (dt.Rows[row]["fldQnEnableDisableOnModify"].ToString() != "")
                {
                    _enabled = Convert.ToBoolean(dt.Rows[row]["fldQnEnableDisableOnModify"].ToString());
                }
                if (dtMod.Rows.Count > 0)
                {
                    string _value = dtMod.Rows[0][_dbfiled].ToString();

                    bindvalues(_lctrlName, _lctrlType, _value, _enabled);
                }

            }
        }


        public void Event(object sender, EventArgs e)
        {
            if (sender.GetType().Name == "LinkButton")
            {
                LinkButton lb = (LinkButton)sender;
                GridViewRow container = (GridViewRow)lb.NamingContainer;
                // lb.Enabled = false;
                string SelButtonId = lb.ID;
                int SelGridViewRow = container.RowIndex;
                GridView gv = (GridView)(divmain.FindControl("gvFamilyDetails"));
                // gv.CssClass = "table table-bordered";

                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    gv.Rows[i].BackColor = System.Drawing.Color.Transparent;
                }

                container.BackColor = System.Drawing.Color.White;
                ViewState["SelGridViewRow"] = SelGridViewRow;
                lb.Focus();
                //    frmHomePage obj = new frmHomePage();
                //    //obj.Page_Load(null, null);
                // BindGridForSelectedRow(SelButtonId, SelGridViewRow);
                TextBox txx = (TextBox)(divmain.FindControl("ddlAlreadyTrained"));
                txx.Text = container.Cells[3].Text;
            }


        }


        private DataControlRowType templateType;
        private string columnName;
        private string columnNameBinding;
        private string controlType; private int count = 0;
        private Int32 selGridViewRow = 0;
        private string SelButtonId = string.Empty;
        public int SelGridViewRow
        {
            get
            {
                return selGridViewRow;
            }

            set
            {
                selGridViewRow = value;
            }
        }

        private void binddtforTrained()
        {


            dts = obal.binddtforTrained(Convert.ToString(ViewState["QueryStringVal"]));

        }
        public frmSection1Reg(DataControlRowType type, string colname, string colNameBinding, string ctlType, int Count)
        {

            templateType = type;
            columnName = colname;
            columnNameBinding = colNameBinding;
            controlType = ctlType;
            count = Count;


        }
        public frmSection1Reg()
        {
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            switch (templateType)
            {

                case DataControlRowType.Header:
                    Literal lc = new Literal();
                    lc.Text = columnName;
                    container.Controls.Add(lc);
                    break;
                case DataControlRowType.DataRow:
                    if (controlType == "Label")
                    {
                        Label lb = new Label();
                        lb.ID = "lb" + columnNameBinding;
                        lb.DataBinding += new EventHandler(this.ctl_OnDataBinding);
                        container.Controls.Add(lb);
                    }
                    else if (controlType == "TextBox")
                    {
                        TextBox tb = new TextBox();
                        //tb.ID = "tb" + columnNameBinding;
                        tb.ID = "txt" + columnNameBinding;
                        //tb.ID = "TextBox";
                        tb.DataBinding += new EventHandler(this.ctl_OnDataBinding);
                        container.Controls.Add(tb);
                    }
                    else if (controlType == "CheckBox")
                    {
                        CheckBox cb = new CheckBox();
                        cb.ID = "cb" + columnNameBinding; ;
                        cb.DataBinding += new EventHandler(this.ctl_OnDataBinding);
                        container.Controls.Add(cb);
                    }
                    else if (controlType == "HyperLink")
                    {
                        HyperLink hl = new HyperLink();
                        hl.ID = "hl" + columnNameBinding; ;
                        hl.DataBinding += new EventHandler(this.ctl_OnDataBinding);
                        container.Controls.Add(hl);
                    }
                    else if (controlType == "LinkButton")
                    {
                        LinkButton lb = new LinkButton();
                        lb.ID = "lnk";
                        lb.Text = "Select";
                        lb.DataBinding += new EventHandler(this.ctl_OnDataBinding);
                        lb.Click += new EventHandler(this.Event);
                        container.Controls.Add(lb);

                    }

                    else if (controlType == "ImageButton")
                    {
                        ImageButton ibtn = new ImageButton();
                        ibtn.ID = columnNameBinding; ;
                        ibtn.DataBinding += new EventHandler(this.ctl_OnDataBinding);
                        ibtn.ImageUrl = "~/img/Edit.png";
                        ibtn.Width = 30;
                        string method = string.Empty; method = "return showLoading();";
                        // ibtn.Attributes.Add("onclick", method);
                        ibtn.Click += new ImageClickEventHandler(this.Event);
                        container.Controls.Add(ibtn);

                    }
                    break;
                case DataControlRowType.Footer:
                    TextBox txtfoot = new TextBox();
                    //tb.ID = "tb" + columnNameBinding;
                    //txtfoot.ID = "txtfoot" + columnName;
                    txtfoot.ID = "txtfoot" + columnNameBinding; ;

                    //tb.DataBinding += new EventHandler(this.ctl_OnDataBinding);
                    txtfoot.Text = "a" + columnName;
                    container.Controls.Add(txtfoot);
                    break;
                default:
                    break;
            }
        }

        public void lnk_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "lnk",
           "<script type = 'text/javascript'>alert('LinkButton Clicked');</script>");
        }

        public void ctl_OnDataBinding(object sender, EventArgs e)
        {
            if (sender.GetType().Name == "Label")
            {
                Label lb = (Label)sender;
                GridViewRow container = (GridViewRow)lb.NamingContainer;
                lb.Text = ((DataRowView)container.DataItem)[columnNameBinding].ToString();
                SelGridViewRow = container.RowIndex;
            }
            else if (sender.GetType().Name == "TextBox")
            {
                TextBox tb = (TextBox)sender;
                GridViewRow container = (GridViewRow)tb.NamingContainer;
                tb.Text = ((DataRowView)container.DataItem)[columnNameBinding].ToString();
                SelGridViewRow = container.RowIndex;
            }
            else if (sender.GetType().Name == "CheckBox")
            {
                CheckBox cb = (CheckBox)sender;
                GridViewRow container = (GridViewRow)cb.NamingContainer;
                cb.Checked = Convert.ToBoolean(((DataRowView)container.DataItem)[columnNameBinding].ToString());
                SelGridViewRow = container.RowIndex;
            }
            else if (sender.GetType().Name == "HyperLink")
            {
                HyperLink hl = (HyperLink)sender;
                GridViewRow container = (GridViewRow)hl.NamingContainer;
                hl.Text = ((DataRowView)container.DataItem)[columnNameBinding].ToString();
                hl.NavigateUrl = ((DataRowView)container.DataItem)[columnNameBinding].ToString();
                SelGridViewRow = container.RowIndex;
            }
            else if (sender.GetType().Name == "LinkButton")
            {
                LinkButton lb = (LinkButton)sender;
                GridViewRow container = (GridViewRow)lb.NamingContainer;
                lb.Text = "Select";
                lb.CssClass = "lbtn icon-edit";
                SelGridViewRow = container.RowIndex;

            }
        }

        public void GetUserRole()
        {
            string UserId = Convert.ToString(Session["UserID"]);
            string UserRoleId = Convert.ToString(Session["RoleID"]);

            if (UserRoleId == "8")
            {

                DropDownList ddlStateName = (DropDownList)(divmain.FindControl("ddlStateName".ToString()));
                DropDownList ddlDistrictName = (DropDownList)(divmain.FindControl("ddlDistrictName".ToString()));
                // DropDownList ddlTuName = (DropDownList)(divmain.FindControl("ddlTuName".ToString()));
                DropDownList ddlCityName = (DropDownList)(divmain.FindControl("ddlCityName".ToString()));

                if (ddlStateName != null)
                {
                    ddlStateName.SelectedValue = stateId;
                }
                if (ddlDistrictName != null)
                {
                    ddlDistrictName.SelectedValue = districtId;
                }
                if (ddlCityName != null)
                {
                    ddlCityName.SelectedValue = cityId;
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string strPrvUrl = Encrypt(PreviousPage, Convert.ToString(ViewState["QueryStringVal"]));

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Popup", "Redirect('" + PreviousPage + "');", true);
        }

    }

    

    //public class GridViewTemplate : ITemplate
    //{
    //    private DataControlRowType templateType;
    //    private string columnName;
    //    private string columnNameBinding;
    //    private string controlType; private int count = 0;
    //    private Int32 selGridViewRow = 0;
    //    private string SelButtonId = string.Empty;
    //    public int SelGridViewRow
    //    {
    //        get
    //        {
    //            return selGridViewRow;
    //        }

    //        set
    //        {
    //            selGridViewRow = value;
    //        }
    //    }

    //    public GridViewTemplate(DataControlRowType type, string colname, string colNameBinding, string ctlType, int Count)
    //    {
    //        templateType = type;
    //        columnName = colname;
    //        columnNameBinding = colNameBinding;
    //        controlType = ctlType;
    //        count = Count;
    //    }
    //    public GridViewTemplate()
    //    {

    //    }
    //    public void InstantiateIn(System.Web.UI.Control container)
    //    {
    //        switch (templateType)
    //        {

    //            case DataControlRowType.Header:
    //                Literal lc = new Literal();
    //                lc.Text = columnName;
    //                container.Controls.Add(lc);
    //                break;
    //            case DataControlRowType.DataRow:
    //                if (controlType == "Label")
    //                {
    //                    Label lb = new Label();
    //                    lb.ID = "lb" + columnNameBinding;
    //                    lb.DataBinding += new EventHandler(this.ctl_OnDataBinding);
    //                    container.Controls.Add(lb);
    //                }
    //                else if (controlType == "TextBox")
    //                {
    //                    TextBox tb = new TextBox();
    //                    //tb.ID = "tb" + columnNameBinding;
    //                    tb.ID = "txt" + columnNameBinding;
    //                    //tb.ID = "TextBox";
    //                    tb.DataBinding += new EventHandler(this.ctl_OnDataBinding);
    //                    container.Controls.Add(tb);
    //                }
    //                else if (controlType == "CheckBox")
    //                {
    //                    CheckBox cb = new CheckBox();
    //                    cb.ID = "cb" + columnNameBinding; ;
    //                    cb.DataBinding += new EventHandler(this.ctl_OnDataBinding);
    //                    container.Controls.Add(cb);
    //                }
    //                else if (controlType == "HyperLink")
    //                {
    //                    HyperLink hl = new HyperLink();
    //                    hl.ID = "hl" + columnNameBinding; ;
    //                    hl.DataBinding += new EventHandler(this.ctl_OnDataBinding);
    //                    container.Controls.Add(hl);
    //                }
    //                else if (controlType == "LinkButton")
    //                {
    //                    LinkButton lb = new LinkButton();
    //                    lb.ID = columnNameBinding; ;
    //                    lb.DataBinding += new EventHandler(this.ctl_OnDataBinding);
    //                    frmHomePage obj = new frmHomePage();
    //                    lb.Click += new EventHandler(obj.Event);
    //                    container.Controls.Add(lb);

    //                }
    //                break;
    //            case DataControlRowType.Footer:
    //                TextBox txtfoot = new TextBox();
    //                //tb.ID = "tb" + columnNameBinding;
    //                //txtfoot.ID = "txtfoot" + columnName;
    //                txtfoot.ID = "txtfoot" + columnNameBinding; ;

    //                //tb.DataBinding += new EventHandler(this.ctl_OnDataBinding);
    //                txtfoot.Text = "a" + columnName;
    //                container.Controls.Add(txtfoot);
    //                break;
    //            default:
    //                break;
    //        }
    //    }

    //    public void ctl_OnDataBinding(object sender, EventArgs e)
    //    {
    //        if (sender.GetType().Name == "Label")
    //        {
    //            Label lb = (Label)sender;
    //            GridViewRow container = (GridViewRow)lb.NamingContainer;
    //            lb.Text = ((DataRowView)container.DataItem)[columnNameBinding].ToString();
    //            SelGridViewRow = container.RowIndex;
    //        }
    //        else if (sender.GetType().Name == "TextBox")
    //        {
    //            TextBox tb = (TextBox)sender;
    //            GridViewRow container = (GridViewRow)tb.NamingContainer;
    //            tb.Text = ((DataRowView)container.DataItem)[columnNameBinding].ToString();
    //            SelGridViewRow = container.RowIndex;
    //        }
    //        else if (sender.GetType().Name == "CheckBox")
    //        {
    //            CheckBox cb = (CheckBox)sender;
    //            GridViewRow container = (GridViewRow)cb.NamingContainer;
    //            cb.Checked = Convert.ToBoolean(((DataRowView)container.DataItem)[columnNameBinding].ToString());
    //            SelGridViewRow = container.RowIndex;
    //        }
    //        else if (sender.GetType().Name == "HyperLink")
    //        {
    //            HyperLink hl = (HyperLink)sender;
    //            GridViewRow container = (GridViewRow)hl.NamingContainer;
    //            hl.Text = ((DataRowView)container.DataItem)[columnNameBinding].ToString();
    //            hl.NavigateUrl = ((DataRowView)container.DataItem)[columnNameBinding].ToString();
    //            SelGridViewRow = container.RowIndex;
    //        }
    //        else if (sender.GetType().Name == "LinkButton")
    //        {
    //            LinkButton lb = (LinkButton)sender;
    //            GridViewRow container = (GridViewRow)lb.NamingContainer;
    //            lb.Text = "Edit";
    //            lb.CssClass = "lbtn icon-edit";
    //            SelGridViewRow = container.RowIndex;

    //        }
    //    }

    //    public void Event(object sender, EventArgs e)
    //    {
    //        if (sender.GetType().Name == "LinkButton")
    //        {
    //            LinkButton lb = (LinkButton)sender;
    //            GridViewRow container = (GridViewRow)lb.NamingContainer;
    //            SelButtonId = lb.ID;
    //            SelGridViewRow = container.RowIndex;
    //            frmHomePage obj = new frmHomePage();
    //            //obj.Page_Load(null, null);
    //            obj.BindGridForSelectedRow(SelButtonId, SelGridViewRow);
    //        }
    //    }
    //}

}