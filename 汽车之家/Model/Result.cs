namespace Model
{
    public class ViewResult
    {
        public bool Result { get; set; }

        public bool Exit { get; set; }

        public string Message { get; set; }
        private bool _ref = true;
        public bool Ref { get { return _ref; } set { _ref = value; } }
    }
}
