using Microsoft.AspNetCore.Mvc;
using RestfulCohort.Models;

namespace RestfulCohort.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public IActionResult GetAll([FromQuery] string? author, [FromQuery] bool? available)
    {
        return Ok(_bookService.GetAll(author, available));
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var book = _bookService.GetById(id);
        if (book == null)
            return NotFound(new { status = 404, message = "Book not found" });

        return Ok(book);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Book book)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _bookService.Create(book);
        return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Book book)
    {
        _bookService.Update(id, book);
        return StatusCode(201);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _bookService.Delete(id);
        return NoContent();
    }
}
