﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using PinballApi.Models.MatchPlay.Tournaments;

namespace PinballApi.Models.MatchPlay
{
    public class SingleRatingPeriod
    {
        [JsonPropertyName("ratingPeriod")]
        public RatingPeriod RatingPeriod { get; set; }

        [JsonPropertyName("tournaments")]
        public List<Tournament> Tournaments { get; set; }

        [JsonPropertyName("challenges")]
        public List<Challenge> Challenges { get; set; }

        [JsonPropertyName("extEvents")]
        public List<ExternalEvent> ExternalEvents { get; set; }
    }
}
