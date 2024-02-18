using System.Net;

namespace C_50285_Nardelli_Nancy_Web_Api.Models
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            ErrorMessages = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSucces { get; set; } = true;

        public List<string> ErrorMessages { get; set; }
        public Object Result { get; set; }
    }
}
