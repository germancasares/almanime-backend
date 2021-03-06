﻿using Application.Interfaces;
using AutoMapper;
using Domain.Constants;
using Domain.DTOs;
using Domain.Enums;
using Domain.Models;
using Domain.VMs;
using Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application
{
    public class FansubService : IFansubService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<FansubService> _logger;

        public FansubService(
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            ILogger<FansubService> logger
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public bool ExistsFullName(string fullname) => _unitOfWork.Fansubs.GetByFullName(fullname) != null;

        public bool ExistsAcronym(string acronym) => _unitOfWork.Fansubs.GetByAcronym(acronym) != null;

        public Fansub GetByID(Guid ID) => _unitOfWork.Fansubs.GetByID(ID);

        public Fansub GetByAcronym(string acronym) => _unitOfWork.Fansubs.GetByAcronym(acronym);

        public IEnumerable<FansubAnimeVM> GetCompletedAnimes(string acronym)
        {
            var animes = _unitOfWork.Animes.GetCompletedByFansub(acronym);

            return animes.Select(a => new FansubAnimeVM
            {
                Slug = a.Slug,
                Name = a.Name,
                CoverImage = a.CoverImageUrl,
                FinishedDate = a.Episodes.Select(e => e.Subtitles.SingleOrDefault(s => s.Fansub.Acronym == acronym).CreationDate).OrderByDescending(d => d).First()
            });
        }

        public IEnumerable<FansubEpisodeVM> GetCompletedEpisodes(string acronym)
        {
            var episodes = _unitOfWork.Episodes.GetByFansub(acronym);

            return episodes.Select(e => new FansubEpisodeVM
            {
                AnimeSlug = e.Anime.Slug,
                AnimeName = e.Anime.Name,
                AnimeCoverImage = e.Anime.CoverImageUrl,
                Number = e.Number,
                Name = e.Name,
                FinishedDate = e.Subtitles.SingleOrDefault(s => s.Fansub.Acronym == acronym).CreationDate,
            });
        }

        public IEnumerable<FansubUserVM> GetMembers(string acronym)
        {
            var users = _unitOfWork.Users.GetByFansub(acronym);

            return users.Select(u=> new FansubUserVM
            {
                Name = u.Name,
                AvatarUrl = u.AvatarUrl,
                Role = u.Memberships.SingleOrDefault(m => m.Fansub.Acronym == acronym).Role,
            });
        }

        public Fansub Create(FansubDTO fansubDTO, Guid identityID)
        {
            var user = _unitOfWork.Users.GetByIdentityID(identityID);
            if (user == null)
            {
                _logger.Emit(ELoggingEvent.UserDoesntExist, new { UserIdentityID = identityID });
                throw new ArgumentException(nameof(identityID));
            }

            var fansub = _unitOfWork.Fansubs.Create(_mapper.Map<Fansub>(fansubDTO));

            _unitOfWork.Memberships.Create(new Membership
            {
                FansubID = fansub.ID,
                UserID = user.ID,
                Role = EFansubRole.Founder
            });

            _unitOfWork.Save();

            return fansub;
        }

        public void Delete(Guid fansubID, Guid identityID)
        {
            var user = _unitOfWork.Users.GetByIdentityID(identityID);
            if (user == null)
            {
                _logger.Emit(ELoggingEvent.UserDoesntExist, new { UserIdentityID = identityID });
                throw new ArgumentException(nameof(identityID));
            }

            var fansub = _unitOfWork.Fansubs.GetByID(fansubID);
            if (fansub == null)
            {
                _logger.Emit(ELoggingEvent.FansubDoesNotExist, new { FansubID = fansubID });
                throw new ArgumentException(nameof(fansubID));
            }

            if (!_unitOfWork.Memberships.IsFounder(fansubID, user.ID))
            {
                _logger.Emit(ELoggingEvent.UserIsNotFounder, new { UserIdentityID = identityID, FansubID = fansubID });
                throw new ArgumentException(ExceptionMessage.OnlyFounderCanDeleteFansub);
            }

            _unitOfWork.Fansubs.DeleteMembers(fansubID);
            _unitOfWork.Fansubs.Delete(fansubID);
            _unitOfWork.Save();
        }
    }
}
