# PinballApi — Development Guide

## Project Structure

```
PinballApi/                     # Library project (netstandard targets: net6/7/8/9)
  BasePinballRankingApi.cs      # Abstract base: ApiKey, JsonSerializerOptions, BaseRequest
  PinballRankingApi.cs          # Universal (current) IFPA API implementation
  PinballRankingApiV1.cs        # Legacy v1 IFPA API (no longer updated)
  PinballRankingApiV2.cs        # Legacy v2 IFPA API (no longer updated)
  OPDBApi.cs                    # OPDB pinball machine database
  PinballMachineApi.cs          # IPDB scraper
  MatchPlayApi.cs               # MatchPlay Events API

  Interfaces/
    IPinballRankingApi.cs       # Universal IFPA interface (primary)
    IPinballRankingApiV1.cs     # v1 interface (legacy)
    IPinballRankingApiV2.cs     # v2 interface (legacy)
    IOpdbApi.cs

  Models/WPPR/
    Universal/                  # Models for the current IFPA API
      Players/                  # Player, PlayerHistory, PlayerResults, PVP, etc.
      Rankings/                 # Ranking lists, custom rankings, country lists
      Tournaments/              # Tournament, TournamentResults, League, etc.
      Series/                   # Series standings, regions, player cards
      Stats/                    # Statistics endpoints
      Director/                 # Director profiles
      Other/                    # CountryDetail, StateProvCountry (reference data)
    v1/                         # Legacy v1 models
    v2/                         # Legacy v2 models

  Converters/                   # Custom JSON converters (see CONVERTER_STRATEGY.md)

PinballApi.Tests/               # Integration tests (requires real API keys)
```

## Which API to Use

**Always use `PinballRankingApi` (Universal)** for new work — it targets `https://api.ifpapinball.com/` directly (no version prefix). v1 and v2 are implemented but the IFPA team has deprecated them and they receive no updates.

## Architecture Patterns

### HTTP Client: Flurl
All HTTP calls use [Flurl](https://flurl.dev/). The `BaseRequest` property in each class builds a pre-configured `IFlurlRequest` with the API key and JSON serializer options.

```csharp
protected override IFlurlRequest BaseRequest => "https://api.ifpapinball.com/"
    .SetQueryParams(new { api_key = ApiKey })
    .WithSettings(settings => { settings.JsonSerializer = new DefaultJsonSerializer(JsonSerializerOptions); });
```

### JSON Deserialization
`System.Text.Json` with case-insensitive property matching and `AllowReadingFromString` for numbers. Many IFPA responses return numbers as JSON strings — this handles most cases automatically.

When the API returns a root wrapper key (common), extract it via `JsonNode`:
```csharp
var json = await request.GetStringAsync();
return JsonNode.Parse(json)["series"].Deserialize<List<Series>>(JsonSerializerOptions);
```

When the root object maps directly, use `GetJsonAsync<T>()`:
```csharp
return await request.GetJsonAsync<Player>();
```

### Custom JSON Converters
See `CONVERTER_STRATEGY.md` for a full decision tree. Short version:
- Complex object + empty string input → `EmptyStringToNullConverter<T>`
- Nullable int/double that might come as a string → `EmptyStringNullableIntDescriptiveConverter` / `EmptyStringNullableDoubleDescriptiveConverter`
- "Not Rated" / "Not Ranked" text → `NotRatedNullableDescriptiveConverter` / `NotRankedNullableDescriptiveConverter`

## Testing

Tests are integration tests against the live IFPA API — **not mocked**. An API key is required via [.NET User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets):

```bash
dotnet user-secrets set "WPPRKey" "your-key-here" --project PinballApi.Tests
dotnet user-secrets set "OPDBToken" "your-token" --project PinballApi.Tests
dotnet user-secrets set "MatchPlayApiToken" "your-token" --project PinballApi.Tests
```

Run all tests:
```bash
dotnet test PinballApi.Tests/PinballApi.Tests.csproj
```

Run just the Universal API tests:
```bash
dotnet test --filter "FullyQualifiedName~PinballRankingApiTestFixture"
```

### Test conventions
- Use `[Ignore("reason")]` for endpoints confirmed broken on the API side
- Use `Assume.That(...)` to skip parameterized test cases that don't apply (e.g. `Youth` system in Women-only endpoints)
- Tests use hardcoded player/tournament IDs for well-known stable records
- New endpoints need at least one test that asserts non-empty results

## IFPA API Notes

### Known broken / undocumented behaviors
- **`GET /tournament/leagues/{period}`** — documented in the OpenAPI spec but returns 404 in practice; `GetLeagues()` throws `NotImplementedException`
- **`GET /series/{code}/past_winners`** — not in the OpenAPI spec but works; used by `GetSeriesWinners()`
- **`sort_mode` / `sort_order` on tournament search** — accepted by the API, not in the spec
- **Player search with spaces** — multi-word names (e.g. `"Julia Randall"`) are broken on the API side; tests for this are `[Ignore]`-d
- **Director search by name** — broken on API side; test is `[Ignore]`-d

### Parameter naming
The API uses `pre_registration` (with underscore) and `distance_unit`-style params. When in doubt, follow the [OpenAPI spec](https://api.ifpapinball.com/docs/api.json).

### Response root keys
Most list responses wrap results under a named key. When the key is wrong, deserialization returns `null` silently. If a new endpoint returns `null` unexpectedly, check the actual JSON root key with a raw HTTP call.

## Adding a New Endpoint

1. Add model class(es) in `Models/WPPR/Universal/<section>/`
2. Add the method signature to `IPinballRankingApi.cs`
3. Implement in `PinballRankingApi.cs` — follow the region structure (`#region Players`, etc.)
4. Add an integration test in `PinballRankingApiTestFixture.cs`
5. Run tests: `dotnet test --filter "FullyQualifiedName~PinballRankingApiTestFixture"`

## NuGet Publishing

The package ID is `PinballApi`. Version is set in `PinballApi/PinballApi.csproj`. CI publishes on tag via `main.yml`.
