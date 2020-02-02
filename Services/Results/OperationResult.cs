namespace Services.Results
{
    public class OperationResult {
        public int Code { get; set; }
        public object Result { get; set; }
        public bool HasError => this.Error != null && this.Error.Code > 0;
        public GenericError Error { get; set; }
    }
}