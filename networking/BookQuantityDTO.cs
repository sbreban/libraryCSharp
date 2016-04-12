using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace networking
{
    [Serializable]
    public class BookQuantityDTO
    {
        private int bookId;
        private int newQuantity;

        public BookQuantityDTO(int bookId, int newQuantity)
        {
            this.bookId = bookId;
            this.newQuantity = newQuantity;
        }

        public int BookId
        {
            get { return bookId; }
            set { bookId = value; }
        }

        public int NewQuantity
        {
            get { return newQuantity; }
            set { newQuantity = value; }
        }
    }
}
