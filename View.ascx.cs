/*
' Copyright (c) 2024  GIBS.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using GIBS.Modules.GIBS_FBFoodOrder.Components;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Drawing;
using DotNetNuke.Web.Client;
//using DotNetNuke.Web.Client.ClientResourceManagement;


namespace GIBS.Modules.GIBS_FBFoodOrder
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from GIBS_FBFoodOrderModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : GIBS_FBFoodOrderModuleBase, IActionable
    {

//        private GridViewHelper helper;
        static string clientLanguage = "";
        public string _instructions = "";
        public int _FoodBankFoodTrackingModuleID;

        //protected override void OnInit(EventArgs e)
        //{
        //    base.OnInit(e);

        //    DotNetNuke.Web.Client.ClientResourceManagement.ClientResourceManager.RegisterScript(this.Page, "https://translate.google.com/translate_a/element.js?cb=googleTranslateElementInit", FileOrder.Js.DefaultPriority, "DnnPageHeaderProvider");

        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

            if (!IsPostBack)
                {
                    

                    if (Request.QueryString["key"] != null)
                    {
                        string key = Request.QueryString["key"].ToString();
                        char separator = '-'; // Space character
                        string[] keys = key.Split(separator); // returned array
                        int visitID = Int32.Parse(keys[0].ToString());
                        int clientID = Int32.Parse(keys[1].ToString());
                        HiddenFieldVisitID.Value = visitID.ToString();
                        //LabelDebug.Text = visitID.ToString() + " ---- " +  clientID.ToString();
                        GetOrder(visitID);
                        GetInstructions();
                     //   GroupIt();
                        FillProductsGrid(visitID, clientID);
                    }
                    else
                    {
                        this.ModuleConfiguration.ModuleControl.ControlTitle = "No Luck";

                    }



                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public void GetOrder(int visitID)
        {

            try
            {       
                Controller controller = new Controller();
                FBFOInfo fbfo = controller.GetOrder(visitID);

                if (fbfo != null)
                {
                    string OrderDetails = "Visit Date: " + fbfo.VisitDate.ToShortDateString() + "<br />"
                                        + fbfo.ClientName.ToString() + "<br />"
                                         + "Bags Allowed: " + fbfo.VisitNumBags.ToString() + "<br />"
                                        + "Household Total: " + fbfo.HouseholdTotal.ToString() + "<br />"
                                        + "Notes: " + fbfo.VisitNotes.ToString();
                    LabelOrderDetails.Text = OrderDetails.ToString();
                    
                    clientLanguage = fbfo.ClientLanguage.ToString();
                    HiddenFieldHouseholdTotal.Value = fbfo.HouseholdTotal.ToString();
                }
               

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void GetInstructions()
        {

            try
            {
                int mid = 0;
                if (Settings.Contains("foodBankFoodTrackingModuleID"))
                {
                    mid = Int16.Parse(Settings["foodBankFoodTrackingModuleID"].ToString());
                }

                List<FBFOInfo> items;
                Controller controller = new Controller();
                items = controller.GetOrderInstructions(mid);
                if (items == null || items.Count == 0)
                {
                    GridViewInstructions.Visible = false;
                }
                else
                {
                    
                    GridViewInstructions.DataSource = items;
                    GridViewInstructions.DataBind();

                }


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        protected string GetClientLanguage()
        {
            //  return ;

            return clientLanguage.ToString().ToLower();
        }


        public void FillProductsGrid(int visitID, int clientID)
        {

            try
            {

                List<FBFOInfo> items;
                Controller controller = new Controller();
                items = controller.GetOrderList(visitID, clientID);
                if(items == null || items.Count == 0)
                {
                    ButtonSaveOrder.Visible = false;
                    LabelDebug.Visible = true;
                    LabelDebug.Text = "This page is no longer valid.";
                }
                else
                {
                    LabelDebug.Visible = false;
                    ButtonSaveOrder.Visible = true;
                    GridViewOrderSheet.DataSource = items;
                    GridViewOrderSheet.DataBind();

                }
                

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        //public void GroupIt()
        //{

        //    try
        //    {

        //        helper = new GridViewHelper(this.GridViewOrderSheet);
        //        helper.RegisterGroup("ProductCategory", true, true);


        //        helper.GroupHeader += new GroupEvent(helper_GroupHeader);
        //        helper.GroupSummary += new GroupEvent(helper_Bug);
        //        helper.ApplyGroupSort();

        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.ProcessModuleLoadException(this, ex);
        //    }

        //}


        //private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
        //{
        //    row.BackColor = Color.FromArgb(236, 236, 236);
        //    row.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>" + row.Cells[0].Text + "</b>";
        //}

        //private void helper_Bug(string groupName, object[] values, GridViewRow row)
        //{
        //    if (groupName == null) return;

        //    row.BackColor = Color.LightCyan;
        //    row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
        //    row.Cells[0].Text = values[0] + " Totals &nbsp;";
        //}

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection
                    {
                        {
                            GetNextActionID(), Localization.GetString("EditModule", LocalResourceFile), "", "", "",
                            EditUrl(), false, SecurityAccessLevel.Edit, true, false
                        }
                    };
                return actions;
            }
        }

        Color colorChoice = Color.LightGoldenrodYellow;
        protected void GridViewOrderSheet_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                // Get the first column value of the current row
                string currentFirstColumnValue = e.Row.Cells[0].Text;

                // Check if it's not the first row
                if (e.Row.RowIndex != 0)
                {
                    // Get the first column value of the previous row
                    string previousFirstColumnValue = GridViewOrderSheet.Rows[e.Row.RowIndex - 1].Cells[0].Text;

                    // Check if the current first column value is different from the previous row's first column value
                    if (currentFirstColumnValue != previousFirstColumnValue)
                    {
                        // If the current row is the start of a new group, alternate between two colors
                        if (colorChoice == Color.LightGoldenrodYellow)
                        {
                            e.Row.Cells[0].BackColor = Color.AliceBlue;
                            colorChoice = Color.AliceBlue;
                        }
                        else
                        {
                            e.Row.Cells[0].BackColor = Color.LightGoldenrodYellow;
                            colorChoice = Color.LightGoldenrodYellow;
                        }
                    }
                    else
                    {
                        // Use the same color as the previous row if the first column value hasn't changed
                        e.Row.Cells[0].BackColor = colorChoice;
                    }
                }
                else
                {
                    // If it's the first row, color the first column cell with the initial color choice
                    e.Row.Cells[0].BackColor = Color.LightGoldenrodYellow;
                    colorChoice = Color.LightGoldenrodYellow;
                }
                //HiddenFieldHouseholdTotal
                int _thh = Int32.Parse(HiddenFieldHouseholdTotal.Value.ToString());
                int limit = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Limit"));
                HiddenField hidLimitQuantities = (HiddenField)e.Row.FindControl("HiddenFieldLimitQuantities");
                if (hidLimitQuantities.Value.ToString().Length >= 3)
                {
                    string dataForArray = hidLimitQuantities.Value.ToString();
                    string[] firstArray = dataForArray.Split(',');
                    string[] secondArray;
                    int customLimit = 0;
                    for (int i = 0; i < firstArray.Length; i++)
                    {
                        secondArray = firstArray[i].Split('=');
                        if (_thh == Convert.ToInt16(secondArray[0]))
                        {
                            customLimit = Convert.ToInt16(secondArray[1]);
                        }
                    }
                    //if (customLimit == 0)
                    //{
                    //    secondArray = firstArray[firstArray.Length - 1].Split('=');
                    //    customLimit = Convert.ToInt16(secondArray[1]);
                    //}
                    if (customLimit > 0)
                    {
                        limit = customLimit;
                    }
                }

                DropDownList ddlQty = (DropDownList)e.Row.FindControl("DropDownListQty");
              
                for (int i = 1; i <= limit; i++)
                {
                    // do stuff
                    ListItem lst = new ListItem(i.ToString(), i.ToString());
                    ddlQty.Items.Add(lst); ;
                }

                //string instructions = DataBinder.Eval(e.Row.DataItem, "OrderingInstructions").ToString();
                //string productCategory = DataBinder.Eval(e.Row.DataItem, "ProductCategory").ToString();
                //if (instructions.Length > 0)
                //{
                //    _instructions += "<b>" + productCategory.ToString() + ":</b> " + instructions.ToString() + "<br />";
                //}

            }
           // LabelInstructions.Text = _instructions.ToString();
        }

        protected void ButtonSaveOrder_Click(object sender, EventArgs e)
        {
            ButtonSaveOrder.Enabled = false;
            ProcessGrid();
            
            GridViewOrderSheet.Visible = false;
            ButtonSaveOrder.Visible = false;
            LabelDebug.Visible=true; 
            LabelDebug.Text = "Your Order has Been Submitted!";
        }

        public void ProcessGrid()
        {

            try
            {
               int visitID =  Int32.Parse(HiddenFieldVisitID.Value.ToString());

                //  UnGroupIt();

               // GridViewOrderSheet.isv
                foreach (GridViewRow row in GridViewOrderSheet.Rows)
                {
                    // row.TableSection
                    if(row.TableSection != TableRowSection.TableHeader) 
                    {
                      //  object rows;
                        if ((DropDownList)row.FindControl("DropDownListQty") is DropDownList ddlQty1)
                        {
                            //if (ddlQty1.SelectedIndex > 0)
                            if (ddlQty1.SelectedValue.ToString() != "0")
                            {
                                //   DropDownList ddlQty = (DropDownList)row.FindControl("DropDownListQty");
                                HiddenField hidProductID = (HiddenField)row.FindControl("HiddenFieldProductID");
                                int productID = Convert.ToInt32(hidProductID.Value.ToString());
                                SaveOrderItem(visitID, productID, Int32.Parse(ddlQty1.SelectedValue.ToString()));
                            }
                        }
                        else
                        {

                        }
                    }
                }


                //foreach (GridViewRow row in GridViewOrderSheet.Rows)
                //{
                //    DropDownList ddlQty1 = (DropDownList)row.FindControl("DropDownListQty");
                //    if (ddlQty1 != null && ddlQty1.SelectedIndex > 0)
                //    {
                //        //   DropDownList ddlQty = (DropDownList)row.FindControl("DropDownListQty");
                //        HiddenField hidProductID = (HiddenField)row.FindControl("HiddenFieldProductID");
                //        int productID = Convert.ToInt32(hidProductID.Value.ToString());
                //        SaveOrderItem(visitID, productID, Int32.Parse(ddlQty1.SelectedValue.ToString()));
                //    }

                //}

                //for (int i = 0; i < GridViewOrderSheet.Rows.Count; i++)
                //{
                //    GridViewRow row = GridViewOrderSheet.Rows[i];
                //    // the rest o your code
                //    DropDownList ddlQty1 = (DropDownList)row.FindControl("DropDownListQty");
                //    if (ddlQty1 != null && ddlQty1.SelectedIndex > 0)
                //    {
                //        //   DropDownList ddlQty = (DropDownList)row.FindControl("DropDownListQty");
                //        HiddenField hidProductID = (HiddenField)row.FindControl("HiddenFieldProductID");
                //        int productID = Convert.ToInt32(hidProductID.Value.ToString());
                //        SaveOrderItem(visitID, productID, Int32.Parse(ddlQty1.SelectedValue.ToString()));
                //    }
                //}

                Controller controller = new Controller();
                FBFOInfo item = new FBFOInfo();
                item.OrderStatusCode = 2;
                item.VisitID = visitID;
                controller.UpdateVisitOrderStatusCode(item);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        public void SaveOrderItem(int _VisitID, int _ProductID, int _Quantity)
        {
            try
            {
                Controller controller = new Controller();
                FBFOInfo item = new FBFOInfo();

                item.VisitID = _VisitID;
                item.ProductID = _ProductID;
                item.Quantity = _Quantity;

                controller.InsertVisitOrderItem(item);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void GridViewOrderSheet_Sorting(object sender, GridViewSortEventArgs e)
        {

        }
    }
}