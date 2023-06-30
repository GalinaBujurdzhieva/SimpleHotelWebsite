namespace MyHotelWebsite.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "MyHotelWebsite";

        // ROLE CONSTANTS
        public const string HotelManagerRoleName = "Manager"; // ACCESS TO ALLCONTROLLERS

        public const string WebsiteAdministratorRoleName = "WebAdministrator";  // ACCESS TO BLOGCONTROLLER & GUESTCOMMENTSCONTROLLER IF THERE IS

        public const string ReceptionistRoleName = "Receptionist"; // ACCESS TO RESERVATIONSCONTROLLER

        public const string ChefRoleName = "Chef";  // ACCESS TO DISHESCONTROLLER

        public const string MaidRoleName = "Maid";  // ACCESS TO ROOMSCONTROLLER

        public const string WaiterRoleName = "Waiter"; // ACCESS TO ORDERSCONTROLLER

        public const string GuestRoleName = "Guest"; // ACCESS TO HIS RESERVATIONS AND ORDERS

        // ROOM PRICES
        public const decimal SingleRoomPrice = 150;

        public const decimal DoubleRoomAdultsPricePerBed = 130;

        public const decimal DoubleRoomChildrenPricePerBed = 80;

        public const decimal StudioAdultsPricePerBed = 170;

        public const decimal StudioChildrenPricePerBed = 100;

        public const decimal ApartmentAdultsPricePerBed = 200;

        public const decimal ApartmentChildrenPricePerBed = 120;
    }
}
