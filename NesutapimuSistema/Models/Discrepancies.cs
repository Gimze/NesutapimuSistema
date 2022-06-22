using System.ComponentModel.DataAnnotations;

namespace NesutapimuSistema.Models
{
    public class Discrepancies
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public enum UrgencyLevel { Low=1, Medium=2, High=3 }
        public UrgencyLevel Urgency { get; set; }

        public enum StatusState { Starting=1, BeingResolved=2, Solved=3}
        public StatusState Status { get; set; }

        public string Text { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public Discrepancies()
        {
            this.CreatedDate = DateTime.Now;
            this.LastUpdatedDate = DateTime.Now;
        }

    }
}
