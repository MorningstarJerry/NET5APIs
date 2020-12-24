using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyNet5APIs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNet5APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyNet5TestController : ControllerBase
    {
        private readonly ILogger<MyNet5TestController> _logger;

        public delegate int Comparison<in T>(T left, T right);

        private static int CompareLength(string left, string right) => left.Length.CompareTo(right.Length);

        public MyNet5TestController(ILogger<MyNet5TestController> logger)
        {
            _logger = logger;
        }

        //https://localhost:5001/api/MyNet5Test
        [HttpGet]
        public List<TodoItem> Get()
        {
            var methodsName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            _logger.LogInformation($"exec method {methodsName}");

            Comparison<string> comparator = CompareLength;
            int result = comparator("aaa", "bbbb");
            _logger.LogInformation($"exec delegate result: {result}");

            List<TodoItem> lst = new List<TodoItem>();
            for (int i = 0; i < 5; i++)
            {
                TodoItem model = new TodoItem();
                model.Id = i;
                model.Name = $"Name{i.ToString()}";
                model.IsComplete = true;
                lst.Add(model);
            }

            return lst;
        }

        //https://localhost:5001/api/MyNet5Test/8
        [HttpGet("{index}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int index)
        {
            var methodsName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            _logger.LogInformation($"exec method {methodsName}");

            List<TodoItem> lst = new List<TodoItem>();
            for (int i = 0; i < 5; i++)
            {
                TodoItem model = new TodoItem();
                model.Id = i;
                model.Name = $"Name{i.ToString()}";
                model.IsComplete = true;
                lst.Add(model);
            }

            if (lst.Find(p => p.Id.Equals(index)) != null)
            {
                _logger.LogInformation($"get index {index}");
                return Ok(lst);
            }

            _logger.LogInformation($"Not found");
            return NotFound();
        }
    }
}