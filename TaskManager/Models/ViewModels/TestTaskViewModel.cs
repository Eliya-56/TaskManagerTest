namespace TaskManager.Models.ViewModels
{
	public class TestTaskViewModel
	{
		public TestTaskViewModel(
			int id,
			string title,
			int status,
			string description)
		{
			Id = id;
			Title = title;
			Status = (TaskStatus)status;
			Description = description;
		}

		public int Id { get; }

		public string Title { get; }

		public TaskStatus Status { get; }

		public string Description { get; }
	}
}
