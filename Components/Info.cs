using System;
using System.Reflection;
using System.Text.RegularExpressions;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Content;
using DotNetNuke.Services.FileSystem;


namespace GIBS.Modules.GIBS_FBFoodOrder.Components
{
    public class FBFOInfo
    {
        private int productID;
        private string productName;
        private string productCategory;
        private int limit;
        private int visitID;
        private int quantity;
        private int clientID;
        private int orderStatusCode;
        //      OrderStatusCode
        //0	OrderEntered //Paper Process
        //1	OrderLinkSent  //Client Sent Order Link via Text
        //2	OrderSubmitted  //Client Submitted Order via Text Web Link
        //3	Order Currently Being Processed  //Order Ready in process of Fulfillment
        //4	OrderFilled  //Order filled, ready for pickup
        private string clientName;
        private string clientLanguage;
        private DateTime visitDate;
        private int visitNumBags;
        private string clientCellPhone;
        private int housedoldTotal;
        private string visitNotes;
        private string limitQuantities;
        private string orderingInstructions;


        //GIBS_FBClients.ClientPhone, GIBS_FBClients.ClientPhoneType  clientPhone  clientPhoneType
        public FBFOInfo()
        {

        }

        public string OrderingInstructions
        {
            get { return orderingInstructions; }
            set { orderingInstructions = value; }
        }
        public string LimitQuantities
        {
            get { return limitQuantities; }
            set { limitQuantities = value; }
        }
        public string VisitNotes
        {
            get { return visitNotes; }
            set { visitNotes = value; }
        }
        public int HouseholdTotal
        {
            get { return housedoldTotal; }
            set { housedoldTotal = value; }
        }
        public string ClientCellPhone
        {
            get { return clientCellPhone; }
            set { clientCellPhone = value; }
        }

        public string ClientName
        {
            get { return clientName; }
            set { clientName = value; }
        }
        public string ClientLanguage
        {
            get { return clientLanguage; }
            set { clientLanguage = value; }
        }
        public DateTime VisitDate
        {
            get { return visitDate; }
            set { visitDate = value; }
        }

        public int VisitNumBags
        {
            get { return visitNumBags; }
            set { visitNumBags = value; }
        }
        public int OrderStatusCode
        {
            get { return orderStatusCode; }
            set { orderStatusCode = value; }
        }

        public int ClientID
        {
            get { return clientID; }
            set { clientID = value; }
        }
        public int VisitID
        {
            get { return visitID; }
            set { visitID = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }
        public string ProductCategory
        {
            get { return productCategory; }
            set { productCategory = value; }
        }
        public int Limit
        {
            get { return limit; }
            set { limit = value; }
        }
    }
}