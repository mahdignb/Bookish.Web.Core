namespace WebAPI.Models
{
    public class EnableTwoStepModel
    {
        public bool EnableTwoStep { get; set; }
        public string? TwoStepCode { get; set; }
    }
}
