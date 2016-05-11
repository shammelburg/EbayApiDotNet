using System;

namespace EbayConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Displays Top Level Categories
                eBayCategory.GetTopLevelCategories();

                // View all categories and IDs
                //eBayCategory.GetAllCategoriesRequest();

                // Verifies item then adds item to ebay.
                //eBayItem.VerifyAddItemRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.ReadLine();
        }
    }
}
