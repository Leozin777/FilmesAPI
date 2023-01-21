using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    private static List<Filme> Filmes = new List<Filme>();
    private static int id = 0;

    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] Filme p_filme)
    {
        p_filme.Id = id++;
        Filmes.Add(p_filme);
        return CreatedAtAction(nameof(RecuperaFilmePorId),
                new { p_id = p_filme.Id },
                p_filme);
    }

    [HttpGet]
    public IEnumerable<Filme> RecuperaFilmes([FromQuery]int p_skip = 0, [FromQuery]int p_take = 50)
    {
        return Filmes.Skip(p_skip).Take(p_take);
    }

    [HttpGet("{p_id}")]
    public IActionResult RecuperaFilmePorId(int p_id)
    {
        var filme = Filmes.FirstOrDefault(filme => filme.Id == p_id);

        if(filme == null) return NotFound();

        return Ok(filme);
    }
}
