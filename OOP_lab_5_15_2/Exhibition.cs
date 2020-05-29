namespace OOP_lab_5_15_2
{
    class Exhibition
    {
        private string _name;
        private string _sculptorSurename;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string SculptorSurename
        {
            get => _sculptorSurename;
            set => _sculptorSurename = value;
        }

        public virtual string UkrainianI(string str)
        {
            char[] ch = str.ToCharArray();

            for (int i = 0; i < ch.Length; ++i)
            {
                if (ch[i] == 'і')
                {
                    ch[i] = 'i';
                }
            }

            return new string(ch);
        }

        public Exhibition()
        {
            _name = "Не вказано";
            _sculptorSurename = "Не вказано";
        }

        public Exhibition(string name, string sculptorSurename)
        {
            _name = UkrainianI(name);
            _sculptorSurename = UkrainianI(sculptorSurename);
        }
    }
}
