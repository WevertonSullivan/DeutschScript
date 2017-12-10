namespace Common
{
    class DS_C
    {
        string _C, _DS;

        public DS_C(string DS, string C)
        {
            this._C = C;
            this._DS = DS;
        }

        public string C { get => _C; set => _C = value; }
        public string DS { get => _DS; set => _DS = value; }
    }
}
