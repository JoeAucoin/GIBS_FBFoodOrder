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
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace GIBS.Modules.GIBS_FBFoodOrder
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Settings class manages Module Settings
    /// 
    /// Typically your settings control would be used to manage settings for your module.
    /// There are two types of settings, ModuleSettings, and TabModuleSettings.
    /// 
    /// ModuleSettings apply to all "copies" of a module on a site, no matter which page the module is on. 
    /// 
    /// TabModuleSettings apply only to the current module on the current page, if you copy that module to
    /// another page the settings are not transferred.
    /// 
    /// If you happen to save both TabModuleSettings and ModuleSettings, TabModuleSettings overrides ModuleSettings.
    /// 
    /// Below we have some examples of how to access these settings but you will need to uncomment to use.
    /// 
    /// Because the control inherits from GIBS_FBFoodOrderSettingsBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Settings : GIBS_FBFoodOrderModuleSettingsBase
    {
        #region Base Method Implementations

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// LoadSettings loads the settings from the Database and displays them
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void LoadSettings()
        {
            try
            {
                if (Page.IsPostBack == false)
                {
                    BindModules();

                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpdateSettings saves the modified settings to the Database
        /// </summary>
        /// -----------------------------------------------------------------------------

        private void BindModules()
        {

            DotNetNuke.Entities.Modules.ModuleController mc = new ModuleController();
            ArrayList existMods = mc.GetModulesByDefinition(this.PortalId, "GIBS Food Bank Food Tracking");

            foreach (DotNetNuke.Entities.Modules.ModuleInfo mi in existMods)
            {
                if (!mi.IsDeleted)
                {

                    ListItem objListItemPage = new ListItem();
                    objListItemPage.Value = mi.ModuleID.ToString();
                  //  objListItemPage.Value = mi.TabID.ToString() + "-" + mi.ModuleID.ToString();
                    objListItemPage.Text = mi.ModuleTitle.ToString();

                    drpModuleID.Items.Add(objListItemPage);

                }
            }

            drpModuleID.Items.Insert(0, new ListItem(Localization.GetString("SelectModule", this.LocalResourceFile), "-1"));

            if (Settings.Contains("foodBankFoodTrackingModuleID"))
            {
                drpModuleID.SelectedValue = Settings["foodBankFoodTrackingModuleID"].ToString();
                //   LabelDebug.Text = Settings["foodBankClientModuleID"].ToString();

            }

        }

        public override void UpdateSettings()
        {
            try
            {
                var modules = new ModuleController();

                FoodBankFoodTrackingModuleID = drpModuleID.SelectedValue.ToString();
               
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion
    }
}