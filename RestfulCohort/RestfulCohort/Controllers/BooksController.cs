using Microsoft.AspNetCore.Mvc;
using RestfulCohort.Models;

namespace RestfulCohort.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
         private static List<Book> Books = new List<Book>();

    // Tüm kitapları listele
    [HttpGet]
    public IActionResult GetAll([FromQuery] string? author, [FromQuery] bool? available)
    {
        var result = Books.AsQueryable();

        if (!string.IsNullOrEmpty(author))
            result = result.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));

        if (available.HasValue)
            result = result.Where(b => b.IsAvailable == available.Value);

        return Ok(result.ToList());
    }

    // ID'ye göre kitap getir
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var book = Books.FirstOrDefault(b => b.Id == id);
        if (book == null)
            return NotFound(new {status = 404, message = "Book not found" });

        return Ok(book);
    }

    // Yeni kitap ekle
    [HttpPost]
    public IActionResult Create([FromBody] Book book)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        book.Id = Books.Count + 1;
        Books.Add(book);

        return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
    }

    // Kitap güncelle
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Book book)
    {
        var existingBook = Books.FirstOrDefault(b => b.Id == id);
        if (existingBook == null)
            return NotFound(new {status = 404, message = "Book not found" });

        existingBook.Title = book.Title;
        existingBook.Author = book.Author;
        existingBook.PageNumber = book.PageNumber;
        existingBook.IsAvailable = book.IsAvailable;


        return StatusCode(201, existingBook); // 201: Created
        //return Ok(existingBook);
    }

    // Kitap sil
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var book = Books.FirstOrDefault(b => b.Id == id);
        if (book == null)
            return NotFound(new {status = 404, message = "Book not found" });

        Books.Remove(book);
        return NoContent();
    }

    [HttpPatch("{id}")] // patch -> title
    public IActionResult UpdatePartial(int id, [FromBody] Book updatedFields)
    {
        var book = Books.FirstOrDefault(b => b.Id == id);
        if (book == null)
            return NotFound(new {status = 404, message = "Book not found" });

        // Sadece gönderilen alanları güncelle
        if (updatedFields.Title != null)
            book.Title = updatedFields.Title;
        
        if (updatedFields.PageNumber != 0)
            book.PageNumber = updatedFields.PageNumber;

        return Ok(book);
    }

    // Kitapları listeleme ve sıralama (Model Binding - [FromQuery])
    [HttpGet("list")]
    public IActionResult GetBooksList(
        [FromQuery] string? author, 
        [FromQuery] bool? available, 
        [FromQuery] string? sortBy)
    {
        var result = Books.AsQueryable();

        // Yazar filtresi
        if (!string.IsNullOrEmpty(author))
            result = result.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));

        // Kullanılabilirlik filtresi
        if (available.HasValue)
            result = result.Where(b => b.IsAvailable == available.Value);

        // Sıralama
        if (!string.IsNullOrEmpty(sortBy))
        {
            if (sortBy.ToLower() == "title")
                result = result.OrderBy(b => b.Title);
            else if (sortBy.ToLower() == "author")
                result = result.OrderBy(b => b.Author);
            else
                result = result.OrderBy(b => b.Id); // Varsayılan olarak Id'ye göre sıralama
        }

        return Ok(result.ToList());
    }

    }
}
