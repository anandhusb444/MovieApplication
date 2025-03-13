namespace MovieApplication.Respones
{
    public class GenericRespones<T>
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public T data { get; set; }
        public string error { get; set; }

        //need to check that if i can use the record rather then the class.
        public GenericRespones(int statusCode, string message,T Data,string error)
        {
            statusCode = this.statusCode;
            message = this.message;
            Data = this.data;
            error = this.error;
        }
    }
}
