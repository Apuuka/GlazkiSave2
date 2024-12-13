//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GlazkiSave
{
    using System;
    using System.Collections.Generic;
    
    public partial class Agent
    {


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

       


        public int ID { get; set; }
        public Nullable<int> AgentTypeID { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Logo { get; set; }
        public string Address { get; set; }
        public int Priority { get; set; }
        public string DirectorName { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }

        

        public string GetAgentType
        {
            get
            {
                return AgentType.Title;
            }
        }

        
        public Agent()
        {
            this.AgentPriorityHistory = new HashSet<AgentPriorityHistory>();
            this.ProductSale = new HashSet<ProductSale>();
        }

        public decimal Prod
        {
            get
            {
                decimal p = 0;
                foreach (ProductSale sales in ProductSale)
                {
                    p = p + sales.Stoimost;
                }
                return p;
            }
        }

        public decimal discription
        {
            get
            {
                if (Prod >= 0 && Prod < 10000)
                    return 0;
                else if (Prod >= 10000 && Prod < 50000)
                    return 5;
                else if (Prod >= 50000 && Prod < 150000)
                    return 10;
                else if (Prod >= 150000 && Prod < 500000)
                    return 20;
                else
                    return 25;

            }
        }

        public virtual AgentType AgentType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgentPriorityHistory> AgentPriorityHistory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSale> ProductSale { get; set; }

    }
}
