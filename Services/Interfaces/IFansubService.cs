﻿using Almanime.Models;
using Almanime.Models.Documents;
using Almanime.Models.DTO;
using Almanime.Models.Enums;

namespace Almanime.Services.Interfaces;

public interface IFansubService
{
    Fansub Create(FansubDTO fansubDTO, string auth0ID);
    IQueryable<Fansub> Get();
    Fansub GetByAcronym(string acronym);
    IEnumerable<Membership> GetMembers(string acronym);
    Dictionary<string, IEnumerable<EPermission>> GetRoles(string acronym);
    IEnumerable<Subtitle> GetSubtitles(string acronym);
    IEnumerable<Subtitle> GetSubtitlesDrafts(string acronym, string auth0ID);
    bool IsMember(string acronym, string auth0ID);
    void Join(string acronym, string auth0ID);
    IReadOnlyCollection<FansubDocument> Search(string fansubName);
    void UpdateRoles(string acronym, string auth0ID, Dictionary<string, IEnumerable<EPermission>> roles);
}
