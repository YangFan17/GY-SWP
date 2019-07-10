

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.EntryExitRegistrations;

namespace GYSWP.EntryExitRegistrations.Dtos
{
    public class CreateOrUpdateEntryExitRegistrationInput
    {
        [Required]
        public EntryExitRegistrationEditDto EntryExitRegistration { get; set; }

    }
}