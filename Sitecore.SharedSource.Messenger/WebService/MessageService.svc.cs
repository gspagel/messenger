using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace Sitecore.SharedSource.Messenger.WebService
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class MessageService
    {
        private static string MessageItemsDefaultPath { get { return "/sitecore/system/modules/messenger/messages"; } }

        [WebGet]
        [OperationContract]
        public Message GetMessage()
        {
            Sitecore.Data.Items.Item messageItem = Sitecore.Context.Database.SelectItems(string.Format("fast:{0}/child::*", MessageItemsDefaultPath)).Where(item => !string.IsNullOrEmpty(item["Message Text"]) && ((Sitecore.Data.Fields.DateField)item.Fields["Broadcast Expiration"]).DateTime.CompareTo(DateTime.Now) < 0).FirstOrDefault();

            if (messageItem != null)
            {
                Message messageObject = new Message { MessageTitle = messageItem["Message Title"], MessageText = messageItem["Message Text"] };
                return messageObject;
            }

            return new Message();
        }
    }
}

namespace Sitecore.SharedSource.Messenger
{
    public struct Message
    {
        public string MessageTitle { get; set; }
        public string MessageText { get; set; }
    }
}
