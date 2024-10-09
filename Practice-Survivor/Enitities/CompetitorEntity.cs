using Practice_Survivor.Enitities;

namespace Practice_Survivor.Enitity
{
    public class CompetitorEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CategoryId { get; set; }


        //relational property ---> bir yarışmacı bir kategoride bulunabilir
        public CategoryEntity Category { get; set; }    
            

    }
}
