using Microsoft.Ajax.Utilities;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using DTOs;
using MeraServer.Services;
using MeraServer.Models;
using System.Threading.Tasks;

namespace MeraServer.Controllers.Api
{
    public class WordsController : ApiController
    {
        private readonly DbManager db;
        private readonly TextService textService;

        public WordsController()
        {
            db = new DbManager();
            textService = new TextService();
        }

        // GET /api/words/GetWordsCount
        [Route("api/words/GetWordsCount")]
        public int GetWordsCount(string word)
        {
            if (!word.IsNullOrWhiteSpace())
            {
                int res = textService.WordCounter(word);
                return res;
            } else
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        // POST /api/words/AddNewArticle
        [HttpPost]
        [Route("api/words/AddNewArticle")]
        public async Task AddNewArticle (TextContainer text)
        {
            if(ModelState.IsValid)
            {
                Article article = new Article() { Subject = text.Subject, Text = text.Text};
                await db.AddArticleToDb(article);
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        // GET /api/words/GetArticleBySubject
        [Route("api/words/GetArticleBySubject")]
        public async Task<TextContainer> GetArticleBySubject(string subject)
        {
            TextContainer text = null;
            if (!string.IsNullOrEmpty(subject))
            {
                Article article = await db.GetArticleBySubject(subject);
                if (article != null)
                {
                    text = new TextContainer()
                    {
                        Subject = article.Subject,
                        Text = article.Text,
                    };                    
                    return text;
                } else
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            } else
            {

                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        // GET /api/words/GetTextsCatalog
        [Route("api/words/GetTextsCatalog")]
        public async Task<Catalog> GetTextsCatalog()
        {
            Catalog result = new Catalog();
            List<string> subjectList = await db.GetSubjectList();
            if (subjectList.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                result.TextSubjectsList = subjectList;
            }
            return result;
        }
    }
}
