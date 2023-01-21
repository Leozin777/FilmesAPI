using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    private static List<Filme> Filmes = new List<Filme>();

    [HttpPost]
    public void AdicionaFilme([FromBody] Filme p_filme)
    {
        Filmes.Add(p_filme);
        Console.WriteLine(p_filme.Titulo);
        Console.WriteLine(p_filme.Duracao);
    }
}
