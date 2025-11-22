using Microsoft.AspNetCore.Mvc.RazorPages;



namespace ToDoList.Client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        public string ApiBaseUrl { get; }
        public List<string> Categories { get; set; } = new();


        public IndexModel(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
            ApiBaseUrl = _config.GetValue<string>("ApiBaseUrl") ?? "https://localhost:7126/api/todolist";
        }


        public async Task OnGet()
        {
            var client = _httpClientFactory.CreateClient();
            try 
            {
                Categories = await client.GetFromJsonAsync<List<string>>($"{ApiBaseUrl}/categories") ?? new List<string>();
            }
            catch (Exception ex)
            {
                Categories = new List<string>();
                Console.WriteLine($"Error fetching categories: {ex.Message}");
            }
        }
    }
}
