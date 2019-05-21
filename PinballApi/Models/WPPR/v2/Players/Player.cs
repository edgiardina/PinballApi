using Newtonsoft.Json;
using PinballApi.Models.v2.WPPR;
using PinballApi.Models.WPPR.v1.Players;
using System;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Players
{
    public class Player
    {
        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("stateprov")]
        public string StateProvince { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("initials")]
        public string Initials { get; set; }

        [JsonProperty("age")]
        public int? Age { get; set; }

        [JsonProperty("gender")]
        public Gender? Gender { get; set; }

        [JsonProperty("excluded_flag")]
        public bool ExcludedFlag { get; set; }

        [JsonProperty("ifpa_registered")]
        public bool IfpaRegistered { get; set; }


        [JsonProperty("profile_photo")]
        public Uri ProfilePhoto { get; set; }

        [JsonProperty("player_stats")]
        public PlayerStats PlayerStats { get; set; }

        [JsonProperty("championshipSeries_us")]
        public List<ChampionshipSeries> ChampionshipSeries { get; set; }
    }
}
