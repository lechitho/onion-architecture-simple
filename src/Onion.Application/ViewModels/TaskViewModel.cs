using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Onion.Application.ViewModels
{
    public class TaskViewModel
    {
        public TaskViewModel()
        {

        }
        public TaskViewModel(Guid taskId, string summary, string description)
        {
            Id = taskId.ToString();
            Summary = summary;
            Description = description;
        }
        public string Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(150)]
        public string Description { get; set; }

        [MaxLength(1500)]
        [JsonProperty(PropertyName = "summary")]
        public string Summary { get; set; } = "";
    }
}
