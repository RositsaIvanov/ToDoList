using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoList.Repository;


namespace ToDoList.Client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly ITodoListRepository _repository;
        public string ApiBaseUrl { get; }
        public List<string> Categories { get; set; } = new();


        public IndexModel(IConfiguration config, ITodoListRepository repository)
        {
            _config = config;
            _repository = repository;
            ApiBaseUrl = _config.GetValue<string>("ApiBaseUrl") ?? "https://localhost:7126/api/todolist";
        }


        public void OnGet()
        {
            Categories = _repository.GetCategories();
        }
    }
}
