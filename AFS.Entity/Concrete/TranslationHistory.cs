using AFS.Core.Entities;

namespace AFS.Entity.Concrete
{
    public class TranslationHistory : BaseEntity, IEntity
    {
        public string Translation { get; set; }
        public string Text { get; set; }
        public string Translated { get; set; }
    }
}
