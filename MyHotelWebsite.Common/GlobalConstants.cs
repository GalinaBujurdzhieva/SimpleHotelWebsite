namespace MyHotelWebsite.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "MyHotelWebsite";

        public const string HotelAdministratorRoleName = "Manager"; // ACCESS TO

        public const string WebsiteAdministratorRoleName = "WebAdministrator";  // ACCESS TO BLOGCONTROLLER & GUESTCOMMENTSCONTROLLER ??

        public const string ReceptionistRoleName = "Receptionist"; // ACCESS TO RESERVATIONSCONTROLLER

        public const string ChefRoleName = "Chef";  // ACCESS TO DISHESCONTROLLER

        public const string MaidRoleName = "Maid";  // ACCESS TO ROOMSCONTROLLER

        public const string WaiterRoleName = "Waiter"; // ACCESS TO ORDERSCONTROLLER
    }
}
