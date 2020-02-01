using PerfectBuild.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PerfectBuild.Services
{
    public class DocumentSpecHandler<T> 
        where T:ISpec,IOrdered
    {
        private List<T> lines;
        private int currentId = 1;
        const int step = 2;

        public DocumentSpecHandler()
        {
            lines = new List<T>();
        }

        public void FillDocument(IEnumerable<T> lines)
        {
            if (lines != null)
            {
                this.lines = lines as List<T>;
            }
        }

        public T this[int id]
        {
            get
            {
                return lines.Where(x => x.Id == id).FirstOrDefault();
            }
        }

        public void AddSpecLine(T line)
        {
            if (line == null)
            {
                throw new ArgumentNullException(nameof(T));
            }
            line.Order = GetLastOrder();
            line.Id = currentId++;
            lines.Add(line);
        }

        internal int GetLastOrder()
        {
            return lines.Count == 0 ? 2 : lines.Max(x => x.Order) + step;
        }

        public void AddSpecLine(IEnumerable<T> lines)
        {
            if (lines == null)
            {
                throw new ArgumentNullException(nameof(T));
            }
            var rangedLines = lines;
            int emptyOrderNumber = GetLastOrder();
            foreach (var item in rangedLines)
            {
                item.Order = emptyOrderNumber;
                item.Id = currentId++;
                emptyOrderNumber += step;
            }
            this.lines.AddRange(rangedLines);
        }

        public IEnumerable<T> MoveLineUp(T line)
        {
            if (line == null)
            {
                throw new ArgumentNullException(nameof(line));
            }

            if (line.Order > lines.Min(x => x.Order) & (lines.Count > 1))
            {
                var lineMoving = lines.Where(x => x.Order == (line.Order - step)).FirstOrDefault();

                if (line.Set > lineMoving.Set)
                {
                    byte tempSet = line.Set;
                    line.Set = lineMoving.Set;
                    lineMoving.Set = tempSet;
                }
                int tempOrderNum = line.Order;
                line.Order = lineMoving.Order;
                lineMoving.Order = tempOrderNum;

                return new List<T> { line, lineMoving }; 
            }
            return null;
        }

        public IEnumerable<T> MoveLineDown(T line)
        {
            if (line == null)
            {
                throw new ArgumentNullException(nameof(line));
            }

            if (line.Order < lines.Max(x => x.Order) & (lines.Count >1))
            {
                var lineMoving = lines.Where(x => x.Order == (line.Order + step)).FirstOrDefault();
              
                if (line.Set < lineMoving.Set)
                {
                    byte tempSet = line.Set;
                    line.Set = lineMoving.Set;
                    lineMoving.Set = tempSet;
                }
                int tempOrderNum = line.Order;
                line.Order = lineMoving.Order;
                lineMoving.Order = tempOrderNum;
                return new List<T> { line, lineMoving };
            }
            return null;
        }

        private void Replace(T line, T lineMoving)
        {
            if (line.Set > lineMoving.Set) 
            {
                byte tempSet = line.Set;
                line.Set = lineMoving.Set;
                lineMoving.Set = tempSet;
            }
            int tempOrderNum = line.Order;
            line.Order = lineMoving.Order;
            lineMoving.Order = tempOrderNum;
        }

        internal IEnumerable<T> GetLines()
        {
            return lines;
        }

        internal int GetOrderStep()
        {
            return step;
        }
    }
}

