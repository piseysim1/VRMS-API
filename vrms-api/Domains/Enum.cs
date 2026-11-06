namespace vrms_api.Domains
{
    public enum LookUpTypes
    {
        BookingStatus = 1,
        RateType = 2,
        VehicleStatus = 3,
        Color = 4,
        BusinessSource = 5

    }
    public enum Color
    {
        White = 8,
        Black = 7,
        Silver = 9,
        Gray = 11,
        Red = 3,
        Blue = 10,
        Green = 186,
        Yellow = 187,
        Orange = 188,
        Brown = 189,
        Beige = 190,
        Gold = 191,
        
    }
    public enum VehicleStatus
    {
        Available = 4,
        Unavailable = 5,
        Rented = 6
    }
    public enum RateType
    {
        Hour = 1,
        Day = 2
    }
    public enum RoleEnum: long
    {
        Admin = 1,
        User = 2
    }
    public static class RoleNames
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }

}
