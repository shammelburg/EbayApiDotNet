using eBay.Service.Core.Soap;
using System;

namespace EbayConsole
{
    public class eBayItem
    {
        /// <summary>
        /// Verify whether item is ready to be added to eBay.
        /// </summary>
        public static void VerifyAddItemRequest()
        {
            eBayAPIInterfaceService service = EbayCalls.eBayServiceCall("VerifyAddItem");

            VerifyAddItemRequestType request = new VerifyAddItemRequestType();
            request.Version = "949";
            request.ErrorLanguage = "en_US";
            request.WarningLevel = WarningLevelCodeType.High;

            var item = new ItemType();

            item.Title = "My Title";
            item.Description = "My Description";
            item.PrimaryCategory = new CategoryType
            {
                CategoryID = "11704" // Other DIY Tools
            };
            item.StartPrice = new AmountType
            {
                Value = 7.98,
                currencyID = CurrencyCodeType.GBP
            };
            // To view ConditionIDs follow the URL
            // http://developer.ebay.com/devzone/guides/ebayfeatures/Development/Desc-ItemCondition.html#HelpingSellersChoosetheRightCondition
            item.ConditionID = 1000;
            item.Country = CountryCodeType.GB;
            item.Currency = CurrencyCodeType.GBP;
            item.DispatchTimeMax = 3;
            item.ListingDuration = "Days_7";
            // Buy It Now fixed price
            item.ListingType = ListingTypeCodeType.FixedPriceItem;
            // Auction
            //item.ListingType = ListingTypeCodeType.Chinese; 
            item.PaymentMethods = new BuyerPaymentMethodCodeTypeCollection
            {
                BuyerPaymentMethodCodeType.PayPal
            };
            // Default testing paypal email address
            item.PayPalEmailAddress = "magicalbookseller@yahoo.com";
            item.PictureDetails = new PictureDetailsType
            {
                PictureURL = new StringCollection
                {
                    "https://avatar-ssl.xboxlive.com/avatar/ii%20burg%20ii/avatar-body.png"
                }
            };
            item.PostalCode = "YOUR POSTCODE";
            item.Quantity = 5; // 1 If Auction
            item.ReturnPolicy = new ReturnPolicyType
            {
                ReturnsAcceptedOption = "ReturnsAccepted",
                ReturnsWithinOption = "Days_30",
                //RefundOption = "MoneyBack",
                Description = "PLease return if unstatisfied.",
                ShippingCostPaidByOption = "Buyer"
            };
            item.ShippingDetails = new ShippingDetailsType
            {
                ShippingType = ShippingTypeCodeType.Flat,
                ShippingServiceOptions = new ShippingServiceOptionsTypeCollection
                {
                     new ShippingServiceOptionsType {
                         ShippingServicePriority = 1,
                         // To find a shipping service follow the URL
                         // https://developer.ebay.com/devzone/xml/docs/Reference/ebay/types/ShippingServiceCodeType.html
                         ShippingService = "UK_Parcelforce48",
                         ShippingServiceCost = new AmountType {
                             Value = 2.50,
                             currencyID = CurrencyCodeType.GBP
                         }
                     }
                }
            };
            item.Site = SiteCodeType.UK;

            request.Item = item;

            VerifyAddItemResponseType response = service.VerifyAddItem(request);
            Console.WriteLine("ItemID: {0}", response.ItemID);

            // If item is verified, the item will be added.
            if (response.ItemID == "0")
            {
                Console.WriteLine("=====================================");
                Console.WriteLine("Add Item Verified");
                Console.WriteLine("=====================================");
                AddItemRequest(item);
            }
        }

        /// <summary>
        /// Add item to eBay. Once verified.
        /// </summary>
        /// <param name="item">Accepts ItemType object from VerifyAddItem method.</param>
        public static void AddItemRequest(ItemType item)
        {
            eBayAPIInterfaceService service = EbayCalls.eBayServiceCall("AddItem");

            AddItemRequestType request = new AddItemRequestType();
            request.Version = "949";
            request.ErrorLanguage = "en_US";
            request.WarningLevel = WarningLevelCodeType.High;
            request.Item = item;

            AddItemResponseType response = service.AddItem(request);

            Console.WriteLine("Item Added");
            Console.WriteLine("ItemID: {0}", response.ItemID); // Item ID
        }

        /// <summary>
        /// Retrieve item details.
        /// </summary>
        /// <param name="ItemID">eBay Item ID</param>
        public static void GetItemRequest(string ItemID)
        {
            eBayAPIInterfaceService service = EbayCalls.eBayServiceCall("GetItem");

            GetItemRequestType request = new GetItemRequestType();
            request.Version = "949";
            request.ItemID = ItemID;
            GetItemResponseType response = service.GetItem(request);

            Console.WriteLine("=====================================");
            Console.WriteLine("Item Iitle - {0}", response.Item.Title);
            Console.WriteLine("=====================================");

            Console.WriteLine("ItemID: {0}", response.Item.ItemID);
            Console.WriteLine("Primary Category: {0}", response.Item.PrimaryCategory.CategoryName);
            Console.WriteLine("Listing Duration: {0}", response.Item.ListingDuration);
            Console.WriteLine("Start Price: {0} {1}", response.Item.StartPrice.Value, response.Item.Currency);
            Console.WriteLine("Payment Type[0]: {0}", response.Item.PaymentMethods[0]);
            Console.WriteLine("PayPal Email Address: {0}", response.Item.PayPalEmailAddress);
            Console.WriteLine("Postal Code: {0}", response.Item.PostalCode);
            // ...Convert response object to JSON to see all
        }
    }
}
