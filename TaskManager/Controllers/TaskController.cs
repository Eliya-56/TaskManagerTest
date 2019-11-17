using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Models;
using TaskManager.Models.Data;
using TaskManager.Models.PostData;
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
			foreach (TestTaskDb taskDb in _testTaskContext.Tasks)
			{
				taskViewModels.Add(new TestTaskViewModel(
					taskDb.Id,
					taskDb.Title,
					taskDb.Status,
					taskDb.Description));
			}

			return View(taskViewModels);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CreateTask(CreateTaskPostData createTaskPostData)
		{
			TestTaskDb testTaskDb = new TestTaskDb
			{
				Title = createTaskPostData.Title,
				Description = createTaskPostData.Decription,
				Status = (int)TaskStatus.New
			};

			_testTaskContext.Add(testTaskDb);
			_testTaskContext.SaveChanges();

			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult UpdateStatus(UpdateTaskStatusPostData statusPostData)
		{
			TestTaskDb taskDb = _testTaskContext.Tasks.First(x => x.Id == statusPostData.Id);
			taskDb.Status = statusPostData.Status;
			_testTaskContext.SaveChanges();

			return RedirectToAction(nameof(Index));
		}
	}
}
