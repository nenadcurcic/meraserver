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

namespace MeraServer.Controllers.Api
{
    public class WordsController : ApiController
    {
        private Dictionary<string, string> TextDb;
        TextService textService;

        public WordsController()
        {
            TextDb = new Dictionary<string, string>();
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

        // POST /api/words/AddNewText
        [HttpPost]
        public void AddNewText (TextContainer text)
        {
            if(ModelState.IsValid)
            {
                TextDb.Add(text.Subject, text.Text);
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        // GET /api/words/GetTextsCatalog
        public Catalog GetTextsCatalog()
        {
            Catalog result = new Catalog();

            if (TextDb.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                foreach (KeyValuePair<string, string> subject in TextDb)
                {
                    result.TextSubjectsList.Add(subject.Key);
                }
            }


            return result;
            
        }

       
    }
}
