namespace Issuing.Domain
{
    public class Card
    {
        public virtual int Id { get; set; }
        public virtual short Status { get; set; }
        public virtual short MemberId { get; set; }
        public virtual string Bin { get; set; }
        public virtual string CardNo { get; set; }
        public virtual bool IsExpired { get; set; }
    }
}
