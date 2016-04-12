using System;

namespace model
{
    [Serializable]
    public class Book
    {

        private String author, title;
        private int id, available;

        public Book(int id, String author, String title, int available)
        {
            this.id = id;
            this.author = author;
            this.title = title;
            this.available = available;
        }

        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Available
        {
            get { return available; }
            set { available = value; }
        }

        public override String ToString()
        {
            return "[" + author + "," + title + "," + available + "]";
        }
    }
}
