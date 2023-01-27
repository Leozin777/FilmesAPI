﻿using AutoMapper;
using FilmesAPI.DTOs;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles;

public class FilmeProfile : Profile
{
	public FilmeProfile()
	{
		CreateMap<CreateFilmeDTO, Filme>();
		CreateMap<UpdateFilmeDTO, Filme>();
		CreateMap<Filme, UpdateFilmeDTO>();
    }
}
