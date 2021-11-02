using FluentNHibernate.Mapping;
using Issuing.Domain;

namespace Issuing.Persistence
{
    public class CardMapping : ClassMap<Card>
    {
        public CardMapping()
        {
            Table("Cards");
            Id(c => c.Id).GeneratedBy.Identity();
            Map(c => c.CardNo).Not.Nullable();
            Map(c => c.Bin).Not.Nullable();
            Map(c => c.Status).Not.Nullable();
            Map(c => c.MemberId).Not.Nullable();
            Map(c => c.IsExpired).Not.Nullable();
        }
    }
}
