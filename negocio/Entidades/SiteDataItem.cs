namespace negocio.Entidades
{
    public class SiteDataItem
    {
        private string text;
        private int id;
        private int parentId;
        private string value;
        public string Value
        {
            get { return value; }
            set { value = value; }
        }
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public int ParentID
        {
            get { return parentId; }
            set { parentId = value; }
        }

        public SiteDataItem(int id, int parentId, string text,string value)
        {
            this.id = id;
            this.parentId = parentId;
            this.text = text;
            this.value = value;
        }
    }
}