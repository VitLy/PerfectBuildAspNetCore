using PerfectBuild.Data;
using PerfectBuild.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PerfectBuild.Services
{
    public class DocumentHeadHandler<T> where T : class, IHead, new()
    {
        private readonly ApplicationContext appContext;
        private const int numberDocumentStep = 1;
        public DocumentHeadHandler(ApplicationContext context)
        {
            this.appContext = context;
        }

        internal int GetNumberDocument(string userId)
        {
            var heads = appContext.Set<T>().Where(x => x.UserId == userId).ToList();
            int maxNum = numberDocumentStep;
            if (heads.Count != 0)
            {
                maxNum = heads.Max(x => x.Number) + numberDocumentStep;
            }
            return maxNum;
        }

        internal bool IsNumberPresent(int docNum, string userId)
        {
            var flagIsDocumentWithNumberPresent = appContext.Set<T>().Where(x => x.UserId == userId & x.Number == docNum).Any();
            return flagIsDocumentWithNumberPresent;
        }

        internal T GetHead(int documentNum, string userId, string documentName, DateTime date)
        {
            return new T { Name = documentName, Number = GetNumberDocument(userId), Date = date };
        }

        internal int FindId(int numDocument, string userId)
        {
            var headId = appContext.Set<T>().Where(x => x.Number == numDocument & x.UserId == userId).FirstOrDefault()?.Id;
            return headId ?? 0;
        }
    }
}
