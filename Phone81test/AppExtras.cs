using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CommonLib
{
    public static class AppExtras
    {
        public static Guid GetId()
        {
            Guid applicationId = Guid.Empty;

            var productId = XDocument.Load("WMAppManifest.xml").Root.Element("App").Attribute("ProductID");

            if (productId != null && !string.IsNullOrEmpty(productId.Value))
                Guid.TryParse(productId.Value, out applicationId);

            return applicationId;
        }


        public static void FindInStore(String searchString)
        {

            // Windows Phone 8 ONLY
            // Package package = Package.Current;
            // PackageId packageId = package.Id;

            //Search for an application, which is the default content type.
            MarketplaceSearchTask marketplaceSearchTask = new MarketplaceSearchTask();

            //  marketplaceSearchTask.SearchTerms = PackageId.Author;

            marketplaceSearchTask.SearchTerms = searchString;

            marketplaceSearchTask.Show();
        }

        public static void ShareSocial(String messageTitle, String message, String searchLink, String appLink)
        {
            ShareLinkTask shareLinkTask = new ShareLinkTask();
            shareLinkTask.Title = messageTitle;
            if (appLink != "")
                shareLinkTask.LinkUri = new Uri(appLink, UriKind.Absolute);
            else
                shareLinkTask.LinkUri = new Uri(searchLink, UriKind.Absolute);
            shareLinkTask.Message = message;
            shareLinkTask.Show();
        }

        public static void SendSMS(String to, String body)
        {
            SmsComposeTask composeSMS = new SmsComposeTask();
            composeSMS.Body = body;
            composeSMS.To = to;
            composeSMS.Show();
        }

        public static void SendEmail(String to, String subject, String body)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();

            emailComposeTask.Subject = subject;

            emailComposeTask.Body = body;
            emailComposeTask.To = to;
            //emailComposeTask.Cc = "cc@example.com";
            //emailComposeTask.Bcc = "bcc@example.com";
            emailComposeTask.Show();
        }
        public static void RateApp()
        {
            MarketplaceReviewTask oRateTask = new MarketplaceReviewTask();
            oRateTask.Show();
        }

    }
}
