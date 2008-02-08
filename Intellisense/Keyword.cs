namespace Intellisense
{
    public struct Keyword
    {
        private string alias;
        private string name;
        private string membre;

        public Keyword(string name, string alias, string membre)
        {
            this.name = name;
            this.alias = alias;
            this.membre = membre;
        }

        public string Alias
        {
            get { return alias; }
            set { alias = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Membre
        {
            get { return membre; }
            set { membre = value; }
        }
    }
}