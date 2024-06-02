<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="GIBS.Modules.GIBS_FBFoodOrder.View" %>



<script type="text/javascript">

    $(function () {
        $(".ddlclass").change(function () {
            if ($(this).find('option:selected').text() != "---") {
               // alert($(this).find('option:selected').text());
            } 
            else {
            //    alert('2');
            }
        });
    });

</script>



<div style="width:95%; margin: auto; padding-bottom:20px;">
<asp:Label ID="LabelDebug" runat="server" Text="Invalid Request" CssClass="errorMessage"></asp:Label></div>

<h4><asp:Label ID="LabelOrderDetails" runat="server" Text=""></asp:Label></h4>
<div>Translate this page:</div>

<div id="google_translate_element"></div>
<script type="text/javascript">
    
    setCookie('googtrans', '/en/<%= GetClientLanguage() %>', 1);
    function googleTranslateElementInit() {

        new google.translate.TranslateElement({ pageLanguage: 'en', includedLanguages: 'es,pt,ht,it,de,fr,pl,ru,uk,en' }, 'google_translate_element');
    }

    function setCookie(key, value, expiry) {
        var expires = new Date();
        expires.setTime(expires.getTime() + (expiry * 24 * 60 * 60 * 1000));
        document.cookie = key + '=' + value + ';expires=' + expires.toUTCString();
    }
</script>
<script type="text/javascript" src="//translate.google.com/translate_a/element.js?cb=googleTranslateElementInit"></script>




<asp:GridView ID="GridViewOrderSheet" runat="server" HorizontalAlign="Center" OnSorting="GridViewOrderSheet_Sorting" 
    AutoGenerateColumns="False" OnRowDataBound="GridViewOrderSheet_RowDataBound" CssClass="table table-striped">
     <Columns>
        
         <asp:BoundField HeaderText="Category" DataField="ProductCategory" Visible="true"></asp:BoundField>
        <asp:BoundField HeaderText="Product" DataField="ProductName" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
        <asp:BoundField HeaderText="Limit" DataField="Limit" ItemStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false"></asp:BoundField>
        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Order" HeaderStyle-HorizontalAlign="Center">                     
            
            <ItemTemplate>
                <asp:HiddenField ID="HiddenFieldLimitQuantities" Value='<%# Eval("LimitQuantities") %>' runat="server" />
                <asp:HiddenField ID="HiddenFieldProductID" Value='<%# Eval("ProductID") %>' runat="server" />
                <asp:DropDownList ID="DropDownListQty" runat="server"><asp:ListItem Text="---" Value="0" /></asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:BoundField HeaderText="ProductID" DataField="ProductID" Visible="false"></asp:BoundField>
     </Columns>

</asp:GridView>

<asp:HiddenField ID="HiddenFieldVisitID" runat="server" />
<asp:HiddenField ID="HiddenFieldHouseholdTotal" runat="server" />

<div>
    <asp:Button ID="ButtonSaveOrder" runat="server" Text="Save Order" OnClick="ButtonSaveOrder_Click" CssClass="btn btn-lg btn-default" Visible="false" />

</div>