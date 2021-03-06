﻿using MeraServer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeraServer.Services
{
    internal class DbManager
    {
        private readonly ArticlesDbContext db;
        internal DbManager()
        {
            db = new ArticlesDbContext();
        }

        //Adding new article to db
        internal async Task AddArticleToDb(Article article)
        {
            //TODO: Add check if subject already exist
            await Task.Run(() => { 
                db.Articles.Add(article);
                db.SaveChanges();
            });
        }

        //Get article by subject
        internal async Task<Article> GetArticleBySubject(string subject)
        {
            Article article = null;
            await Task.Run(() =>
            {
                article = db.Articles.SingleOrDefault(a => a.Subject == subject);
            });

            return article;
        }

        //Get list of articles subjects from db
        internal async Task<List<string>> GetSubjectList()
        {
            List<string> subjectList = new List<string>();

            await Task.Run(() => {
                subjectList = db.Articles.Select(a => a.Subject).ToList();
            });

            return subjectList;
        }
    }
}