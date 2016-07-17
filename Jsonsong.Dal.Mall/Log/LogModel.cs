using Jsonsong.Dal.Common.MongoDB;

namespace Jsonsong.Dal.Mall.Log
{
    public class LogModel : BaseMongoEntity
    {
        public string CategoryName { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int Level { get; set; }

        public string Ip { get; set; }

        public string Method { get; set; }

    }
}