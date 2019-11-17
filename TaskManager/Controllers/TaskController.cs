using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskManager.Models.Data;
using TaskManager.Models.ViewModels;

namespace TaskManager.Controllers
{
	public class TaskController : Controller
	{
		private readonly TestTaskContext _testTaskContext;

		public TaskController(TestTaskContext testTaskContext)
		{
			_testTaskContext = testTaskContext;
		}

		[HttpGet]
		public IActionResult Index()
		{
			List<TestTaskViewModel> taskViewModels = new List<TestTaskViewModel>();
			foreach (var taskDb in _testTaskContext.Tasks)
			{
				taskViewModels.Add(new TestTaskViewModel(
					taskDb.Id,
					taskDb.Title,
					taskDb.Status,
					taskDb.Description));
			}

			return View(taskViewModels);
		}

		[HttpGet]
		public IActionResult CreateTask()
		{
			return View();
		}
	}
}
