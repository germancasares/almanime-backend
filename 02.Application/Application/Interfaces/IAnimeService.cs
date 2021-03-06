﻿using Domain.DTOs;
using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IAnimeService
    {
        Anime GetByID(Guid guid);
        Anime GetByKitsuID(int kitsuID);
        Anime GetBySlug(string slug);

        Anime Create(AnimeDTO animeDTO);
        void Update(AnimeDTO animeDTO);
        IEnumerable<Anime> GetSeason(int year, ESeason season);
        int GetAnimesInSeason(int year, ESeason season);
    }
}
