
using static vrms_api.Models.ShareModel;

namespace vrms_api.Helper
{
    public static class Utl
    {
        public static double Zvl(string value, double defaultValue = 0)
        {
            return double.TryParse(value, out var parsedValue) ? parsedValue : defaultValue;
        }

        // convert int to 
        public static int Zvl(string value, int defaultValue = 0)
        {
            return int.TryParse(value, out var parsedValue) ? parsedValue : defaultValue;
        }
        public static ApiResponse<T> ToApiResponse<T>(this T data, string message = null, int statusCode = StatusCodes.Status200OK)
        {
            return new ApiResponse<T>
            {
                ResponseType = ResponseType.Success,
                StatusCode = statusCode,
                Message = message,
                Result = data
            };
        }
    }
}
