using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models.PostData
{
	public class CreateTaskPostData
	{
		[Required]
		public string Title { get; set; }

		[Required]
		public string Decription { get; set; }
	}
}
