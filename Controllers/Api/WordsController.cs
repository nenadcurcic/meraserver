using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DTOs;
using System.Data.Common;
using MeraServer.Services;
using MeraServer.Models;
using System.Threading.Tasks;

namespace MeraServer.Controllers.Api
{
    public class WordsController : ApiController
    {
        DbManager db;
        TextService textService;

        public WordsController()
        {
            db = new DbManager();
            textService = new TextService();
        }
        
        // GET /api/words
        public int GetWordsCount(string word)
        {
            if (!word.IsNullOrWhiteSpace())
            { 
                return textService.WordCounter(word);
            } else
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        // POST /api/words/AddNewArticle
        [HttpPost]
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
