using RestfulCohort.Models;

public interface IBookService
{
    List<Book> GetAll(string? author, bool? available);
    Book? GetById(int id);
    void Create(Book book);
    void Update(int id, Book book);
    void Delete(int id);
}
