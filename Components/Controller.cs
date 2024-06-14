using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using GIBS.Modules.GIBS_FBFoodOrder.Data;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Content.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Framework;

namespace GIBS.Modules.GIBS_FBFoodOrder.Components
{
    public class Controller
    {
        public List<FBFOInfo> GetOrderList(int visitID, int clientID)
        {
            return CBO.FillCollection<FBFOInfo>(DataProvider.Instance().GetOrderList(visitID, clientID));
        }

        public void InsertVisitOrderItem(FBFOInfo info)
        {
            //check we have some content to store
            if (info.VisitID > 0)
            {
                DataProvider.Instance().InsertVisitOrderItem(info.VisitID, info.ProductID, info.Quantity);
            }
        }

        public void UpdateVisitOrderStatusCode(FBFOInfo info)
        {
            //check we have some content to store
            if (info.VisitID > 0)
            {
                DataProvider.Instance().UpdateVisitOrderStatusCode(info.VisitID, info.OrderStatusCode);
            }
        }

        public FBFOInfo GetOrder(int visitID)
        {
            return CBO.FillObject<FBFOInfo>(DataProvider.Instance().GetOrder(visitID));
        }

        public List<FBFOInfo> GetOrderInstructions(int moduleID)
        {
            return CBO.FillCollection<FBFOInfo>(DataProvider.Instance().GetOrderInstructions(moduleID));
         //   return CBO.FillObject<FBFOInfo>(DataProvider.Instance().GetOrderInstructions(moduleID));
        }

    }
}