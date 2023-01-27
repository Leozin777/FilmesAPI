using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.DTOs;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDTO p_filmeDto)
    {
        Filme m_filme = _mapper.Map<Filme>(p_filmeDto);
        _context.Filmes.Add(m_filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaFilmePorId),
                new { p_id = m_filme.Id },
                m_filme);
    }

    [HttpGet]
    public IEnumerable<Filme> RecuperaFilmes([FromQuery]int p_skip = 0, [FromQuery]int p_take = 50)
    {
        return _context.Filmes.Skip(p_skip).Take(p_take);
    }

    [HttpGet("{p_id}")]
    public IActionResult RecuperaFilmePorId(int p_id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == p_id);

        if(filme == null) return NotFound();

        return Ok(filme);
    }

    [HttpPut("{p_id}")]
    public IActionResult AtualizaFilme(int p_id, [FromBody] UpdateFilmeDTO p_filmeDto)
    {
        var m_filmeDoBanco = _context.Filmes.FirstOrDefault(m_filmeDoBanco => m_filmeDoBanco.Id == p_id);

        if(m_filmeDoBanco == null)
            return NotFound();

        _mapper.Map(p_filmeDto, m_filmeDoBanco);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPatch("{p_id}")]
    public IActionResult AtualizaFilmeParcial(int p_id, JsonPatchDocument<UpdateFilmeDTO> p_patch)
    {
        var m_filmeDoBanco = _context.Filmes.FirstOrDefault(m_filmeDoBanco => m_filmeDoBanco.Id == p_id);

        if(m_filmeDoBanco == null) return NotFound();

        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDTO>(m_filmeDoBanco);
        p_patch.ApplyTo(filmeParaAtualizar, ModelState);

        if (!TryValidateModel(filmeParaAtualizar))
            return ValidationProblem(ModelState);

        _mapper.Map(filmeParaAtualizar, m_filmeDoBanco);
        _context.SaveChanges();
        return NoContent();
    }
}
