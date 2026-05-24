# PinballApi - .NET API for pinball data  

[![NuGet](https://img.shields.io/nuget/v/PinballApi.svg)](https://www.nuget.org/packages/PinballApi/)
[![dotnet build & test](https://github.com/edgiardina/PinballApi/actions/workflows/main.yml/badge.svg)](https://github.com/edgiardina/PinballApi/actions/workflows/main.yml)

.NET client library wrapping pinball data sources: **IFPA/WPPR**, **OPDB**, **IPDB**, and **MatchPlay**.

## Data Sources

| Source | What it provides | Auth required |
|--------|-----------------|---------------|
| [IFPA](https://www.ifpapinball.com/) | Player rankings, tournament results, series standings (WPPR system) | [API key](https://www.ifpapinball.com/api/request_api_key.php) |
| [OPDB](https://opdb.org/) | Pinball machine database (canonical machine IDs, metadata) | [API token](https://opdb.org/register) |
| [IPDB](https://www.ipdb.org/) | Classic pinball machine database | None |
| [MatchPlay Events](https://next.matchplay.events) | Tournament bracket software, ratings | [API token](https://next.matchplay.events/api-docs/#authenticating-requests) |

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

### OPDB

```csharp
using PinballApi;

var opdb = new OPDBApi("YOUR_OPDB_TOKEN");
var machine = await opdb.GetMachineByOpdbId("G45wn-MQwN3");
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
var user = await matchPlay.GetUser(12345);
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

## Known Limitations

- `GetLeagues()` — the endpoint (`GET /tournament/leagues/{period}`) is documented but was returning 404 at last check; method throws `NotImplementedException`.
- `GET /series/{code}/past_winners` — used by `GetSeriesWinners()` but not in the official OpenAPI spec; works in practice.
- Player search with multi-word names (e.g. `"Julia Randall"`) may not work correctly — IFPA API limitation.
- Director search by name is currently broken on the API side.

## Legacy API Versions (v1, v2)

`PinballRankingApiV1` and `PinballRankingApiV2` wrap the older versioned IFPA endpoints. **The IFPA team recommends migrating to the Universal (unversioned) API** — these are no longer updated upstream. Use `PinballRankingApi` (Universal) for all new work.
