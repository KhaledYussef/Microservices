using Article.Services;

using Microsoft.AspNetCore.Mvc;

namespace Article.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController(IArticleRepository articleRepository) : ControllerBase
    {
        private readonly IArticleRepository _articleRepository = articleRepository;

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_articleRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var article = _articleRepository.Get(id);
            if (article is null)
                return NotFound();

            return Ok(article);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedId = _articleRepository.Delete(id);
            if (deletedId == 0)
                return NotFound();

            return NoContent();
        }
    }
}
