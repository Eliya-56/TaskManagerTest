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
		public IActionResult Index(string message)
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

			ViewBag.Message = message;
			return View(taskViewModels);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CreateTask(CreateTaskPostData createTaskPostData)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction(nameof(Index), new { message = "Please fill all fields to create task" });
			}

			TestTaskDb testTaskDb = new TestTaskDb
			{
				Title = createTaskPostData.Title,
				Description = createTaskPostData.Decription,
				Status = (int)TaskStatus.New
			};

			TestTaskDb createdTask = _testTaskContext.Add(testTaskDb).Entity;
			_testTaskContext.SaveChanges();

			return RedirectToAction(nameof(Index), new { message = $"Task {createdTask.Id} created" });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult UpdateStatus(UpdateTaskStatusPostData statusPostData)
		{
			TestTaskDb taskDb = _testTaskContext.Tasks.First(x => x.Id == statusPostData.Id);
			taskDb.Status = statusPostData.Status;
			_testTaskContext.SaveChanges();

			return RedirectToAction(nameof(Index), new { message = $"Status of task {statusPostData.Id} updated" });
		}
	}
}
