﻿using Persistence.Data.Repositories.Interfaces;
using System;

namespace Persistence.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();

        IAnimeRepository Animes { get; }
        IBookmarkRepository Bookmarks { get; }
        IEpisodeRepository Episodes { get; }
        IFansubRepository Fansubs { get; }
        IMembershipRepository Memberships { get; }
        IStorageRepository Storage { get; }
        ISubtitleRepository Subtitles { get; set; }
        ISubtitlePartialRepository SubtitlePartials { get; set; }
        IUserRepository Users { get; }
    }
}
