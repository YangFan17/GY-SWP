using System.ComponentModel.DataAnnotations;

namespace GYSWP.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}