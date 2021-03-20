namespace DevPlatform.Domain.ServiceResponseModels.DatabaseService
{
    /// <summary>
    /// Install database response model
    /// </summary>
    public class InstallResponse
    {
        public string Message { get; set; }
        public bool Succeeded { get; set; }
    }
}
