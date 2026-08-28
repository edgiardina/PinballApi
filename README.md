# PinballApi - .NET API for pinball data  

[![NuGet](https://img.shields.io/nuget/v/PinballApi.svg)](https://www.nuget.org/packages/PinballApi/)
[![dotnet build & test](https://github.com/edgiardina/PinballApi/actions/workflows/main.yml/badge.svg)](https://github.com/edgiardina/PinballApi/actions/workflows/main.yml)

.NET client library wrapping pinball data sources: **IFPA/WPPR**, **OPDB**, **IPDB**, and **MatchPlay**.

> ### Breaking change in 4.0.0: OPDB moved to MatchPlay
>
> OPDB shut its own API endpoints down on **1 October 2026**. The database is still alive and
> still gets weekly updates, but the data now comes from the MatchPlay API.
>
> **`OPDBApi`, `IOpdbApi` and every model under `PinballApi.Models.OPDB` were removed in 4.0.0.**
> Those endpoints no longer answer, so the wrapper could not keep working. Use `MatchPlayApi`
> instead. See [OPDB & PinTips](#opdb--pintips-via-matchplay) for the replacement calls and
> [Migrating from `OPDBApi`](#migrating-from-opdbapi) for the mapping.

## Data Sources

| Source | What it provides | Auth required |
|--------|-----------------|---------------|
| [IFPA](https://www.ifpapinball.com/) | Player rankings, tournament results, series standings (WPPR system) | [API key](https://www.ifpapinball.com/api/request_api_key.php) |
| [MatchPlay Events](https://app.matchplay.events) | Tournament software, ratings, OPDB machine data, PinTips | [API token](https://app.matchplay.events/account/tokens) |
| [IPDB](https://www.ipdb.org/) | Classic pinball machine database | None |

[OPDB](https://opdb.org/) machine data is served through MatchPlay. The opdb.org API shut down on
1 October 2026 and this library no longer wraps it.

## Installation

```
dotnet add package PinballApi
```

## Quick Start

### IFPA / WPPR (Universal API — use this)

```csharp
using PinballApi;
using PinballApi.Models.WPPR.Universal.Rankings;

var api = new PinballRankingApi("YOUR_IFPA_API_KEY");

// Player lookup
var player = await api.GetPlayer(16927);
Console.WriteLine($"{player.FirstName} {player.LastName} — Rank #{player.WpprRank}");

// Tournament search near a location
var tournaments = await api.TournamentSearch(
    latitude: 41.8240, longitude: -71.4128, radius: 50,
    distanceType: DistanceType.Miles,
    startDate: DateTime.Now, endDate: DateTime.Now.AddMonths(3));

// Rankings
var wpprTop100 = await api.RankingSearch(RankingType.Wppr, count: 100);
var womenTop50  = await api.RankingSearch(RankingType.Women, RankingSystem.Open, count: 50);

// Series (e.g. NACS)
var regions = await api.GetRegions("NACS", DateTime.Now.Year);
var standings = await api.GetSeriesStandingsForRegion("NACS", "RI", 2024);

// Directory data
var countries = await api.GetCountriesList();
var stateProvs = await api.GetStateProvList();
```

### OPDB (via MatchPlay)

```csharp
using PinballApi;
using PinballApi.Models.MatchPlay.Opdb;

var matchPlay = new MatchPlayApi("YOUR_MATCHPLAY_TOKEN");

// One machine, with credits and images
var machine = await matchPlay.GetOpdbEntry("G4ODR-MLzY7", includePeople: true, includeImages: true);
Console.WriteLine($"{machine.Name} ({machine.Manufacturer.Name}, {machine.Year})");

// Playing tips
var tips = await matchPlay.GetPinTipsByOpdbId("G4ODR");

// Split an OPDB id into its group, machine and alias parts
var parts = OpdbIdParts.Parse("G0l8P-M85d9-A1ZNY");   // parts.EntryType == OpdbEntryType.Alias
```

### IPDB

```csharp
using PinballApi;

var ipdb = new PinballMachineApi();
var machine = await ipdb.GetMachineByIpdbId(3648);
```

### MatchPlay

```csharp
using PinballApi;

var matchPlay = new MatchPlayApi("YOUR_MATCHPLAY_TOKEN");
var profile = await matchPlay.GetProfile(12345);

// Let the client wait out a rate limit window instead of throwing on HTTP 429.
// A wait can last a full minute, so leave this off when you cannot block.
var patient = new MatchPlayApi("YOUR_MATCHPLAY_TOKEN", rateLimitRetryCount: 2);
```

Every call takes a `CancellationToken`, and every paged endpoint has an `Enumerate` twin that
walks the pages for you:

```csharp
await foreach (var tournament in matchPlay.EnumerateTournaments(playedUserId: 12345, cancellationToken: token))
{
    Console.WriteLine(tournament.Name);
}
```

A failed call raises `PinballApiException`, so the HTTP layer stays out of your code:

```csharp
try
{
    var tournament = await matchPlay.GetTournament(999999999);
}
catch (PinballApiException ex) when (ex.IsNotFound)
{
    // ex.StatusCode, ex.ResponseBody and ex.RequestUrl are all available.
    // ex.IsRateLimited and ex.IsUnauthorized cover the other common cases.
}
```

## IFPA API Coverage

The `PinballRankingApi` class implements `IPinballRankingApi` and covers these endpoint groups from the [IFPA API 2.1 spec](https://api.ifpapinball.com/docs/):

### Players
| Method | IFPA Endpoint |
|--------|---------------|
| `GetPlayer(id)` | `GET /player/{id}` |
| `GetPlayers(ids)` | `GET /player` |
| `PlayerSearch(name, country, stateProv, tournament, tournamentPosition)` | `GET /player/search` |
| `GetPlayerResults(id, system, type)` | `GET /player/{id}/results/{ranking_system}/{type}` |
| `GetPlayerHistory(id, system, activeOnly)` | `GET /player/{id}/rank_history` |
| `GetPlayerVersusPlayer(id, system)` | `GET /player/{id}/pvp` |
| `GetPlayerVersusPlayerComparison(id, id2)` | `GET /player/{id}/pvp/{id2}` |

### Rankings
| Method | IFPA Endpoint |
|--------|---------------|
| `RankingSearch(type, system, count, startPos, country)` | `GET /rankings/{type}` |
| `ProRankingSearch(system)` | `GET /rankings/pro/{ranking_system}` |
| `GetRankingCountries()` | `GET /rankings/country_list` |
| `GetCustomRankings()` | `GET /rankings/custom/list` |
| `GetCustomRankingViewResult(id, count, startPos)` | `GET /rankings/custom/{id}` |

### Tournaments
| Method | IFPA Endpoint |
|--------|---------------|
| `GetTournament(id)` | `GET /tournament/{id}` |
| `TournamentSearch(...)` | `GET /tournament/search` |
| `GetTournamentResults(id)` | `GET /tournament/{id}/results` |
| `GetTournamentFormats()` | `GET /tournament/formats` |
| `GetRelatedTournaments(id)` | `GET /tournament/{id}/related` |

### Series
| Method | IFPA Endpoint |
|--------|---------------|
| `GetSeries()` | `GET /series/list` |
| `GetRegions(code, year)` | `GET /series/{code}/regions` |
| `GetSeriesOverallStanding(code, year)` | `GET /series/{code}/overall_standings` |
| `GetSeriesStandingsForRegion(code, region, year)` | `GET /series/{code}/standings` |
| `GetSeriesTournamentsForRegion(code, region, year)` | `GET /series/{code}/tournaments` |
| `GetSeriesPlayerCard(playerId, code, region, year)` | `GET /series/{code}/player_card/{playerId}` |
| `GetSeriesWinners(code, region)` | `GET /series/{code}/past_winners` |
| `GetRegionReps(code)` | `GET /series/{code}/region_reps` |
| `GetSeriesStats(code, region, year)` | `GET /series/{code}/stats` |

### Directors
| Method | IFPA Endpoint |
|--------|---------------|
| `GetDirector(id)` | `GET /director/{id}` |
| `GetDirectorTournaments(id, period)` | `GET /director/{id}/tournaments/{time_period}` |
| `GetCountryDirectors()` | `GET /director/country` |
| `GetDirectorsBySearch(name, count)` | `GET /director/search` |

### Stats
| Method | IFPA Endpoint |
|--------|---------------|
| `GetOverallStatistics()` | `GET /stats/overall` |
| `GetEventsByYearStatistics(system)` | `GET /stats/events_by_year` |
| `GetLargestTournamentStatistics(system)` | `GET /stats/largest_tournaments` |
| `GetLucrativeTournamentStatistics(system)` | `GET /stats/lucrative_tournaments` |
| `GetPlayersByYearStatistics()` | `GET /stats/players_by_year` |
| `GetPlayersByStateStatistics(system)` | `GET /stats/state_players` |
| `GetTournamentsByStateStatistics(system)` | `GET /stats/state_tournaments` |
| `GetPlayersByCountryStatistics(system)` | `GET /stats/country_players` |
| `GetPlayersPointsByGivenPeriod(start, end, system, limit)` | `GET /stats/points_given_period` |
| `GetPlayersEventsAttendedByGivenPeriod(start, end, system, limit)` | `GET /stats/events_attended_period` |

### Other / Reference
| Method | IFPA Endpoint |
|--------|---------------|
| `GetCountriesList()` | `GET /other/countries` |
| `GetStateProvList()` | `GET /other/stateprovs` |

## MatchPlay API Coverage

`MatchPlayApi` implements `IMatchPlayApi`, so you can inject it and replace it in tests.

### Tournaments & games
| Method | MatchPlay Endpoint |
|--------|--------------------|
| `GetTournaments(...)` | `GET /api/tournaments` |
| `GetTournament(id, include...)` | `GET /api/tournaments/{id}` |
| `GetStandings(id)` | `GET /api/tournaments/{id}/standings` |
| `GetRounds(id)` | `GET /api/tournaments/{id}/rounds` |
| `GetGames(...)` | `GET /api/games` |
| `GetSinglePlayerGames(...)` | `GET /api/tournaments/{id}/single-player-games` |
| `GetCards(...)` | `GET /api/tournaments/{id}/cards` |
| `GetIfpaEstimate(...)` | `POST /api/ifpa/wppr-estimator` |

### Resolving ids
MatchPlay returns bare `playerId` and `arenaId` values to keep responses small. Ask for the
tournament players and arenas with the include flags first, then resolve whatever ids are left.
Each call takes up to 25 ids (`MatchPlayApi.MaxResolveIds`).

| Method | MatchPlay Endpoint |
|--------|--------------------|
| `ResolveUnknownPlayers(ids)` | `GET /api/players/resolve-unknown` |
| `ResolveUnknownArenas(ids)` | `GET /api/arenas/resolve-unknown` |
| `ResolveUnknownUsers(ids)` | `GET /api/users/resolve-unknown` |
| `ResolveUnknownTournamentPlayers(id, ids)` | `GET /api/tournaments/{id}/players/resolve-unknown` |
| `ResolveUnknownTournamentArenas(id, ids)` | `GET /api/tournaments/{id}/arenas/resolve-unknown` |

The tournament variants also fill in the pivot data, such as the player seed and the arena label.

### Summaries
These need a completed tournament. MatchPlay returns an empty list for one that is still open.

| Method | MatchPlay Endpoint |
|--------|--------------------|
| `GetTournamentArenaSummary(id)` | `GET /api/tournaments/{id}/summary/arenas` |
| `GetTournamentPlayerArenaSummary(id)` | `GET /api/tournaments/{id}/summary/player-arenas` |
| `GetTournamentMatchSummary(id)` | `GET /api/tournaments/{id}/summary/matches` |

## OPDB & PinTips (via MatchPlay)

`MatchPlayApi` covers the [OPDB and PinTips endpoints](https://docs.matchplay.events/opdb-and-pintips-api)
that replaced the opdb.org API.

| Method | MatchPlay Endpoint |
|--------|--------------------|
| `GetOpdbEntry(opdbId, includePeople, includeImages)` | `GET /api/opdb/entry/{opdbId}` |
| `GetOpdbChangelog()` | `GET /api/opdb/changelog` |
| `GetPinTipsByOpdbId(opdbId)` | `GET /api/pintips?opdbId=` |
| `GetPinTipsByArenaId(arenaId)` | `GET /api/pintips?arenaId=` |

An OPDB entry is a machine group, a machine or an alias. Read `OpdbEntry.EntryType` to tell them
apart, or use the `IsMachineGroup`, `IsMachine` and `IsAlias` helpers. `OpdbIdParts.Parse()` and
`OpdbIdParts.TryParse()` split an OPDB id into its group, machine and alias parts.

### Data exports

MatchPlay asks that you do **not** call the per-entry endpoints in a loop. Download an export
once, store it, and serve searches and typeaheads from your own store. The exports need no API
token and are hosted on a CDN.

| Method | Contents |
|--------|----------|
| `GetOpdbExport()` | Every OPDB entry, about 5 MB. Returns `List<OpdbEntry>`. |
| `GetOpdbSlimExport()` | Name, manufacturer and backglass image only, about 2 MB. Returns `List<OpdbSlimEntry>`. |
| `GetPinTipsExport()` | Every PinTip, about 1 MB. Returns `List<PinTip>`. |

The raw URLs are also public as `MatchPlayApi.OpdbExportUrl`, `MatchPlayApi.OpdbSlimExportUrl`,
`MatchPlayApi.PinTipsExportUrl` and `MatchPlayApi.OpdbLegacyExportUrl`.

```csharp
var machines = await matchPlay.GetOpdbSlimExport();
var backglass = machines
    .Where(m => m.EntryType == OpdbEntryType.Machine && m.PrimaryBackglassImage != null)
    .ToDictionary(m => m.OpdbId, m => m.PrimaryBackglassImage.Urls.Medium);
```

### Migrating from `OPDBApi`

`OPDBApi` and `IOpdbApi` were removed in 4.0.0. Swap the client for `MatchPlayApi` and use a
[MatchPlay API token](https://app.matchplay.events/account/tokens) in place of the OPDB token.

| Removed call | Replacement |
|--------------|-------------|
| `GetMachineInfo(opdbId)` | `MatchPlayApi.GetOpdbEntry(opdbId)` |
| `Export()` | `MatchPlayApi.GetOpdbExport()` |
| `GetMachineInfoByIpdbId(ipdbId)` | No endpoint. Index `GetOpdbExport()` by `OpdbEntry.IpdbId`. |
| `Search(query)` | No endpoint. Search your own copy of `GetOpdbExport()`. |
| `TypeAheadSearch(query)` | Removed by OPDB on purpose. Serve typeahead from `GetOpdbSlimExport()`. |

The model shape also changed. `PinballApi.Models.OPDB.PinballMachine` became
`PinballApi.Models.MatchPlay.Opdb.OpdbEntry`:

| Old member | New member |
|------------|------------|
| `OpdbId` | `OpdbId`, plus `OpdbGroup` and `OpdbMachine` for the parent ids |
| `IsMachine` / `IsAlias` | `EntryType`, or the `IsMachine`, `IsMachineGroup` and `IsAlias` helpers |
| `PhysicalMachine` (`int`) | `PhysicalMachine` (`bool`) |
| `Shortname` | `ShortName` |
| `ManufactureDate` (`DateTime`) | `ManufactureDate` (`DateTime?`), plus `Year` |
| `Features` (`List<string>`) | `Features` (`List<OpdbFeature>`) |
| `Keywords` | Removed upstream. |
| — | New: `People`, `NameSort`, `PinballPrimerUrl`, `PinballRulesUrl`, `PinballCardsUrl`, `BobsGuideUrl`, `CompetitionSetupUrl`, `CompetitionNotesUrl` |

## Known Limitations

- `GetLeagues()` — the endpoint (`GET /tournament/leagues/{period}`) is documented but was returning 404 at last check; method throws `NotImplementedException`.
- `GET /series/{code}/past_winners` — used by `GetSeriesWinners()` but not in the official OpenAPI spec; works in practice.
- Player search with multi-word names (e.g. `"Julia Randall"`) may not work correctly — IFPA API limitation.
- MatchPlay `GET /api/rating-periods` returns `401 Not allowed (token)` for some tokens. This is a permission on the MatchPlay side.
- MatchPlay `GET /api/tournaments/{id}/queues` returns 403 unless the token has scorekeeper scope.
- MatchPlay rate limits several endpoints to 6 requests per minute, well under the documented 120. Confirmed on `/api/search` and `/api/tournaments/{id}/summary/*`. Read the `x-ratelimit-*` response headers.
- MatchPlay reads boolean query params by presence. Sending `flag=false` turns the flag **on**. The wrapper sends a flag only when you set it.
- Director search by name is currently broken on the API side.

## Legacy API Versions

### IFPA v1 and v2

`PinballRankingApiV1` and `PinballRankingApiV2` wrap the older versioned IFPA endpoints. **The IFPA team recommends migrating to the Universal (unversioned) API** — these are no longer updated upstream. Use `PinballRankingApi` (Universal) for all new work.

### opdb.org

`OPDBApi` and `IOpdbApi` wrapped the opdb.org API. OPDB shut those endpoints down on
1 October 2026, so both types and their models were removed in 4.0.0. See
[Migrating from `OPDBApi`](#migrating-from-opdbapi).
