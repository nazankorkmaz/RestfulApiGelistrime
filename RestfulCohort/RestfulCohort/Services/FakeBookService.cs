using RestfulCohort.Models;
public class FakeBookService : IBookService
{
    private static List<Book> Books = new List<Book>();

    public List<Book> GetAll(string? author, bool? available)
    {
        var result = Books.AsQueryable();
        if (!string.IsNullOrEmpty(author))
            result = result.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
        if (available.HasValue)
            result = result.Where(b => b.IsAvailable == available.Value);

        return result.ToList();
    }

    public Book? GetById(int id) => Books.FirstOrDefault(b => b.Id == id);

    public void Create(Book book)
    {
        book.Id = Books.Count + 1;
        Books.Add(book);
    }

    public void Update(int id, Book book)
    {
        var existingBook = Books.FirstOrDefault(b => b.Id == id);
        if (existingBook != null)
        {
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.PageNumber = book.PageNumber;
            existingBook.IsAvailable = book.IsAvailable;
        }
    }

    public void Delete(int id)
    {
        var book = Books.FirstOrDefault(b => b.Id == id);
        if (book != null)
            Books.Remove(book);
    }
}
