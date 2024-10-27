namespace Domain.Dtos.AppLayerDtos
{
    public sealed record ApiResponseDto
    {
        private ApiResponseDto(bool suc, string msg, object? objt)
        {
            SuccesResponse = suc;
            Message = msg;
            Result = objt;
        }
        public string Message { get; }
        public bool SuccesResponse { get; }
        public object? Result { get; }

        public static ApiResponseDto Success(string msg, object obj) => new(true, msg, obj);

        public static ApiResponseDto Failure(string msg) => new(false, msg, null);
    }
}