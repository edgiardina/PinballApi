# JSON Converters Strategy

## Overview
This document explains when to use each JSON converter in the PinballApi codebase.

## Converter Types

### 1. EmptyStringToNullConverter<T> (for Reference Types)
**Use for**: Complex objects that might receive empty strings instead of JSON objects
**Example**: `"winner": ""` ? `Winner = null`
```csharp
[JsonConverter(typeof(EmptyStringToNullConverter<RelatedTournamentWinner>))]
public RelatedTournamentWinner Winner { get; set; }
```

### 2. EmptyStringToNullableConverter<T> (for Simple Value Types)
**Use for**: Simple nullable value types that only need empty string ? null conversion
**Example**: `"age": ""` ? `Age = null` (but `"age": "25"` ? should fail gracefully)
```csharp
[JsonConverter(typeof(EmptyStringToNullableConverter<int>))]
public int? Age { get; set; }
```

### 3. EmptyStringNullableIntDescriptiveConverter
**Use for**: Integer properties that might receive:
- Empty strings: `""` ? `null`
- Numeric strings: `"25"` ? `25`
- JSON numbers: `25` ? `25`

```csharp
[JsonConverter(typeof(EmptyStringNullableIntDescriptiveConverter))]
public int? ProRank { get; set; }
```

### 4. EmptyStringNullableDoubleDescriptiveConverter
**Use for**: Double properties that might receive:
- Empty strings: `""` ? `null`
- Numeric strings: `"85.5"` ? `85.5`
- JSON numbers: `85.5` ? `85.5`

```csharp
[JsonConverter(typeof(EmptyStringNullableDoubleDescriptiveConverter))]
public double? EfficiencyValue { get; set; }
```

### 5. NotRatedNullableDescriptiveConverter
**Use for**: Double properties that might receive the specific text "Not Rated"
```csharp
[JsonConverter(typeof(NotRatedNullableDescriptiveConverter))]
public double? RatingsValue { get; set; }
```

### 6. NotRankedNullableDescriptiveConverter
**Use for**: Integer properties that might receive the specific text "Not Ranked"
```csharp
[JsonConverter(typeof(NotRankedNullableDescriptiveConverter))]
public int? EfficiencyRank { get; set; }
```

## Decision Tree

```
Is it a complex object?
?? YES ? Use EmptyStringToNullConverter<T>
?? NO ? Is it a nullable value type?
    ?? YES ? Does it need to parse numeric strings?
    ?   ?? YES ? Does it handle specific descriptive text?
    ?   ?   ?? YES ? Use NotRated/NotRanked*DescriptiveConverter
    ?   ?   ?? NO ? Use EmptyStringNullable*DescriptiveConverter
    ?   ?? NO ? Use EmptyStringToNullableConverter<T>
    ?? NO ? Use standard JSON serialization
```

## Examples by Property Type

| Property | Converter | Reason |
|----------|-----------|---------|
| `Winner` (complex object) | `EmptyStringToNullConverter<RelatedTournamentWinner>` | Empty string ? null for objects |
| `Age` (simple nullable int) | `EmptyStringToNullableConverter<int>` | Only empty string ? null |
| `PlayerId` (nullable int, might be string) | `EmptyStringNullableIntDescriptiveConverter` | Parses numeric strings |
| `EfficiencyValue` (nullable double, might be string) | `EmptyStringNullableDoubleDescriptiveConverter` | Parses numeric strings |
| `RatingsValue` (nullable double, "Not Rated") | `NotRatedNullableDescriptiveConverter` | Handles "Not Rated" text |
| `EfficiencyRank` (nullable int, "Not Ranked") | `NotRankedNullableDescriptiveConverter` | Handles "Not Ranked" text |