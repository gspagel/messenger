using System;

namespace Sitecore.SharedSource.Messenger
{
    public class MessageCleanupAgent
    {
        private static string MessageItemsDefaultPath { get { return "/sitecore/system/modules/messenger/messages"; } }

        public void Run()
        {
            // Get the path to the message items
            string pathToMessageItems = Sitecore.Configuration.Settings.GetSetting("Messenger.ItemsPath", MessageItemsDefaultPath);

            try
            {
                Sitecore.Data.Items.Item messagesRootItem = Sitecore.Context.Database.GetItem(pathToMessageItems);

                if (messagesRootItem != null)
                {
                    // Assume that all child items are message items
                    // Remove any item whose expiration date is earlier than now
                    Sitecore.Collections.ChildList messageItems = messagesRootItem.Children;

                    foreach (Sitecore.Data.Items.Item item in messageItems)
                    {
                        if (item == null)
                        {
                            continue;
                        }

                        if (!string.IsNullOrEmpty(item["Broadcast Expiration"]))
                        {
                            DateTime expirationDate = ((Sitecore.Data.Fields.DateField)item.Fields["Broadcast Expiration"]).DateTime;

                            if (expirationDate.CompareTo(DateTime.Now) >= 0)
                            {
                                item.Delete();
                            }
                        }
                    }
                }
            }
            catch (Exception x)
            {
                Sitecore.Diagnostics.Log.Warn("Message Cleanup Agent", x, this);
            }
        }
    }
}