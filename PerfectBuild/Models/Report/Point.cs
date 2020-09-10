using Newtonsoft.Json;
using System;

namespace PerfectBuild.Models.Report
{
    [JsonObject]
    public class Point<Ty>
    {
        [JsonProperty(PropertyName = "y")]
        public Ty Y { get; set; }
    }

    [JsonObject]
    public class Point<Tx, Ty> : Point<Ty> 
    {
        [JsonProperty(PropertyName = "x")]
        public Tx X { get; set; }

        [JsonProperty(PropertyName ="label")]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "indexLabel")]
        public string IndexLabel { get; set; }
        

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Point<Tx,Ty> p = (Point<Tx,Ty>)obj;
                return (X.Equals( p.X) && (Y.Equals(p.Y)));
            }
        }

        public override int GetHashCode()
        {
            return (X.GetHashCode() + Y.GetHashCode());
        }

        public override string ToString()
        {
            return String.Format("Point({0}, {1})", X, Y);
        }
    }
}