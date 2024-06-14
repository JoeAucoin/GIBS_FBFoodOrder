<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="GIBS.Modules.GIBS_FBFoodOrder.Settings" %>

<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>


	<h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead"><a href="" class="dnnSectionExpanded"><%=LocalizeString("BasicSettings")%></a></h2>
	<fieldset>

	 <div class="dnnFormItem">
            <dnn:label id="lblFoodBankFoodInventoryModuleID" runat="server" suffix=":" controlname="drpModuleID" />
            <asp:dropdownlist id="drpModuleID" Runat="server" Width="325"></asp:dropdownlist>
        </div>	
		</fieldset>