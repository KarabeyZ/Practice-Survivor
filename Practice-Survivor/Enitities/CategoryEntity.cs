using Practice_Survivor.Enitity;

namespace Practice_Survivor.Enitities
{
    public class CategoryEntity : BaseEntity
    {
        public string Name { get; set; }


        //Relational Property --> Bir kategorinin birden fazla yarışmacısı olabilir
        public ICollection<CompetitorEntity> Competitors { get; set; }
    }
}
