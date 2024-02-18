namespace C_50285_Nardelli_Nancy_Web_Api.Infrastructure
{
    public class ApiErrorResponse
    {
        public int Status { get; set; }
        public List<ResponseError> Error { get; set; }

        public class ResponseError
        {
            public string Error { get; set; }

        }
    }
}
