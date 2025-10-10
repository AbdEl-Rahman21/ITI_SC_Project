namespace ITI_SC_Project.Helpers
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        public static ServiceResult Ok(string? message = null) => new() { Success = true, Message = message };

        public static ServiceResult Fail(string message) => new() { Success = false, Message = message };
    }
}
