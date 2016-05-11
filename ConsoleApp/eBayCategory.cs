using eBay.Service.Core.Soap;
using System;

namespace EbayConsole
{
    public class eBayCategory
    {
        /// <summary>
        /// Returns a list of the top level categories for the UK.
        /// </summary>
        public static void GetTopLevelCategories()
        {
            eBayAPIInterfaceService service = EbayCalls.eBayServiceCall("GetCategories");

            GetCategoriesRequestType request = new GetCategoriesRequestType();
            request.Version = "949";
            request.CategorySiteID = "3"; // UK
            request.LevelLimit = 1; // Top Level
            request.DetailLevel = new DetailLevelCodeTypeCollection { DetailLevelCodeType.ReturnAll };
            GetCategoriesResponseType response = service.GetCategories(request);

            Console.WriteLine("=====================================");
            Console.WriteLine("Top-Level Categories");
            Console.WriteLine("=====================================");
            foreach (dynamic cat in response.CategoryArray)
            {
                Console.WriteLine("{0} - {1}", cat.CategoryID, cat.CategoryName);
            }

            // Uncomment to return sub categories
            /*Will prompt for CategoryID to return Sub categories
            Console.WriteLine("=====================================");
            Console.WriteLine("Please enter a Top-Level Category ID: ");
            Console.WriteLine("=====================================");
            var TopLevel = Console.ReadLine();
            GetCategories2Request(TopLevel);*/
        }

        /// <summary>
        /// Returns a list of Level 2 categories.
        /// </summary>
        /// <param name="TopLevel">Top level category ID</param>
        public static void GetLevel2Categories(string TopLevel)
        {
            eBayAPIInterfaceService service = EbayCalls.eBayServiceCall("GetCategories");

            GetCategoriesRequestType request = new GetCategoriesRequestType();
            
            request.Version = "949";
            request.CategorySiteID = "3";
            request.LevelLimit = 2;
            request.CategoryParent = new StringCollection { TopLevel };
            request.DetailLevel = new DetailLevelCodeTypeCollection { DetailLevelCodeType.ReturnAll };
            GetCategoriesResponseType response = service.GetCategories(request);

            Console.WriteLine("=====================================");
            Console.WriteLine("Level 2 Categories");
            Console.WriteLine("=====================================");

            foreach (dynamic cat in response.CategoryArray)
            {
                Console.WriteLine("{0} - {1}", cat.CategoryID, cat.CategoryName);
            }
        }

        /// <summary>
        /// Get features for a specific category ID
        /// </summary>
        /// <param name="CategoryID">Any CategoryID</param>
        public static void GetCategoryFeaturesRequest(string CategoryID)
        {
            eBayAPIInterfaceService service = EbayCalls.eBayServiceCall("GetCategoryFeatures");

            GetCategoryFeaturesRequestType request = new GetCategoryFeaturesRequestType();
            request.Version = "949";
            request.WarningLevel = WarningLevelCodeType.High;
            request.CategoryID = CategoryID;
            request.FeatureID = new FeatureIDCodeTypeCollection {
                FeatureIDCodeType.ConditionValues,
                FeatureIDCodeType.ListingDurations,
                FeatureIDCodeType.HandlingTimeEnabled,
                FeatureIDCodeType.MaxFlatShippingCost,
                FeatureIDCodeType.PayPalRequired,
                FeatureIDCodeType.BestOfferEnabled,
                FeatureIDCodeType.ReturnPolicyEnabled
            };

            request.DetailLevel = new DetailLevelCodeTypeCollection { DetailLevelCodeType.ReturnAll };
            GetCategoryFeaturesResponseType response = service.GetCategoryFeatures(request);

            Console.WriteLine("=====================================");
            Console.WriteLine("Category Features");
            Console.WriteLine("=====================================");

            Console.WriteLine("Ack: {0}", response.Ack);
            Console.WriteLine("Version: {0}", response.Version);
            Console.WriteLine("Build: {0}", response.Build);
            Console.WriteLine("Category Version: {0}", response.CategoryVersion);
            Console.WriteLine("Update Time: {0}", response.UpdateTime);
            Console.WriteLine("Return Policy Enabled: {0}", response.SiteDefaults.ReturnPolicyEnabled);
            Console.WriteLine("PayPal Required: {0}", response.SiteDefaults.PayPalRequired);
            // ...
        }

        /// <summary>
        /// Returns metadata for a specific Detail.
        /// </summary>
        public static void GeteBayDetailsRequest()
        {
            eBayAPIInterfaceService service = EbayCalls.eBayServiceCall("GeteBayDetails");

            GeteBayDetailsRequestType request = new GeteBayDetailsRequestType();
            request.Version = "949";
            request.DetailName = new DetailNameCodeTypeCollection
            {
                DetailNameCodeType.ReturnPolicyDetails
            };

            GeteBayDetailsResponseType response = service.GeteBayDetails(request);

            Console.WriteLine("=====================================");
            Console.WriteLine("eBay Request Details");
            Console.WriteLine("=====================================");

            Console.WriteLine("Ack: {0}", response.Ack);
            Console.WriteLine("Version: {0}", response.Version);
            Console.WriteLine("Build: {0}", response.Build);
            // ...
        }

        /// <summary>
        /// Get a list of all categories and IDs
        /// This will help with setting the leaf category for you item.
        /// </summary>
        public static void GetAllCategoriesRequest()
        {
            eBayAPIInterfaceService service = EbayCalls.eBayServiceCall("GetCategories");

            GetCategoriesRequestType request = new GetCategoriesRequestType();
            request.Version = "949";
            request.CategorySiteID = "3";
            request.DetailLevel = new DetailLevelCodeTypeCollection { DetailLevelCodeType.ReturnAll };
            GetCategoriesResponseType response = service.GetCategories(request);

            Console.WriteLine("=====================================");
            Console.WriteLine("CategoryID - Name List");
            Console.WriteLine("=====================================");

            foreach (dynamic cat in response.CategoryArray)
            {
                Console.WriteLine("{0} - {1}", cat.CategoryID, cat.CategoryName);
            }
        }
    }
}
