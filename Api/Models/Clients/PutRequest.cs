using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Models.Clients
{
    public class PutRequest
    {
        [Required]
        public string ClientId { get; set; }

        public List<PutSecrets> ClientSecrets { get; set; }

        public List<string> Scopes { get; set; }
    }

    public class PutSecrets
    {
        [Required]
        public string Value { get; set; }

        public DateTime? ExpresIn { get; set; }
    }
}