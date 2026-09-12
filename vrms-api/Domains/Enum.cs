namespace vrms_api.Domains
{
    public enum LookupTypes
    {
        BookingStatus = 1,
        RateType = 2,
        VehicleStatus = 3,
        Color = 4,

    }
    public enum Color
    {
        White = 180,
        Black = 181,
        Silver = 182,
        Gray = 183,
        Red = 184,
        Blue = 185,
        Green = 186,
        Yellow = 187,
        Orange = 188,
        Brown = 189,
        Beige = 190,
        Gold = 191,
        Bronze = 192,   
        Maroon = 193,
        PearlWhite = 194,
        MetallicSilver = 195,
        GunmetalGray = 196,
        MatteBlack = 197,
        ElectricBlue = 198,
        DeepPurple = 199,
        CandyRed = 200,
        LimeGreen = 201,
        Copper = 202,
        Turquoise = 203,
        Champagne = 204,
        MidnightBlue = 205,
        SunsetOrange = 206,
        Burgundy = 207,
        Teal = 208
    }
    public enum VehicleStatus
    {
        Available = 1,
        Unavailable = 2,
        Rented = 3,
    }
    public enum RateType
    {
        Hour = 1,
        Day = 2
    }
}
